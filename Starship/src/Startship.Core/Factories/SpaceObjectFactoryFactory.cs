using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models.Interfaces;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class SpaceObjectFactoryFactory : ISpaceObjectFactoryFactory
    {
        private readonly IPositionGenerator positionGenerator;

        public SpaceObjectFactoryFactory(IPositionGenerator positionGenerator)
        {
            this.positionGenerator = positionGenerator;
        }

        public ISpaceObjectFactory CreateFactory(ObjectsEnum type)
        {
            switch (type)
            {
                case ObjectsEnum.Monster:
                {
                    return new MonsterFactory(positionGenerator);
                }
                case ObjectsEnum.Planet:
                {
                    return new PlanetFactory(positionGenerator);
                }
                default:
                {
                    throw new NotImplementedException($"Factory for type {type} not implmented");
                }
            }
        }
    }
}
