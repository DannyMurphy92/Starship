using System;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class PositionGenerator : IPositionGenerator
    {
        private readonly IRandomGenerator randomGenerator;
        
        public PositionGenerator(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public Position Generate()
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
