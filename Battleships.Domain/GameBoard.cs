using System.Collections.Generic;
using System.Linq;

namespace Battleships.Domain
{
    public class GameBoard
    {
        private readonly IList<ShipPosition> _ships = new List<ShipPosition>();

        public GameBoard(int columnCount, int rowCount)
        {
            ColumnCount = columnCount;
            RowCount = rowCount;
        }

        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }

        private bool CollidesWithOther(ShipPosition shipPosition)
        {
            return _ships.Any(sp => sp.CollidesWith(shipPosition));
        }

        public bool AddShip(ShipPosition shipPosition)
        {
            if (CollidesWithOther(shipPosition))
                return false;

            _ships.Add(shipPosition);
            return true;
        }

        public HitResult Hit(Coordinates hitCoordinates)
        {
            var ship = _ships.FirstOrDefault(s => s.Hit(hitCoordinates));
            if (ship == null)
                return HitResult.Miss;

            return ship.IsSunken ? HitResult.HitAndSunk : HitResult.Hit;
        }
    }
}
