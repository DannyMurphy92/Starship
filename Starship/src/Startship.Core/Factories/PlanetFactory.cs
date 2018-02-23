using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class PlanetFactory : IPlanetFactory
    {
        private readonly IPositionGenerator positionGenerator;
        private readonly IRandomGenerator randomGenerator;

        public PlanetFactory(IPositionGenerator positionGenerator, IRandomGenerator randomGenerator)
        {
            this.positionGenerator = positionGenerator;
            this.randomGenerator = randomGenerator;
        }

        public Planet Create()
        {
            var isHabitable = randomGenerator.GenerateBool(40);
            var area = randomGenerator.GenerateDouble() * 100000000;
            return new Planet(positionGenerator.Generate(), isHabitable, area);
        }

        //Assumption made that string input will always be in same format, comma seperated string
        public Planet CreateFromString(string input)
        {
            var valid = true;
            var arguments = input.Split(',');

            if (arguments.Length == 6)
            {
                valid &= double.TryParse(arguments[1], out var x);
                valid &= double.TryParse(arguments[2], out var y);
                valid &= double.TryParse(arguments[3], out var z);
                valid &= bool.TryParse(arguments[4], out var isHabitable);
                valid &= double.TryParse(arguments[5], out var area);
                if (valid)
                {
                    var position = new Position(x, y, z);
                    return new Planet(position, isHabitable, area);
                }
            }
            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
