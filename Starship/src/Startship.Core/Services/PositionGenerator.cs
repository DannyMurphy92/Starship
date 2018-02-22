using System;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class PositionGenerator : IPositionGenerator
    {
        public Position Generate()
        {
            var rdn = new Random();

            return new Position()
            {
                XCoor = GenerateCoordinate(rdn, 0, 999),
                YCoor = GenerateCoordinate(rdn, 0, 999),
                ZCoor = GenerateCoordinate(rdn, 0, 999),
            };
        }

        private double GenerateCoordinate(Random rdn, double min, double max)
        {
            return rdn.NextDouble() * (max - min) + min;
        }
    }
}
