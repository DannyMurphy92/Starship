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
                try
                {
                    if (inp.StartsWith(ObjectsEnum.Planet.ToString()))
                    {
                        result.Add(planetFactory.CreateFromString(inp));
                    }
                    else if(inp.StartsWith(ObjectsEnum.Monster.ToString()))
                    {
                        result.Add(monsterFactory.CreateFromString(inp));
                    }
                }
                catch
                {
                    Console.WriteLine($"Error parsing space object {inp}");
                }
            }

            return result;
        }
    }
}
