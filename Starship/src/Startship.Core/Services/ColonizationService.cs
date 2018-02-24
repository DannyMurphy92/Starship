﻿using System.Collections.Generic;
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

            var destination = FindAndColonizeNextDestination(startPosition, planets, ref timeLimitInSecs);
            startPosition = destination.Position;

            while (timeLimitInSecs >= 0)
            {
                conqueredPlanets.Add(destination);

                destination = FindAndColonizeNextDestination(startPosition, planets, ref timeLimitInSecs);
                startPosition = destination.Position;
            }

            return conqueredPlanets;
        }

        public IEnumerable<Planet> ConquerTheUniverseMins(Position startPosition, IList<Planet> planets, double timeLimitInMins)
        {
            return ConquerTheUniverseSecs(startPosition, planets, timeLimitInMins * 60);
        }

        public IEnumerable<Planet> ConquerTheUniverseHours(Position startPosition, IList<Planet> planets, double timeLimitInHours)
        {
            return ConquerTheUniverseMins(startPosition, planets, timeLimitInHours * 60);
        }
        
        private Planet FindAndColonizeNextDestination(Position startPos, IList<Planet> planets, ref double remainingTime)
        {
            var travelTime = 10 * 60;

            var destination = travelService.FindNearestObject(startPos, planets);
            planets.Remove(destination);
            remainingTime -= travelTime + destination.Area * .5 * .43;

            return destination;
        }
    }
}
