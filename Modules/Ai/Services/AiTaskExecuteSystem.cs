using System.Text;
using System.Threading.Tasks;
using SoulCenterProject.Modules.ProjectManagment.Views;
using SoulCenterProject.Test_phase_items;
using STask = SoulCenterProject.Modules.ProjectManagment.Views.STask;

namespace SoulCenterProject.Modules.Ai.Services // or your preferred namespace
{
    public class AiTaskExecuteSystem
    {
        private readonly GeminiAiService _geminiAiService;

        public AiTaskExecuteSystem()
        {
            _geminiAiService = new GeminiAiService();
        }

        public async Task<string> ExecuteTaskAsync(STask task, string functionDetails) // Add functionDetails parameter
        {
            string prompt = GeneratePromptForTask(task, functionDetails); // Pass functionDetails to GeneratePromptForTask
            string generatedCode = await _geminiAiService.GenerateTextAsync(prompt, null, null);
            return generatedCode;
        }

        public string GeneratePromptForTask(STask task, string functionDetails)
        {
            StringBuilder promptBuilder = new StringBuilder();

            promptBuilder.AppendLine("## Task Description:");
            promptBuilder.AppendLine(task.Description);
            promptBuilder.AppendLine();

            // Get function details from Text_FunctionDetails.Text
            promptBuilder.AppendLine("## Function Instruction Details:");
            promptBuilder.AppendLine(functionDetails); // Assuming "Form1" is your form's name 
            promptBuilder.AppendLine();

            promptBuilder.AppendLine("## System Instructions:");
            promptBuilder.AppendLine("- Write clean and well-formatted C# code.");
            promptBuilder.AppendLine("- Use appropriate `using` statements for necessary namespaces.");
            promptBuilder.AppendLine("- Implement robust error handling using try-catch blocks.");
            promptBuilder.AppendLine("- Use the Roslyn Compiler API if it is helpful for this task.");
            promptBuilder.AppendLine("- Dont generate any Test Methods OR Test Class");
            promptBuilder.AppendLine("- Dont generate any Explain");
            promptBuilder.AppendLine("- Just Generate The Task Code ");
            promptBuilder.AppendLine("- Use This template in the Response with this Format ");
            promptBuilder.AppendLine();

            promptBuilder.AppendLine("## Generate C# Code:");
            promptBuilder.AppendLine("[StartCode]"); // Start code block
            promptBuilder.AppendLine("// AI Model will generate the C# code here");
            promptBuilder.AppendLine("[EndCode]"); // End code block

            return promptBuilder.ToString();
        }
    }
}