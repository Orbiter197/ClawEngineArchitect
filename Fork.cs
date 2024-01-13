using System;

namespace ClawEngineArchitect
{
    public class Fork: TableObject
    {
        public ForkState State {get; set; }

        public Fork(float coordinate): base(coordinate) {
            State = ForkState.Clear;
        }
    }

    public enum ForkState {
        Clear,
        Occuped
    }
}