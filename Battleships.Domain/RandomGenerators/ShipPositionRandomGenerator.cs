namespace Battleships.Domain.RandomGenerators
{
    public interface IShipPositionRandomGenerator
    {
        ShipPosition NextPosition(Ship ship, GameBoard gameBoard);
    }

    public class ShipPositionRandomGenerator : IShipPositionRandomGenerator
    {
        private readonly IDirectionRandomGenerator _directionGenerator;
        private readonly ICoordinatesRandomGenerator _coordinatesGenerator;

        public ShipPositionRandomGenerator(
            IDirectionRandomGenerator directionGenerator,
            ICoordinatesRandomGenerator coordinatesGenerator
            )
        {
            _directionGenerator = directionGenerator;
            _coordinatesGenerator = coordinatesGenerator;
        }

        public ShipPosition NextPosition(Ship ship, GameBoard gameBoard)
        {
            var direction = _directionGenerator.NextDirection();

            var maxColIndex = gameBoard.ColumnCount - ship.Length * direction.ColumnIncrement;
            var maxRowIndex = gameBoard.RowCount - ship.Length * direction.RowIncrement;

            if (maxColIndex <= 0 || maxRowIndex <= 0)
                return null;

            var coordinates = _coordinatesGenerator.NextCoordinate(maxColIndex, maxRowIndex);
            return new ShipPosition(ship, coordinates, direction);
        }
    }
}
