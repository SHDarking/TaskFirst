using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst
{
    public static class AppManager
    {
        private static MainProcessManager _processesManager = new();
        private static ProgramStatus _status = ProgramStatus.Stopped;
        private static DateTime _startProgram;
        public static void StartProgram()
        {
            _startProgram = DateTime.Now.Date;
            ConsoleMenu(); 
        }

        private static void ConsoleMenu()
        {
            Console.WriteLine("*****    Program menu    *****");
            Console.WriteLine("Select an action");
            while (_status != ProgramStatus.Exit)
            {
                if (_status == ProgramStatus.Stopped)
                {
                    Console.WriteLine("1. Start program.\n2. Exit from program.");
                    string? inputedAnswer = Console.ReadLine();
                    if (!string.IsNullOrEmpty(inputedAnswer) && (inputedAnswer == "1" || inputedAnswer == "2"))
                    {
                        switch (int.Parse(inputedAnswer))
                        {
                            case 1:
                                RunProcesses();
                                Thread loggerUploadChecker = new Thread(UploadMetaLogByMidnight);
                                loggerUploadChecker.Start();
                                break;
                            case 2:
                                Exit();
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if(_status == ProgramStatus.inProgress)
                {
                    Console.WriteLine("1. Stop program?");
                    string? inputedAnswer = Console.ReadLine();
                    if (!string.IsNullOrEmpty(inputedAnswer) && inputedAnswer == "1")
                    {
                        switch (int.Parse(inputedAnswer))
                        {
                            case 1:
                                StopProcesses();
                                break;                            
                            default:
                                break;
                        }
                    }
                }
                
            }
        }

        private static void RunProcesses()
        {
            _status = ProgramStatus.inProgress;
            Thread thread = new Thread(_processesManager.StartProcesses);
            thread.Name = "MainProcessManager";
            thread.Start();
            Console.WriteLine("Program is running");
        }

        private static void StopProcesses()
        {
            _status = ProgramStatus.Stopped;
            _processesManager.StopProcesses();
            Console.WriteLine("Program was stopped");
        }
        private static void Exit()
        {
            Console.WriteLine("Exit from program");
            Console.WriteLine(UpdloadMetaLogAtExit());
            _status = ProgramStatus.Exit;
        }

        public static ProgramStatus GetProgramStatus() => _status;

        private static void UploadMetaLogByMidnight()
        {
            while (_status != ProgramStatus.Exit)
            {
                if (DateTime.Now.Date.Subtract(_startProgram).Days >= 1)
                {
                    Logger logger = new Logger();
                    FileHandler.WriteLog(logger);
                    _startProgram = DateTime.Now.Date;
                    logger.ResetLogger();
                }
                Thread.Sleep(60000);
            }
        }

        private static string UpdloadMetaLogAtExit()
        {
            Logger logger = new Logger();
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("parsed_files: " + logger.ParsedFiles.ToString());
            builder.AppendLine("parsed_lines: " + logger.ParsedLines.ToString());
            builder.AppendLine("found_errors: " + logger.FoundErrors.ToString());
            builder.Append("invalid_files: [");
            logger.InvalidFiles.ForEach(e => builder.Append(e + ','));
            builder.Append("]");
            return builder.ToString();
        }

        public enum ProgramStatus
        {
            inProgress,
            Stopped,
            Exit
        }
    }
}
