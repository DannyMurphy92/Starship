using System;
using System.Collections.Generic;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class ColonizationService : IColonizationService
    {
        public IEnumerable<Planet> ConquerTheUniverseSecs(Position startPosition, IEnumerable<Planet> planets, int timeInSecs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planet> ConquerTheUniverseMins(Position startPosition, IEnumerable<Planet> planets, int timeInMins)
        {
            return ConquerTheUniverseMins(startPosition, planets, timeInMins * 60);
        }

        public IEnumerable<Planet> ConquerTheUniverseHours(Position startPosition, IEnumerable<Planet> planets, int timeInHours)
        {
            return ConquerTheUniverseMins(startPosition, planets, timeInHours * 60);
        }
    }
}
