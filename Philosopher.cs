using System;

namespace ClawEngineArchitect
{
    /// <summary>
    /// Philosopher class
    /// </summary>
    public class Philosopher: TableObject
    {
        /// <summary>
        /// Each new Philosopher will have a unique id
        /// </summary>
        private static int id = 0;
        
        /// <summary>
        /// State of Philosopher
        /// </summary>
        public PhilosopherState State {get; set;}

        /// <summary>
        /// Unique id
        /// </summary>
        public int Id {get; private set;}

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public Philosopher() 
        {
            State = PhilosopherState.Thinking;
            Id = id++;
        }

    }

     public enum PhilosopherState {
        Thinking,
        Eating
    }
}