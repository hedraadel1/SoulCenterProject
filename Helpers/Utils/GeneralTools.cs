using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SoulCenterProject.Helpers.Utils
{


    public class GeneralTools
    {
        private static readonly string ConversationHistoryFilePath = Path.Combine("E:\\", "conversation_history.txt");
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void SaveResponse(string userInput, string parsedResult, string chatResponse, string senderName)
        {
            string userMessage = $"{senderName}: {userInput}";
            string messageToSave = $"You: {userMessage}";
            AppendToFile(ConversationHistoryFilePath, messageToSave);

            messageToSave = "----------------------------------------------";
            AppendToFile(ConversationHistoryFilePath, messageToSave);

            messageToSave = $"Gemini: {chatResponse}";
            AppendToFile(ConversationHistoryFilePath, messageToSave);
        }

        public void SaveTxtFile(string filePath, string jsonDateTime, string content)
        {
            string jsonContent = $"{jsonDateTime}: {content}";
            string messageToSave = $"JsonDetails: {jsonContent}";
            AppendToFile(filePath, messageToSave);

            messageToSave = "----------------------------------------------";
            AppendToFile(filePath, messageToSave);
        }

        public void UpdateTextFileFromRadRichText(string filePath, string input)
        {
            File.WriteAllText(filePath, input);
        }

        public void ClearTextFile(string filePath)
        {
            File.WriteAllText(filePath, string.Empty);
        }

        public string LoadLastNMessages(int numberOfMessagesToLoad)
        {
            if (!File.Exists(ConversationHistoryFilePath))
            {
                return string.Empty;
            }

            string[] allLines = File.ReadAllLines(ConversationHistoryFilePath);
            int startIndex = Math.Max(0, allLines.Length - numberOfMessagesToLoad);
            var lastMessages = allLines.Skip(startIndex).Take(numberOfMessagesToLoad);

            return string.Join(Environment.NewLine, lastMessages);
        }

        public string ReadTextFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                else
                {
                    Logger.Error($"File not found at path: {filePath}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error reading file: {ex.Message}");
                return null;
            }
        }

        public string GetLastApiRequestOrResponse(string responseType)
        {
            try
            {
                // Get the directory where the assembly (your application) is located
                string assemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // Path to the "Geminifiles" folder
                string geminiFilesPath = Path.Combine(assemblyLocation, "Geminifiles");

                // Check if the directory exists
                if (!Directory.Exists(geminiFilesPath))
                {
                    return "Error: Geminifiles folder not found.";
                }

                // Get files of type request or response, sorted by creation time (newest first)
                var files = Directory.EnumerateFiles(geminiFilesPath, $"Gemini_{responseType}_*.txt")
                    .OrderByDescending(f => new FileInfo(f).CreationTime);

                // Get the first (newest) file
                string lastFile = files.FirstOrDefault();

                // Read the content of the file
                if (lastFile != null)
                {
                    string jsonContent = File.ReadAllText(lastFile);

                    // Try to parse the JSON and format it
                    if (JToken.Parse(jsonContent) is JObject jsonObject)
                    {
                        return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    }
                    else
                    {
                        return jsonContent;
                    }
                }
                else
                {
                    return $"No {responseType} files found.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error
                Console.WriteLine($@"Error reading {responseType} file: {ex.Message}");
                return $"Error reading {responseType} file.";
            }
        }
        private static void AppendToFile(string filePath, string content)
        {
            try
            {
                File.AppendAllText(filePath, content + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error writing to file: {ex.Message}");
            }
        }
    }


}
