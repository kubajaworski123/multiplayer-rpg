using System;
using System.IO;
using System.Text.Json;

namespace MazeGame.Model.Configuration
{
    public static class ConfigLoader
    {
        private const string ConfigFileName = "config.json";

        public static Config LoadConfig()
        {
            if (!File.Exists(ConfigFileName))
            {
                var defaultConfig = new Config();
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(defaultConfig, options);
                File.WriteAllText(ConfigFileName, json);
                return defaultConfig;
            }

            try
            {
                string existingJson = File.ReadAllText(ConfigFileName);
                return JsonSerializer.Deserialize<Config>(existingJson) ?? new Config();
            }
            catch (Exception ex)
            {
                return new Config();
            }
        }
    }
}