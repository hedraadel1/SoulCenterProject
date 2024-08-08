using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SoulCenterProject.Interfaces;
using SoulCenterProject.Models.Soul_Models;
using SoulCenterProject.Modules.Ai.Services;

namespace SoulCenterProject.Services
{
    /// <summary>
    /// Manages the extraction, storage, and retrieval of context and entities from conversations.
    /// </summary>
    public class ContextAndEntityManagerService : IContextAndEntityManager
    {
        private readonly IMemoryManager _memoryManager;
        private readonly GeminiAiService _geminiAiService;
        private readonly IConversationManager _conversationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextAndEntityManagerService"/> class.
        /// </summary>
        /// <param name="memoryManager">The memory manager to use for saving and retrieving context and entities.</param>
        /// <param name="geminiAiService">The Gemini AI service to use for extracting context and entities.</param>
        /// <param name="conversationManager">The conversation manager to use for retrieving messages for history.</param>
        public ContextAndEntityManagerService(IMemoryManager memoryManager, GeminiAiService geminiAiService, IConversationManager conversationManager)
        {
            _memoryManager = memoryManager;
            _geminiAiService = geminiAiService;
            _conversationManager = conversationManager;
        }


        /// <summary>
        /// Extracts context and entities from a given message using a language model.
        /// </summary>
        /// <param name="message">The message to analyze.</param>
        /// <returns>A task representing the asynchronous operation, containing a tuple with the extracted context and a list of entities.</returns>
        public async Task<(Context Context, List<Soul_Entities> Entities)> ExtractContextAndEntitiesAsync(Soul_Message message)
        {
            try
            {
                // 1. Get conversation history WITH message content (JOIN)
                var conversationHistory = await _conversationManager.GetMessagesForHistoryAsync(message.ConversationID.Value);
                var chatHistory = conversationHistory.Select(m => new ChatMessage
                {
                    Role = m.MessageSenderType,
                    // Access MessageContent directly from Soul_Message (after the JOIN)
                    Content = m.MessageContent
                }).ToList();

                // 2. Extract context and entities using Gemini API
                string systemInstruction = @"
                    You are a helpful AI assistant.
                    Please provide me with a summary of the context of the conversation and identify any entities in the following message.
                    Return the information in JSON format with the following structure: 
                    ```json
                    {
                        ""context"": {
                            ""topic1"": ""summary1"",
                            ""topic2"": ""summary2"",
                            // ... 
                        },
                        ""entities"": [
                            {
                                ""entityType"": ""type1"",
                                ""entityValue"": ""value1""
                            },
                            {
                                ""entityType"": ""type2"",
                                ""entityValue"": ""value2""
                            },
                            // ... 
                        ]
                    }
                    ```
                "; //  systemInstruction  مفصلة  
                string response = await _geminiAiService.GenerateTextAsync(message.MessageContent, chatHistory, systemInstruction);

                // 3. Parse JSON response
                var parsedResponse = JsonConvert.DeserializeObject<GeminiResponse>(response);

                // 4. Save to memory
                await SaveContextAndEntitiesAsync(message.ConversationID.Value, new Context { ContextItems = parsedResponse.Context }, parsedResponse.Entities);

                // 5. Return extracted data
                return (new Context { ContextItems = parsedResponse.Context }, parsedResponse.Entities);
            }
            catch (Exception ex)
            {
                throw new Exception("Error extracting context and entities.", ex);
            }
        }

        /// <summary>
        /// Saves the extracted context and entities to memory.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="context">The extracted context.</param>
        /// <param name="entities">The extracted entities.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SaveContextAndEntitiesAsync(int conversationId, Context context, List<Soul_Entities> entities)
        {
            try
            {
                var contextJson = JsonConvert.SerializeObject(context);
                await _memoryManager.SaveToMemoryAsync(conversationId, "context", contextJson);

                if (entities != null)
                {
                    foreach (var entity in entities)
                    {
                        await _memoryManager.SaveToMemoryAsync(conversationId, $"entity_{entity.EntityID}", JsonConvert.SerializeObject(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving context and entities to memory.", ex);
            }
        }

        /// <summary>
        /// Retrieves the memory (context and entities) for a given conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to retrieve data for.</param>
        /// <returns>A task representing the asynchronous operation, containing a tuple with the retrieved context and a list of entities.</returns>
        public async Task<(Context Context, List<Soul_Entities> Entities)> GetMemoryAsync(int conversationId)
        {
            try
            {
                var contextJson = await _memoryManager.GetFromMemoryAsync(conversationId, "context");
                var context = string.IsNullOrEmpty(contextJson) ? new Context() : JsonConvert.DeserializeObject<Context>(contextJson);

                var entities = new List<Soul_Entities>();
                // Retrieve all entity keys from memory
                var entityKeys = await _memoryManager.GetEntityKeysAsync(conversationId);

                foreach (var entityKey in entityKeys)
                {
                    var entityJson = await _memoryManager.GetFromMemoryAsync(conversationId, entityKey);
                    entities.Add(JsonConvert.DeserializeObject<Soul_Entities>(entityJson));
                }

                return (context, entities);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving memory for conversation.", ex);
            }
        }

        // Class to parse Gemini's response (for temporary entity and context extraction)
        private class GeminiResponse
        {
            [JsonProperty("context")]
            public Dictionary<string, string> Context { get; set; }

            [JsonProperty("entities")]
            public List<Soul_Entities> Entities { get; set; }
        }
    }
}