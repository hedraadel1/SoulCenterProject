using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace SoulCenterProject
{
    public class GeminiRequestService
    {
        private readonly string apiKey;
        private readonly string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-pro-latest:generateContent";

        public GeminiRequestService(string apiKey)
        {
            this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }
 
        public async Task<string> GenerateTextAsync(string userMessage, string conversationHistory = "")
        {
            try
            {
                var client = new HttpClient();

                var requestData = new Dictionary<string, object>()
                {
                    { "contents", new List<object>()
                        {
                            new Dictionary<string, object>()
                            {
                                {
                                    systemInstructions = new { text = @"please write any code marked with triple backticks like this <<```(.*?)```>> " },
                                    "parts", new List<object>()
                                    {
                                        new { text = userMessage },
                                        new { text = conversationHistory }
                                    }
                                }
                            }
                        }
                    },
                    { "generationConfig", new
                    {
                        temperature = 0.9,
                        topK = 1,
                        topP = 1,
                        maxOutputTokens = 2048,
                        stopSequences = new List<string>()
                    }},
                    { "safetySettings", new
                    {
                        categories = new List<string>()
                    {
                        "HARM_CATEGORY_HARASSMENT",
                        "HARM_CATEGORY_HATE_SPEECH",
                        "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                        "HARM_CATEGORY_DANGEROUS_CONTENT"
                    },
                        threshold = "BLOCK_MEDIUM_AND_ABOVE"
                    }}
                };

                var json = JsonConvert.SerializeObject(requestData);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{apiUrl}?key={apiKey}", data);

                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var result = JObject.Parse(resultJson);

                    if (ValidateResponseStructure(result))
                    {
                       // var generatedText = ExtractResponseText(result);
                        var ggeneratedTexte = ExtractTextFromResponse(result);
                        // SaveResponse(userMessage, result, generatedText, "Hedra"); // Assuming this function exists
                        return generatedText;
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected JSON response structure from Gemini API.");
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Gemini API request failed with status code {response.StatusCode}: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                // ... 

                throw new Exception("Failed to generate text from Gemini API.", ex);
            }
        }

        private bool ValidateResponseStructure(JObject response)
        {
            return response.ContainsKey("0")
                && response["0"] is JArray candidates
                && candidates.Count > 0
                && candidates[0] is JObject candidate
                && candidate.ContainsKey("content")
                && candidate["content"] is JObject content
                && content.ContainsKey("parts")
                && content["parts"] is JArray parts
                && parts.Count > 0;
        }
        private string ExtractResponseText(JObject response)
        {
            var parts = (JArray)response["0"]["candidates"][0]["content"]["parts"];
            var text = new StringBuilder();
            foreach (JObject part in parts)
            {
                text.Append(part["text"].ToString());
            }
            return text.ToString();
        }
        private string ExtractTextFromResponse(string json)
        {
            // ... existing code ...

            // Check for code presence using a regex (adjust the pattern as needed)
            var codeRegex = new Regex("```(.*?)```", RegexOptions.Singleline);
            var codeMatch = codeRegex.Match(firstPart["text"].ToString());

            if (codeMatch.Success)
            {
                // Extract and display code
                string code = codeMatch.Groups[1].Value;
                ResponseContainer_Code.Text = code;

                // Remove code from the main response text
                return firstPart["text"].ToString().Replace(codeMatch.Value, "");
            }
            else
            {
                // No code found, return the full response text
                return firstPart["text"].ToString();
            }
        }
    }
}