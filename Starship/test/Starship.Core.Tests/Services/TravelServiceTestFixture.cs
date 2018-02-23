using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using Starship.Core.Models;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services;

namespace Starship.Core.Tests.Services
{
    [TestFixture]
    public class TravelServiceTestFixture
    {
        private IFixture fixture;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [Test]
        public void FindNEarestObject_WhenInvoked_ReturnsTheObjectNearestToCurrentPosition()
        {
            // Arrange
            var currX = new Coordinate(111, 111, 11, 1);
            var currY = new Coordinate(111, 111, 11, 1);
            var currZ = new Coordinate(111, 111, 11, 1);
            var currentPos = new Position(currX, currY, currZ);

            var nearX = new Coordinate(111, 111, 11, 2);
            var nearY = new Coordinate(111, 111, 11, 1);
            var nearZ = new Coordinate(111, 111, 11, 1);
            var nearPos = new Position(nearX, nearY, nearZ);

            var possiblePositions = new List<BaseSpaceObject>();
            for (int i = 0; i < 20; i++)
            {
                possiblePositions.Add(new Planet(fixture.Create<Position>(), true, 1));
            }

            var nearObj = new Planet(nearPos, true, 1);
            possiblePositions.Add(nearObj);

            var subject = fixture.Create<TravelService>();

            // Act
            var result = subject.FindNearestObject(currentPos, possiblePositions);

            // Assert
            var rPos = result.Position;
            Assert.IsTrue(rPos.X == nearX && rPos.Y == nearY && rPos.Z == nearZ);
        }
    }
}
