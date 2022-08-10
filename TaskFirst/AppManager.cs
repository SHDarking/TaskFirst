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
        public static void StartProgram()
        {
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
                    Console.WriteLine("1. Start program.\n2. Exit from program.\n");
                    string? inputedAnswer = Console.ReadLine();
                    if (!string.IsNullOrEmpty(inputedAnswer) && (inputedAnswer == "1" || inputedAnswer == "2"))
                    {
                        switch (int.Parse(inputedAnswer))
                        {
                            case 1:
                                RunProcesses();
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
                    Console.WriteLine("1. Stop program?\n");
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
            _status = ProgramStatus.Exit;
        }

        public static ProgramStatus GetProgramStatus() => _status;

        public enum ProgramStatus
        {
            inProgress,
            Stopped,
            Exit
        }
    }
}
