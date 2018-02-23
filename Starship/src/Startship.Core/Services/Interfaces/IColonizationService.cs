using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Services.Interfaces
{
    public interface IColonizationService
    {
        IEnumerable<Planet> ConquerTheUniverseSecs(Position startPosition, IEnumerable<Planet> planets, int timeInSecs);

        IEnumerable<Planet> ConquerTheUniverseMins(Position startPosition, IEnumerable<Planet> planets, int timeInMins);

        IEnumerable<Planet> ConquerTheUniverseHours(Position startPosition, IEnumerable<Planet> planets, int timeInHours);
    }
}
