using System;

namespace ClawEngineArchitect
{
    public class Fork: TableObject
    {
        private static int id = 0;

        public ForkState State {get; set; }

        public int Id {get; private set;}

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