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
        private readonly int probHabitable;
        private readonly double maxArea;
        private readonly double minArea;

        public PlanetFactory(
            IPositionFactory positionFactory, 
            IRandomGenerator randomGenerator, 
            int probHabitable,
            double minArea,
            double maxArea)
        {
            this.positionFactory = positionFactory;
            this.randomGenerator = randomGenerator;
            this.probHabitable = probHabitable;
            this.minArea = minArea;
            this.maxArea = maxArea;
        }

        public Planet Create()
        {
            var isHabitable = randomGenerator.GenerateBool(probHabitable);
            var area = randomGenerator.GenerateDouble(minArea, maxArea);
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
                bool isHabitable;
                double area;

                valid &= bool.TryParse(arguments[4], out isHabitable);
                valid &= double.TryParse(arguments[5], out area);
                if (valid)
                {
                    return new Planet(position, isHabitable, area);
                }
            }
            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
