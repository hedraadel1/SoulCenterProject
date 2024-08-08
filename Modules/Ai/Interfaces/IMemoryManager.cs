using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Interfaces
{
    /// <summary>
    /// Defines the interface for managing memory.
    /// </summary>
    public interface IMemoryManager
    {
        /// <summary>
        /// Saves data to memory, both in RAM and the database.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="key">The unique key for the data.</param>
        /// <param name="value">The data to be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveToMemoryAsync(int conversationId, string key, string value);

        /// <summary>
        /// Retrieves data from memory. First checks RAM, then the database.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="key">The unique key for the data.</param>
        /// <returns>The retrieved data, or null if not found.</returns>
        Task<string> GetFromMemoryAsync(int conversationId, string key);

        /// <summary>
        /// Removes data from memory (both RAM and database).
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="key">The unique key for the data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ForgetMemoryAsync(int conversationId, string key);

        Task<List<string>> GetEntityKeysAsync(int conversationId);
    }
}
