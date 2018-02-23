using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Models
{
    public class Monster : BaseSpaceObject
    {
        public Monster(Position position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return $"{ObjectsEnum.Monster}, {Position.X}, {Position.Y}, {Position.Z}";
        }
    }
}
