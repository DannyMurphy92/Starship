using System;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class PositionGenerator : IPositionGenerator
    {
        private IRandomGenerator randomGenerator;
        
        public PositionGenerator(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public Position Generate()
        {
            return new Position()
            {
                XCoor = GenerateCoordinate(0, 999),
                YCoor = GenerateCoordinate(0, 999),
                ZCoor = GenerateCoordinate(0, 999),
            };
        }

        private double GenerateCoordinate(double min, double max)
        {
            return randomGenerator.GenerateDouble() * (max - min) + min;
        }
    }
}
