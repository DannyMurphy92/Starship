using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class PlanetFactory : IPlanetFactory
    {
        private readonly IPositionFactory positionFactory;
        private readonly IRandomGenerator randomGenerator;

        public PlanetFactory(IPositionFactory positionFactory, IRandomGenerator randomGenerator)
        {
            this.positionFactory = positionFactory;
            this.randomGenerator = randomGenerator;
        }

        public Planet Create()
        {
            var isHabitable = randomGenerator.GenerateBool(40);
            var area = randomGenerator.GenerateDouble() * 100000000;
            return new Planet(positionFactory.Create(), isHabitable, area);
        }

        //Assumption made that string input will always be in same format, comma seperated string
        public Planet CreateFromString(string input)
        {
            var arguments = input.Split(',');

            if (arguments.Length == 6)
            {
                var valid = true;
                var position = positionFactory.CreateFromString(arguments[1], arguments[2], arguments[3]);
                valid &= bool.TryParse(arguments[4], out var isHabitable);
                valid &= double.TryParse(arguments[5], out var area);
                if (valid)
                {
                    return new Planet(position, isHabitable, area);
                }
            }
            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
