using SoulCenterProject.Interfaces;
using SoulCenterProject.Models.Soul_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Services
{
    internal class ILlamaService : ILlama
    {
        public Task<(string context, List<Soul_Entities> entities)> ExtractContextAndEntitiesAsync(object messageContent)
        {
            throw new NotImplementedException();
        }
    }
}
