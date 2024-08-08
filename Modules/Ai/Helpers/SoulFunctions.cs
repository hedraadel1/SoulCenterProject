using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq; //  متأكد انك  عامل  Install  لـ Dapper 
using MySql.Data.MySqlClient;

namespace SoulCenterProject
{
    public static class SoulFunctions
    {
        // Function  لفتح برنامج
        public static string connectionString =
       "Server=178.18.251.168;Database=newgyral_erpnew;Uid=newgyral_erpnew;Pwd=Mm102030@@@;";
        public static SoulFunctionResult OpenProgram(string programPath)
        {
            try
            {
                Process.Start(programPath);
                return new SoulFunctionResult { Success = true };
            }
            catch (Exception ex)
            {
                return new SoulFunctionResult
                {
                    Success = false,
                    ErrorMessage = $"Error opening program: {ex.Message}"
                };
            }
        }


        public static SoulFunctionResult ExecuteSqlQuery(string sqlQuery)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var result = connection.Query<dynamic>(sqlQuery).ToList();

                    return new SoulFunctionResult
                    {
                        Success = true,
                        Result = result
                    };
                }
            }
            catch (Exception ex)
            {
                return new SoulFunctionResult
                {
                    Success = false,
                    ErrorMessage = $"Error executing SQL query: {ex.Message}"
                };
            }
        }
    




    // Function  لقراءة محتوى ملف
    public static string ReadFileContent(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                // Error Handling
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }

        // Function  لكتابة محتوى في  ملف
        public static bool WriteToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
                return true;
            }
            catch (Exception ex)
            {
                // Error Handling
                Console.WriteLine($"Error writing to file: {ex.Message}");
                return false;
            }
        }

        // Function  للحصول على  تاريخ  اليوم
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Function  للحصول على  الوقت الحالي
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
    public class SoulFunctionResult
    {
        public bool Success { get; set; }
        public object Result { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class SoulFunctionCallRequest
    {
        public string ResponseType { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
