namespace ClawEngineArchitect
{
    public class Fork: TableObject
    {
        /// <summary>
        /// Each new Fork will have a unique id
        /// </summary>
        private static int id = 0;

        /// <summary>
        /// State of Fork
        /// </summary>
        public ForkState State {get; set; }

        /// <summary>
        /// Unique id
        /// </summary>
        public int Id {get; private set;}

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public Fork() 
        {
            State = ForkState.Clear;
            Id = id++;
        }
    }

    public enum ForkState {
        Clear,
        Occuped
    }
}