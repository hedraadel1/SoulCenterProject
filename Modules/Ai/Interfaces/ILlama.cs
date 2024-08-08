using System.Collections.Generic;
using System.Threading.Tasks;
using SoulCenterProject.Models.Soul_Models;

namespace SoulCenterProject.Interfaces 
{
    public interface ILlama
    {
        Task<(string context, List<Soul_Entities> entities)> ExtractContextAndEntitiesAsync(object messageContent);
    }
}