using Battleships.Domain.RandomGenerators;

namespace Battleships.Domain.Builders
{
    public class GameBoardBuilder
    {
        private readonly IShipPositionRandomGenerator _shipPositionGenerator;

        public GameBoardBuilder(IShipPositionRandomGenerator shipPositionGenerator)
        {
            _shipPositionGenerator = shipPositionGenerator;
        }
        public GameBoard Build(int columnCount, int rowCount, params Ship[] ships)
        {
            var board = new GameBoard(columnCount, rowCount);

            foreach (var ship in ships)
            {
                ShipPosition newPosition;
                do
                {
                    newPosition = _shipPositionGenerator.NextPosition(ship, board);
                } while (!board.AddShip(newPosition));
            }
            
            return board;
        }
    }
}
