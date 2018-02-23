using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class CoordinateFactory : ICoordinateFactory
    {
        private readonly IRandomGenerator randomGenerator;

        public CoordinateFactory(IRandomGenerator randomGenerator)
        {
            this.randomGenerator = randomGenerator;
        }

        public Coordnate Create()
        {
            return new Coordnate(
                randomGenerator.GenerateInt(999), 
                randomGenerator.GenerateInt(999),
                randomGenerator.GenerateInt(99),
                randomGenerator.GenerateInt(9));
        }
    }
}
