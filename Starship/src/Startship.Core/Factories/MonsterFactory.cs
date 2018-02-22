using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IPositionGenerator positionGenerator;

        public MonsterFactory(IPositionGenerator positionGenerator)
        {
            this.positionGenerator = positionGenerator;
        }

        public Monster Create()
        {
            return new Monster(positionGenerator.Generate());
        }
    }
}
