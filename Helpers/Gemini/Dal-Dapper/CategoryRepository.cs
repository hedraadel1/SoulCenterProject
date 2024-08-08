using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SoulCenterProject.Helpers.Gemini.Model;

namespace SoulCenterProject.Helpers.Gemini.Dal_Dapper
{
    /// <summary>
    /// Provides an implementation of the <see cref="ICategoryRepository"/> interface for managing categories.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection _db;

        public CategoryRepository(IDbConnection db)
        {
            _db = db;
        }


        public async Task<List<SoulCategory>> GetCategoriesByType(string tableType)
        {
            var results = await _db.QueryAsync<SoulCategory>(
              "SELECT * FROM SoulCategory WHERE TableType = @TableType",
              new { TableType = tableType });

            return results.ToList();
        }
        public async Task<List<SoulCategory>> GetAllCategories()
        {
            return (await _db.QueryAsync<SoulCategory>("SELECT * FROM Soul_Categories")).ToList();
        }




        public async Task<SoulCategory> GetCategory(int CategoryID)
        {
            return await _db.QueryFirstOrDefaultAsync<SoulCategory>(
                "SELECT * FROM Soul_Categories WHERE CategoryID = @CategoryID",
                new { CategoryID = CategoryID });
        }

        public async Task<IEnumerable<SoulCategory>> GetCategories()
        {
            return await _db.QueryAsync<SoulCategory>("SELECT * FROM Soul_Categories");
        }
        public async Task<SoulCategory> AddCategory(SoulCategory soulcategory)
        {
            await _db.ExecuteScalarAsync(
              @"INSERT INTO Soul_Categories (CategoryName, TableType)
      VALUES (@CategoryName, @TableType);", soulcategory);

            // Assuming CategoryID is set automatically by MariaDB

            return soulcategory;
        }

        public async Task UpdateCategory(SoulCategory soulcategory)
        {
            await _db.ExecuteAsync(
                "UPDATE Soul_Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID",
                soulcategory);
        }

        public async Task DeleteCategory(int CategoryID)
        {
            await _db.ExecuteAsync(
                "DELETE FROM Soul_Categories WHERE CategoryID = @CategoryID",
                new { CategoryID = CategoryID });
        }
    }
}
