using NUnit.Framework;
using NSubstitute;
using System;
using Battleships.Domain.RandomGenerators;
using FluentAssertions;

namespace Battleships.Domain.Tests
{
    public class CoordinatesRandomGeneratorTests
    {
        [TestFixture]
        public class WhenRandomCoordinatesRequested
        {
            private Func<Coordinates> _subject;

            private const int _expectedColumnIndex = 4;
            private const int _expectedRowIndex = 7;
            private const int _maxColumnIndex = 10;
            private const int _maxRowIndex = 11;

            [OneTimeSetUp]
            public void Init()
            {
                var randomMock = Substitute.For<Random>();
                randomMock.Next(_maxColumnIndex).Returns(_expectedColumnIndex);
                randomMock.Next(_maxRowIndex).Returns(_expectedRowIndex);

                var generator = new CoordinatesRandomGenerator(randomMock);
                _subject = () => generator.NextCoordinate(_maxColumnIndex, _maxRowIndex);
            }

            [Test]
            public void ItShouldHaveCorrectColumnIndex()
            {
                _subject().Column.Should().Be(_expectedColumnIndex);
            }

            [Test]
            public void IsShouldHaveCorrectRowIndex()
            {
                _subject().Row.Should().Be(_expectedRowIndex);
            }
        }
    }
}
