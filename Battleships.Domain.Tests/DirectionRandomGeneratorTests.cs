using NUnit.Framework;
using NSubstitute;
using System;
using Battleships.Domain.RandomGenerators;
using FluentAssertions;

namespace Battleships.Domain.Tests
{
    public class DirectionRandomGeneratorTests
    {
        [TestFixture]
        public class WhenRandomDirectionIsRequested
        {
            private Func<Direction> _subject;

            private const int _directionsCount = 2;
            private const int _randomDirectionIndex = 0;

            [OneTimeSetUp]
            public void Init()
            {
                var randomMock = Substitute.For<Random>();
                randomMock.Next(_directionsCount).Returns(_randomDirectionIndex);

                var generator = new DirectionRandomGenerator(randomMock);
                _subject = () => generator.NextDirection();
            }

            [Test]
            public void ItShouldHaveNoneEmptyDirection()
            {
                _subject().Should().NotBeNull();
            }
        }
    }
}
