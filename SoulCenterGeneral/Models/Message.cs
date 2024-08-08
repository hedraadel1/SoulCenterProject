//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    public enum MessageType
    {
        BasicType,
        TagType
    }

    public class Message
    {
        public int Id { get; set; }

        public string Summary { get; set; }

        public string Tags { get; set; } // Comma-separated list

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public MessageType Type { get; set; }

        public int UserId { get; set; } // If applicable
    }
}
