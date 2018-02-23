using System.Collections.Generic;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Factories.Interfaces
{
    public interface IBatchSpaceObjectFactory
    {
        IEnumerable<BaseSpaceObject> Generate(int amount);

        IEnumerable<BaseSpaceObject> GenerateFromStrings(IEnumerable<string> input);
    }
}
