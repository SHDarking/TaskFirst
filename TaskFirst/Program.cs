using System.Text.RegularExpressions;

namespace TaskFirst
{
    public static class Program
    {
        public static void Main(string[] args)
        {            
            if (ConfigManager.IsExistConfigAndSettings())
            {
                AppManager.StartProgram();
            }
            else
            {
                Console.WriteLine("Configuration file was deleted or empty, fix that and retry start a program.");
            }
        }
    }
}
