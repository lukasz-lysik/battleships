using System;

namespace Battleships.Domain.RandomGenerators
{
    public interface IDirectionRandomGenerator
    {
        Direction NextDirection();
    }

    public class DirectionRandomGenerator : IDirectionRandomGenerator
    {
        private readonly Random _randomGenerator;

        private readonly Direction[] _allDirections = {
            new Direction(1, 0),
            new Direction(0, 1)
        };

        public DirectionRandomGenerator(Random randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public Direction NextDirection()
        {
            return _allDirections[_randomGenerator.Next(_allDirections.Length)];
        }
    }
}
