using System.Collections.Generic;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Factories.Interfaces
{
    public interface IBatchSpaceObjectFactory
    {
        IEnumerable<BaseSpaceObject> Create(int amount);
        
        BaseSpaceObject CreateFromString(string input);
    }
}
