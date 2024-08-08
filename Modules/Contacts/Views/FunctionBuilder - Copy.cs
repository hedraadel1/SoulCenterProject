// ReSharper disable All
using SoulCenterProject.Modules.Ai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Newtonsoft.Json;
using Telerik.Documents.Media;
using DevExpress.XtraEditors;
using System.Drawing;
using Color = System.Drawing.Color;

using DevExpress.Utils;
using DevExpress.XtraRichEdit.Fields.Expression;
using Telerik.WinControls.UI;
// ReSharper disable All


namespace SoulCenterProject.Modules.ProjectManagment.Views
{
    /// <summary>
    /// A form that allows users to generate and execute C# code tasks using an AI model.
    /// </summary>
    public partial class FunctionBuilder : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        string GeneratePromptDesignAsyncErrorMsg;

        /// <summary>
        /// Stores the generated plan for C# functions.
        /// </summary>
        private string _cFunctionInstandPlanDetails;

        /// <summary>
        /// A list of tasks generated from the AI response.
        /// </summary>
        private List<STask> _generatedTasks = new List<STask>();

        /// <summary>
        /// The number of tasks determined by the AI model.
        /// </summary>
        private int _TaskCount;

        public string GenerateFunctionInstructionAsyncErrorMsg { get; private set; }
        public string ValidateFunctionInstructionAsyncErrorMsg { get; private set; }
        public string SplitIntoTasksAsyncErrorMsg { get; private set; }
        public string ParseTasksIntoListboxErrorMsg { get; private set; }
        public string ParseTasksFromAiResponseErrormsg { get; private set; }

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionBuilder"/> class.
        /// </summary>
        public FunctionBuilder()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the Button_DetermineTaskCount control.
        /// This event triggers the process of determining the task count and splitting the plan into tasks.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

            Button_DetermineTaskCount.Enabled = true;

            AddlogText("Stage 7: Split Into Tasks Started...");
            Button_DetermineTaskCount.Enabled = false;

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

        /// <summary>
        /// Handles the Click event of the Button_ExcuteTasks control.
        /// This event triggers the execution of the generated tasks.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_ExcuteTasks_Click(object sender, EventArgs e)
        {
            Button_ExcuteTasks.Enabled = false;
            AddlogText("Task Execution Started...");

            AiTaskExecuteSystem taskExecutor = new AiTaskExecuteSystem();

            foreach (STask task in _generatedTasks)
            {
                AddlogText($"Executing Task {task.TaskNumber}: {task.Description}");
                string functionDetails = Text_FunctionInstruction_Results.Text;
                string prompt = taskExecutor.GeneratePromptForTask(task, functionDetails); // Pass functionDetails here
                AddlogText($"Generated Prompt: {prompt}");

                string generatedCode = await taskExecutor.ExecuteTaskAsync(task, functionDetails);

                AddlogText($"Generated Code for Task {task.TaskNumber}:\n{generatedCode}");
                GeneratedCode("==========================");
                GeneratedCode($"Generated Code for Task {task.TaskNumber}:\n{generatedCode}");
            }

            AddlogText("Task Execution Completed!");
            Button_ExcuteTasks.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the Button_GenerateAutomatic control.
        /// This event triggers the automatic generation of C# code.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_GenerateAutomatic_Click(object sender, EventArgs e)
        {
            Button_GenerateAutomatic.Enabled = false;
            await StartFunctionBuildingProcessAsync();
            Button_GenerateAutomatic.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the Button_Export_FunctionDetails control.
        /// This event exports the function details to a specified location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Export_FunctionDetails_Click(object sender, EventArgs e)
        {
            importorexport(false, Text_FunctionDetails);
            ShowFlyoutPanel(Text_FunctionDetails.Text, Text_FunctionDetails);
        }

        /// <summary>
        /// Handles the Click event of the Button_Export_GetInstructionPrompt control.
        /// This event exports the instruction prompt to a specified location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Export_GetInstructionPrompt_Click(object sender, EventArgs e)
        {
            importorexport(false, Text_GetInstructionPrompt);
        }

        /// <summary>
        /// Handles the Click event of the Button_GetFunctioninstruction control.
        /// This event triggers the process of generating function instructions.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the Button_GetInstructionPrompt control.
        /// This event triggers the process of generating an instruction prompt.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_GetInstructionPrompt_ClickAsync(object sender, EventArgs e)
        {
            AddlogText("Process Started...");
            Button_GetInstructionPrompt.Enabled = false;

            // Stage 1: Generate Prompt Design
            if (!await GeneratePromptDesignAsync())
            {
                Button_GetInstructionPrompt.Enabled = true;
                AddlogText("Stage 1: Generate Prompt Design Failed!");
            }

            Button_GetInstructionPrompt.Enabled = true;
            AddlogText("Stage 1: Generate Prompt Design Completed!");
        }

        /// <summary>
        /// Handles the Click event of the Button_Import_FunctionDetails control.
        /// This event imports function details from a specified location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Import_FunctionDetails_Click(object sender, EventArgs e)
        {
            importorexport(true, Text_FunctionDetails);
        }

        /// <summary>
        /// Handles the Click event of the Button_Import_GetInstructionPrompt control.
        /// This event imports an instruction prompt from a specified location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Import_GetInstructionPrompt_Click(object sender, EventArgs e)
        {
            importorexport(true, Text_GetInstructionPrompt);
        }

        /// <summary>
        /// Handles the Click event of the Button_ValidateFunctionInstruction control.
        /// This event triggers the validation of function instructions.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the CheckedChanged event of the checkEdit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
        }

        #endregion Event Handlers

        #region Helper Methods

        /// <summary>
        /// Adds log text to the specified RichTextBox control.
        /// </summary>
        /// <param name="text">The text to add to the log.</param>
        private void AddlogText(string text)
        {
            if (Text_Logtext.InvokeRequired)
            {
                Text_Logtext.Invoke(new Action(() => AddlogText(text))); // Invoke on UI thread
                return;
            }

            Text_Logtext.ForeColor = System.Drawing.Color.Red;
            Text_Logtext.AppendText("============" + Environment.NewLine);
            Text_Logtext.AppendText(text + Environment.NewLine);
            Text_Logtext.AppendText("============" + Environment.NewLine);

            Text_Logtext.SelectionStart = Text_Logtext.TextLength;
            Text_Logtext.ScrollToCaret();
        }

        /// <summary>
        /// Builds the prompt for determining the task count.
        /// </summary>
        /// <returns>The prompt string.</returns>
        private string BuildDetermineTaskCountPrompt()
        {
            StringBuilder prompt = new StringBuilder();

            prompt.AppendLine("You are an AI expert in breaking down software development tasks.");
            prompt.AppendLine("## C# Function Instructions and Plan:");
            prompt.AppendLine(_cFunctionInstandPlanDetails);
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

        /// <summary>
        /// Builds the prompt for generating function instructions.
        /// </summary>
        /// <returns>The prompt string.</returns>
        private string BuildGetfunctionInstructionPrompt()
        {
            string instructionPromptText = "";
            string functionDetailsText = "";
            string projectDetailsText = "";
            string projectStructureText = "";
            bool functionDetailsChecked = false;
            bool projectDetailsChecked = false;
            bool projectStructureChecked = false;

            // Access UI controls safely using Invoke
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    instructionPromptText = Text_GetInstructionPrompt.Text;
                    functionDetailsText = Text_FunctionDetails.Text;
                    projectDetailsText = Text_ProjectDetails.Text;
                    projectStructureText = Text_ProjectStructure.Text;
                    functionDetailsChecked = checkEdit_FunctionDetails.Checked;
                    projectDetailsChecked = checkEdit_ProjectDetails.Checked;
                    projectStructureChecked = checkEdit_ProjectStructure.Checked;
                }));
            }
            else
            {
                instructionPromptText = Text_GetInstructionPrompt.Text;
                functionDetailsText = Text_FunctionDetails.Text;
                projectDetailsText = Text_ProjectDetails.Text;
                projectStructureText = Text_ProjectStructure.Text;
                functionDetailsChecked = checkEdit_FunctionDetails.Checked;
                projectDetailsChecked = checkEdit_ProjectDetails.Checked;
                projectStructureChecked = checkEdit_ProjectStructure.Checked;
            }

            string[] promptParts = {
            "**Objective**",
            instructionPromptText,
        };

            if (functionDetailsChecked)
            {
                promptParts = promptParts.Concat(new string[] { "**Function Details**" }).ToArray();
                promptParts = promptParts.Concat(new string[] { functionDetailsText }).ToArray();
            }

            promptParts = promptParts.Concat(new string[] { "" }).ToArray();

            if (projectDetailsChecked)
            {
                promptParts = promptParts.Concat(new string[] { "**Project Details and Purpose**" }).ToArray();
                promptParts = promptParts.Concat(new string[] { projectDetailsText }).ToArray();
            }

            promptParts = promptParts.Concat(new string[] { "" }).ToArray();

            if (projectStructureChecked)
            {
                promptParts = promptParts.Concat(new string[] { "**Project Structure **" }).ToArray();
                promptParts = promptParts.Concat(new string[] { projectStructureText }).ToArray();
            }

            promptParts = promptParts.Concat(new string[] { "" }).ToArray();

            string finalPrompt = PromptBuilder(promptParts);
            return finalPrompt;
        }

        /// <summary>
        /// Builds the prompt for validating function instructions.
        /// </summary>
        /// <returns>The prompt string.</returns>
        private string BuildValidateFunctionInstructionPrompt()
        {
            if (Text_FuncinstValidationRulesPrompt.InvokeRequired)
            {
                return (string)Text_FuncinstValidationRulesPrompt.Invoke(new Func<string>(() => Text_FuncinstValidationRulesPrompt.Text));
            }
            if (Text_InstructionPrompt_Validation.InvokeRequired)
            {
                return (string)Text_InstructionPrompt_Validation.Invoke(new Func<string>(() => Text_InstructionPrompt_Validation.Text));
            }
            if (Text_FunctionInstruction_Results.InvokeRequired)
            {
                return (string)Text_FunctionInstruction_Results.Invoke(new Func<string>(() => Text_FunctionInstruction_Results.Text));
            }

            string[] promptParts = {
                                           "**Objective**",
                                           Text_FuncinstValidationRulesPrompt.Text,
                                       };

            promptParts = promptParts.Concat(new string[] { "**The Validation Rules Start**" }).ToArray();
            promptParts = promptParts.Concat(new string[] { Text_InstructionPrompt_Validation.Text }).ToArray();
            promptParts = promptParts.Concat(new string[] { "**The Validation Rules End **" }).ToArray();
            promptParts = promptParts.Concat(new string[] { "" }).ToArray();
            promptParts = promptParts.Concat(new string[] { "**The Generated Content Start**" }).ToArray();
            promptParts = promptParts.Concat(new string[] { Text_FunctionInstruction_Results.Text }).ToArray();
            promptParts = promptParts.Concat(new string[] { "**The Generated Content End **" }).ToArray();
            promptParts = promptParts.Concat(new string[] { "" }).ToArray();
            promptParts = promptParts.Concat(new string[] { "" }).ToArray();

            string finalPrompt = PromptBuilder(promptParts);
            return finalPrompt;
        }

        /// <summary>
        /// Builds the prompt for splitting the plan into tasks.
        /// </summary>
        /// <returns>The prompt string.</returns>
        private string BuildSplitIntoTasksPrompt()
        {
            StringBuilder prompt = new StringBuilder();
            prompt.AppendLine("You are an AI expert in task management and C# development.");
            prompt.AppendLine("Please split the provided plan into specific tasks for implementation:");
            prompt.AppendLine();
            prompt.AppendLine("## C# Function Instructions and Plan:");
            prompt.AppendLine(_cFunctionInstandPlanDetails);
            prompt.AppendLine();
            prompt.AppendLine($"Split this plan into {_TaskCount} distinct tasks, following these rules:");
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


        /// <summary>
        /// Asynchronously determines the task count.
        /// </summary>
        /// <returns>True if the task count is determined successfully; otherwise, false.</returns>
        private async Task<bool> DetermineTaskCountAsync()
        {
            try
            {
                string prompt = BuildDetermineTaskCountPrompt();
                string generatedText = await ExcutePrompt("Determine Task Count", prompt);

                string taskCountString = ExtractTextBetweenMarkers(generatedText, "````StartTaskCount````", "````EndTaskCount````");

                if (string.IsNullOrEmpty(taskCountString) || !int.TryParse(taskCountString, out int taskCount))
                {
                    AddlogText("Error: Invalid task count received from AI.");
                    return false;
                }

                _TaskCount = taskCount;
                AddlogText(_TaskCount.ToString());

                return true;
            }
            catch (Exception ex)
            {
                AddlogText($"Error in DetermineTaskCountAsync: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Executes a prompt with the AI model.
        /// </summary>
        /// <param name="Excuteprompt">The prompt to execute.</param>
        /// <param name="finalPrompt">The final prompt string.</param>
        /// <returns>The generated text from the AI model.</returns>
        private async Task<string> ExcutePrompt(string Excuteprompt, string finalPrompt)
        {
            try
            {
                // Use Invoke to ensure UI updates are done on the UI thread
                if (InvokeRequired)
                {
                    return (string)Invoke(
                        new Func<string, string, Task<string>>(ExcutePrompt),
                        Excuteprompt,
                        finalPrompt);
                }

                // Update UI elements on the UI thread
                BarItem_MarqueeProgressBar.Visibility = BarItemVisibility.Always;
                BarItem_MarqueeProgressBar.EditValue = 0; // Set to numeric 0
                BarItem_ExcuteingNow.Caption = Excuteprompt;

                // Create the timer on the UI thread
                Timer timer = new Timer();
                timer.Interval = 1000;
                int secondsElapsed = 0;
                timer.Start();
                timer.Tick += (s, t) =>
                    {
                        // Update UI elements on the UI thread
                        if (InvokeRequired)
                        {
                            Invoke(
                                new Action(
                                    () =>
                                        {
                                            secondsElapsed++;
                                            BarItem_MarqueeProgressBar.EditValue = secondsElapsed;
                                        }));
                        }
                        else
                        {
                            secondsElapsed++;
                            BarItem_MarqueeProgressBar.EditValue = secondsElapsed;
                        }
                    };


                try
                {
                    // Call GenerateResponse directly with await
                    string generatedText = await GenerateResponse(finalPrompt, null, null);
                    return generatedText;
                }
                finally
                {
                    // Stop the timer and update UI on the UI thread
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            timer.Stop();
                            timer.Dispose();
                            BarItem_MarqueeProgressBar.Visibility = BarItemVisibility.Never;
                        }));
                    }
                    else
                    {
                        timer.Stop();
                        timer.Dispose();
                        BarItem_MarqueeProgressBar.Visibility = BarItemVisibility.Never;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// Extracts the text between two specified markers within a given string.
        /// </summary>
        /// <param name="text">The input text to search within.</param>
        /// <param name="startMarker">The marker indicating the beginning of the desired text.</param>
        /// <param name="endMarker">The marker indicating the end of the desired text.</param>
        /// <returns>The extracted text between the markers, or an empty string if either marker is not found.</returns>
        private string ExtractTextBetweenMarkers(string text, string startMarker, string endMarker)
        {
            int startIndex = text.IndexOf(startMarker) + startMarker.Length;
            int endIndex = text.IndexOf(endMarker, startIndex);

            if (startIndex >= 0 && endIndex >= 0)
            {
                return text.Substring(startIndex, endIndex - startIndex);
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// Asynchronously generates the function instruction.
        /// </summary>
        /// <returns>True if the function instruction is generated successfully; otherwise, false.</returns>
        private async Task<bool> GenerateFunctionInstructionAsync()
        {
            try
            {
                string generatedText = await ExcutePrompt("Generate function Instruction", Text_InstructionPrompt.Text);

                // Use Invoke to ensure UI updates are done on the UI thread
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                        {
                            Text_FunctionInstruction_Results.Text = generatedText;
                            _cFunctionInstandPlanDetails = generatedText;
                        }));
                }
                else
                {
                    Text_FunctionInstruction_Results.Text = generatedText;
                    _cFunctionInstandPlanDetails = generatedText;
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

        /// <summary>
        /// Asynchronously generates the prompt design.
        /// </summary>
        /// <returns>True if the prompt design is generated successfully; otherwise, false.</returns>
        private async Task<bool> GeneratePromptDesignAsync()
        {

            // You need to await the ExcutePrompt call here 
            string generatedText = await ExcutePrompt("Get function Instruction Prompt", BuildGetfunctionInstructionPrompt());

            string ThePromptDetails = ExtractTextBetweenMarkers(generatedText, "````StartPrompt````", "````EndPrompt````");
            string Thevalidationrules = ExtractTextBetweenMarkers(generatedText, "````Startvalidationrules````", "````Endvalidationrules````");

            // Update UI on the UI thread
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                    {
                        Text_InstructionPrompt.Text = ThePromptDetails;
                        Text_InstructionPrompt_Validation.Text = Thevalidationrules;
                    }));
            }
            else
            {
                Text_InstructionPrompt.Text = ThePromptDetails;
                Text_InstructionPrompt_Validation.Text = Thevalidationrules;
            }
            return true;

        }

        /// <summary>
        /// Generates a response using the Gemini AI service.
        /// </summary>
        /// <param name="userMessage">The user's message.</param>
        /// <param name="conversationHistory">The conversation history.</param>
        /// <param name="Systeminstructions">The system instructions.</param>
        /// <returns>The generated text response.</returns>
        private async Task<string> GenerateResponse(string userMessage, List<ChatMessage> conversationHistory = null, string Systeminstructions = null)
        {
            try
            {

            }
            catch (Exception exception)
            {
                return Task.FromResult(new T
                {
                    Errors = new List<string>()
                                                            {
                                                                exception.Message
                                                            }
                });
            }
            GeminiAiService gemini = new GeminiAiService();

            string GenerateRes = await Task.Run(() => gemini.GenerateTextAsync(userMessage, conversationHistory, Systeminstructions, ""));
            string generatedText = GenerateRes;
            return generatedText;
        }


        /// <summary>
        /// Appends generated code to the designated RichTextBox control.
        /// </summary>
        /// <param name="codedetails">The code details to append.</param>
        private void GeneratedCode(string codedetails)
        {
            if (Text_Generatedcode.InvokeRequired)
            {
                Text_Generatedcode.Invoke(new Action(() => GeneratedCode(codedetails)));
                return;
            }

            Text_Generatedcode.Text = "============" + Environment.NewLine;
            Text_Generatedcode.Text = codedetails + Environment.NewLine;
            Text_Generatedcode.Text = "============" + Environment.NewLine;

            GeneratedSyntexCode(codedetails);

        }
        private void GeneratedSyntexCode(string codedetails)
        {
            if (Panel_GeneratedCode.InvokeRequired)
            {
                Panel_GeneratedCode.Invoke(new Action(() => GeneratedSyntexCode(codedetails)));
                return;
            }
            RadSyntaxEditor radsy = new RadSyntaxEditor();
            radsy.Text = "============" + Environment.NewLine;
            radsy.Text = codedetails + Environment.NewLine;
            radsy.Text = "============" + Environment.NewLine;

            Panel_GeneratedCode.Controls.Add(radsy);

        }


        /// <summary>
        /// Imports or exports text from/to a RichTextBox control.
        /// </summary>
        /// <param name="isim">True to import, false to export.</param>
        /// <param name="text">The RichTextBox control.</param>
        private void importorexport(bool isim, RichTextBox text)
        {
            string generaltext = Text_General.Text;
            if (isim)
            {
                text.Text = generaltext;
            }
            else
            {
                Text_General.Text = text.Text;
            }
        }

        /// <summary>
        /// Parses tasks from an AI response.
        /// </summary>
        /// <param name="aiResponse">The AI response string.</param>
        /// <returns>A list of parsed tasks.</returns>
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

                // Update ListBox on the UI thread
                BeginInvoke(new Action(() =>
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
                ParseTasksFromAiResponseErrormsg = "Error in : " + ex.Message;
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed", "Insert Splited Tasks into Listbox", ParseTasksFromAiResponseErrormsg);
                throw;
            }
        }


        /// <summary>
        /// Builds a prompt string from an array of prompt parts.
        /// </summary>
        /// <param name="promptParts">The array of prompt parts.</param>
        /// <returns>The combined prompt string.</returns>
        private string PromptBuilder(string[] promptParts)
        {
            StringBuilder prompt = new StringBuilder();

            foreach (string part in promptParts)
            {
                prompt.AppendLine(part);
            }

            return prompt.ToString();
        }

        /// <summary>
        /// Asynchronously splits the plan into tasks.
        /// </summary>
        /// <returns>True if the plan is split successfully; otherwise, false.</returns>
        private async Task<bool> SplitIntoTasksAsync()
        {
            try
            {
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Working", "Split Into Tasks", "");
                if (_TaskCount <= 0)
                {
                    string errormsg = "Error: Invalid Task Count. Please run Task Count first.";
                    AddlogText(errormsg);
                    UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed", "Split Into Tasks", errormsg);
                    return false;
                }

                string prompt = BuildSplitIntoTasksPrompt();
                string generatedText = await ExcutePrompt("Split Into Tasks And Insert Into Listbox", prompt);
                // Update Text_functionTasks on the UI thread
                BeginInvoke(new Action(() =>
                {
                    Text_functionTasks.Text = generatedText;
                }));

                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Completed", "Split Into Tasks", "");


                return ParseTasksIntoListbox(generatedText);

            }
            catch (Exception ex)
            {
                string SplitIntoTasksAsyncErrorMsg = $"Error in SplitIntoTasksAsync: {ex.Message}";
                AddlogText(SplitIntoTasksAsyncErrorMsg);
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed", "Split Into Tasks", SplitIntoTasksAsyncErrorMsg);

                return false;
            }
        }



        private bool ParseTasksIntoListbox(string generatedText)
        {
            try
            {
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Working", "Insert Splited Tasks into Listbox", "");

                _generatedTasks = ParseTasksFromAiResponse(generatedText);

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
                UpdateStageStatus(Text_functionTasks, Button_DetermineTaskCount, "Failed", "Insert Splited Tasks into Listbox", ParseTasksIntoListboxErrorMsg);

                return false;
            }
        }

        /// <summary>
        /// Asynchronously starts the function building process.
        /// </summary>
        /// <returns>True if the process completes successfully; otherwise, false.</returns>
        public async Task<bool> StartFunctionBuildingProcessAsync()
        {
            // Run the process in a background thread
            return await Task.Run(async () =>
            {
                try
                {
                    AddlogText("Automated Process Started...");
                    AddlogText("----------------------Start(1)------------------------");
                    // Stage 1: Generate Prompt Design
                    AddlogText("Stage 1: Generate Prompt Design Started...");

                    UpdateStageStatus(Text_InstructionPrompt, Button_GetInstructionPrompt, "Working", "Generate Prompt Design", "");

                    if (!await GeneratePromptDesignAsync())
                    {
                        AddlogText("Stage 1: Generate Prompt Design Failed!");
                        UpdateStageStatus(Text_InstructionPrompt, Button_GetInstructionPrompt, "Failed", "Generate Prompt Design", GeneratePromptDesignAsyncErrorMsg);

                        return false;
                    }

                    AddlogText("Stage 1: Generate Prompt Design Completed!");
                    UpdateStageStatus(Text_InstructionPrompt, Button_GetInstructionPrompt, "Completed", "Generate Prompt Design", "");

                    AddlogText("----------------------End(1)------------------------");
                    AddlogText("===============================================");
                    AddlogText("----------------------Start(2)------------------------");

                    // Stage 2: Generate Function Instruction
                    AddlogText("Stage 2: Generate Function Instruction Started...");
                    UpdateStageStatus(Text_FunctionInstruction_Results, Button_GetFunctioninstruction, "Working", "Generate Function Instruction", "");

                    if (!await GenerateFunctionInstructionAsync())
                    {
                        AddlogText("Stage 2: Generate Function Instruction Failed!");
                        UpdateStageStatus(Text_FunctionInstruction_Results, Button_GetFunctioninstruction, "Failed", "Generate Function Instruction", GenerateFunctionInstructionAsyncErrorMsg);

                        return false;
                    }
                    AddlogText("Stage 2: Generate Function Instruction Completed!");
                    UpdateStageStatus(Text_FunctionInstruction_Results, Button_GetFunctioninstruction, "Completed", "Generate Function Instruction", "");

                    AddlogText("----------------------End(2)------------------------");

                    AddlogText("===============================================");

                    AddlogText("----------------------Start(3)------------------------");

                    // Stage 3: Validate Function Instruction
                    AddlogText("Stage 3: Validate Function Instruction Started...");
                    UpdateStageStatus(Text_validationtest, Button_ValidateFunctionInstruction, "Working", "Validate Function Instruction", "");

                    if (!await ValidateFunctionInstructionAsync())
                    {
                        AddlogText("Stage 3: Validate Function Instruction Failed!");
                        UpdateStageStatus(Text_validationtest, Button_ValidateFunctionInstruction, "Failed", "Validate Function Instruction", ValidateFunctionInstructionAsyncErrorMsg);

                        return false;
                    }
                    AddlogText("Stage 3: Validate Function Instruction Completed!");
                    UpdateStageStatus(Text_validationtest, Button_ValidateFunctionInstruction, "Completed", "Validate Function Instruction", "");

                    AddlogText("----------------------End(3)------------------------");

                    AddlogText("===============================================");

                    AddlogText("----------------------Start(4)------------------------");
                    // Stage 6: Determine Task Count
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
                    // Stage 7: Split Into Tasks
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

        /// <summary>
        /// Asynchronously validates the function instruction.
        /// </summary>
        /// <returns>True if the function instruction is valid; otherwise, false.</returns>
        private async Task<bool> ValidateFunctionInstructionAsync()
        {
            try
            {
                string generatedText = await ExcutePrompt("Validate Function Instruction", BuildValidateFunctionInstructionPrompt());
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



        /// <summary>
        /// Updates the UI status of a specific stage in the code generation process.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox associated with the stage.</param>
        /// <param name="button">The SimpleButton associated with the stage.</param>
        /// <param name="state">The state of the stage (Working, Completed, Failed).</param>
        /// <param name="stageName">The name of the stage.</param>
        /// <param name="message">An optional error message for the Failed state.</param>

        public void UpdateStageStatus(RichTextBox richTextBox, SimpleButton button, string state, string stageName, string message = "")
        {
            // Find the existing item in the ListBox (if it exists)
            int existingItemIndex = ListBox_LogList.FindStringExact(stageName);

            Color stageColor;
            Timer flashTimer = null; // Timer for flashing animation 

            switch (state)
            {
                case "Working":
                    // Use Invoke to update UI controls from the timer thread
                    if (richTextBox.InvokeRequired)
                    {
                        richTextBox.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                        return;
                    }

                    richTextBox.BackColor = Color.White;
                    button.BackColor = Color.Cornsilk;

                    stageColor = Color.Yellow;

                    // Create and start a timer for the flashing animation
                    flashTimer = new Timer();
                    flashTimer.Interval = 500; // Flash every 500 milliseconds
                    flashTimer.Tick += (s, args) =>
                    {
                        if (richTextBox.InvokeRequired)
                        {
                            richTextBox.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                            return;
                        }
                        if (button.InvokeRequired)
                        {
                            button.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                            return;
                        }
                        richTextBox.BackColor = richTextBox.BackColor == Color.White ? Color.Yellow : Color.White;
                        button.Appearance.BackColor = button.Appearance.BackColor == Color.White ? Color.Yellow : Color.White;
                    };
                    flashTimer.Start();
                    break;

                case "Completed":
                    if (richTextBox.InvokeRequired)
                    {
                        richTextBox.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                        return;
                    }
                    richTextBox.BackColor = Color.LightGreen;
                    button.BackColor = Color.Green;

                    stageColor = Color.Green;

                    // Stop the flash timer if it's running
                    if (flashTimer != null)
                    {
                        flashTimer.Stop();
                        flashTimer.Dispose();
                    }
                    break;

                case "Failed":
                    if (richTextBox.InvokeRequired)
                    {
                        richTextBox.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                        return;
                    }
                    richTextBox.BackColor = Color.FromArgb(255, 128, 128); // Light Red
                    button.BackColor = Color.Red;

                    stageColor = Color.Red;

                    // Stop the flash timer if it's running
                    if (flashTimer != null)
                    {
                        flashTimer.Stop();
                        flashTimer.Dispose();
                    }
                    break;

                default:
                    return; // Ignore unknown states
            }

            // Update or add the stage name in ListBox_LogList
            if (ListBox_LogList.InvokeRequired)
            {
                ListBox_LogList.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                return;
            }

            if (existingItemIndex >= 0)
            {
                ListBox_LogList.Items[existingItemIndex] = stageName;
            }
            else
            {
                ListBox_LogList.BeginUpdate();
                ListBox_LogList.Items.Add(stageName);
                ListBox_LogList.EndUpdate();
            }

            // Use DrawItem event for background color
            ListBox_LogList.DrawItem += (s, e) =>
            {
                if (e.Index >= 0)
                {
                    e.DrawBackground();
                    Brush myBrush = Brushes.White;
                    if (e.Index == existingItemIndex)
                    {
                        myBrush = new SolidBrush(stageColor);
                    }
                    e.Graphics.FillRectangle(myBrush, e.Bounds);
                    e.Graphics.DrawString(ListBox_LogList.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                    e.DrawFocusRectangle();
                }
            };

            // Add FlyoutPanel handling for Failed state 
            if (state == "Failed" && existingItemIndex >= 0)
            {
                ListBox_LogList.SelectedIndexChanged += (s, args) =>
                {
                    if (ListBox_LogList.InvokeRequired)
                    {
                        ListBox_LogList.Invoke(new Action(() => UpdateStageStatus(richTextBox, button, state, stageName, message)));
                        return;
                    }

                    if (ListBox_LogList.SelectedIndex == existingItemIndex)
                    {
                        ShowFlyoutPanel(message, richTextBox); // Pass the RichTextBox as the owner control
                    }
                };
            }
        }

        //  FlyoutPanel implementation
        private void ShowFlyoutPanel(string message, Control ownerControl)
        {
            FlyoutPanel flyoutPanel = new FlyoutPanel();
            flyoutPanel.OwnerControl = ownerControl;
            FlyoutPanelControl flyoutPanelControl = new FlyoutPanelControl();
            flyoutPanelControl.Dock = DockStyle.Fill;

            // Add a label to display the error message
            Label errorMessageLabel = new Label();
            errorMessageLabel.Text = message;
            errorMessageLabel.Dock = DockStyle.Fill; // Or set AutoSize to true and dock as needed

            flyoutPanelControl.Controls.Add(errorMessageLabel);
            flyoutPanel.Controls.Add(flyoutPanelControl);

            flyoutPanel.ShowBeakForm();
        }


        #endregion Helper Methods
    }

    /// <summary>
    /// Represents the type of import/export operation.
    /// </summary>
    public enum isimport
    {
        /// <summary>
        /// Import operation.
        /// </summary>
        Import,

        /// <summary>
        /// Export operation.
        /// </summary>
        Export
    }

    public class AiTaskResponse
    {
        [JsonProperty("tasks")]
        public List<STask> Tasks { get; set; }
    }

    /// <summary>
    /// Represents a software development task.
    /// </summary>
    public class STask
    {
        /// <summary>
        /// Gets or sets the task number.
        /// </summary>
        public int TaskNumber { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the code associated with the task.
        /// </summary>
        public string Code { get; set; }
    }
}