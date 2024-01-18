using System;

namespace ClawEngineArchitect
{
    internal class Table
    {
        Object _lock = new();
        List<TableObject> tableObjects; // order matters

        public Table(int n) 
        {
            tableObjects = new List<TableObject>(2*n);
            for (int i = 0; i < n; i++) 
            {
                tableObjects.Add(new Fork());
                tableObjects.Add(new Philosopher());
            }
        }

        public void StartMeeting() {
            Console.WriteLine("smt");
            for (int i = 0; i < tableObjects.Count; i++) 
            {
                if (tableObjects[i] is Philosopher philosopher)
                {
                    Fork leftFork = FindForkLeft(i);
                    Fork rightFork = FindForkRight(i);
                    Thread thread = new(() => GrabForksOrThink(philosopher, leftFork, rightFork));
                    thread.Start();
                }
            }
            Console.WriteLine("smt end");
        }

        public Fork FindForkLeft(int i) 
        {
            // If list contains no forks, program will block
            while (true) 
            {
                if (tableObjects[i] is Fork fork) 
                    return fork;
                else if (i == 0)
                    i = tableObjects.Count - 1;
                else
                    i--;
            }
        }
        public Fork FindForkRight(int i) 
        {
            // If list contains no forks, program will block
            while (true) 
            {
                if (tableObjects[i] is Fork fork) 
                    return fork;
                else if (i == tableObjects.Count -1)
                    i = 0;
                else
                    i++;
            }
        }


        public void GrabForksOrThink(Philosopher philosopher, Fork leftFork, Fork rightFork) {
            Console.WriteLine("Philosopher {0} is thinking", philosopher.Id);
            philosopher.State = PhilosopherState.Thinking;

            lock (_lock) {
                Console.WriteLine("Philosopher {0} start eating, occuping Fork {1} and {2}", 
                philosopher.Id, leftFork.Id, rightFork.Id);

                philosopher.State = PhilosopherState.Eating;
                leftFork.State = ForkState.Occuped;
                rightFork.State = ForkState.Occuped;
                
                Thread.Sleep(1000);

                philosopher.State = PhilosopherState.Thinking;
                leftFork.State = ForkState.Clear;
                rightFork.State = ForkState.Clear;

                Console.WriteLine("Philosopher {0} stops eating, clearing Fork {1} and {2}", 
                philosopher.Id, leftFork.Id, rightFork.Id);
            }
        }





        

    
    }
}