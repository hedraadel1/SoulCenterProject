using SoulCenterProject.Models.Soul_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoulCenterProject.Interfaces
{
    /// <summary>
    /// Defines the interface for managing conversations.
    /// </summary>
    public interface IConversationManager
    {
        /// <summary>
        /// Creates a new conversation.
        /// </summary>
        /// <param name="conversationName">The name of the new conversation.</param>
        /// <param name="agentId">The ID of the agent handling the conversation.</param>
        /// <returns>The newly created conversation.</returns>
        Task<Soul_Conversation> CreateConversationAsync(string conversationName, int agentId);

        /// <summary>
        /// Retrieves an existing conversation by its ID.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to retrieve.</param>
        /// <returns>The conversation, or null if not found.</returns>
        Task<Soul_Conversation> GetConversationAsync(int conversationId);

        /// <summary>
        /// Updates an existing conversation.
        /// </summary>
        /// <param name="conversation">The updated conversation object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateConversationAsync(Soul_Conversation conversation);

        /// <summary>
        /// Adds a new message to an existing conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to add the message to.</param>
        /// <param name="message">The message to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddMessageToConversationAsync(int conversationId, Soul_Message message);

        /// <summary>
        /// Retrieves messages for a given conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation.</param>
        /// <returns>A list of messages for the conversation.</returns>
        Task<List<Soul_Message>> GetMessagesAsync(int conversationId);

        /// <summary>
        /// Retrieves messages that should be included in the AI history for a given conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of messages for the conversation history.</returns>
        Task<List<Soul_Message>> GetMessagesForHistoryAsync(int conversationId);

        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="message">The message to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateMessageAsync(Soul_Message message);

        /// <summary>
        /// Deletes an existing message.
        /// </summary>
        /// <param name="messageId">The ID of the message to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteMessageAsync(int messageId);

        /// <summary>
        /// Changes the SendToAiHistory value of a message.
        /// </summary>
        /// <param name="messageId">The ID of the message to update.</param>
        /// <param name="sendToAiHistory">The new value for SendToAiHistory.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ChangeSendToAiHistoryAsync(int messageId, bool sendToAiHistory);

        /// <summary>
        /// Changes the PinTheMessage value of a message.
        /// </summary>
        /// <param name="messageId">The ID of the message to update.</param>
        /// <param name="pinTheMessage">The new value for PinTheMessage.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ChangePinTheMessageAsync(int messageId, bool pinTheMessage);
    }
}