using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
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
            var monster = fixture.Create<Monster>();
            var planetString = monster.ToString();
            var subject = fixture.Create<MonsterFactory>();

            // Act
            var result = subject.CreateFromString(planetString);

            // Assert
            var pPos = monster.Position;
            var rPos = result.Position;
            Assert.IsTrue(pPos.X == rPos.X && pPos.Y == rPos.Y && pPos.Z == rPos.Z);
        }

        [Test]
        public void CreateFromString_WhenXDoesNotParseToDouble_ThrowsException()
        {
            // Arrange
            var monster = fixture.Create<Monster>();
            var monsterString = monster.ToString();
            var monsterArgs = monsterString.Split(',');
            monsterArgs[1] = "ffff";
            monsterString = string.Join(",", monsterArgs);
            var subject = fixture.Create<MonsterFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(monsterString);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }

        [Test]
        public void CreateFromString_WhenYDoesNotParseToDouble_ThrowsException()
        {
            // Arrange
            var monster = fixture.Create<Monster>();
            var monsterString = monster.ToString();
            var monsterArgs = monsterString.Split(',');
            monsterArgs[2] = "ffff";
            monsterString = string.Join(",", monsterArgs);
            var subject = fixture.Create<MonsterFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(monsterString);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }

        [Test]
        public void CreateFromString_WhenZDoesNotParseToDouble_ThrowsException()
        {
            // Arrange
            var monster = fixture.Create<Monster>();
            var monsterString = monster.ToString();
            var monsterArgs = monsterString.Split(',');
            monsterArgs[3] = "ffff";
            monsterString = string.Join(",", monsterArgs);
            var subject = fixture.Create<MonsterFactory>();

            // Act
            TestDelegate act = () => subject.CreateFromString(monsterString);

            // Assert 
            Assert.That(act, Throws.ArgumentException
                .With.Property("Message").EqualTo("Input is not a valid argument"));
        }
    }
}
