//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    // ... (Your existing models) ...

    public class User
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }
        // In a full setup, you might navigate to the 'Role' object from here
    }

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }
        // Potentially a list of 'Permission' objects if we represent that directly
    }

    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Project
    {
        public string Category { get; set; }

        public string category_id { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ProgrammingLanguage { get; set; }

        public string Tags { get; set; }
    }

    public class Idea
    {
        public string Details { get; set; }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }
    }

    public class Requirement
    {
        public string Details { get; set; }

        public int Id { get; set; }

        public int? IdeaId { get; set; } // Note: Optional link to idea 

        public int ProjectId { get; set; }

        public string Status { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }
    }

    public class Task
    {
        public string AssignedUsers { get; set; } // Comma-separated, refine later if needed

        public DateTime? Deadline { get; set; } // Note: Optional deadline

        public string Details { get; set; }

        public int Id { get; set; }

        public int RelatedItemId { get; set; }

        public string RelatedItemType { get; set; }

        public string Status { get; set; }

        public string Tags { get; set; }

        public string Title { get; set; }
    }

    public class GeminiSession
    {
        public string Details { get; set; }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Tags { get; set; }
    }

    public class Documentation
    {
        public string Details { get; set; }

        public string DocType { get; set; }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }
    }

    public class Note
    {
        public string Details { get; set; }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }
    }
}
