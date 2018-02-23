using System;
using System.Collections.Generic;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class BatchSpaceObjectFactory : IBatchSpaceObjectFactory
    {
        private readonly IRandomGenerator randomGenerator;
        private readonly IMonsterFactory monsterFactory;
        private readonly IPlanetFactory planetFactory;

        public BatchSpaceObjectFactory(
            IRandomGenerator randomGenerator, 
            IMonsterFactory monsterFactory, 
            IPlanetFactory planetFactory)
        {
            this.randomGenerator = randomGenerator;
            this.monsterFactory = monsterFactory;
            this.planetFactory = planetFactory;
        }

        public IEnumerable<BaseSpaceObject> Generate(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var genPlanet = randomGenerator.GenerateBool(50);

                if (genPlanet)
                {
                    yield return planetFactory.Create();
                }
                else
                {
                    yield return monsterFactory.Create();
                }
            }
        }

        public IEnumerable<BaseSpaceObject> GenerateFromStrings(IEnumerable<string> input)
        {
            var result = new List<BaseSpaceObject>();
            foreach (var inp in input)
            {
                var res = GenerateFromString(inp);
                if (res != null)
                {
                    result.Add(res);
                }
            }

            return result;
        }

        public BaseSpaceObject GenerateFromString(string input)
        {
            try
            {
                if (input.StartsWith(ObjectsEnum.Planet.ToString()))
                {
                    return planetFactory.CreateFromString(input);
                }
                if (input.StartsWith(ObjectsEnum.Monster.ToString()))
                {
                    return monsterFactory.CreateFromString(input);
                }
            }
            catch
            {
                Console.WriteLine($"Error parsing space object {input}");
            }

            return null;
        }
    }
}
