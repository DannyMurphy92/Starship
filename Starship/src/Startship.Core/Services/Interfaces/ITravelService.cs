using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Services.Interfaces
{
    public interface ITravelService
    {
        BaseSpaceObject FindNearestObject(Position currentPos, IList<BaseSpaceObject> spaceObjects);
    }
}
