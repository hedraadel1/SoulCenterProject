using System.Collections.Generic;
using System.Threading.Tasks;
using SoulCenterProject.Helpers.Gemini.Model;

namespace SoulCenterProject.Helpers.Gemini.Dal_Dapper
{
    public interface ICategoryRepository
    {
        Task<List<SoulCategory>> GetCategoriesByType(string tableType);
        Task<List<SoulCategory>> GetAllCategories();
        Task<SoulCategory> GetCategory(int id);
        Task<IEnumerable<SoulCategory>> GetCategories();
        Task<SoulCategory> AddCategory(SoulCategory category);
        Task UpdateCategory(SoulCategory category);
        Task DeleteCategory(int id);

    }
}