using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

using Newtonsoft.Json;

using SoulCenterProject.Modules.Ai.Services;

using Telerik.WinControls.UI;

using Color = System.Drawing.Color;
// ReSharper disable All



namespace SoulCenterProject.Modules.ProjectManagment.Views
{
    /// <summary>
    /// A form that allows users to generate and execute C# code tasks using an AI model.
    /// </summary>
    public partial class FunctionBuilder : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private string generatePromptDesignAsyncErrorMsg;
        private string _cFunctionInstantPlanDetails;
        private readonly List<STask> _generatedTasks = new List<STask>();
        private int _taskCount;

        public string GenerateFunctionInstructionAsyncErrorMsg { get; private set; }
        public string ValidateFunctionInstructionAsyncErrorMsg { get; private set; }
        public string SplitIntoTasksAsyncErrorMsg { get; private set; }
        public string ParseTasksIntoListboxErrorMsg { get; private set; }
        public string ParseTasksFromAiResponseErrorMsg { get; private set; }
        public string GeneratePromptDesignAsyncErrorMsg { get; private set; }

        public GeminiAiService gemini;
        #endregion

        #region Constructor

        public FunctionBuilder()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private async void Button_DetermineTaskCount_Click(object sender, EventArgs e)
        {
            AddlogText("Stage 6: Determine Task Count Started...");
            Button_DetermineTaskCount.Enabled = false;

            if (await DetermineTaskCountAsync())
            {
                AddlogText("Stage 6: Determine Task Count Completed!");
            }
            else
            {
                AddlogText("Stage 6: Determine Task Count Failed!");
            }

            // SplitIntoTasksAsync() after DetermineTaskCountAsync() 
            AddlogText("Stage 7: Split Into Tasks Started...");
            if (await SplitIntoTasksAsync())
            {
                AddlogText("Stage 7: Split Into Tasks Completed!");
            }
            else
            {
                AddlogText("Stage 7: Split Into Tasks Failed!");
            }

            Button_DetermineTaskCount.Enabled = true;
        }

        private async void Button_ExcuteTasks_Click(object sender, EventArgs e)
        {
            Button_ExcuteTasks.Enabled = false;
            AddlogText("Task Execution Started...");

            AiTaskExecuteSystem taskExecutor = new AiTaskExecuteSystem();

            foreach (STask task in _generatedTasks)
            {
                AddlogText($"Executing Task {task.TaskNumber}: {task.Description}");
                string functionDetails = Text_FunctionInstruction_Results.Text;
                string prompt = taskExecutor.GeneratePromptForTask(task, functionDetails);
                AddlogText($"Generated Prompt: {prompt}");

                string generatedCode = await taskExecutor.ExecuteTaskAsync(task, functionDetails);

                AddlogText($"Generated Code for Task {task.TaskNumber}:\n{generatedCode}");
                GeneratedCode("==========================");
                GeneratedCode($"Generated Code for Task {task.TaskNumber}:\n{generatedCode}");
            }

            AddlogText("Task Execution Completed!");
            Button_ExcuteTasks.Enabled = true;
        }

        private async void Button_GenerateAutomatic_Click(object sender, EventArgs e)
        {
            Button_GenerateAutomatic.Enabled = false;
            await StartFunctionBuildingProcessAsync();
            Button_GenerateAutomatic.Enabled = true;
        }

        private void Button_Export_FunctionDetails_Click(object sender, EventArgs e)
        {
            importorexport(false, Text_FunctionDetails);
            ShowFlyoutPanel(Text_FunctionDetails.Text, Text_FunctionDetails);
        }

        private void Button_Export_GetInstructionPrompt_Click(object sender, EventArgs e)
        {
            importorexport(false, Text_GetInstructionPrompt);
        }

        private async void Button_GetFunctioninstruction_Click(object sender, EventArgs e)
        {
            AddlogText("Stage 2: Generate Function Instruction Started...");
            Button_GetFunctioninstruction.Enabled = false;

            if (await GenerateFunctionInstructionAsync())
            {
                AddlogText("Stage 2: Generate Function Instruction Completed!");
            }
            else
            {
                AddlogText("Stage 2: Generate Function Instruction Failed!");
            }

            Button_GetFunctioninstruction.Enabled = true;
        }

        private async void Button_GetInstructionPrompt_ClickAsync(object sender, EventArgs e)
        {
            AddlogText("Process Started...");
            Button_GetInstructionPrompt.Enabled = false;
            await GeneratePromptDesignAsync();
            Button_GetInstructionPrompt.Enabled = true;
            AddlogText("Stage 1: Generate Prompt Design Failed!");
         

            AddlogText("Stage 1: Generate Prompt Design Completed!");
            Button_GetInstructionPrompt.Enabled = true;
        }

        private void Button_Import_FunctionDetails_Click(object sender, EventArgs e)
        {
            importorexport(true, Text_FunctionDetails);
        }

        private void Button_Import_GetInstructionPrompt_Click(object sender, EventArgs e)
        {
            importorexport(true, Text_GetInstructionPrompt);
        }

        private async void Button_ValidateFunctionInstruction_Click(object sender, EventArgs e)
        {
            AddlogText("Stage 3: Validate Function Instruction Started...");
            Button_ValidateFunctionInstruction.Enabled = false;

            if (await ValidateFunctionInstructionAsync())
            {
                AddlogText("Stage 3: Validate Function Instruction Completed!");
            }
            else
            {
                AddlogText("Stage 3: Validate Function Instruction Failed!");
            }

            Button_ValidateFunctionInstruction.Enabled = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            // Consider removing this empty handler.
        }

        #endregion

        #region Helper Methods

        private void AddlogText(string text)
        {
            if (Text_Logtext.InvokeRequired)
            {
                // Corrected Invoke call using Action delegate:
                Text_Logtext.Invoke(new Action(() => AddlogText(text)));
                return;
            }

            Text_Logtext.ForeColor = Color.Red;
            Text_Logtext.AppendText("============" + Environment.NewLine +
                                    text + Environment.NewLine +
                                    "============" + Environment.NewLine);
            Text_Logtext.SelectionStart = Text_Logtext.TextLength;
            Text_Logtext.ScrollToCaret();
        }
       
        #region Prompt Building

        private string BuildDetermineTaskCountPrompt()
        {
            var prompt = new StringBuilder();
            prompt.AppendLine("You are an AI expert in breaking down software development tasks.");
            prompt.AppendLine("## C# Function Instructions and Plan:");
            prompt.AppendLine(_cFunctionInstantPlanDetails);
            prompt.AppendLine("Based on the provided information and considering the following rules:");
            prompt.AppendLine("1 - Tasks should be structured from bottom-up (dependencies first).");
            prompt.AppendLine("2 - Each function should have its own task.");
            prompt.AppendLine("3 - Each function should include error handling and logging.");
            prompt.AppendLine("4 - The first task should be creating an outline (classes, interfaces, etc.).");
            prompt.AppendLine("Please suggest an appropriate number of tasks to split the C# function implementation into:");
            prompt.AppendLine();
            prompt.AppendLine("Provide your answer in the following format:");
            prompt.AppendLine("````StartTaskCount````");
            prompt.AppendLine("10");
            prompt.AppendLine("````EndTaskCount````");

            return prompt.ToString();
        }

        private string BuildGetfunctionInstructionPrompt()
        {
            string GetControlText(Control ctrl) =>
                ctrl.InvokeRequired
                    ? (string)ctrl.Invoke((Func<string>)(() => ctrl.Text))
                    : ctrl.Text;

            string instructionPromptText = GetControlText(Text_GetInstructionPrompt);
            string functionDetailsText = GetControlText(Text_FunctionDetails);
            string projectDetailsText = GetControlText(Text_ProjectDetails);
            string projectStructureText = GetControlText(Text_ProjectStructure);

            bool GetControlChecked(CheckEdit ctrl) =>
                ctrl.InvokeRequired
                    ? (bool)ctrl.Invoke((Func<bool>)(() => ctrl.Checked))
                    : ctrl.Checked;

            bool functionDetailsChecked = GetControlChecked(checkEdit_FunctionDetails);
            bool projectDetailsChecked = GetControlChecked(checkEdit_ProjectDetails);
            bool projectStructureChecked = GetControlChecked(checkEdit_ProjectStructure);

            var prompt = new StringBuilder();
            prompt.AppendLine("**Objective**");
            prompt.AppendLine(instructionPromptText);

            if (functionDetailsChecked)
            {
                prompt.AppendLine("**Function Details**");
                prompt.AppendLine(functionDetailsText);
            }

            if (projectDetailsChecked)
            {
                prompt.AppendLine("**Project Details and Purpose**");
                prompt.AppendLine(projectDetailsText);
            }

            if (projectStructureChecked)
            {
                prompt.AppendLine("**Project Structure**");
                prompt.AppendLine(projectStructureText);
            }

            return prompt.ToString();
        }

        private string BuildValidateFunctionInstructionPrompt()
        {
            string GetControlText(Control ctrl) =>
                ctrl.InvokeRequired
                    ? (string)ctrl.Invoke((Func<string>)(() => ctrl.Text))
                    : ctrl.Text;

            string validationRulesPromptText = GetControlText(Text_FuncinstValidationRulesPrompt);
            string instructionPromptValidationText = GetControlText(Text_InstructionPrompt_Validation);
            string functionInstructionResultsText = GetControlText(Text_FunctionInstruction_Results);

            var prompt = new StringBuilder();
            prompt.AppendLine("**Objective**");
            prompt.AppendLine(validationRulesPromptText);
            prompt.AppendLine("**The Validation Rules Start**");
            prompt.AppendLine(instructionPromptValidationText);
            prompt.AppendLine("**The Validation Rules End**");
            prompt.AppendLine("**The Generated Content Start**");
            prompt.AppendLine(functionInstructionResultsText);
            prompt.AppendLine("**The Generated Content End**");

            return prompt.ToString();
        }

        private string BuildSplitIntoTasksPrompt()
        {
            var prompt = new StringBuilder();
            prompt.AppendLine("You are an AI expert in task management and C# development.");
            prompt.AppendLine("Please split the provided plan into specific tasks for implementation:");
            prompt.AppendLine();
            prompt.AppendLine("## C# Function Instructions and Plan:");
            prompt.AppendLine(_cFunctionInstantPlanDetails);
            prompt.AppendLine();
            prompt.AppendLine($"Split this plan into {_taskCount} distinct tasks, following these rules:");
            prompt.AppendLine("1 - Tasks should be structured from bottom-up (dependencies first).");
            prompt.AppendLine("2 - Each function should have its own task.");
            prompt.AppendLine("3 - Each function should include error handling and logging.");
            prompt.AppendLine("4 - The first task should be creating an outline of the necessary components (classes, interfaces, etc.).");
            prompt.AppendLine("5 - For each task, provide a clear and concise description. Do not write any code at this stage.");
            prompt.AppendLine("Output the tasks in JSON format, like this:");
            prompt.AppendLine("{");
            prompt.AppendLine("  \"tasks\": [");
            prompt.AppendLine("    {");
            prompt.AppendLine("      \"taskNumber\": 1,");
            prompt.AppendLine("      \"description\": \"Description of task 1\"");
            prompt.AppendLine("    },");
            prompt.AppendLine("    {");
            prompt.AppendLine("      \"taskNumber\": 2,");
            prompt.AppendLine("      \"description\": \"Description of task 2\"");
            prompt.AppendLine("    }");
            prompt.AppendLine("    // ... more tasks");
            prompt.AppendLine("  ]");
            prompt.AppendLine("}");

            return prompt.ToString();
        }

        #endregion

        private async Task<bool> DetermineTaskCountAsync()
        {
            try
            {
                string prompt = BuildDetermineTaskCountPrompt();
                string generatedText = await ExecutePromptAsync("Determine Task Count", prompt);

                string taskCountString = ExtractTextBetweenMarkers(generatedText, "````StartTaskCount````", "````EndTaskCount````");

                if (string.IsNullOrEmpty(taskCountString) || !int.TryParse(taskCountString, out int taskCount))
                {
                    AddlogText("Error: Invalid task count received from AI.");
                    return false;
                }

                _taskCount = taskCount;
                AddlogText(_taskCount.ToString());

                return true;
            }
            catch (Exception ex)
            {
                AddlogText($"Error in DetermineTaskCountAsync: {ex.Message}");
                return false;
            }
        }

        private async Task<string> ExecutePromptAsync(string executingNow, string finalPrompt)
        {
            // Update UI to show progress
            UpdateUI(BarItemVisibility.Always, 0, executingNow);

            AddlogText("ExecutePromptAsync() Function" + " inside " + " In Stage " + executingNow);



            try
            {
                AddlogText("ExecutePromptAsync() Function" + " inside " + " get generatedText with GenerateResponse(finalPrompt) ");


                // Call the AI text generation function and await its result
                string generatedText = await GenerateResponse(finalPrompt);
                AddlogText(" inside " + "ExecutePromptAsync() Function" + " the generatedText has received ");

                // Return the generated text
                return generatedText;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during text generation
                // Log the error, display an error message to the user, etc.

                // Example:
                Console.WriteLine($@"Error in ExecutePromptAsync: {ex.Message}");

                // Re-throw the exception if necessary
                throw;
            }
        }

        private void UpdateUI(BarItemVisibility visibility, int progressValue, string executingNow)
        {
            // Update the progress bar visibility, value, and caption
            BarItem_MarqueeProgressBar.Visibility = visibility;
            BarItem_MarqueeProgressBar.Caption = progressValue.ToString();
            BarItem_ExcuteingNow.Caption = executingNow;
        }

        private void UpdateProgressBar(int value)
        {
            BarItem_MarqueeProgressBar.Caption = value.ToString();
        }
        private string ExtractTextBetweenMarkers(string text, string startMarker, string endMarker)
        {
            int startIndex = text.IndexOf(startMarker) + startMarker.Length;
            int endIndex = text.IndexOf(endMarker, startIndex);

            return startIndex >= 0 && endIndex >= 0
                ? text.Substring(startIndex, endIndex - startIndex)
                : string.Empty;
        }

        private async Task<bool> GenerateFunctionInstructionAsync()
        {
            try
            {
                string generatedText = await ExecutePromptAsync("Generate Function Instruction", Text_InstructionPrompt.Text);

                if (InvokeRequired)
                {
                    Invoke((Action)(() =>
                    {
                        Text_FunctionInstruction_Results.Text = generatedText;
                        _cFunctionInstantPlanDetails = generatedText;
                    }));
                }
                else
                {
                    Text_FunctionInstruction_Results.Text = generatedText;
                    _cFunctionInstantPlanDetails = generatedText;
                }

                return true;
            }
            catch (Exception ex)
            {
                AddlogText($"Error in GenerateFunctionInstructionAsync: {ex.Message}");
                GenerateFunctionInstructionAsyncErrorMsg = ex.Message;
                return false;
            }
        }

        private async Task<bool> GeneratePromptDesignAsync()
        {
            AddlogText("Get the generatedText String By Excuteing ExecutePromptAsync() Function");
            string generatedText = await ExecutePromptAsync("Get Function Instruction Prompt", BuildGetfunctionInstructionPrompt());
            AddlogText(" inside " + "GeneratePromptDesignAsync() Function" + " the generatedText has received from ExecutePromptAsync()");

            string thePromptDetails = ExtractTextBetweenMarkers(generatedText, "````StartPrompt````", "````EndPrompt````");
            AddlogText(" inside " + "GeneratePromptDesignAsync() Function" + " extract the thePromptDetails with ExtractTextBetweenMarkers() from generatedText");

            string theValidationRules = ExtractTextBetweenMarkers(generatedText, "````Startvalidationrules````", "````Endvalidationrules````");
            AddlogText(" inside " + "GeneratePromptDesignAsync() Function" + " extract the theValidationRules with ExtractTextBetweenMarkers() from generatedText");
            if (Text_Logtext.InvokeRequired)
            {
                Text_Logtext.Invoke(new Action(() =>
                    {
                        Text_Logtext.ForeColor = Color.Black;
                        AddlogText(" inside GeneratePromptDesignAsync() Function: We will fill Text_InstructionPrompt ");

                        Text_Logtext.ForeColor = Color.Blue;
                        Text_InstructionPrompt.Text = thePromptDetails;
                        AddlogText(" inside GeneratePromptDesignAsync() Function: Text_InstructionPrompt has been filled");

                        Text_Logtext.ForeColor = Color.Black;
                        AddlogText(" inside GeneratePromptDesignAsync() Function: We will fill Text_InstructionPrompt_Validation ");
                        Text_InstructionPrompt_Validation.Text = theValidationRules;
                        Text_Logtext.ForeColor = Color.Blue;
                        AddlogText(" inside GeneratePromptDesignAsync() Function: Text_InstructionPrompt_Validation has been filled");
                        Text_Logtext.ForeColor = Color.Red;
                    }));
            }
            else
            {
                // If already on the UI thread, no need for Invoke
                Text_Logtext.ForeColor = Color.Black;
                AddlogText(" inside GeneratePromptDesignAsync() Function: We will fill Text_InstructionPrompt ");

                Text_Logtext.ForeColor = Color.Blue;
                Text_InstructionPrompt.Text = thePromptDetails;
                AddlogText(" inside GeneratePromptDesignAsync() Function: Text_InstructionPrompt has been filled");

                Text_Logtext.ForeColor = Color.Black;
                AddlogText(" inside GeneratePromptDesignAsync() Function: We will fill Text_InstructionPrompt_Validation ");
                Text_InstructionPrompt_Validation.Text = theValidationRules;
                Text_Logtext.ForeColor = Color.Blue;
                AddlogText(" inside GeneratePromptDesignAsync() Function: Text_InstructionPrompt_Validation has been filled");
                Text_Logtext.ForeColor = Color.Red;
            };
            return true;
        }

        private async Task<string> GenerateResponse(string userMessage, List<Ai.Services.ChatMessage> conversationHistory = null, string systemInstructions = "", string rules = "")
        {
            AddlogText(" inside " + "GenerateResponse() Function" + " and will Generate the text from the ai api (gemini.GenerateTextAsync )");


            AddlogText(" inside " + "GenerateResponse() Function" + " gemini instance has been created");

            string geeratedresponse = await gemini.GenerateTextAsync(userMessage, conversationHistory, systemInstructions, rules);
            AddlogText(" inside " + "GenerateResponse() Function" + " the content has been generated from the ai api ");

            return geeratedresponse;
        }

        private void GeneratedCode(string codeDetails)
        {
            if (Text_Generatedcode.InvokeRequired)
            {
                Text_Generatedcode.Invoke((Action)(() => GeneratedCode(codeDetails)));
                return;
            }

            Text_Generatedcode.Text = "============" + Environment.NewLine +
                                       codeDetails + Environment.NewLine +
                                       "============" + Environment.NewLine;

            GeneratedSyntexCode(codeDetails);
        }

        private void GeneratedSyntexCode(string codeDetails)
        {
            if (Panel_GeneratedCode.InvokeRequired)
            {
                Panel_GeneratedCode.Invoke((Action)(() => GeneratedSyntexCode(codeDetails)));
                return;
            }

            var radSy = new RadSyntaxEditor
            {
                Text = "============" + Environment.NewLine +
                       codeDetails + Environment.NewLine +
                       "============" + Environment.NewLine
            };

            Panel_GeneratedCode.Controls.Add(radSy);
        }

        private void importorexport(bool isImport, RichTextBox textBox)
        {
            string generalText = Text_General.Text;

            if (isImport)
            {
                textBox.Text = generalText;
            }
            else
            {
                Text_General.Text = textBox.Text;
            }
        }

        private List<STask> ParseTasksFromAiResponse(string aiResponse)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<AiTaskResponse>(aiResponse);

                if (response == null || response.Tasks == null)
                {
                    AddlogText("Error: Could not parse tasks from the AI response.");
                    return new List<STask>();
                }

                if (response.Tasks.Count == 0)
                {
                    AddlogText("Warning: AI response contains no tasks.");
                    return response.Tasks;
                }

                BeginInvoke((Action)(() =>
                {
                    foreach (var task in response.Tasks)
                    {
                        ListBox_TaskList.Items.Add($"Task {task.TaskNumber}: {task.Description}");
                        AddlogText($"Task {task.TaskNumber}: {task.Description}");
                    }
                }));

                return response.Tasks;
            }
            catch (Exception ex)
            {
                ParseTasksFromAiResponseErrorMsg = "Error in : " + ex.Message;
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed",
                    "Insert Splited Tasks into Listbox", ParseTasksFromAiResponseErrorMsg);
                throw;
            }
        }

        private async Task<bool> SplitIntoTasksAsync()
        {
            try
            {
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Working", "Split Into Tasks", "");

                if (_taskCount <= 0)
                {
                    string errorMsg = "Error: Invalid Task Count. Please run Task Count first.";
                    AddlogText(errorMsg);
                    UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed", "Split Into Tasks",
                        errorMsg);
                    return false;
                }

                string prompt = BuildSplitIntoTasksPrompt();
                string generatedText = await ExecutePromptAsync("Split Into Tasks And Insert Into Listbox", prompt);

                BeginInvoke((Action)(() => { Text_functionTasks.Text = generatedText; }));

                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Completed", "Split Into Tasks", "");

                return ParseTasksIntoListbox(generatedText);
            }
            catch (Exception ex)
            {
                string splitIntoTasksAsyncErrorMsg = $"Error in SplitIntoTasksAsync: {ex.Message}";
                AddlogText(splitIntoTasksAsyncErrorMsg);
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed", "Split Into Tasks",
                    splitIntoTasksAsyncErrorMsg);

                return false;
            }
        }

        private bool ParseTasksIntoListbox(string generatedText)
        {
            try
            {
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Working",
                    "Insert Splited Tasks into Listbox", "");

                _generatedTasks.Clear(); // Clear the existing list
                _generatedTasks.AddRange(ParseTasksFromAiResponse(generatedText));


                foreach (var task in _generatedTasks)
                {
                    AddlogText($"Task: {task.Description}");
                }
                return true;
            }
            catch (JsonReaderException ex)
            {
                AddlogText($"Error parsing JSON response: {ex.Message}");
                ParseTasksIntoListboxErrorMsg = "Error parsing JSON response" + " " + ex.Message;
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed",
                    "Insert Splited Tasks into Listbox", ParseTasksIntoListboxErrorMsg);

                return false;
            }
        }

        public async Task<bool> StartFunctionBuildingProcessAsync()
        {
            // Using Task.Run for asynchronous execution in a background thread
            return await Task.Run(async () =>
            {
                try
                {
                    AddlogText("Automated Process Started...");
                    AddlogText("----------------------Start(1)------------------------");

                    AddlogText("Stage 1: Generate Prompt Design Started...");
                    // UpdateStageStatus(Text_InstructionPrompt, Button_GetInstructionPrompt, "Working", "Generate Prompt Design", "");

                    if (!await GeneratePromptDesignAsync())
                    {
                        AddlogText("Stage 1: Generate Prompt Design Failed!");
                        //    UpdateStageStatus(Text_InstructionPrompt, Button_GetInstructionPrompt, "Failed",
                        //       "Generate Prompt Design", GeneratePromptDesignAsyncErrorMsg);
                        return false;
                    }

                    AddlogText("Stage 1: Generate Prompt Design Completed!");
                    //UpdateStageStatus(Text_InstructionPrompt, Button_GetInstructionPrompt, "Completed",
                    //   "Generate Prompt Design", "");
                    AddlogText("----------------------End(1)------------------------");
                    AddlogText("===============================================");
                    AddlogText("----------------------Start(2)------------------------");

                    AddlogText("Stage 2: Generate Function Instruction Started...");
                    UpdateStageStatus(Text_FunctionInstruction_Results, Button_GetFunctioninstruction, "Working",
                        "Generate Function Instruction", "");

                    if (!await GenerateFunctionInstructionAsync())
                    {
                        AddlogText("Stage 2: Generate Function Instruction Failed!");
                        UpdateStageStatus(Text_FunctionInstruction_Results, Button_GetFunctioninstruction, "Failed",
                            "Generate Function Instruction", GenerateFunctionInstructionAsyncErrorMsg);
                        return false;
                    }

                    AddlogText("Stage 2: Generate Function Instruction Completed!");
                    UpdateStageStatus(Text_FunctionInstruction_Results, Button_GetFunctioninstruction, "Completed",
                        "Generate Function Instruction", "");
                    AddlogText("----------------------End(2)------------------------");
                    AddlogText("===============================================");
                    AddlogText("----------------------Start(3)------------------------");

                    AddlogText("Stage 3: Validate Function Instruction Started...");
                    UpdateStageStatus(Text_validationtest, Button_ValidateFunctionInstruction, "Working",
                        "Validate Function Instruction", "");

                    if (!await ValidateFunctionInstructionAsync())
                    {
                        AddlogText("Stage 3: Validate Function Instruction Failed!");
                        UpdateStageStatus(Text_validationtest, Button_ValidateFunctionInstruction, "Failed",
                            "Validate Function Instruction", ValidateFunctionInstructionAsyncErrorMsg);
                        return false;
                    }

                    AddlogText("Stage 3: Validate Function Instruction Completed!");
                    UpdateStageStatus(Text_validationtest, Button_ValidateFunctionInstruction, "Completed",
                        "Validate Function Instruction", "");
                    AddlogText("----------------------End(3)------------------------");
                    AddlogText("===============================================");
                    AddlogText("----------------------Start(4)------------------------");

                    AddlogText("Stage 6: Determine Task Count Started...");

                    if (!await DetermineTaskCountAsync())
                    {
                        AddlogText("Stage 6: Determine Task Count Failed!");
                        return false;
                    }

                    AddlogText("Stage 6: Determine Task Count Completed!");
                    AddlogText("----------------------End(4)------------------------");
                    AddlogText("===============================================");
                    AddlogText("----------------------Start(5)------------------------");

                    AddlogText("Stage 7: Split Into Tasks Started...");

                    if (!await SplitIntoTasksAsync())
                    {
                        AddlogText("Stage 7: Split Into Tasks Failed!");
                        return false;
                    }

                    AddlogText("Stage 7: Split Into Tasks Completed!");
                    AddlogText("----------------------End(5)------------------------");
                    AddlogText("===============================================");
                    AddlogText("===============================================");
                    AddlogText("Automated Process Completed Successfully!");
                    AddlogText("===============================================");
                    AddlogText("===============================================");

                    return true;
                }
                catch (Exception ex)
                {
                    AddlogText($"Critical Error: {ex.Message}");
                    return false;
                }
            });
        }

        private async Task<bool> ValidateFunctionInstructionAsync()
        {
            try
            {
                string generatedText = await ExecutePromptAsync("Validate Function Instruction",
                    BuildValidateFunctionInstructionPrompt());
                Text_validationtest.Text = generatedText;
                AddlogText(generatedText);
                return true;
            }
            catch (Exception ex)
            {
                AddlogText($"Error in ValidateFunctionInstructionAsync: {ex.Message}");
                ValidateFunctionInstructionAsyncErrorMsg = ex.Message;
                return false;
            }
        }


        public void UpdateStageStatus(RichTextBox richTextBox, SimpleButton button, string state, string stageName,
            string message = "")
        {
            //int existingItemIndex = ListBox_LogList.FindStringExact(stageName);

            //Color stageColor;
            //Timer flashTimer = null;

            //switch (state)
            //{
            //    case "Working":
            //        if (richTextBox.InvokeRequired)
            //        {
            //            richTextBox.Invoke((Action)(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
            //            return;
            //        }

            //        richTextBox.BackColor = Color.White;
            //        button.BackColor = Color.Cornsilk;
            //        stageColor = Color.Yellow;

            //        flashTimer = new Timer { Interval = 500 };
            //        flashTimer.Tick += (s, args) =>
            //        {
            //            if (richTextBox.InvokeRequired || button.InvokeRequired)
            //            {
            //                Invoke((Action)(() =>
            //                    UpdateStageStatus(richTextBox, button, state, stageName, message)));
            //                return;
            //            }

            //            richTextBox.BackColor = richTextBox.BackColor == Color.White ? Color.Yellow : Color.White;
            //            button.Appearance.BackColor = button.Appearance.BackColor == Color.White ? Color.Yellow : Color.White;
            //        };
            //        flashTimer.Start();
            //        break;

            //    case "Completed":
            //        if (richTextBox.InvokeRequired)
            //        {
            //            richTextBox.Invoke((Action)(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
            //            return;
            //        }

            //        richTextBox.BackColor = Color.LightGreen;
            //        button.BackColor = Color.Green;
            //        stageColor = Color.Green;

            //        if (flashTimer != null)
            //        {
            //            flashTimer.Stop();
            //            flashTimer.Dispose();
            //        }
            //        break;

            //    case "Failed":
            //        if (richTextBox.InvokeRequired)
            //        {
            //            richTextBox.Invoke((Action)(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
            //            return;
            //        }

            //        richTextBox.BackColor = Color.FromArgb(255, 128, 128); // Light Red
            //        button.BackColor = Color.Red;
            //        stageColor = Color.Red;

            //        if (flashTimer != null)
            //        {
            //            flashTimer.Stop();
            //            flashTimer.Dispose();
            //        }
            //        break;

            //    default:
            //        return;
            //}

            //if (ListBox_LogList.InvokeRequired)
            //{
            //    ListBox_LogList.Invoke((Action)(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
            //    return;
            //}

            //if (existingItemIndex >= 0)
            //{
            //    ListBox_LogList.Items[existingItemIndex] = stageName;
            //}
            //else
            //{
            //    ListBox_LogList.BeginUpdate();
            //    ListBox_LogList.Items.Add(stageName);
            //    ListBox_LogList.EndUpdate();
            //}

            //ListBox_LogList.DrawItem += (s, e) =>
            //{
            //    if (e.Index >= 0)
            //    {
            //        e.DrawBackground();
            //        Brush myBrush = Brushes.White;
            //        if (e.Index == existingItemIndex)
            //        {
            //            myBrush = new SolidBrush(stageColor);
            //        }
            //        e.Graphics.FillRectangle(myBrush, e.Bounds);
            //        e.Graphics.DrawString(ListBox_LogList.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds,
            //            StringFormat.GenericDefault);
            //        e.DrawFocusRectangle();
            //    }
            //};

            //if (state == "Failed" && existingItemIndex >= 0)
            //{
            //    ListBox_LogList.SelectedIndexChanged += (s, args) =>
            //    {
            //        if (ListBox_LogList.InvokeRequired)
            //        {
            //            ListBox_LogList.Invoke((Action)(() => UpdateStageStatus(richTextBox, button, state, stageName,
            //                message)));
            //            return;
            //        }

            //        if (ListBox_LogList.SelectedIndex == existingItemIndex)
            //        {
            //            ShowFlyoutPanel(message, richTextBox);
            //        }
            //    };
            //}
        }

        private void ShowFlyoutPanel(string message, Control ownerControl)
        {
            FlyoutPanel flyoutPanel = new FlyoutPanel
            {
                OwnerControl = ownerControl
            };
            FlyoutPanelControl flyoutPanelControl = new FlyoutPanelControl
            {
                Dock = DockStyle.Fill
            };

            Label errorMessageLabel = new Label
            {
                Text = message,
                Dock = DockStyle.Fill
            };

            flyoutPanelControl.Controls.Add(errorMessageLabel);
            flyoutPanel.Controls.Add(flyoutPanelControl);
            flyoutPanel.ShowBeakForm();
        }

        #endregion
        int secondsElapsed = 0;
        private void timer_Progress_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            UpdateProgressBar(secondsElapsed);
        }

        private void FunctionBuilder_Load(object sender, EventArgs e)
        {
            gemini = new GeminiAiService();
        }

        private void Button_CountDetermineTaskCount_Click(object sender, EventArgs e)
        {

        }
    }


    public class AiTaskResponse
    {
        [JsonProperty("tasks")]
        public List<STask> Tasks { get; set; }
    }

    public class STask
    {
        public int TaskNumber { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
