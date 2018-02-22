using System;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        public double GenerateDouble()
        {
            var rdn = new Random();

            return rdn.NextDouble();
        }
    }
}
