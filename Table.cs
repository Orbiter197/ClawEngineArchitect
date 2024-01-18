using System.Data;

namespace ClawEngineArchitect
{
    internal class Table
    {
        /// <summary>
        /// Collection of Philosophers and Forks
        /// </summary>
        private TableObject[] tableObjects; // order matters

        private int Delay { get; set; }

        /// <summary>
        /// Creates a Table.
        /// Forks and Philosophers are osciliting in collection.
        /// </summary>
        /// <param name="forksCount">Number of forks.</param>
        /// <param name="delay">Time for eating.</param>
        /// <exception cref="ArgumentException">If number of forks is less than 2,
        /// throws an exception</exception>
        public Table(int forksCount, int delay)
        {
            if (forksCount < 2)
            {
                throw new ArgumentException("Number of forks cannot be less than 2");
            }

            tableObjects = new TableObject[2 * forksCount];
            for (int i = 0; i < forksCount; i++)
            {
                tableObjects[2 * i] = new Fork();
                tableObjects[2 * i + 1] = new Philosopher();
            }

            Delay = delay;
        }

        /// <summary>
        /// Creates a Table.
        /// Forks and Philosophers has random order in collection.
        /// </summary>
        /// <param name="forksCount">Number of forks.</param>
        /// <param name="philosophersCount">Number of philosophers.</param>
        /// <param name="delay">Time for eating.</param>
        /// <exception cref="ArgumentException">If number of forks is less than 2,
        /// throws an exception</exception>
        public Table(int forksCount, int philosophersCount, int delay)
        {
            if (forksCount < 2)
            {
                throw new ArgumentException("Number of forks cannot be less than 2");
            }

            if (philosophersCount <= 0)
            {
                throw new ArgumentException("Number of philosophers must be positive");
            }

            tableObjects = new TableObject[forksCount + philosophersCount];
            Random random = new();
            for (int i = 0; i < tableObjects.Length; i++)
            {
                if (random.NextSingle() >= ((float)forksCount) / (forksCount + philosophersCount))
                {
                    tableObjects[i] = new Philosopher();
                    philosophersCount--;
                }
                else
                {
                    tableObjects[i] = new Fork();
                    forksCount--;
                }
            }

            Delay = delay;
        }


        /// <summary>
        /// Starts the meeting. Displays the result in console
        /// </summary>
        /// <param name="interval">Interval in ms.</param>
        /// <exception cref="ArgumentException"></exception>
        public void StartMeeting(int interval)
        {
            if (interval <= 0)
            {
                throw new ArgumentException("Interval must be positive");
            }
            Console.WriteLine("Meeting started");
            for (int i = 0; i < tableObjects.Length; i++)
            {
                if (tableObjects[i] is Philosopher philosopher)
                {
                    Fork leftFork = FindForkLeft(i);
                    Fork rightFork = FindForkRight(i);
                    Thread thread = new(() => GrabForksOrThink(philosopher, leftFork, rightFork));
                    thread.Start();
                }
            }
            

            bool haveAllEaten;
            do
            {
                Console.WriteLine(DateTime.Now);
                haveAllEaten = true;
                for (int i = 0; i < tableObjects.Length; i++)
                {
                    if (tableObjects[i] is Philosopher philosopher)
                    {
                        Console.WriteLine("Philosopher {0} is {1}. He has {2}eaten", philosopher.Id,
                        philosopher.State == PhilosopherState.Eating ? "Eating" : "thinking",
                        philosopher.HasEaten ? "" : "not ");

                        haveAllEaten &= philosopher.HasEaten;
                    }

                }
                Thread.Sleep(interval);
            }
            while (!haveAllEaten);

            Console.WriteLine("Meeting ended");

        }

        /// <summary>
        /// Finds a fork left from current position.
        /// If start of the array is reached, continues search from the end
        /// </summary>
        /// <param name="i">Index in an array from which is seached</param>
        /// <returns>A Fork that is found.</returns>
        public Fork FindForkLeft(int i)
        {
            // If list contains no forks, program will block
            while (true)
            {
                if (tableObjects[i] is Fork fork)
                    return fork;
                else if (i == 0)
                    i = tableObjects.Length - 1;
                else
                    i--;
            }
        }

        /// <summary>
        /// Finds a fork right from current position.
        /// If end of the array is reached, continues search from the start
        /// </summary>
        /// <param name="i">Index in an array from which is seached</param>
        /// <returns>A Fork that is found.</returns
        public Fork FindForkRight(int i)
        {
            // If list contains no forks, program will block
            while (true)
            {
                if (tableObjects[i] is Fork fork)
                    return fork;
                else if (i == tableObjects.Length - 1)
                    i = 0;
                else
                    i++;
            }
        }

        /// <summary>
        /// A method which is executed in a thread
        /// </summary>
        /// <param name="philosopher">A philosopher which will occupy forks.</param>
        /// <param name="leftFork">Left fork.</param>
        /// <param name="rightFork">Right fork.</param>
        public void GrabForksOrThink(Philosopher philosopher, Fork leftFork, Fork rightFork)
        {
            lock (leftFork) lock (rightFork)
                {

                    philosopher.State = PhilosopherState.Eating;
                    leftFork.State = ForkState.Occuped;
                    rightFork.State = ForkState.Occuped;

                    Thread.Sleep(Delay);

                    philosopher.State = PhilosopherState.Thinking;
                    leftFork.State = ForkState.Clear;
                    rightFork.State = ForkState.Clear;

                }
        }

    }
}