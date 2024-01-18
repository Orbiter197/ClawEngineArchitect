using System;

namespace ClawEngineArchitect
{
    internal class Table
    {
        /// <summary>
        /// Collection of Philosophers and Forks
        /// </summary>
        private TableObject[] tableObjects; // order matters

        /// <summary>
        /// Creates a Table.
        /// Forks and Philosophers are osciliting in collection.
        /// </summary>
        /// <param name="forksCount">Number of forks.</param>
        /// <exception cref="ArgumentException">If number of forks is less than 2,
        /// throws an exception</exception>
        public Table(int forksCount)
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
        }

        /// <summary>
        /// Creates a Table.
        /// Forks and Philosophers has random order in collection.
        /// </summary>
        /// <param name="forksCount">Number of forks.</param>
        /// <param name="philosophersCount">Number of philosophers.</param>
        /// <exception cref="ArgumentException">If number of forks is less than 2,
        /// throws an exception</exception>
        public Table(int forksCount, int philosophersCount)
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
        }


        /// <summary>
        /// Starts the meeting.
        /// </summary>
        public void StartMeeting()
        {
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
            Console.WriteLine("Philosopher {0} is thinking", philosopher.Id);
            philosopher.State = PhilosopherState.Thinking;

            lock (leftFork) lock (rightFork)
                {
                    Console.WriteLine("Philosopher {0} start eating, occuping Fork {1} and {2}",
                    philosopher.Id, leftFork.Id, rightFork.Id);

                    philosopher.State = PhilosopherState.Eating;
                    leftFork.State = ForkState.Occuped;
                    rightFork.State = ForkState.Occuped;

                    Thread.Sleep(10);

                    philosopher.State = PhilosopherState.Thinking;
                    leftFork.State = ForkState.Clear;
                    rightFork.State = ForkState.Clear;

                    Console.WriteLine("Philosopher {0} stops eating, clearing Fork {1} and {2}",
                    philosopher.Id, leftFork.Id, rightFork.Id);

                }
        }

    }
}