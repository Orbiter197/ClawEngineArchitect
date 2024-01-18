namespace ClawEngineArchitect
{
    /// <summary>
    /// Philosopher class
    /// </summary>
    public class Philosopher : TableObject
    {
        /// <summary>
        /// Each new Philosopher will have a unique id
        /// </summary>
        private static int id = 0;

        public bool HasEaten { get; private set; }
        private PhilosopherState state;

        /// <summary>
        /// State of Philosopher
        /// </summary>
        public PhilosopherState State
        {
            get => state;
            set
            {
                if (value == PhilosopherState.Thinking && state == PhilosopherState.Eating)
                {
                    HasEaten = true;
                }
                state = value;
            }
        }

        /// <summary>
        /// Unique id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public Philosopher()
        {
            HasEaten = false;
            State = PhilosopherState.Thinking;
            Id = id++;
        }

    }

    public enum PhilosopherState
    {
        Thinking,
        Eating
    }
}