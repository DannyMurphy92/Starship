using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Models.Interfaces;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class PlanetFactory : IPlanetFactory
    {
        private readonly IPositionGenerator positionGenerator;

        public PlanetFactory(IPositionGenerator positionGenerator)
        {
            this.positionGenerator = positionGenerator;
        }
        public Planet Create()
        {
            return new Planet
            {
                Position = positionGenerator.Generate()
            };
        }
    }
}
