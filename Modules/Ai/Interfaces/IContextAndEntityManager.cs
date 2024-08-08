using System.Collections.Generic;
using System.Threading.Tasks;
using SoulCenterProject.Models.Soul_Models;

namespace SoulCenterProject.Interfaces
{
    /// <summary>
    /// Defines the interface for extracting and managing context and entities.
    /// </summary>
    public interface IContextAndEntityManager
    {
        /// <summary>
        /// Extracts context and entities from a given message using a language model.
        /// </summary>
        /// <param name="message">The message to analyze.</param>
        /// <returns>A tuple containing the extracted context and a list of entities.</returns>
        Task<(Context Context, List<Soul_Entities> Entities)> ExtractContextAndEntitiesAsync(Soul_Message message);

        /// <summary>
        /// Saves the extracted context and entities to memory.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="context">The extracted context.</param>
        /// <param name="entities">The extracted entities.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveContextAndEntitiesAsync(int conversationId, Context context, List<Soul_Entities> entities);

        /// <summary>
        /// Retrieves the memory (context and entities) for a given conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to retrieve data for.</param>
        /// <returns>A tuple containing the retrieved context and a list of entities.</returns>
        Task<(Context Context, List<Soul_Entities> Entities)> GetMemoryAsync(int conversationId);
    }
}
