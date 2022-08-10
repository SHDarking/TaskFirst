using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst
{
    public class MainProcessManager
    {
        private static Dictionary<Thread, MainProcess> threads = new Dictionary<Thread, MainProcess>();
        
        public MainProcessManager()
        {

        }

        public void StartProcesses()
        {
            //int countOfThreads = 0;
            while (AppManager.GetProgramStatus() == AppManager.ProgramStatus.inProgress)
            {
                if (FileHandler.AreHaveNewSourceFiles())
                {
                    MainProcess process = new MainProcess();
                    Thread thread = new Thread(process.Run);
                    thread.Name = $"Thread {threads.Count}";
                    threads.Add(thread, process);
                    
                    thread.Start();
                }
                List<Thread> notWorkingThreads = threads.Keys.ToList().Where(e => !e.IsAlive).ToList();
                notWorkingThreads.ForEach(e => threads.Remove(e));
                Thread.Sleep(1000);
            }
        }

        public void StopProcesses()
        {
            List<Thread> workingThreads = threads.Keys.ToList().Where(e => e.IsAlive).ToList();
            var handler = threads.Where(e => workingThreads.Any(x => e.Key == x)).ToList();
            handler.ForEach(e => e.Value.Stop());
            threads.Clear();
        }
    }
}
