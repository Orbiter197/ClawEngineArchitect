using System;

namespace ClawEngineArchitect // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(5);
            table.StartMeeting();
        }
    }
}