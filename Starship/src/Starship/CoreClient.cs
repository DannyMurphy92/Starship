using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Castle.Windsor;
using Starship.Core;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Installer;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Cli
{
    public class CoreClient
    {
        public static async Task CreateUniverseFileAsync()
        {
            var startPos = new Position(
                new Coordinate(123, 123, 99, 1), 
                new Coordinate(98, 98, 11, 1), 
                new Coordinate(456, 456, 99, 9) 
                );

            var container = new WindsorContainer();
            string file = "Universe.txt";

            container.Install(new CoreInstaller(GetSettings()));

            var batchFac = container.Resolve<IBatchSpaceObjectFactory>();

            var objects = batchFac.Create(15000).ToList();

            var fAccessor = container.Resolve<IFileAccessor>();
            await fAccessor.WriteSpaceObjectsToFileAsync(objects, file);

            var fileObjs = await fAccessor.ReadSpaceObjectFromFileAsync(file);

            var planets = fileObjs.ToList().OfType<Planet>().ToList();

            var colonization = container.Resolve<IColonizationService>();

            //NOTE: My planets do not range from 1m - 100m area, time to colonize 50% @ 0.43km2/s 
            //ranges from 215,000 - 21,500,000 secs but there are only 86,400 secs in a 24hrs
            var colonized = colonization.ConquerTheUniverseHours(startPos, planets, 24).ToList();
            await fAccessor.WriteSpaceObjectsToFileAsync(colonized, "ColonizedPlanets.txt");
            Console.WriteLine($"Successfully colonized {colonized.Count()} planets");
        }

        private static Settings GetSettings()
        {
            return new Settings
            {
                ProbPlanetIsHabitable = int.Parse(ConfigurationManager.AppSettings["ProbPlanetIsHabitable"]),
                MinPlanetArea = double.Parse(ConfigurationManager.AppSettings["MinPlanetArea"]),
                MaxPlanetArea = double.Parse(ConfigurationManager.AppSettings["MaxPlanetArea"]),
                ProbPlanet = int.Parse(ConfigurationManager.AppSettings["ProbPlanet"]),
                Area1Limit = int.Parse(ConfigurationManager.AppSettings["Area1Limit"]),
                Area2Limit = int.Parse(ConfigurationManager.AppSettings["Area2Limit"]),
                Area3Limit = int.Parse(ConfigurationManager.AppSettings["Area3Limit"]),
                Area4Limit = int.Parse(ConfigurationManager.AppSettings["Area4Limit"]),
                TravelTimeInMins = int.Parse(ConfigurationManager.AppSettings["TravelTimeInMins"]),
                ColonizationRate = double.Parse(ConfigurationManager.AppSettings["ColonizationRate"]),
                PercentageToColonize = double.Parse(ConfigurationManager.AppSettings["PercentageToColonize"])
            };
        }
    }
}
