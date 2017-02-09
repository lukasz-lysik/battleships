using System;

namespace Battleships.Domain.RandomGenerators
{
    public interface ICoordinatesRandomGenerator
    {
        Coordinates NextCoordinate(int maxColumnIndex, int maxRowIndex);
    }

    public class CoordinatesRandomGenerator : ICoordinatesRandomGenerator
    {
        private readonly Random _randomGenerator;

        public CoordinatesRandomGenerator(Random randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public Coordinates NextCoordinate(int maxColumnIndex, int maxRowIndex)
        {
            return new Coordinates(
                _randomGenerator.Next(maxColumnIndex),
                _randomGenerator.Next(maxRowIndex)
                );
        }
    }
}
