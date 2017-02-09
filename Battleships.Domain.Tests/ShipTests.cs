using FluentAssertions;
using NUnit.Framework;
using System;

namespace Battleships.Domain.Tests
{
    public class ShipTests
    {
        [TestFixture]
        public class WhenCreated
        {
            private Func<Ship> _subject;

            private const int _expectedLength = 5;

            [OneTimeSetUp]
            public void Init()
            {
                _subject = () => new Ship(_expectedLength);
            }

            [Test]
            public void ItShouldHaveCorrectLength()
            {
                _subject().Length.Should().Be(_expectedLength);
            }
        }
    }
}
