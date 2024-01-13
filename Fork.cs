using System;

namespace ClawEngineArchitect
{
    public class Fork
    {
        public float Coordinate {get; private set;}
        public ForkState State {get; set; }

        Fork(float coordinate) {
            Coordinate = coordinate;
            State = ForkState.Clear;
        }
    }

    public enum ForkState {
        Clear,
        Occuped
    }
}