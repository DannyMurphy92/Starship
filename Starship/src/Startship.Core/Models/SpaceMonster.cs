using Starship.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starship.Core.Models
{
    public class SpaceMonster : ISpaceObject
    {
        public double XCoor { get; set; }

        public double ZCoor { get; set; }

        public double YCoor { get; set; }
    }
}
