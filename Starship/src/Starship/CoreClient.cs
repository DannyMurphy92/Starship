using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Installer;

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

            Console.WriteLine(x.Count);
        }
    }
}
