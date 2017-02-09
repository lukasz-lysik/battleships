using FluentAssertions;
using NUnit.Framework;
using System;

namespace Battleships.Domain.Tests
{
    public class ShipPositionTests
    {
        [TestFixture]
        public class WhenShipCreateddInTheMiddle
        {
            private Func<ShipPosition> _subject;

            private readonly Ship _ship = new Ship(2);
            private readonly Coordinates _position = new Coordinates(2, 3);
            private readonly Direction _direction = new Direction(0, 1);

            [OneTimeSetUp]
            public void Init()
            {
                _subject = () => new ShipPosition(_ship, _position, _direction);
            }

            [Test]
            public void ItShouldHaveCorrectOccupiedArrea()
            {
                _subject().OccupiedArea.Should().BeEquivalentTo(
                    new Coordinates(1, 2),
                    new Coordinates(1, 3),
                    new Coordinates(1, 4),
                    new Coordinates(1, 5),
                    new Coordinates(2, 2),
                    new Coordinates(2, 3),
                    new Coordinates(2, 4),
                    new Coordinates(2, 5),
                    new Coordinates(3, 2),
                    new Coordinates(3, 3),
                    new Coordinates(3, 4),
                    new Coordinates(3, 5)
                    );
            }

            [Test]
            public void ItShouldHaveCorrectShipArea()
            {
                _subject().ShipArea.Should().BeEquivalentTo(
                    new Coordinates(2, 3),
                    new Coordinates(2, 4)
                    );
            }
        }

        [TestFixture]
        public class WhenShipCreatedOnTheSide
        {
            private Func<ShipPosition> _subject;

            private readonly Ship _ship = new Ship(3);
            private readonly Coordinates _position = new Coordinates(2, 0);
            private readonly Direction _direction = new Direction(1, 0);

            [OneTimeSetUp]
            public void Init()
            {
                _subject = () => new ShipPosition(_ship, _position, _direction);
            }

            [Test]
            public void ItShouldHaveCorrectOccupiedArrea()
            {
                _subject().OccupiedArea.Should().BeEquivalentTo(
                    new Coordinates(1, 0),
                    new Coordinates(2, 0),
                    new Coordinates(3, 0),
                    new Coordinates(4, 0),
                    new Coordinates(5, 0),
                    new Coordinates(1, 1),
                    new Coordinates(2, 1),
                    new Coordinates(3, 1),
                    new Coordinates(4, 1),
                    new Coordinates(5, 1)
                    );
            }

            [Test]
            public void ItShouldHaveCorrectShipArrea()
            {
                _subject().ShipArea.Should().BeEquivalentTo(
                    new Coordinates(2, 0),
                    new Coordinates(3, 0),
                    new Coordinates(4, 0)
                    );
            }
        }

        [TestFixture]
        public class WhenTwoShipsCollide
        {
            private Func<bool> _subject;

            private readonly Ship _ship = new Ship(3);
            private readonly Coordinates _position = new Coordinates(2, 0);
            private readonly Direction _direction = new Direction(1, 0);

            private readonly ShipPosition _otherPosition = new ShipPosition
            (
                new Ship(5),
                new Coordinates(2, 1),
                new Direction(0, 1)
            );

            [OneTimeSetUp]
            public void Init()
            {
                var position = new ShipPosition(_ship, _position, _direction);
                _subject = () => position.CollidesWith(_otherPosition);
            }

            [Test]
            public void ItShouldRecogniseCollision()
            {
                _subject().Should().BeTrue();
            }
        }

    }


    [TestFixture]
    public class WhenTwoShipsDontCollide
    {
        private Func<bool> _subject;

        private readonly Ship _ship = new Ship(3);
        private readonly Coordinates _position = new Coordinates(2, 0);
        private readonly Direction _direction = new Direction(1, 0);

        private readonly ShipPosition _otherPosition = new ShipPosition
        (
            new Ship(5),
            new Coordinates(2, 2),
            new Direction(0, 1)
        );

        [OneTimeSetUp]
        public void Init()
        {
            var position = new ShipPosition(_ship, _position, _direction);
            _subject = () => position.CollidesWith(_otherPosition);
        }

        [Test]
        public void ItShouldRecogniseNoCollision()
        {
            _subject().Should().BeFalse();
        }
    }

    [TestFixture]
    public class WhenHitAttemptSuccessful
    {
        private Func<bool> _subject;

        [OneTimeSetUp]
        public void Init()
        {
            var shipPosition = new ShipPosition
            (
                new Ship(5),
                new Coordinates(2, 2),
                new Direction(0, 1)
            );

            _subject = () => shipPosition.Hit(new Coordinates(2, 3));
        }

        [Test]
        public void ItShouldReturnTrue()
        {
            _subject().Should().BeTrue();
        }
    }

    [TestFixture]
    public class WhenHitAttemptUnsuccessful
    {
        private Func<bool> _subject;

        [OneTimeSetUp]
        public void Init()
        {
            var shipPosition = new ShipPosition
            (
                new Ship(5),
                new Coordinates(2, 2),
                new Direction(0, 1)
            );

            _subject = () => shipPosition.Hit(new Coordinates(4, 3));
        }

        [Test]
        public void ItShouldReturnFalse()
        {
            _subject().Should().BeFalse();
        }
    }

    [TestFixture]
    public class WhenAllHitAttemptsSuccessful
    {
        private Func<bool> _subject;

        [OneTimeSetUp]
        public void Init()
        {
            var shipPosition = new ShipPosition
            (
                new Ship(3),
                new Coordinates(2, 2),
                new Direction(0, 1)
            );

            shipPosition.Hit(new Coordinates(2, 2));
            shipPosition.Hit(new Coordinates(2, 3));
            shipPosition.Hit(new Coordinates(2, 4));

            _subject = () => shipPosition.IsSunken;
        }

        [Test]
        public void ItShouldBeSunken()
        {
            _subject().Should().BeTrue();
        }
    }
}