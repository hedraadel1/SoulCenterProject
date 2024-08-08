//-----------------------------------------------------------------------
// <copyright file="GeminiAiService.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static SoulCenterProject.soulstudio;

namespace SoulCenterProject.Modules.Ai.Services
{
    using SoulCenterProject.Modules.Ai.Model;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "StyleCop.SA1309")]
    public class GeminiAiService
    {

        private string _apiKey = "AIzaSyAOhXQtle2SHm-LC6KcoG_2NOFMbTP9n_w";
        private string _apiKey2 = "AIzaSyA53KFWPIGa2cjb7EHf4HDlKajKzMnR418";


        soulstudio _soulstu = new soulstudio();


        /// <summary>
        /// #GenerateTextAsync Generates text using the Gemini AI service.
        /// </summary>
        ///! <param name="userMessage">The user's input message.</param>
        /// <param name="conversationHistory">A list of previous messages to provide context.</param>
        /// <param name="systemInstructions">System instructions for the AI model.</param>
        /// <param name="rules">Specific rules to guide the AI's response (optional).</param>
        /// <returns>A task representing the asynchronous operation, containing the generated text.</returns>

        public async Task<string> GenerateTextAsync(string userMessage, List<ChatMessage> conversationHistory = null, string systemInstructions = "", string rules = "",string response_mime_type="")
        {
            try
            {
                generationConfig GenerationConfigvar;
                //////
                _soulstu.ReportSoulLog(new SoulLog
                {
                    LogAddress = "GenerateTextAsync() method",
                    LogDetails = "Starting Gemini AI request...",
                    Step = OperationStep.Working
                });
                //////
                
                if (response_mime_type != null)
                {
                     GenerationConfigvar = new generationConfig
                    {
                        temperature = 1,
                        topK = 64,
                        topP = 0.95,
                        maxOutputTokens = 8192,
                        response_mime_type = "application/json"
                    };
                }
                else
                {
                     GenerationConfigvar = new generationConfig
                    {
                        temperature = 1,
                        topK = 64,
                        topP = 0.95,
                        maxOutputTokens = 8192
                    };
                }
               
                //////
                using (var client = new HttpClient())
                {
                    //1- prepare the Request Body
                    string jsonRequestBody = GeminiRequestBuilder.BuildRequestBodyjson(userMessage, conversationHistory, systemInstructions, GenerationConfigvar);
                    
                    //2- Api url
                    var requestUri = new Uri(
                        $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-pro:generateContent?key={this._apiKey}");

                    //3-prepare the Request content 
                    var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    //4-Save a copy from the request in txt file for debuging 
                    await SaveApiRequestAndResponseToFileAsync(jsonRequestBody, responseType: "request");

                    //5-Send the request and wait the response
                    var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                  
                    //6- Extract the generated text from the response
                    var responseString = await response.Content.ReadAsStringAsync();

                    //7- Save a copy from the response in txt file for debuging 
                    await SaveApiRequestAndResponseToFileAsync(responseString, responseType: "response");

                    //8- Parse the JSON response and return it in string
                    return ExtractTextFromResponse(responseString);
                }
            }
            catch (Exception ex)
            {
                _soulstu.ReportSoulLog(new SoulLog
                {
                    LogAddress = "GenerateTextAsync() method",
                    LogDetails = $"Error: {ex.Message}",
                    Step = OperationStep.Failed
                });
                var err = "Error generating text from Gemini AI." + ex.ToString();
                return err;
            }
        }

 
        public async Task SaveApiRequestAndResponseToFileAsync(string contentToSave, string responseType)
        {
            try
            {
                // Get the directory where the assembly (your application) is located
                string assemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // Create the "Geminifiles" folder if it doesn't exist
                string geminiFilesPath = Path.Combine(assemblyLocation, "Geminifiles");
                Directory.CreateDirectory(geminiFilesPath);

                // Generate a unique filename with request/response type
                string fileName = $"Gemini_{responseType}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(geminiFilesPath, fileName);

                // Write the content to the file asynchronously
                await Task.Run(() => File.WriteAllText(filePath, contentToSave));

                // Log to UI (if needed)
                _soulstu.ReportSoulLog(new SoulLog
                {
                    LogAddress = "SaveApiRequestAndResponseToFileAsync() method",
                    LogDetails = $"{responseType} saved to: {filePath}",
                    Step = OperationStep.Working
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error 
                Console.WriteLine($@"Error saving {responseType}: {ex.Message}");
                throw; // Re-throw to propagate the exception if needed 
            }
        }


        private string ExtractTextFromResponse(string json)
        {
            // Parse the JSON response
            var responseObject = JObject.Parse(json);
            var firstCandidate = responseObject["candidates"]?[0];
            var content = firstCandidate?["content"];
            Debug.Assert(content != null, nameof(content) + " != null");
            var firstPart = content?["parts"]?[0];
            return firstPart?["text"]?.ToString();
        }
         
    }

    public class ChatMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
        public string MessageDateTime { get; set; }
    }
    public class SoulFunction
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDescription { get; set; }
        public string FunctionCategory { get; set; }
        public string Parameters { get; set; } //  ممكن  تخليه  List<Parameter>  لو  حبيت
        public string ReturnType { get; set; }
    }
    public enum OperationSteps
    {
        Completed,

        Error,

        Working,

    }
}
