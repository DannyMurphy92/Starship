using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models;

namespace Starship.Core.Factories.Interfaces
{
    public interface IMonsterFactory
    {
        Monster Create();
    }
}
