using System;

namespace ClawEngineArchitect
{
    public class Philosopher
    {
        public float Coordinate {get; private set;}
        public PhilosopherState State {get; private set;}

        Philosopher(float coordinate) {
            Coordinate = coordinate;
            State = PhilosopherState.Thinking;
        }

        public enum PhilosopherState {
            Thinking,
            Eating
        }
    }
}