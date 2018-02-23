using Starship.Core.Models;

namespace Starship.Core.Factories.Interfaces
{
    public interface ICoordinateFactory
    {
        Coordinate Create();

        Coordinate CreateFromString(string input);
    }
}
