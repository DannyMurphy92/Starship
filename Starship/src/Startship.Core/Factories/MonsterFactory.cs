using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Models.Interfaces;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class MonsterFactory : ISpaceObjectFactory
    {
        private readonly IPositionGenerator positionGenerator;

        public MonsterFactory(IPositionGenerator positionGenerator)
        {
            this.positionGenerator = positionGenerator;
        }

        public ISpaceObject Create()
        {
            return new Monster
            {
                Position = positionGenerator.Generate()
            };
        }
    }
}
