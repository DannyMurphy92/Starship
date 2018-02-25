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
        //NOTE: My planets do not range from 1m - 100m area, time to colonize 50% @ 0.43km2/s 
        //ranges from 215,000 - 21,500,000 secs but there are only 86,400 secs in a 24hrs
        public static async Task CreateUniverseFileAsync()
        {
            var numberOfItem = int.Parse(ConfigurationManager.AppSettings["NumberOfSpaceItems"]);
            var timeForColonizing = int.Parse(ConfigurationManager.AppSettings["TimeInHoursForColonizing"]);
            var universeFile = ConfigurationManager.AppSettings["FullUniverseFile"];
            var colonizedPlanetsFile = ConfigurationManager.AppSettings["ColonizedPlanetsFile"];

            var startPos = new Position(
                new Coordinate(123, 123, 99, 1), 
                new Coordinate(98, 98, 11, 1), 
                new Coordinate(456, 456, 99, 9) 
                );

            var container = new WindsorContainer();
            container.Install(new CoreInstaller(GetSettings()));

            var batchFac = container.Resolve<IBatchSpaceObjectFactory>();
            var fAccessor = container.Resolve<IFileAccessor>();
            var colonization = container.Resolve<IColonizationService>();

            //Create all the items in the universe and write to file
            var objects = batchFac.Create(numberOfItem).ToList();
            await fAccessor.WriteSpaceObjectsToFileAsync(objects, universeFile);

            //Read all items in universe from file
            var fileObjs = await fAccessor.ReadSpaceObjectFromFileAsync(universeFile);
            var planets = fileObjs.ToList().OfType<Planet>().ToList();

            //Colonize planets and write colonized planets to file
            var colonized = colonization.ConquerTheUniverseHours(startPos, planets, timeForColonizing).ToList();
            await fAccessor.WriteSpaceObjectsToFileAsync(colonized, colonizedPlanetsFile);
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
