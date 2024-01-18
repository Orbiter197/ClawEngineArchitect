using System;

namespace ClawEngineArchitect // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            int forks = Convert.ToInt32(Console.ReadLine());
            int philosophers = Convert.ToInt32(Console.ReadLine());
            Table table = new Table(forks, philosophers);
            table.StartMeeting();
        }
    }
}