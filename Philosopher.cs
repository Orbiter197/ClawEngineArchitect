using System;

namespace ClawEngineArchitect
{
    public class Philosopher: TableObject
    {
        public PhilosopherState State {get; private set;}

        public Philosopher(float coordinate): base(coordinate) {
            State = PhilosopherState.Thinking;
        }

        public enum PhilosopherState {
            Thinking,
            Eating
        }
    }
}