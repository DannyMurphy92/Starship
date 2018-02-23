using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class CoordinateFactory : ICoordinateFactory
    {
        private readonly IRandomGenerator randomGenerator;

        public CoordinateFactory(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public Coordinate Create()
        {
            return new Coordinate(
                randomGenerator.GenerateInt(999), 
                randomGenerator.GenerateInt(999),
                randomGenerator.GenerateInt(99),
                randomGenerator.GenerateInt(9));
        }

        public Coordinate CreateFromString(string input)
        {
            var arguments = input.Split('.');
            if (arguments.Length == 4)
            {
                var valid = true;
                valid &= int.TryParse(arguments[0], out var a1);
                valid &= int.TryParse(arguments[1], out var a2);
                valid &= int.TryParse(arguments[2], out var a3);
                valid &= int.TryParse(arguments[3], out var a4);

                if (valid)
                {
                    return new Coordinate(a1, a2, a3, a4);
                }
            }

            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
