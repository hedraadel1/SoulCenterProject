using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SoulCenterProject.Helpers.Gemini
{
    public class GeminiApi22
    {
        private HttpClient _httpClient = new HttpClient();  // Create a private member to hold the HttpClient instance


        public void SaveResponse(string UserInput, string parsedResult, string chatResponse, string senderName)
        {
            string filePath = @"E:\conversation_history.txt";
            string usermessage = senderName + ": " + UserInput;
            string messageToSave;

            messageToSave = $"You: {usermessage}";
            File.AppendAllText(filePath, messageToSave + Environment.NewLine);

            messageToSave = $"----------------------------------------------";
            File.AppendAllText(filePath, messageToSave + Environment.NewLine);

            messageToSave = $"Gemini: {chatResponse}";
            File.AppendAllText(filePath, messageToSave + Environment.NewLine);
        }

        public void SaveTxtFile(string filePath, string JsonDatetime,string Content)
        {
            string JsonContent = JsonDatetime + ": " + Content;
            string JsonToSave;

            JsonToSave = $"JsonDetails: {JsonContent}";
            File.AppendAllText(filePath, JsonToSave + Environment.NewLine);

            JsonToSave = $"----------------------------------------------";
            File.AppendAllText(filePath, JsonToSave + Environment.NewLine);
        }
        public void UpdateTextFileFromRadRichText(string filePath, string input)
        {
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the RadRichTextBox content to the file
                writer.Write(input);
            }
        }

        public void ClearTextFile(string filePath)
        {
            // Open the file for writing with 'truncate' mode
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                // No need to write anything, opening in truncate mode clears the content
            }
        }
        public string LoadLastNMessages(int numberOfMessagesToLoad)
        {
            string filePath = @"E:\conversation_history.txt";

            // 1. Check if the file exists
            if (!File.Exists(filePath))
            {
                return "";
            }

            // 2. Read all lines from the file
            string[] allLines = File.ReadAllLines(filePath);

            // 3. Get the last N messages 
            int startIndex = Math.Max(0, allLines.Length - numberOfMessagesToLoad);
            var lastMessages = allLines.Skip(startIndex).Take(numberOfMessagesToLoad);

            // 4. Combine the messages into a single string (adjust formatting as needed)
            return string.Join(Environment.NewLine, lastMessages);
        }
        public string ReadTextFile(string filePath)
        {
            try
            {
                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read the entire file contents
                    string fileContent = File.ReadAllText(filePath);

                    return fileContent;
                }
                else
                {
                    // Handle the case where the file doesn't exist
                    Console.WriteLine($"Error: File not found at path: {filePath}");
                    return null; // Or throw an exception if you prefer
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during file reading
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null; // Or throw an exception if you prefer
            }
        }
       
        
        private HttpClient ConnectHttpClient()
        {
            var client = new HttpClient();
           
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            
        }

        //   "htt ps://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro-latest:generateContent?key=",


     private static string geminiapikey = "AIzaSyDaZFLO437UaDGqxGp-EyhAniQkGhXAxrY";

          private static string apiurl = @"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=";
           private static string geminiapi = apiurl + geminiapikey;
        public async Task<string> ConnectGeminiAsync(string Usermessage)
        {
            try
            {

                string oldMessages = "";//LoadLastNMessages(3);

                //if (string.IsNullOrWhiteSpace(oldMessages))
                //{
                //    oldMessages = "No messages yet.";
                //}

                var client = ConnectHttpClient();

                var requestData = new
                {
                    contents = new List<object>
                    {
                        new
                        {
                            parts = new List<object>
                            {
                                new { text = Usermessage },
                                new { text = oldMessages } // Include the loaded history
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.9,
                        topK = 1,
                        topP = 1,
                        maxOutputTokens = 2048,
                        stopSequences = new List<string>()
                    },
                    safetySettings = new List<object>
                    {
                        new { category = "HARM_CATEGORY_HARASSMENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
                        new { category = "HARM_CATEGORY_HATE_SPEECH", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
                        new { category = "HARM_CATEGORY_SEXUALLY_EXPLICIT", threshold = "BLOCK_MEDIUM_AND_ABOVE" },
                        new { category = "HARM_CATEGORY_DANGEROUS_CONTENT", threshold = "BLOCK_MEDIUM_AND_ABOVE" }
                    }
                };

                string json = JsonConvert.SerializeObject(requestData);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

               

                HttpResponseMessage response = await client.PostAsync(geminiapi,data);
                             string chatResponse = "";
                
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    // Parse the response and add it to the ListView
                    var parsedResult = JsonConvert.DeserializeObject<dynamic>(result);

                    if (parsedResult != null && parsedResult.Count > 0 && parsedResult[0].candidates != null &&
                        parsedResult[0].candidates.Count > 0)
                    {
                        if (parsedResult[0].candidates[0].content != null &&
                            parsedResult[0].candidates[0].content.parts != null &&
                            parsedResult[0].candidates[0].content.parts.Count > 0)
                        {
                            var partscount = parsedResult.Count;

                            for (int i = 0; i < partscount; i++)
                            {
                                chatResponse += parsedResult[1].candidates[0].content.parts[0].text;
                            }

                            // Save the conversation to a file
                            SaveResponse(Usermessage, parsedResult, chatResponse, "Hedra");

                            return chatResponse;
                        }

                        else
                        {
                            return "Error: Could not extract response text. Check JSON structure.";
                        }
                    }
                    else
                    {
                        return "Error: Could not parse JSON response.";
                    }
                }
            }

            catch (Exception ex)
            {
                return "Context: not parse JSON response." + ex.Message.ToString();
            }

            return "An error occurred. Please check the logs.";
        }

    }
}
