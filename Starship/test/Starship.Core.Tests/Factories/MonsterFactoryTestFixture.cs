using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Starship.Core.Factories;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class MonsterFactoryTestFixture
    {
        private IFixture fixture;

        private Mock<IPositionGenerator> positionGenMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            positionGenMock = fixture.Freeze<Mock<IPositionGenerator>>();
        }
        
        [Test]
        public void Create_WhenInvoked_ReturnsAMonsterObject()
        {
            // Arrange
            var subject = fixture.Create<MonsterFactory>();

            // Act
            var result = subject.Create();

            // Assert
            Assert.AreEqual(typeof(Monster), result.GetType());
        }

        [Test]
        public void Create_WhenInvoked_CallsCoordinateGenerator()
        {
            // Arrange
            var subject = fixture.Create<MonsterFactory>();

            // Act
            subject.Create();

            // Assert
            positionGenMock.Verify( p => p.Generate(), Times.Once);
        }
    }
}
