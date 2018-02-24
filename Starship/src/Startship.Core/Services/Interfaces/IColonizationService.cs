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
        IEnumerable<Planet> ConquerTheUniverseSecs(Position startPosition, IList<Planet> planets, double timeInSecs);

        IEnumerable<Planet> ConquerTheUniverseMins(Position startPosition, IList<Planet> planets, double timeInMins);

        IEnumerable<Planet> ConquerTheUniverseHours(Position startPosition, IList<Planet> planets, double timeInHours);
    }
}
