using System.Configuration;
using System.Collections.Specialized;

namespace TaskFirst
{
    public static class ConfigManager
    {
        private static string _pathToInputDirectory = string.Empty;
        private static string _pathToOutputDirectory = string.Empty;
        public static string PathToInputDirectory 
        {
            get
            {
                if (string.IsNullOrEmpty(_pathToInputDirectory))
                {
                    _pathToInputDirectory = GetConfigValue("InputDirectory");
                }
                return _pathToInputDirectory;
            }
        }
        public static string PathToOutputDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_pathToOutputDirectory))
                {
                    _pathToOutputDirectory = GetConfigValue("OutputDirectory");
                }
                return _pathToOutputDirectory;
            }
        }
                        
        private static string? GetConfigValue(string key) => ConfigurationManager.AppSettings.Get(key);

        // Checking function on if existing config file and settings of path to input and output directory
        // Need to modify algorithm checking settings 
        public static bool IsExistConfigAndSettings()
        {
            if (File.Exists(System.Reflection.Assembly.GetEntryAssembly().Location + ".config"))
            {
                if (!string.IsNullOrEmpty(GetConfigValue("InputDirectory")) && !string.IsNullOrEmpty(GetConfigValue("OutputDirectory")))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
