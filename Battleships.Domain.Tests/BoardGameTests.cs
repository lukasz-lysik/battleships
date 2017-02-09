using FluentAssertions;
using NUnit.Framework;
using System;

namespace Battleships.Domain.Tests
{
    public class BoardGameTests
    {
        [TestFixture]
        public class WhenCreated
        {
            private Func<GameBoard> _subject;

            private const int _knownColumnCount = 11;
            private const int _knownRowCount = 12;

            [OneTimeSetUp]
            public void Init()
            {
                _subject = () => new GameBoard(_knownColumnCount, _knownRowCount);
            }

            [Test]
            public void ItShouldHaveCorrectColumnCount()
            {
                _subject().ColumnCount.Should().Be(_knownColumnCount);
            }

            [Test]
            public void ItShouldHaveCorrectRowCount()
            {
                _subject().RowCount.Should().Be(_knownRowCount);
            }
        }

        [TestFixture]
        public class WhenAddingCollidingShips
        {
            private Func<Tuple<bool,bool>> _subject;

            private readonly ShipPosition _ship1 = new ShipPosition
            (
                new Ship(4),
                new Coordinates(3, 2),
                new Direction(1, 0)
            );

            private readonly ShipPosition _ship2 = new ShipPosition
            (
                new Ship(5),
                new Coordinates(2, 1),
                new Direction(0, 1)
            );

            [OneTimeSetUp]
            public void Init()
            {
                var board = new GameBoard(10, 10);
                _subject = () => new Tuple<bool, bool>
                (
                    board.AddShip(_ship1),
                    board.AddShip(_ship2)
                );
            }

            [Test]
            public void ItShouldAddFirstShip()
            {
                _subject().Item1.Should().BeTrue();
            }

            [Test]
            public void ItShouldRejectSecondShip()
            {
                _subject().Item2.Should().BeFalse();
            }
        }
    }
}
