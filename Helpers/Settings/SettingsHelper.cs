//-----------------------------------------------------------------------
// <copyright file="SettingsHelper.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Newtonsoft.Json;
using System.IO;

namespace SoulCenterProject.Helpers.Settings
{
    /// <summary>
    /// Helper class for loading and saving application settings.
    /// </summary>
    /// <remarks>
    /// This class provides methods for loading and saving database settings from/to a JSON file.
    /// </remarks>
    public static class SettingsHelper
    {
        private static readonly string SettingsFilePath = Path.Combine("Helpers", "JsonFiles", "DatabaseSettings.json");

        /// <summary>
        /// Loads database settings from the settings file.
        /// </summary>
        /// <returns>The loaded database settings.</returns>
        /// <remarks>
        /// This method reads database settings from the DatabaseSettings.json file and deserializes them into a
        /// DatabaseSettings object.
        /// </remarks>
        public static DatabaseSettings LoadDatabaseSettings()
        {
            if(File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<DatabaseSettings>(json);
            } else
            {
                // Handle file not found or empty settings file
                return null;
            }
        }

        /// <summary>
        /// Saves database settings to the settings file.
        /// </summary>
        /// <param name="settings">The database settings to save.</param>
        /// <remarks>
        /// This method serializes the provided DatabaseSettings object into JSON format and writes it to the
        /// DatabaseSettings.json file.
        /// </remarks>
        public static void SaveDatabaseSettings(DatabaseSettings settings)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}