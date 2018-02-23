using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class PositionFactory : IPositionFactory
    {
        private readonly ICoordinateFactory coordinateFactory;
        
        public PositionFactory(ICoordinateFactory coordinateFactory)
        {
            this.coordinateFactory = coordinateFactory;
        }

        public Position Create()
        {
            return new Position(
                coordinateFactory.Create(),
                coordinateFactory.Create(),
                coordinateFactory.Create());
        }

        public Position CreateFromString(string x, string y, string z)
        {
            return new Position(
                coordinateFactory.CreateFromString(x),
                coordinateFactory.CreateFromString(y),
                coordinateFactory.CreateFromString(z));
        }
    }
}
