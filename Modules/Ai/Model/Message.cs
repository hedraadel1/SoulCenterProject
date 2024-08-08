//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace SoulCenterProject.Modules.Ai.Model
{
    public class Message
    {
        public string Summary { get; set; }

        public string Tags { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public MessageType Type { get; set; }

        public string UserId { get; set; }
    }
}
