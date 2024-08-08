//-----------------------------------------------------------------------
// <copyright file="ApiKeyInfo.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SoulCenterProject.Models.Ai_Models
{
    public class ApiKeyInfo
    {
        /// <summary>
        /// The company associated with the AI model.
        /// </summary>
        public string ApiCompany { get; set; }

        /// <summary>
        /// The API key for the AI model.
        /// </summary>
        public string Apikey { get; set; }

        /// <summary>
        /// The name of the account associated with the API key.
        /// </summary>
        public string ApiKey_AccountName { get; set; }
    }
}