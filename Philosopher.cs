using System;

namespace ClawEngineArchitect
{
    
    public class Philosopher: TableObject
    {
        private static int id = 0;
        
        public PhilosopherState State {get; set;}

        public int Id {get; private set;}

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