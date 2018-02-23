using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class PositionFactory : IPositionFactory
    {
        private readonly IRandomGenerator randomGenerator;
        
        public PositionFactory(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public Position Create()
        {
            return new Position(
                GenerateCoordinate(0, 999),
                GenerateCoordinate(0, 999),
                GenerateCoordinate(0, 999));
        }

        private double GenerateCoordinate(double min, double max)
        {
            return randomGenerator.GenerateDouble() * (max - min) + min;
        }
    }
}
