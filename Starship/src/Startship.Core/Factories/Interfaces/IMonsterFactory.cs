using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models;
using Starship.Core.Models.Interfaces;

namespace Starship.Core.Factories.Interfaces
{
    public interface IMonsterFactory
    {
        Monster Create();
    }
}
