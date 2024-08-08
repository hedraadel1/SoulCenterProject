using Dapper;

using MySql.Data.MySqlClient;

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static SoulCenterProject.soulstudio;

namespace SoulCenterProject.Modules.Ai.Services
{
    public class AiModelTools
    {
        // استبدل القيم دي ببيانات الاتصال بقاعدة البيانات بتاعتك 
        private static string connectionString =
            "Server=178.18.251.168;Database=newgyral_erpnew;Uid=newgyral_erpnew;Pwd=Mm102030@@@;";

        public static async Task ExecuteTasks()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                // جلب المهام الرئيسية المجدولة اللي تاريخ البدء بتاعها هو النهاردة
                string sqlMainTasks = "SELECT * FROM Soul_Tasks WHERE Status = 'Scheduled' AND StartDate = @today";
                var mainTasks = connection.Query<SoulTask>(sqlMainTasks, new { today = DateTime.Today }).ToList();


                // معالجة كل مهمة رئيسية
                foreach (var mainTask in mainTasks)
                {
                    // جلب التاسكات الفرعية المرتبة حسب SortOrder
                    string sqlSubtasks = "SELECT * FROM Soul_Subtasks WHERE TaskID = @mainTaskId ORDER BY SortOrder";
                    var subtasks = connection.Query<SoulSubtask>(sqlSubtasks, new { mainTaskId = mainTask.TaskID })
                        .ToList();


                    // التحقق من وجود تاسكات فرعية
                    if (subtasks.Count == 0)
                    {
                        // إنشاء تاسك فرعية بنفس تفاصيل المهمة الرئيسية
                        CreateSubtaskForMainTask(mainTask);


                        // تحديث قائمة التاسكات الفرعية
                        subtasks = connection.Query<SoulSubtask>(sqlSubtasks, new { mainTaskId = mainTask.TaskID })
                            .ToList();
                    }


                    // تحديث حالة المهمة الرئيسية لـ WorkingOn
                    string sqlUpdateMainToWorking =
                        "UPDATE Soul_Tasks SET Status = 'WorkingOn' WHERE TaskID = @mainTaskId";
                    connection.Execute(sqlUpdateMainToWorking, new { mainTaskId = mainTask.TaskID });


                    // تنفيذ كل تاسك فرعية 
                    foreach (var subtask in subtasks)
                    {
                        await SendSubtaskToAgent(subtask.SubtaskID, subtask.SubtaskDescription);
                    }


                    // التحقق من اكتمال جميع التاسكات الفرعية
                    bool allSubtasksCompleted = connection.QuerySingle<bool>(
                        "SELECT COUNT(*) = 0 FROM Soul_Subtasks WHERE TaskID = @mainTaskId AND Status <> 'Completed'",
                        new { mainTaskId = mainTask.TaskID });

                    if (allSubtasksCompleted)
                    {
                        // تحديث حالة المهمة الرئيسية لـ Completed
                        string sqlUpdateMainToCompleted =
                            "UPDATE Soul_Tasks SET Status = 'Completed' WHERE TaskID = @mainTaskId";
                        connection.Execute(sqlUpdateMainToCompleted, new { mainTaskId = mainTask.TaskID });
                    }
                }
            }
        }

        private static async Task SendSubtaskToAgent(int subtaskId, string subtaskDescription)
        {
            GeminiAiService gemini = new GeminiAiService();


            // هنا هنبعت الـ subtaskDescription  ليك (Soly) ونستنى الرد 
            string response = await gemini.GenerateTextAsync(subtaskDescription);


            // نحدث حالة الـ subtask  في قاعدة البيانات لـ Completed
            using (var connection = new MySqlConnection(connectionString))
            {
                string sqlUpdate = "UPDATE Soul_Subtasks SET Status = 'Completed' WHERE SubtaskID = @subtaskId";
                connection.Execute(sqlUpdate, new { subtaskId });
            }
        }

        private static void CreateSubtaskForMainTask(SoulTask mainTask)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql =
                    @"INSERT INTO Soul_Subtasks (TaskID, SubtaskDescription, Status, SortOrder, StartDate, DueDate, AssignedTo) 
                       VALUES (@TaskId, @TaskDescription, @Status, 1, @StartDate, @DueDate, @AssignedTo)";
                connection.Execute(sql, mainTask);
            }
        }

        private static void CreateSubtaskForMainTask(int mainTaskId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                // جلب تفاصيل المهمة الرئيسية 
                string sqlMainTask = "SELECT * FROM Soul_Tasks WHERE TaskID = @mainTaskId";
                var mainTask = connection.QuerySingle<SoulTask>(sqlMainTask, new { mainTaskId });


                // إنشاء تاسك فرعية بنفس تفاصيل المهمة الرئيسية 
                string sqlSubtask =
                    "INSERT INTO Soul_Subtasks (TaskID, SubtaskDescription, Status, SortOrder, StartDate, DueDate, AssignedTo) " +
                    "VALUES (@mainTaskId, @description, @status, 1, @startDate, @dueDate, @assignedTo)";
                connection.Execute(sqlSubtask, new
                {
                    mainTaskId,
                    description = mainTask.TaskDescription,
                    status = mainTask.Status,
                    startDate = mainTask.StartDate,
                    dueDate = mainTask.DueDate,
                    assignedTo = mainTask.AssignedTo
                });
            }
        }

        private static soulstudio soulstu = new soulstudio();

        public static void AnalyzeResponse(string response)
        {
            // التحقق من وجود TaskSpliter في الرد
            if (!response.Contains("TaskSpliterStart") || !response.Contains("TaskSpliterEnd"))
            {
                return; // لو مفيش TaskSpliter يبقى نخرج من الـ function 
            }


            // Regex لاستخراج البيانات بين العلامات 
            string pattern = @"TaskSpliter-(MainTask|Subtask)-(.*?)-Start(.*?)TaskSpliter-\1-\2-End";
            MatchCollection matches = Regex.Matches(response, pattern, RegexOptions.Singleline);
            ///////////////Log To Ui//////////////////
            SoulLog log6 = new SoulLog
            {
                LogAddress = "AnalyzeResponse() method 129 >> Regex لاستخراج البيانات بين العلامات ",
                LogDetails = "extractedText >>>>> " + matches,
                Step = OperationStep.Working
            };
            soulstu.ReportSoulLog(log6);
            ///////////////Log To Ui//////////////////
            // معالجة المهام الرئيسية والفرعية
            int mainTaskId = -1;
            foreach (Match match in matches)
            {
                string taskType = match.Groups[1].Value;
                string fieldName = match.Groups[2].Value;
                string fieldValue = match.Groups[3].Value.Trim();

                if (taskType == "MainTask")
                {
                    if (fieldName == "Title")
                    {
                        // إنشاء مهمة رئيسية جديدة 
                        mainTaskId = CreateMainTask(fieldValue);
                    }
                    else if (mainTaskId != -1)
                    {
                        // تحديث تفاصيل المهمة الرئيسية
                        UpdateMainTask(mainTaskId, fieldName, fieldValue);
                    }
                }
                else if (taskType == "Subtask" && mainTaskId != -1)
                {
                    // إنشاء مهمة فرعية جديدة
                    CreateSubtask(mainTaskId, fieldName, fieldValue);
                }
            }
        }

        private static int CreateMainTask(string title)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO Soul_Tasks (TaskDescription) VALUES (@title); SELECT LAST_INSERT_ID();";
                return connection.QuerySingle<int>(sql, new { title });
            }
        }

        private static void UpdateMainTask(int taskId, string fieldName, string fieldValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = $"UPDATE Soul_Tasks SET {fieldName} = @fieldValue WHERE TaskID = @taskId";
                connection.Execute(sql, new { taskId, fieldValue });
            }
        }

        private static void CreateSubtask(int mainTaskId, string fieldName, string fieldValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql =
                    "INSERT INTO Soul_Subtasks (TaskID, SubtaskDescription, SortOrder) VALUES (@mainTaskId, @fieldValue, @sortOrder)";
                int sortOrder = GetNextSubtaskSortOrder(mainTaskId);
                connection.Execute(sql, new { mainTaskId, fieldValue, sortOrder });
            }
        }

        private static int GetNextSubtaskSortOrder(int mainTaskId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "SELECT COALESCE(MAX(SortOrder), 0) + 1 FROM Soul_Subtasks WHERE TaskID = @mainTaskId";
                return connection.QuerySingle<int>(sql, new { mainTaskId });
            }
        }

        public class SoulTask
        {
            public int TaskID { get; set; }
            public string TaskDescription { get; set; }
            public string Status { get; set; }
            public string Priority { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? StartDate { get; set; }
            public int AssignedTo { get; set; }
        }

        public class SoulSubtask
        {
            public int SubtaskID { get; set; }
            public int TaskID { get; set; }
            public string SubtaskDescription { get; set; }
            public string Status { get; set; }
            public int SortOrder { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? DueDate { get; set; }
            public int AssignedTo { get; set; }
        }
    }
}