using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFirst.FileHandlerHelpers;
using TaskFirst.Models;

namespace TaskFirst
{
    public class FileHandler
    {
        private static object locker = new object();
        private readonly string _pathToInputDirectory;
        private readonly string _pathToOutputDirectory;
       
        

        public FileHandler()
        {
            _pathToInputDirectory = ConfigManager.PathToInputDirectory;
            _pathToOutputDirectory = ConfigManager.PathToOutputDirectory;
        }
        public static string? GetRawFiles()
        {
            List<string>? selectedFiles = null;
            lock (locker)
            {
                Indexer indexer = Indexer.GetInstanceIndexer();
                selectedFiles = Directory.GetFiles(ConfigManager.PathToInputDirectory)
                    .Where(e => indexer.FilesInIndex.All(f => e != f))
                    .Where(x => Path.GetExtension(x) == ".txt" || Path.GetExtension(x) == ".csv").ToList();
                if (selectedFiles.Count == 0)
                {
                    return null;
                }
                
                indexer.FilesInIndex.Add(selectedFiles.First());
            }
            return selectedFiles.First();
        }

        public List<InputModel> ReadFileToModel(string path)
        {            
            List<InputModel> result = new List<InputModel>();
            var extension = Path.GetExtension(path);
            FileExtension fileExtension;
            switch (extension)
            {
                case ".txt":
                    fileExtension = FileExtension.txt;
                    break;
                case ".csv":
                    fileExtension = FileExtension.csv;
                    break;
                default:
                    fileExtension = FileExtension.txt;
                    break;
            }
            result = new Reader().Read(path, fileExtension);            
            return result;
        }

        public void WriteToJsonFile(List<OutputModel> models)
        {
            Writer writer = new Writer();
            writer.WriteModelToJsonFile(CreateJsonFileForWriting(),models);
        }

        private string CreateJsonFileForWriting()
        {
            string nameOfTodayDirectory = DateTime.Now.Date.ToString("MM-dd-yyyy");
            string pathToTodayDirectory = _pathToOutputDirectory + "\\" + nameOfTodayDirectory;
            Directory.CreateDirectory(pathToTodayDirectory);
            var files = Directory.GetFiles(pathToTodayDirectory).Where(e => Path.GetExtension(e) == ".json").ToList();
            int indexOfFile = 1;
            if (files.Count != 0)
            {
                indexOfFile = files.Count + 1;
            }
            string nameOfFile = $"output{indexOfFile}.json";
            string pathToFile = pathToTodayDirectory + '\\' + nameOfFile;
            File.Create(pathToFile).Close();
            return pathToFile;

        }

        public static bool AreHaveNewSourceFiles()
        {
            Indexer indexer = Indexer.GetInstanceIndexer();
            var files = Directory.GetFiles(ConfigManager.PathToInputDirectory)
                .Where(e => Path.GetExtension(e) == ".txt" || Path.GetExtension(e) == ".csv").ToList();
            var count = files.Count;
            bool haveNewFiles = false;
            if (files.Count != 0)
            {
                haveNewFiles = files.All(e => indexer.FilesInIndex.All(f => e != f));
            }
            return haveNewFiles;
        }

        public void DeleteInputFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                File.Delete(path);
            }
        }
        
        public static void WriteLog(Logger logger)
        {            
            string pathToDirectory = ConfigManager.PathToOutputDirectory + "\\" + DateTime.Now.Date.AddDays(-1).ToString("MM-dd-yyyy");
            new Writer().WriteLogToFile(pathToDirectory, logger);
        }
        
        public enum FileExtension
        {
            txt,
            csv
        }
        
    }
}
