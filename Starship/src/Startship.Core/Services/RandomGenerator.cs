using System;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        public double GenerateDouble()
        {
            return new Random().NextDouble();
        }

        public bool GenerateBool(int probabilityTrue)
        {
            return new Random().Next(100) < probabilityTrue;
        }
    }
}
