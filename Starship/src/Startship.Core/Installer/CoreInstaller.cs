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
                    .ImplementedBy<PlanetFactory>());
            container.Register(
                Component.For<IBatchSpaceObjectFactory>()
                    .ImplementedBy<BatchSpaceObjectFactory>());
            container.Register(
                Component.For<IFileAccessor>()
                    .ImplementedBy<FileAccessor>());
            container.Register(
                Component.For<ICoordinateFactory>()
                    .ImplementedBy<CoordinateFactory>());
            container.Register(
                Component.For<ITravelService>()
                    .ImplementedBy<TravelService>());
            container.Register(
                Component.For<IColonizationService>()
                    .ImplementedBy<ColonizationService>());

        }
    }
}
