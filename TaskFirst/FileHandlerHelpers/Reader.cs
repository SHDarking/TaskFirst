using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFirst.Models;

namespace TaskFirst.FileHandlerHelpers
{
    internal class Reader
    {
        private string _pattern = @"[a-zA-Z\s]+,[a-zA-Z\s]*,\s*\W\w+,\s*[a-zA-Z]+\s*\d+,\s*\d+\W,\s*\d+.\d+,\s*\d{4}-\d{2}-\d{2},\s*\W*\d+\W*,\s*\w+";
        public List<InputModel>? Read(string path, FileHandler.FileExtension extension)
        {
            List<InputModel>? list = null;
            Parser parser = new Parser();
            using (StreamReader stream = new StreamReader(path))
            {
                switch (extension)
                {
                    case FileHandler.FileExtension.txt:
                        list = ReadFromTxtToModel(stream,parser);
                        break;
                    case FileHandler.FileExtension.csv:
                        list = ReadFromCsvToModel(stream,parser);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        private List<InputModel> ReadFromTxtToModel(StreamReader stream, Parser parser)
        {
            List<InputModel> list = new();
            while (!stream.EndOfStream)
            {
                InputModel? model = parser.StringParser(stream.ReadLine(), _pattern);
                if (model != null)
                    list.Add(model);
            }
            return list;
        }

        private List<InputModel> ReadFromCsvToModel(StreamReader stream, Parser parser)
        {
            List<InputModel> list = new();
            while (!stream.EndOfStream)
            {
                InputModel? model = parser.StringParser(stream.ReadLine(), _pattern);
                if (model != null)
                    list.Add(model);
            }
            return list;
        }
 
        

    }
}
