using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class CoordinateFactory : ICoordinateFactory
    {
        private readonly IRandomGenerator randomGenerator;
        private readonly int area1Limit;
        private readonly int area2Limit;
        private readonly int area3Limit;
        private readonly int area4Limit;

        public CoordinateFactory(
            IRandomGenerator randomGenerator, 
            int area1Limit, 
            int area2Limit, 
            int area3Limit, 
            int area4Limit)
        {
            this.randomGenerator = randomGenerator;
            this.area1Limit = area1Limit;
            this.area2Limit = area2Limit;
            this.area3Limit = area3Limit;
            this.area4Limit = area4Limit;
        }

        public Coordinate Create()
        {
            return new Coordinate(
                randomGenerator.GenerateInt(area1Limit), 
                randomGenerator.GenerateInt(area2Limit),
                randomGenerator.GenerateInt(area3Limit),
                randomGenerator.GenerateInt(area4Limit));
        }

        public Coordinate CreateFromString(string input)
        {
            var arguments = input.Split('.');
            if (arguments.Length == 4)
            {
                var valid = true;
                int a1;
                int a2;
                int a3;
                int a4;

                valid &= int.TryParse(arguments[0], out a1);
                valid &= int.TryParse(arguments[1], out a2);
                valid &= int.TryParse(arguments[2], out a3);
                valid &= int.TryParse(arguments[3], out a4);

                if (valid)
                {
                    return new Coordinate(a1, a2, a3, a4);
                }
            }

            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
