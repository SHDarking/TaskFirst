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
    }
}
