using TaskFirst.Models;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskFirst.FileHandlerHelpers
{
    internal class Parser
    {
        public InputModel? StringParser(string? line,string pattern)
        {
            InputModel? inputModel = null;
            
            if (!string.IsNullOrEmpty(line) && Regex.IsMatch(line, pattern))
            {
                inputModel = new();
                inputModel.FirstName = Regex.Match(line, @"^[a-zA-Z]+\s*[a-zA-Z]*\s*").Value.Trim(',');
                inputModel.LastName = Regex.Match(line, @",\s*[a-zA-Z]*\s*,").Value.Trim(new char[] { ',', ' ' });
                inputModel.Address = Regex.Match(line, @",\s*\W[a-zA-Z]+,\s*[a-zA-Z]+\s*[0-9]+,\s*[0-9]+\W").Value.Trim(new char[] { ',', ' ', '\"','"' });                
                inputModel.Payment = decimal.Parse(Regex.Match(line, @",\s*\d+.\d+,").Value.Trim(new char[] { ',', ' ' }).TrimStart(), CultureInfo.InvariantCulture);
                inputModel.Date = DateTime.ParseExact(Regex.Match(line, @",\s*\d{4}-\d{2}-\d{2},").Value.Trim(new char[] { ',', ' ' }), "yyyy-dd-MM", null);
                inputModel.AccountNumber = long.Parse(Regex.Match(line, @",\s*\W*\d+\W*,").NextMatch().Value.Trim(new char[] { ',', ' ', '\"', '"' }));
                inputModel.Service = Regex.Matches(line, @",\s*[a-zA-Z]+").Last().Value.Trim(new char[] { ',', ' ' });

            }
            else
            {
                Logger logger = new Logger();
                logger.IncreaseFoundErrors();
                
            }
            return inputModel;
        }

        public string ToJson(List<OutputModel> list) => JsonSerializer.Serialize(list,new JsonSerializerOptions() { WriteIndented = true });
        public string ParseLog(Logger logger) => JsonSerializer.Serialize(logger,new JsonSerializerOptions() { WriteIndented = true });
    }
}
