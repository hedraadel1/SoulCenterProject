using Dapper;
using MySql.Data.MySqlClient;
using SoulCenterProject.Interfaces; 
using SoulCenterProject.Models.Soul_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Ai.Services
{
    /// <summary>
    /// Manages conversations and related data, including messages.
    /// </summary>
    public class ConversationManagerService : IConversationManager
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationManagerService"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public ConversationManagerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates a new conversation.
        /// </summary>
        /// <param name="conversationName">The name of the new conversation.</param>
        /// <param name="agentId">The ID of the agent handling the conversation.</param>
        /// <returns>A task representing the asynchronous operation, containing the newly created conversation.</returns>
        public async Task<Soul_Conversation> CreateConversationAsync(string conversationName, int agentId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = @"
                        INSERT INTO Soul_Conversation (ConversationName, AgentID) VALUES (@conversationName, @agentId);
                        SELECT LAST_INSERT_ID();";

                    var conversationId = await connection.ExecuteScalarAsync<int>(sql, new { conversationName, agentId });

                    return new Soul_Conversation { ConversationID = conversationId, ConversationName = conversationName, AgentID = agentId };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating conversation.", ex);
            }
        }

        /// <summary>
        /// Adds a new message to an existing conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to add the message to.</param>
        /// <param name="message">The message to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddMessageToConversationAsync(int conversationId, Soul_Message message)
        {
            try
            {
                using (var connection = new MySqlConnector.MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            // 1. Insert the message with content
                            var insertMessageSql = @"
                        INSERT INTO Soul_Message (MessageType, ConversationID, MessageSenderType, 
                                                MessageSenderName, MessageDatetime, MessageWordsCount, 
                                                RepliedToMessageID, SendToAiHistory, PinTheMessage, MessageContent)
                        VALUES (@MessageType, @ConversationID, @MessageSenderType, @MessageSenderName, 
                                @MessageDatetime, @MessageWordsCount, @RepliedToMessageID, 
                                @SendToAiHistory, @PinTheMessage, @MessageContent);
                        SELECT LAST_INSERT_ID();";

                            var messageId = await connection.ExecuteScalarAsync<int>(
                                insertMessageSql,
                                new
                                {
                                    message.MessageType,
                                    message.ConversationID,
                                    message.MessageSenderType,
                                    message.MessageSenderName,
                                    message.MessageDatetime,
                                    message.MessageWordsCount,
                                    message.RepliedToMessageID,
                                    message.SendToAiHistory,
                                    message.PinTheMessage,
                                    message.MessageContent // Directly insert message content
                                },
                                transaction
                            );

                            // 2. Update message count for the conversation
                            var updateConversationSql = @"
                        UPDATE Soul_Conversation 
                        SET MessagesCount = MessagesCount + 1 
                        WHERE ConversationID = @ConversationId";
                            await connection.ExecuteAsync(updateConversationSql, new { conversationId }, transaction);

                            // Update the message object with the generated MessageID
                            message.MessageID = messageId;

                            // Commit transaction 
                            await transaction.CommitAsync();
                        }
                        catch (Exception innerEx)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception("Error adding message to conversation. Transaction rolled back.", innerEx);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding message to conversation.", ex);
            }
        }

        public Soul_Conversation GetConversation(string conversationId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var conversation = connection.QuerySingleOrDefault<Soul_Conversation>(
                   "SELECT * FROM Soul_Conversation WHERE ConversationID = @conversationId",
                   new { conversationId });

                return conversation;
            }
        }
        /// <summary>
        /// Retrieves an existing conversation by its ID.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the retrieved conversation, or null if not found.</returns>
        /// <exception cref="Exception">Error retrieving conversation.</exception>
        public async Task<Soul_Conversation> GetConversationAsync(int conversationId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    // Retrieve the conversation from the database
                    var conversation = connection.QuerySingleOrDefault<Soul_Conversation>(
                        "SELECT * FROM Soul_Conversation WHERE ConversationID = @conversationId",
                        new { conversationId });

                    return conversation;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving conversation.", ex);
            }

        }

        /// <summary>
        /// Updates an existing conversation.
        /// </summary>
        /// <param name="conversation">The updated conversation object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateConversationAsync(Soul_Conversation conversation)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    // Update the conversation in the database
                    await connection.ExecuteAsync(
                        "UPDATE Soul_Conversation SET ConversationName = @ConversationName, AgentID = @AgentID, StartDateTime = @StartDateTime, MessagesCount = @MessagesCount WHERE ConversationID = @ConversationID",
                        conversation);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating conversation.", ex);
            }
        }

        /// <summary>
        /// Retrieves messages for a given conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of messages for the conversation.</returns>
        public async Task<List<Soul_Message>> GetMessagesAsync(int conversationId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = @"
                SELECT * 
                FROM Soul_Message     
                WHERE ConversationID = @ConversationId 
                AND SendToAiHistory = 1
                OR PinTheMessage = 1
                ORDER BY MessageDatetime";

                    // Since we're retrieving all message data from Soul_Message directly, 
                    // we can use Query<Soul_Message>
                    var messages = await connection.QueryAsync<Soul_Message>(sql, new { ConversationId = conversationId });

                    return messages.AsList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving messages for conversation.", ex);
            }
        }

        /// <summary>
        /// Retrieves messages that should be included in the AI history for a given conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of messages for the conversation history.</returns>
        public async Task<List<Soul_Message>> GetMessagesForHistoryAsync(int conversationId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = @"
                SELECT *
                FROM Soul_Message 
                WHERE ConversationID = @ConversationId AND SendToAiHistory = 1
                ORDER BY MessageDatetime";

                    // Similar to GetMessagesAsync, we can use Query<Soul_Message> here
                    var messages = await connection.QueryAsync<Soul_Message>(sql, new { ConversationId = conversationId });
                    return messages.AsList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving messages for conversation history.", ex);
            }
        }
        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="message">The message to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateMessageAsync(Soul_Message message)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    //  Update the message
                    var updateMessageSql = @"
                        UPDATE Soul_Message 
                        SET MessageType = @MessageType, MessageSenderType = @MessageSenderType,MessageContent = @MessageContent, MessageSenderName = @MessageSenderName,
                            MessageDatetime = @MessageDatetime, MessageWordsCount = @MessageWordsCount, RepliedToMessageID = @RepliedToMessageID, 
                            SendToAiHistory = @SendToAiHistory, PinTheMessage = @PinTheMessage 
                        WHERE MessageID = @MessageID";
                    await connection.ExecuteAsync(updateMessageSql, message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating message.", ex);
            }
        }

        /// <summary>
        /// Deletes an existing message.
        /// </summary>
        /// <param name="messageId">The ID of the message to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteMessageAsync(int messageId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    
                    // 2. Delete the message
                    var deleteMessageSql = "DELETE FROM Soul_Message WHERE MessageID = @messageId";
                    await connection.ExecuteAsync(deleteMessageSql, new { messageId });

                    // 3. Update the MessagesCount in Soul_Conversation
                    var conversationIdSql = "SELECT ConversationID FROM Soul_Message WHERE MessageID = @messageId";
                    var conversationId = await connection.ExecuteScalarAsync<int>(conversationIdSql, new { messageId });
                    await connection.ExecuteAsync(
                        "UPDATE Soul_Conversation SET MessagesCount = MessagesCount - 1 WHERE ConversationID = @conversationId",
                        new { conversationId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting message.", ex);
            }
        }

        /// <summary>
        /// Changes the SendToAiHistory value of a message.
        /// </summary>
        /// <param name="messageId">The ID of the message to update.</param>
        /// <param name="sendToAiHistory">The new value for SendToAiHistory.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ChangeSendToAiHistoryAsync(int messageId, bool sendToAiHistory)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = "UPDATE Soul_Message SET SendToAiHistory = @sendToAiHistory WHERE MessageID = @messageId";
                    await connection.ExecuteAsync(sql, new { sendToAiHistory, messageId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing SendToAiHistory.", ex);
            }
        }

        /// <summary>
        /// Changes the PinTheMessage value of a message.
        /// </summary>
        /// <param name="messageId">The ID of the message to update.</param>
        /// <param name="pinTheMessage">The new value for PinTheMessage.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ChangePinTheMessageAsync(int messageId, bool pinTheMessage)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = "UPDATE Soul_Message SET PinTheMessage = @pinTheMessage WHERE MessageID = @messageId";
                    await connection.ExecuteAsync(sql, new { pinTheMessage, messageId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing PinTheMessage.", ex);
            }
        }
    }
}