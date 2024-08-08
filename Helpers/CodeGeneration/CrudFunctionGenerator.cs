using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoulCenterProject.Helpers.CodeGeneration
{
    public class CrudFunctionGenerator
    {
        public string GenerateCreateFunctionCode(string tableName, List<string> columnNames, string entityClassName)
        {
            if (string.IsNullOrEmpty(tableName) || columnNames == null || columnNames.Count == 0 || string.IsNullOrEmpty(entityClassName))
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            StringBuilder codeBuilder = new StringBuilder();

            //  بداية  الـ Function
            codeBuilder.AppendLine($@"public async Task<{entityClassName}> Create{entityClassName}Async({entityClassName} entity)");
            codeBuilder.AppendLine($@"{{");
            codeBuilder.AppendLine($@"    try");
            codeBuilder.AppendLine($@"    {{");
            codeBuilder.AppendLine($@"        using (var connection = new MySqlConnection(_connectionString))");
            codeBuilder.AppendLine($@"        {{");

            //  بناء  جملة  INSERT
            string columns = string.Join(", ", columnNames);
            string values = string.Join(", ", columnNames.Select(c => $"@{c}"));
            codeBuilder.AppendLine($@"            string sql = @""INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT LAST_INSERT_ID();"";");

            //  بناء  Object  لـ  Parameters
            codeBuilder.AppendLine($@"            var parameters = new {{ ");
            for (int i = 0; i < columnNames.Count; i++)
            {
                string columnName = columnNames[i];
                codeBuilder.AppendLine($@"                {columnName} = entity.{columnName},");
            }
            codeBuilder.AppendLine($@"            }};");

            //  تنفيذ  SQL  وإرجاع  ID
            codeBuilder.AppendLine($@"            var newEntityId = await connection.ExecuteScalarAsync<int>(sql, parameters);");
            codeBuilder.AppendLine($@"            entity.ID = newEntityId; "); //  افتراضًا،  اسم  الـ  ID  هو  "ID"
            codeBuilder.AppendLine($@"            return entity;");

            //  نهاية  الـ Function
            codeBuilder.AppendLine($@"        }}");
            codeBuilder.AppendLine($@"    }}");
            codeBuilder.AppendLine($@"    catch (Exception ex)");
            codeBuilder.AppendLine($@"    {{");
            codeBuilder.AppendLine($@"        throw new Exception(""Error creating {entityClassName}"", ex);");
            codeBuilder.AppendLine($@"    }}");
            codeBuilder.AppendLine($@"}}");

            return codeBuilder.ToString();
        }
        public string GenerateSimpleInsertSql(string tableName, List<string> columnNames, List<object> values, bool includeLastInsertId = false)
        {
            if (string.IsNullOrEmpty(tableName) || columnNames == null || values == null || columnNames.Count != values.Count)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            string columns = string.Join(", ", columnNames);
            string formattedValues = string.Join(", ", values.Select(v => $"'{Regex.Escape(v?.ToString() ?? "")}'"));

            string sql = $@"INSERT INTO {tableName} ({columns}) VALUES ({formattedValues});";

            if (includeLastInsertId)
            {
                sql += " SELECT LAST_INSERT_ID();";
            }

            return sql;
        }
    }
}
