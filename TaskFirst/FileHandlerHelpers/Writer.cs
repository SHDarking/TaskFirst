using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFirst.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskFirst.FileHandlerHelpers
{
    internal class Writer
    {
        public async void WriteModelToJsonFile(string path, List<OutputModel> list)
        {
            Parser parser = new Parser();
            var json = parser.ToJson(list);
            using (StreamWriter stream = new StreamWriter(path))
            {
                await stream.WriteAsync(json);
            }
        }
        public void WriteLogToFile(string path, Logger logger)
        {
            string log = new Parser().ParseLog(logger);
            File.WriteAllText(path + "\\meta.log", log);
        }
    }
}
