using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models.Interfaces;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class BatchSpaceObjectGenertor : IBatchSpaceObjectGenertor
    {
        private readonly IRandomGenerator randomGenerator;
        private readonly IMonsterFactory monsterFactory;
        private readonly IPlanetFactory planetFactory;

        public BatchSpaceObjectGenertor(
            IRandomGenerator randomGenerator, 
            IMonsterFactory monsterFactory, 
            IPlanetFactory planetFactory)
        {
            this.randomGenerator = randomGenerator;
            this.monsterFactory = monsterFactory;
            this.planetFactory = planetFactory;
        }

        public IEnumerable<ISpaceObject> Generate(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var genPlanet = randomGenerator.GenerateBool();

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
    }
}
