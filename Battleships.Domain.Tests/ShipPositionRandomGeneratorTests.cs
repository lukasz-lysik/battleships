using NUnit.Framework;
using NSubstitute;
using System;
using Battleships.Domain.RandomGenerators;
using FluentAssertions;

namespace Battleships.Domain.Tests
{
    public class ShipPositionRandomGeneratorTests
    {
        [TestFixture]
        public class WhenVerticalPositionWasGenerated
        {
            private Func<ShipPosition> _subject;

            private const int _expectedColumnIndex = 3;
            private const int _expectedRowIndex = 9;
            private const int _maxColumnIndex = 10;
            private const int _maxRowIndex = 10;
            private const int _shipSize = 4;

            private ICoordinatesRandomGenerator _coordinatesGenerator;

            [OneTimeSetUp]
            public void Init()
            {
                var directionGenerator = Substitute.For<IDirectionRandomGenerator>();
                directionGenerator.NextDirection().Returns(new Direction(1, 0));

                _coordinatesGenerator = Substitute.For<ICoordinatesRandomGenerator>();
                _coordinatesGenerator
                    .NextCoordinate(6, 10)
                    .Returns(new Coordinates(_expectedColumnIndex, _expectedRowIndex));

                var gameBoard = new GameBoard(_maxColumnIndex, _maxRowIndex);
                var ship = new Ship(_shipSize);
                
                var generator = new ShipPositionRandomGenerator(directionGenerator, _coordinatesGenerator);
                _subject = () => generator.NextPosition(ship, gameBoard);
            }

            [Test]
            public void ItShouldCallCoordinatesGeneratorWithProperParameters()
            {
                _subject();
                _coordinatesGenerator.Received().NextCoordinate(6, 10);
            }

            [Test]
            public void ItShouldReturnValue()
            {
                _subject().Should().NotBeNull();
            }

            [Test]
            public void ItShouldReturnCorrectCoordinates()
            {
                _subject().Coordinates.Should().Be(new Coordinates(3, 9));
            }

            [Test]
            public void ItShouldReturnCorrectDirection()
            {
                _subject().Direction.Should().Be(new Direction(1, 0));
            }

            [Test]
            public void ItShouldReturnCorrectShip()
            {
                _subject().Ship.Should().Be(new Ship(4));
            }
        }

        [TestFixture]
        public class WhenShipDoesntFitTheBoard
        {
            private Func<ShipPosition> _subject;

            private const int _expectedColumnIndex = 3;
            private const int _expectedRowIndex = 9;
            private const int _maxColumnIndex = 10;
            private const int _maxRowIndex = 10;
            private const int _shipSize = 11;

            private ICoordinatesRandomGenerator _coordinatesGenerator;

            [OneTimeSetUp]
            public void Init()
            {
                var directionGenerator = Substitute.For<IDirectionRandomGenerator>();
                directionGenerator.NextDirection().Returns(new Direction(1, 0));

                _coordinatesGenerator = Substitute.For<ICoordinatesRandomGenerator>();

                var gameBoard = new GameBoard(_maxColumnIndex, _maxRowIndex);
                var ship = new Ship(_shipSize);

                var generator = new ShipPositionRandomGenerator(directionGenerator, _coordinatesGenerator);
                _subject = () => generator.NextPosition(ship, gameBoard);
            }

            [Test]
            public void ItShouldNotCallCoordinatesGeneratorWithProperParameters()
            {
                _subject();
                _coordinatesGenerator.DidNotReceive().NextCoordinate(Arg.Any<int>(), Arg.Any<int>());
            }

            [Test]
            public void ItShouldNotReturnAnyValue()
            {
                _subject().Should().BeNull();
            }
        }
    }
}
