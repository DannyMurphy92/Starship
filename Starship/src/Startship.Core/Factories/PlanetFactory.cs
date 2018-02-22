using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Models.Interfaces;
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
    }
}
