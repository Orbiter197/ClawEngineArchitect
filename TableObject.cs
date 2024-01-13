using System;

namespace ClawEngineArchitect
{
    public abstract class TableObject
    {
        private float coordinate;

        protected float Coordinate { 
            get => coordinate; 
            private set 
            {
                if (value < 0 || value >= 1)
                    throw new InvalidDataException(String.Format("Invalid coordinate recieved. Coordinate must be in [0, 1). Recieved {0}", value));
                coordinate = value;
            }
        }

        public TableObject(float coordinate) {
            Coordinate = coordinate;
        }

    }
}