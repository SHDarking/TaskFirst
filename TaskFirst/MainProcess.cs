using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFirst.Models;

namespace TaskFirst
{
    internal class MainProcess
    {
        private readonly FileHandler _fileHandler;
        private readonly Convertor _convertor;
        private ProcessStatus _processStatus;
        
        public MainProcess()
        {
            _fileHandler = new FileHandler();
            _convertor = new Convertor();
            _processStatus = ProcessStatus.IsStopped;
        }

        public void Run()
        {
            _processStatus = ProcessStatus.InProgress;
            List<InputModel>? inputList = null;
            List<OutputModel>? outputList = null;
            string? pathToInputFile = null;
            if (_processStatus == ProcessStatus.InProgress)
            {
                Console.WriteLine($"Process is running, Thread is {Thread.CurrentThread.Name}");
                pathToInputFile = FileHandler.GetFileFromIndex();
                if (pathToInputFile == null)
                {
                    _processStatus = ProcessStatus.IsStopped;
                }
                else
                {
                    inputList = _fileHandler.ReadFileToModel(pathToInputFile);
                }
                             
            }
            if (_processStatus == ProcessStatus.InProgress && inputList != null)
            {
                Console.WriteLine($"Converting models, {Thread.CurrentThread.Name}");
                outputList = _convertor.ConvertToOutputModel(inputList);
            }
            if (_processStatus == ProcessStatus.InProgress)
            {
                Console.WriteLine($"Write converted data, {Thread.CurrentThread.Name}");
                _fileHandler.WriteToJsonFile(outputList);
                Indexer indexer = Indexer.GetInstanceIndexer();
                indexer.FilesInIndex.Remove(pathToInputFile);
                _fileHandler.DeleteInputFile(pathToInputFile);
            }
        }

        public void Stop()
        {
            _processStatus = ProcessStatus.IsStopped;
            Console.WriteLine("Process stopping");
        }

        enum ProcessStatus
        {
            InProgress,
            IsStopped
        }
    }
}
