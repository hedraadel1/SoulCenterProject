using Dapper;

using MySql.Data.MySqlClient;
using SoulCenterProject;
using SoulCenterProject.Models.Soul_Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

public class SoulStudioService
{
    private readonly string _connectionString;
    // جلب كل البيانات من جدول Soul_Prompts
    public List<Soul_Prompts> GetAllPrompts()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Query<Soul_Prompts>("SELECT * FROM Soul_Prompts").ToList();
        }
    }

    // جلب بيانات عنصر محدد بواسطة PromptID
    public Soul_Prompts GetPromptById(int promptId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Soul_Prompts>(
                "SELECT * FROM Soul_Prompts WHERE PromptID = @PromptID", new { PromptID = promptId });
        }
    }

    // إضافة عنصر جديد
    public void AddPrompt(Soul_Prompts prompt)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(
                @"INSERT INTO Soul_Prompts (PromptName, PromptDescription, CategoryID, FullPromptText, CreatedDate, CreatedBy) 
                    VALUES (@PromptName, @PromptDescription, @CategoryID, @FullPromptText, @CreatedDate, @CreatedBy)", prompt);
        }
    }

    // تعديل عنصر موجود
    public void UpdatePrompt(Soul_Prompts prompt)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(
                @"UPDATE Soul_Prompts SET 
                        PromptName = @PromptName, 
                        PromptDescription = @PromptDescription, 
                        CategoryID = @CategoryID, 
                        FullPromptText = @FullPromptText, 
                        CreatedDate = @CreatedDate, 
                        CreatedBy = @CreatedBy 
                      WHERE PromptID = @PromptID", prompt);
        }
    }

    // حذف عنصر
    public void DeletePrompt(int promptId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(
                "DELETE FROM Soul_Prompts WHERE PromptID = @PromptID", new { PromptID = promptId });
        }
    }
    public void SaveResponse(string UserInput, string parsedResult, string chatResponse, string senderName)
    {
        string filePath = @"C:\conversation_history.txt";
        string usermessage = senderName + ": " + UserInput;
        string messageToSave;

        messageToSave = $"You: {usermessage}";
        File.AppendAllText(filePath, messageToSave + Environment.NewLine);

        messageToSave = $"----------------------------------------------";
        File.AppendAllText(filePath, messageToSave + Environment.NewLine);

        messageToSave = $"Gemini: {chatResponse}";
        File.AppendAllText(filePath, messageToSave + Environment.NewLine);

    }

    public string LoadLastNMessages(int numberOfMessagesToLoad)
    {
        string filePath = @"C:\conversation_history.txt";

        // 1. Check if the file exists
        if (!File.Exists(filePath))
        {
            return "";
        }

        // 2. Read all lines from the file
        string[] allLines = File.ReadAllLines(filePath);

        // 3. Get the last N messages 
        int startIndex = Math.Max(0, allLines.Length - numberOfMessagesToLoad);
        var lastMessages = allLines.Skip(startIndex).Take(numberOfMessagesToLoad);

        // 4. Combine the messages into a single string (adjust formatting as needed)
        return string.Join(Environment.NewLine, lastMessages);
    }
    public SoulStudioService(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Inserts a new SoulPromptComponents record into the database.
    /// </summary>
    /// <param name="component">The SoulPromptComponents object to insert.</param>
    /// <returns>The ComponentID of the newly inserted record, or -1 if an error occurred.</returns>
    /// <summary>
    /// Inserts a new Soul_Prompts_Types record if it doesn't exist.
    /// </summary>
    /// <param name="componentTypeName">The ComponentTypeName to insert.</param>
    /// <returns>The ComponentTypeID of the newly inserted record, or -1 if an error occurred.</returns>
    public int InsertSoulPromptType(string componentTypeName)
    {
        if (string.IsNullOrEmpty(componentTypeName))
        {
            return -1;
        }


        // Check if the type already exists
        int existingTypeId = GetComponentTypeID(componentTypeName);
        if (existingTypeId != -1)
        {
            return existingTypeId; // Type already exists, return its ID
        }

        string sql = @"
                INSERT INTO Soul_Prompts_Types (ComponentTypeName) VALUES (@ComponentTypeName);
                SELECT LAST_INSERT_ID();";

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                int componentTypeID = connection.ExecuteScalar<int>(sql, new { ComponentTypeName = componentTypeName });
                return componentTypeID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error inserting Soul_Prompts_Types: {ex.Message}");
                return -1;
            }
        }
    }

    /// <summary>
    /// Inserts a new Soul_PromptCategory record if it doesn't exist.
    /// </summary>
    /// <param name="categoryName">The CategoryName to insert.</param>
    /// <returns>The CategoryID of the newly inserted record, or -1 if an error occurred.</returns>
    public int InsertSoulPromptCategory(string categoryName)
    {
        if (string.IsNullOrEmpty(categoryName))
        {
            return -1;
        }


        // Check if the category already exists
        int existingCategoryId = GetCategoryID(categoryName);
        if (existingCategoryId != -1)
        {
            return existingCategoryId; // Category already exists, return its ID
        }

        string sql = @"
                INSERT INTO Soul_PromptComponentCategories (CategoryName) VALUES (@CategoryName);
                SELECT LAST_INSERT_ID();";

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                int categoryID = connection.ExecuteScalar<int>(sql, new { CategoryName = categoryName });
                return categoryID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error inserting Soul_PromptComponentCategories: {ex.Message}");
                return -1;
            }
        }
    }

    /// <summary>
    /// Inserts a new SoulPromptComponents record into the database.
    /// </summary>
    /// <param name="component">The SoulPromptComponents object to insert.</param>
    /// <returns>The ComponentID of the newly inserted record, or -1 if an error occurred.</returns>
    public int InsertSoulPromptComponent(SoulPromptComponents component)
    {
        // Validation
        if (component == null || string.IsNullOrEmpty(component.ComponentName))
        {
            component.ReturnMessage = "Component is null or ComponentName is null";
            return -1;
        }


        // Get ComponentTypeID or insert if not found
        int componentTypeID = component.ComponentTypeID;
        if (componentTypeID == -1)
        {
            component.ReturnMessage = "componentTypeID is -1 when try to Insert new Prompt Component";
            return -1;
            //componentTypeID = InsertSoulPromptType(component.componentTypeName);
        }


        // Get CategoryID or insert if not found
        int categoryID = component.CategoryID;
        if (categoryID == -1)
        {
            component.ReturnMessage = "categoryID is -1 when try to Insert new Prompt Component";

            return -1;
            //categoryID = InsertSoulPromptCategory(component.CategoryName);
        }


        // Proceed with inserting the SoulPromptComponents only if both IDs are valid
        if (componentTypeID == -1 && categoryID == -1)
        {
            string sql = @"
                    INSERT INTO Soul_PromptComponents (ComponentName, ComponentValue, ComponentTypeID, CategoryID, Custom1, Custom2, Custom3, Custom4, Custom5, isEnabled, isActive) 
                    VALUES (@ComponentName, @ComponentValue, @ComponentTypeID, @CategoryID, @Custom1, @Custom2, @Custom3, @Custom4, @Custom5, @isEnabled, @isActive);
                    SELECT LAST_INSERT_ID();";

            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    int componentID = connection.ExecuteScalar<int>(sql, new
                    {
                        component.ComponentName,
                        component.ComponentValue,
                        ComponentTypeID = componentTypeID,
                        CategoryID = categoryID,
                        component.Custom1,
                        component.Custom2,
                        component.Custom3,
                        component.Custom4,
                        component.Custom5,
                        component.isEnabled,
                        component.isActive
                    });

                    return componentID;
                }
                catch (Exception ex)
                {
                    component.ReturnMessage = "categoryID is -1 when try to Insert new Prompt Component -- The Error is : " + ex.Message;

                    Console.WriteLine($@"Error inserting SoulPromptComponent: {ex.Message}");
                    return -1;
                }
            }
        }
        else
        {
            // Handle the case where either ComponentTypeID or CategoryID is still invalid 
            component.ReturnMessage = "Error: Could not obtain valid IDs for ComponentType or Category";

            Console.WriteLine($@"Error: Could not obtain valid IDs for ComponentType or Category.");
            return -1;
        }
    }

    /// <summary>
    /// Gets the ComponentTypeID for a given ComponentTypeName from the Soul_Prompts_Types table.
    /// </summary>
    /// <param name="componentTypeName">The ComponentTypeName to search for.</param>
    /// <returns>The ComponentTypeID, or -1 if not found or an error occurred.</returns>
    public int GetComponentTypeID(string componentTypeName)
    {
        // Validation
        if (string.IsNullOrEmpty(componentTypeName))
        {
            return -1; // Return -1 if componentTypeName is invalid
        }

        string sql = "SELECT ComponentTypeID FROM Soul_Prompts_Types WHERE ComponentTypeName = @ComponentTypeName";

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                // Use QueryFirstOrDefault to handle the case when no record is found
                int componentTypeID =
                    connection.QueryFirstOrDefault<int>(sql, new { ComponentTypeName = componentTypeName });


                // Return -1 if componentTypeID is 0 (indicating no record found)
                return componentTypeID == 0 ? -1 : componentTypeID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error getting ComponentTypeID: {ex.Message}");
                return -1; // Return -1 if an error occurred
            }
        }
    }

    /// <summary>
    /// Gets the CategoryID for a given CategoryName from the Soul_PromptCategory table.
    /// </summary>
    /// <param name="categoryName">The CategoryName to search for.</param>
    /// <returns>The CategoryID, or -1 if not found or an error occurred.</returns>
    public int GetCategoryID(string categoryName)
    {
        // Validation
        if (string.IsNullOrEmpty(categoryName))
        {
            return -1; // Return -1 if categoryName is invalid
        }

        string sql = "SELECT ID FROM Soul_PromptComponentCategories WHERE CategoryName = @CategoryName";

        using (IDbConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                // Use QueryFirstOrDefault to handle the case when no record is found
                int categoryID = connection.QueryFirstOrDefault<int>(sql, new { CategoryName = categoryName });


                // Return -1 if categoryID is 0 (indicating no record found)
                return categoryID == 0 ? -1 : categoryID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error getting CategoryID: {ex.Message}");
                return -1; // Return -1 if an error occurred
            }
        }
    }
    public Soul_Agents GetAgentDataByAgentId(int agentId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var agentData = connection.QuerySingleOrDefault<Soul_Agents>(
                "SELECT * FROM Soul_Agents WHERE AgentID = @AgentID",
                new { AgentID = agentId });
            return agentData;
        }
    }
    public SoulPromptComponents GetPromptComponentsByID(int Componentid)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var soulPromptComponents = connection.QuerySingleOrDefault<SoulPromptComponents>(
                "SELECT * FROM Soul_PromptComponents WHERE ComponentID = @Componentid",
                new { ComponentID = Componentid });
            return soulPromptComponents;
        }
    }
    public SoulModelInfo GetModelinfotDataByname(string name)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var ModelinfoData = connection.QuerySingleOrDefault<SoulModelInfo>(
                "SELECT * FROM Soul_ModelInfo WHERE Name = @Name",
                new { Name = name });
            return ModelinfoData;
        }
    }
    public void InsertNewAgentData(Soul_Agents agentModel)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var sql = @"INSERT INTO Agents (AgentName, AgentApiID, AgentModelID, AgentMaxOutput, AgentMaxInput, 
                                         AgentHARMCATEGORYHARASSMENT, AgentHARMCATEGORYHATESPEECH, 
                                         AgentHARMCATEGORYSEXUALLYEXPLICIT, AgentHARMCATEGORYDANGEROUSCONTENT, 
                                         AgentType, AgentLanguage, AgentCapabilities, AgentSettings, 
                                         DefaultConverstion, IsActive, CreatedDate, AgentDescription, AgentAvatar) 
                         VALUES (@AgentName, @AgentApiID, @AgentModelID, @AgentMaxOutput, @AgentMaxInput, 
                                 @AgentHARMCATEGORYHARASSMENT, @AgentHARMCATEGORYHATESPEECH, 
                                 @AgentHARMCATEGORYSEXUALLYEXPLICIT, @AgentHARMCATEGORYDANGEROUSCONTENT, 
                                 @AgentType, @AgentLanguage, @AgentCapabilities, @AgentSettings, 
                                 @DefaultConverstion, @IsActive, @CreatedDate, @AgentDescription, @AgentAvatar)";
            connection.Execute(sql, agentModel);
        }
    }




    public void BuildPromptAndSystemInstructions(
       string richTextBox1Text,
       string richTextBox2Text,
       string richTextBox3Text,
       string richTextBox4Text,
       SoulPromptComponents systemInstructionsComponent,
       SoulPromptComponents promptComponent,
       SoulPromptComponents domainKnowledgeComponent,
       bool isPromptDomainKnowledge,
       SoulPromptComponents examplesComponent,
       bool isPromptExamples,
       SoulPromptComponents contextComponent,
       bool isPromptContext,
       SoulPromptComponents styleComponent,
       SoulPromptComponents rulesComponent,
       bool isPromptRules,
       SoulPromptComponents evaluationCriteriaComponent,
       bool isPromptEvaluationCriteria,
       SoulPromptComponents responseTemplateComponent,
       bool isPromptResponseTemplate,
       SoulPromptComponents promptNotesComponent,
       bool isPromptPromptNotes,
       out string systemInstructions,
       out string prompt
   )
    {
        //  تهيئة المتغيرات
        prompt = "";
        systemInstructions = "";

        //  إضافة محتوى الـ RichTextBoxes
        prompt += richTextBox1Text;
        prompt += richTextBox2Text;

        //  استبدال الاختصارات في الـ RichTextBoxات 3 و 4
        prompt = prompt.Replace("{x}", richTextBox3Text); //  استبدال {x} 
        prompt = prompt.Replace("{y}", richTextBox4Text); //  استبدال {y} - ممكن تغيير الاختصار

        //  إضافة محتوى الـ ComboBoxes
        if (systemInstructionsComponent != null)
        {
            systemInstructions += systemInstructionsComponent.ComponentValue;
        }

        if (promptComponent != null)
        {
            prompt += promptComponent.ComponentValue;
        }

        if (domainKnowledgeComponent != null)
        {
            if (isPromptDomainKnowledge)
            {
                prompt += domainKnowledgeComponent.ComponentValue;
            }
            else
            {
                systemInstructions += domainKnowledgeComponent.ComponentValue;
            }
        }

        if (examplesComponent != null)
        {
            if (isPromptExamples)
            {
                prompt += examplesComponent.ComponentValue;
            }
            else
            {
                systemInstructions += examplesComponent.ComponentValue;
            }
        }

        if (contextComponent != null)
        {
            if (isPromptContext)
            {
                prompt += contextComponent.ComponentValue;
            }
            else
            {
                systemInstructions += contextComponent.ComponentValue;
            }
        }

        if (styleComponent != null)
        {
            systemInstructions += styleComponent.ComponentValue;
        }

        if (rulesComponent != null)
        {
            if (isPromptRules)
            {
                prompt += rulesComponent.ComponentValue;
            }
            else
            {
                systemInstructions += rulesComponent.ComponentValue;
            }
        }

        if (evaluationCriteriaComponent != null)
        {
            if (isPromptEvaluationCriteria)
            {
                prompt += evaluationCriteriaComponent.ComponentValue;
            }
            else
            {
                systemInstructions += evaluationCriteriaComponent.ComponentValue;
            }
        }

        if (responseTemplateComponent != null)
        {
            if (isPromptResponseTemplate)
            {
                prompt += responseTemplateComponent.ComponentValue;
            }
            else
            {
                systemInstructions += responseTemplateComponent.ComponentValue;
            }
        }

        if (promptNotesComponent != null)
        {
            if (isPromptPromptNotes)
            {
                prompt += promptNotesComponent.ComponentValue;
            }
            else
            {
                systemInstructions += promptNotesComponent.ComponentValue;
            }
        }
    }


}