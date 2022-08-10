using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst
{
    public class Logger
    {
        private static int _parsedFiles = 0;
        private static int _parsedLines = 0;
        private static int _foundErrors = 0;
        private static List<string> _invalidFiles = new();

        public int ParsedFiles { get { return _parsedFiles; } }
        public int ParsedLines { get { return _parsedLines; } }
        public int FoundErrors { get { return _foundErrors; } }
        public List<string> InvalidFiles { get { return _invalidFiles; } }

        public void IncreaseParsedFiles() => _parsedFiles++;
        public void IncreaseParsedLines() => _parsedLines++;
        public void IncreaseFoundErrors() => _foundErrors++;
        public void AddInvalidFile(string fileName) => _invalidFiles.Add(fileName);
        
        public void ResetLogger()
        {
            _parsedFiles = 0;
            _parsedLines = 0;
            _foundErrors = 0;
            _invalidFiles = new();
    }

    }
}
