using System.Collections.Generic;
using System.Linq;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class ColonizationService : IColonizationService
    {
        private readonly ITravelService travelService;

        public ColonizationService(ITravelService travelService)
        {
            this.travelService = travelService;
        }

        public IEnumerable<Planet> ConquerTheUniverseSecs(Position startPosition, IList<Planet> planets, double timeLimitInSecs)
        {
            var conqueredPlanets = new List<Planet>();

            var travelTime = 10 * 60;


            var x = travelService.FindNearestObject(startPosition, planets);
            var destination = x;
            var travelAndColTime = travelTime + destination.Area * .5 * .43;

            timeLimitInSecs -= travelAndColTime;

            while (timeLimitInSecs >= 0)
            {
                conqueredPlanets.Add(destination);

                destination = travelService.FindNearestObject(startPosition, planets) as Planet;
                travelAndColTime = travelTime + destination.Area * .5 * .43;
                timeLimitInSecs -= travelAndColTime;
            }

            return conqueredPlanets;
        }

        public IEnumerable<Planet> ConquerTheUniverseMins(Position startPosition, IList<Planet> planets, double timeLimitInMins)
        {
            return ConquerTheUniverseMins(startPosition, planets, timeLimitInMins * 60);
        }

        public IEnumerable<Planet> ConquerTheUniverseHours(Position startPosition, IList<Planet> planets, double timeLimitInHours)
        {
            return ConquerTheUniverseMins(startPosition, planets, timeLimitInHours * 60);
        }
    }
}
