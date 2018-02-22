using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starship.Core.Models.Interfaces
{
    public interface ISpaceObject
    {
        double XCoor { get; set; }

        double ZCoor { get; set; }

        double YCoor { get; set; }
    }
}
