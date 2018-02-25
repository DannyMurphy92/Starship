using System.IO.Abstractions;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Starship.Core.Factories;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Installer
{
    public class CoreInstaller : IWindsorInstaller
    {
        private readonly Settings settings;
        public CoreInstaller(Settings settings)
        {
            this.settings = settings;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRandomGenerator>()
                    .ImplementedBy<RandomGenerator>());
            container.Register(
                Component.For<IPositionFactory>()
                    .ImplementedBy<PositionFactory>());
            container.Register(
                Component.For<IMonsterFactory>()
                    .ImplementedBy<MonsterFactory>());
            container.Register(
                Component.For<IPlanetFactory>()
                    .ImplementedBy<PlanetFactory>()
                    .DependsOn(Dependency.OnValue("probHabitable", settings.ProbPlanetIsHabitable))
                    .DependsOn(Dependency.OnValue("minArea", settings.MinPlanetArea))
                    .DependsOn(Dependency.OnValue("maxArea", settings.MaxPlanetArea)));
            container.Register(
                Component.For<IBatchSpaceObjectFactory>()
                    .ImplementedBy<BatchSpaceObjectFactory>()
                    .DependsOn(Dependency.OnValue("probPlanet", settings.ProbPlanet)));
            container.Register(
                Component.For<IFileSystem>()
                    .ImplementedBy<FileSystem>());
            container.Register(
                Component.For<IFileAccessor>()
                    .ImplementedBy<FileAccessor>());
            container.Register(
                Component.For<ICoordinateFactory>()
                    .ImplementedBy<CoordinateFactory>()
                    .DependsOn(Dependency.OnValue("area1Limit", settings.Area1Limit))
                    .DependsOn(Dependency.OnValue("area2Limit", settings.Area2Limit))
                    .DependsOn(Dependency.OnValue("area3Limit", settings.Area3Limit))
                    .DependsOn(Dependency.OnValue("area4Limit", settings.Area4Limit)));
            container.Register(
                Component.For<ITravelService>()
                    .ImplementedBy<TravelService>());
            container.Register(
                Component.For<IColonizationService>()
                    .ImplementedBy<ColonizationService>()
                    .DependsOn(Dependency.OnValue("travelTimeInMins", settings.TravelTimeInMins))
                    .DependsOn(Dependency.OnValue("colonizationRateKmPerSec", settings.ColonizationRate))
                    .DependsOn(Dependency.OnValue("pcToColonize", settings.PercentageToColonize)));

        }
    }
}
