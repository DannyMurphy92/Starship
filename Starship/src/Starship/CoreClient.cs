using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Windsor;
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
            var container = new WindsorContainer();
            string file = "Universe.txt";

            container.Install(new CoreInstaller());

            var batchFac = container.Resolve<IBatchSpaceObjectFactory>();

            var objects = batchFac.Generate(1500).ToList();

            var fAccessor = container.Resolve<IFileAccessor>();
            await fAccessor.WriteSpaceObjectsToFileAsync(objects, file);

            var fileObjs = await fAccessor.ReadSpaceObjectFromFileAsync(file);

            var planets = fileObjs.ToList().Where(f => f is Planet).ToList();

            Console.WriteLine(objects.Count);
        }
    }
}
