using System.Collections.Generic;
using Starship.Core.Models.Interfaces;

namespace Starship.Core.Factories.Interfaces
{
    public interface IBatchSpaceObjectFactory
    {
        IEnumerable<ISpaceObject> Generate(int amount);
    }
}
