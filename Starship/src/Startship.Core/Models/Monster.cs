﻿using Starship.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starship.Core.Models
{
    public class Monster : ISpaceObject
    {
        public Monster(Position position)
        {
            Position = position;
        }
        public Position Position { get; }
    }
}
