using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SoulCenterProject.Modules.Ai.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoulCenterProject.Modules.Ai.Model
{
    public class GeminiRequest
    {
        public List<content> contents { get; set; }
 
        public List<safetysetting> safetySettings { get; set; }
        public content systemInstruction { get; set; }
        public generationConfig generationConfig { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new StringEnumConverter());
        }
    }
    public class content
    {
        public List<part> parts { get; set; }
        public string role { get; set; }
    }

    public class part
    {
        public string text { get; set; }
    }
       
     

    public class safetysetting
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HarmCategory Category { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HarmBlockThreshold Threshold { get; set; }
    }

    public enum HarmCategory
    {
        HARM_CATEGORY_UNSPECIFIED,
        HARM_CATEGORY_DEROGATORY,
        HARM_CATEGORY_TOXICITY,
        HARM_CATEGORY_VIOLENCE,
        HARM_CATEGORY_SEXUAL,
        HARM_CATEGORY_MEDICAL,
        HARM_CATEGORY_DANGEROUS,
        HARM_CATEGORY_HARASSMENT,
        HARM_CATEGORY_HATE_SPEECH,
        HARM_CATEGORY_SEXUALLY_EXPLICIT,
        HARM_CATEGORY_DANGEROUS_CONTENT
    }

    public enum HarmBlockThreshold
    {
        HARM_BLOCK_THRESHOLD_UNSPECIFIED,
        BLOCK_LOW_AND_ABOVE,
        BLOCK_MEDIUM_AND_ABOVE,
        BLOCK_ONLY_HIGH,
        BLOCK_NONE
    }

    public class generationConfig
    {
        public double? temperature { get; set; }
        public double? topP { get; set; }
        public int? topK { get; set; }
        public int? maxOutputTokens { get; set; }
        public string response_mime_type { get; set; }
    }
     
    public static class GeminiRequestBuilder
    {
        public static string BuildRequestBodyjson(string userMessage,
                                    List<ChatMessage> conversationHistory,
                                    string systemInstruction,
                                    generationConfig generationConfig)
        {
            // 1. Process conversationHistory into Content objects
            var contents = new List<content>();
            if (conversationHistory != null)
            {
                foreach (var message in conversationHistory)
                {
                    contents.Add(new content
                    {
                        role = message.Role,
                        parts = new List<part>
                    {
                        new part { text = $"(this message was in this datetime - {message.MessageDateTime:yyyy-MM-dd HH:mm:ss}) {message.Content}" }
                    }
                    });
                }
                if (userMessage!= null) { 
                contents.Add(new content
                {
                    role = "user",
                    parts = new List<part>
                    { 
                        new part { text = $" {userMessage}" }
                    }
                });
                }
            }

            // 2. Create systemInstruction Content object 
            var systemInstructionContent = new content
            {
                role = "system",
                parts = new List<part>
            {
                new part { text = $"{systemInstruction}" }
            }
            };

            // 3. Create static safetySettings 

            var requestBody = new GeminiRequest
            {
                contents = contents,
                systemInstruction = systemInstructionContent,
                generationConfig = generationConfig

            };
            return requestBody.ToJson();
        }
    }
    
   
}
