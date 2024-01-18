namespace ClawEngineArchitect // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of forks");
            int forks = Convert.ToInt32(Console.ReadLine());
            if (forks < 2)
            {
                Console.WriteLine("Number of forks cannot be less than 2");
                return;
            }

            Console.WriteLine("Enter number of philosophers");
            int philosophers = Convert.ToInt32(Console.ReadLine());
            if (philosophers <= 0)
            {
                Console.WriteLine("Number of philosophers must be positive");
                return;
            }

            Console.WriteLine("Enter time for eating in ms");
            int delay = Convert.ToInt32(Console.ReadLine());
            if (delay <= 0)
            {
                Console.WriteLine("Time for eating must be positive");
                return;
            }

            Table table = new(forks, philosophers, delay);

            Console.WriteLine("Enter interval in ms to inspect state");
            int interval = Convert.ToInt32(Console.ReadLine());
            if (interval <= 0)
            {
                Console.WriteLine("Interval must be positive");
                return;
            }
            table.StartMeeting(interval);
        }
    }
}