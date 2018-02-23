using Starship.Core.Models;

namespace Starship.Core.Factories.Interfaces
{
    public interface IPlanetFactory
    {
        Planet Create();

        Planet CreateFromString(string input);
    }
}
