//-----------------------------------------------------------------------
// <copyright file="MySQLRetriever.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SoulCenterProject.Models;

namespace SoulCenterProject.Test_phase_items.xx___OldSqlOpertationManage
{
    public class MySQLRetriever
    {
        private readonly string _connectionString;

        public MySQLRetriever(string connectionString) { _connectionString = connectionString; }

        public List<Category> GetAllCategories()
        {
            // (Similar to GetAllProjects with necessary adjustments) 
            List<Category> categories = new List<Category>();

            using(MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM categories";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Category category = new Category();
                            category.Id = reader.GetInt32("id");
                            category.Name = reader.GetString("name");

                            categories.Add(category);
                        }
                    }
                } catch(MySqlException ex)
                {
                    // Handle MySQL errors
                    Console.WriteLine("Error retrieving categories: " + ex.Message);
                }
            }

            return categories;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            using(MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM projects";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Project project = new Project();
                            project.Id = reader.GetInt32("id");
                            project.Name = reader.GetString("name");
                            // ... (Map other fields from reader)

                            projects.Add(project);
                        }
                    }
                } catch(MySqlException ex)
                {
                    // Handle MySQL errors appropriately
                    // Example: Log the error, show a message box to the user, etc.
                    Console.WriteLine("Error retrieving projects: " + ex.Message);
                }
            }

            return projects;
        }

        // You can add more retrieval methods for other data entities here ...
    }
}