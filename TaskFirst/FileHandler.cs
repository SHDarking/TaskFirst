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
        private readonly string _pathToInputDirectory;
        private readonly string _pathToOutputDirectory;
        private string _pathToInputFile;
        private FileExtension _fileExtension;

        public FileHandler()
        {
            _pathToInputDirectory = ConfigManager.PathToInputDirectory;
            _pathToOutputDirectory = ConfigManager.PathToOutputDirectory;
        }
        public FileHandler GetFileFromIndex()
        {
            // getting path to input file from index
            var selectedFile = Directory.GetFiles(_pathToInputDirectory).First();
            _pathToInputFile = selectedFile;
            var fileExtension = Path.GetExtension(selectedFile);
            switch (fileExtension)
            {
                case "txt":
                    _fileExtension = FileExtension.txt;
                    break;
                case "csv":
                    _fileExtension= FileExtension.csv;
                    break;
                default:
                    _fileExtension = FileExtension.txt;
                    break;
            }
            return this;
        }

        public List<InputModel>? ReadFileToModel()
        {
            List<InputModel>? result = new List<InputModel>();
            result = new Reader().Read(_pathToInputFile, _fileExtension);
            if (result == null)
            {
                // call logger 
            }
            return result;
        }
        
        
        

        public bool WriteFile(List<OutputModel> models, string path)
        {
            Writer writer = new Writer();
            return true;
        }

        public enum FileExtension
        {
            txt,
            csv
        }
        
    }
}
