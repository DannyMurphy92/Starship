using System;
using System.Linq;
using Castle.Windsor;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Installer;
using Starship.Core.Services.Interfaces;

namespace Starship.Cli
{
    public class CoreClient
    {
        public static void CreateUniverseFile()
        {
            var container = new WindsorContainer();

            container.Install(new CoreInstaller());

            var batchFac = container.Resolve<IBatchSpaceObjectFactory>();

            var x = batchFac.Generate(500).ToList();

            var fAccessor = container.Resolve<IFileAccessor>();
            fAccessor.WriteSpaceObjectsToFile(x, "Universe.txt");

            Console.WriteLine(x.Count);
        }
    }
}
