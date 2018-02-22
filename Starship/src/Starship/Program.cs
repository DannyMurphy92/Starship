using System;

namespace Starship.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CoreClient.CreateUniverseFile();
            Console.ReadLine();
        }
    }
}
