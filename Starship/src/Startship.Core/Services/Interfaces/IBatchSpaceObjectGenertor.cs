using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models.Interfaces;

namespace Starship.Core.Services.Interfaces
{
    public interface IBatchSpaceObjectGenertor
    {
        IEnumerable<ISpaceObject> Generate(int amount);
    }
}
