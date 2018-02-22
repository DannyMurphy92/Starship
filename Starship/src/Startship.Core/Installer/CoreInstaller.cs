﻿using Castle.MicroKernel.Registration;
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
                Component.For<IPositionGenerator>()
                    .ImplementedBy<PositionGenerator>());
            container.Register(
                Component.For<ISpaceObjectFactoryFactory>()
                    .ImplementedBy<SpaceObjectFactoryFactory>());
        }
    }
}
