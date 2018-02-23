using System;
using System.Threading.Tasks;

namespace Starship.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CoreClient.CreateUniverseFileAsync().Wait();
            Console.ReadLine();
        }
    }
}
