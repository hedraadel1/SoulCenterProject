using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

namespace SoulCenterProject
{
    public class GeminiApi
    {
        private string _apiKey;
        private string _apiEndpoint;
        private string _projectId;
        private string _locationId;
        private string _modelId;

        public GeminiApi(string apiKey, string apiEndpoint, string projectId, string locationId, string modelId)
        {
            _apiKey = apiKey;
            _apiEndpoint = apiEndpoint;
            _projectId = projectId;
            _locationId = locationId;
            _modelId = modelId;
        }

        public async Task<string> GenerateText(string prompt, string systemInstructions = "",
            List<ChatMessage> conversationHistory = null)
        {
            var client = new RestClient(_apiEndpoint);
            var request =
                new RestRequest(
                    $"v1/projects/{_projectId}/locations/{_locationId}/publishers/google/models/{_modelId}:streamGenerateContent",
                    Method.Post);

            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddHeader("Content-Type", "application/json");


            // Define request body using classes
            var requestBody = new RequestBody
            {
                contents = new Content[]
                {
                    new Content
                    {
                        role = "user",
                        parts = new Part[]
                        {
                            new Part
                            {
                                text = prompt
                            }
                        }
                    }
                },
                systemInstruction = new Part[]
                {
                    new Part
                    {
                        text = systemInstructions
                    }
                },
                generationConfig = new GenerationConfig
                {
                    maxOutputTokens = 8192,
                    temperature = 1,
                    topP = 0.95
                },
                safetySettings = new SafetySetting[]
                {
                    new SafetySetting
                    {
                        category = "HARM_CATEGORY_HATE_SPEECH",
                        threshold = "BLOCK_ONLY_HIGH"
                    },
                    new SafetySetting
                    {
                        category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                        threshold = "BLOCK_ONLY_HIGH"
                    },
                    new SafetySetting
                    {
                        category = "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                        threshold = "BLOCK_ONLY_HIGH"
                    },
                    new SafetySetting
                    {
                        category = "HARM_CATEGORY_HARASSMENT",
                        threshold = "BLOCK_ONLY_HIGH"
                    }
                }
            };

            if (conversationHistory != null)
            {
                requestBody.contents = conversationHistory.Select(message => new Content
                {
                    role = message.Role,
                    parts = new Part[]
                    {
                        new Part
                        {
                            text = message.Content
                        }
                    }
                }).ToArray();
            }

            request.AddJsonBody(requestBody);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Process the response here, extract the generated text.
                return response.Content;
            }
            else
            {
                // Handle error here.
                throw new Exception($"Error calling Gemini API: {response.ErrorMessage}");
            }
        }
    }

    public class Content
    {
        public string role { get; set; }
        public Part[] parts { get; set; }
    }

    public class Part
    {
        public string text { get; set; }
    }

    public class RequestBody
    {
        public Content[] contents { get; set; }
        public Part[] systemInstruction { get; set; }
        public GenerationConfig generationConfig { get; set; }
        public SafetySetting[] safetySettings { get; set; }
    }

    public class GenerationConfig
    {
        public int maxOutputTokens { get; set; }
        public int temperature { get; set; }
        public double topP { get; set; }
    }

    public class SafetySetting
    {
        public string category { get; set; }
        public string threshold { get; set; }
    }

    public class ChatMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
        public string MessageDateTime { get; set; }
    }
}