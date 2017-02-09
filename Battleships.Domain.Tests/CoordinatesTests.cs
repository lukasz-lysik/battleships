using FluentAssertions;
using NUnit.Framework;
using System;

namespace Battleships.Domain.Tests
{
    public class CoordinatesTests
    {
        [TestFixture]
        public class WhenCreatedWithTwoNumericCoordinates
        {
            private Func<Coordinates> _subject;

            private const int _knownColumnIndex = 5;
            private const int _knownRowIndex = 7;

            [OneTimeSetUp]
            public void Init()
            {
                _subject = () => new Coordinates(_knownColumnIndex, _knownRowIndex);
            }

            [Test]
            public void ItShouldHaveCorrectColumnIndex()
            {
                _subject().Column.Should().Be(_knownColumnIndex);
            }

            [Test]
            public void ItShouldHaveCorrectRowIndex()
            {
                _subject().Row.Should().Be(_knownRowIndex);
            }
        }
    }
}
