using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class ColonizationServiceTestFixture
    {
        private IFixture fixture;

        private Mock<ITravelService> travelService;

        private IList<Planet> planets;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            travelService = fixture.Freeze<Mock<ITravelService>>();
        }

        [SetUp]
        public void Setup()
        {
            SetUpPlanets();

            travelService.SetupSequence(
                    t => t.FindNearestObject(It.IsAny<Position>(), It.IsAny<IList<Planet>>()))
                 .Returns(planets[0])
                .Returns(planets[1])
                .Returns(planets[2]);
        }

        [TearDown]
        public void Teardown()
        {
            travelService.Reset();
        }


        [Test]
        public void ConquerTheUniverseSecs_WhenTimeLimitOnlyAllowsForConqueringSomePlanets_OnlyConquersSomePlanets()
        {
            // Arrange
            var timeLimit = planets[0].Area * .5 * .43;
            timeLimit += planets[1].Area * .5 * .43;
            timeLimit += 10 * 60 *2;

            var subject = fixture.Create<ColonizationService>();

            // Act
            var result = subject.ConquerTheUniverseSecs(fixture.Create<Position>(), planets, timeLimit).ToList();

            // Assert
            Assert.AreEqual(2, result.Count());
            travelService.Verify(t => t.FindNearestObject(It.IsAny<Position>(), It.IsAny<IList<Planet>>()), Times.Exactly(3));
            Assert.AreEqual(planets[0], result[0]);
            Assert.AreEqual(planets[1], result[1]);
        }


        [Test]
        public void ConquerTheUniverseSecs_WhenTimeLimitOnlyAllowsForConqueringOfAPlanet_DoesNotConquerPlanet()
        {
            // Arrange
            var timeLimit = planets[0].Area * .5 * .4;

            var subject = fixture.Create<ColonizationService>();

            // Act
            var result = subject.ConquerTheUniverseSecs(fixture.Create<Position>(), planets, timeLimit).ToList();

            // Assert
            Assert.AreEqual(0, result.Count());
            travelService.Verify(t => t.FindNearestObject(It.IsAny<Position>(), It.IsAny<IList<Planet>>()), Times.Once);
        }

        private void SetUpPlanets()
        {
            planets = new List<Planet>();
            planets.Add(new Planet(fixture.Create<Position>(), true, 100000));
            planets.Add(new Planet(fixture.Create<Position>(), true, 100000));
            planets.Add(new Planet(fixture.Create<Position>(), true, 100000));
        }
    }
}
