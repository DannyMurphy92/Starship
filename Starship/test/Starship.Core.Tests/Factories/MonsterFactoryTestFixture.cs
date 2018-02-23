using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Starship.Core.Factories;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;

namespace Starship.Core.Tests.Factories
{
    [TestFixture]
    public class MonsterFactoryTestFixture
    {
        private IFixture fixture;

        private Mock<IPositionFactory> positionGenMock;

        private Monster monster;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            positionGenMock = fixture.Freeze<Mock<IPositionFactory>>();
        }

        [SetUp]
        public void Setup()
        {
            monster = fixture.Create<Monster>();

            positionGenMock.Setup(p => p.Create())
                .Returns(fixture.Create<Position>());

            positionGenMock.Setup(p => p.CreateFromString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => monster.Position);
        }

        [TearDown]
        public void Teardown()
        {
            positionGenMock.Reset();
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
            positionGenMock.Verify( p => p.Create(), Times.Once);
        }

        [Test]
        public void CreateFromString_WhenNotPassedCommaListOf6Items_ThrowsException()
        {
            // Arrange
            var invalidStr = "This is not a valid monster string";
            var subject = fixture.Create<MonsterFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(invalidStr);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }

        [Test]
        public void CreateFromString_WhenIsValidMonsterString_ReturnsMatchingMonsterObject()
        {
            // Arrange
            var monsterString = monster.ToString();
            var subject = fixture.Create<MonsterFactory>();

            // Act
            var result = subject.CreateFromString(monsterString);

            // Assert
            var pPos = monster.Position;
            var rPos = result.Position;
            Assert.IsTrue(pPos.X == rPos.X && pPos.Y == rPos.Y && pPos.Z == rPos.Z);
        }
    }
}
