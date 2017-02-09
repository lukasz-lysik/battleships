using System.Collections.Generic;
using System.Linq;

namespace Battleships.Domain
{
    public class ShipPosition
    {
        private List<Coordinates> _hits = new List<Coordinates>();

        public ShipPosition(Ship ship, Coordinates coordinates, Direction direction)
        {
            Ship = ship;
            Coordinates = coordinates;
            Direction = direction;
            OccupiedArea = CalculateOccupiedArea(ship, coordinates, direction);
            ShipArea = CalculateShipArea(ship, coordinates, direction);
        }

        private Coordinates[] CalculateOccupiedArea(Ship ship, Coordinates startPosition, Direction direction)
        {
            var endPosition = new Coordinates(
                startPosition.Column + direction.ColumnIncrement * (ship.Length - 1),
                startPosition.Row + direction.RowIncrement * (ship.Length - 1)
                );

            var area = new List<Coordinates>();

            for (var c = startPosition.Column - 1; c <= endPosition.Column + 1; c++)
            {
                for (var r = startPosition.Row - 1; r <= endPosition.Row + 1; r++)
                {
                    if (c >= 0 && r >= 0)
                    {
                        area.Add(new Coordinates(c, r));
                    }
                }
            }
            return area.ToArray();
        }

        private Coordinates[] CalculateShipArea(Ship ship, Coordinates startPosition, Direction direction)
        {
            var endPosition = new Coordinates(
                startPosition.Column + direction.ColumnIncrement * (ship.Length - 1),
                startPosition.Row + direction.RowIncrement * (ship.Length - 1)
                );

            var area = new List<Coordinates>();

            for (var c = startPosition.Column; c <= endPosition.Column; c++)
            {
                for (var r = startPosition.Row; r <= endPosition.Row; r++)
                {
                    if (c >= 0 && r >= 0)
                    {
                        area.Add(new Coordinates(c, r));
                    }
                }
            }
            return area.ToArray();
        }

        public bool CollidesWith(ShipPosition shipPosition)
        {
            return OccupiedArea.Intersect(shipPosition.ShipArea).Any();
        }
        
        public Ship Ship { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Direction Direction { get; private set; }
        public Coordinates[] OccupiedArea { get; }
        public Coordinates[] ShipArea { get; }

        public bool IsSunken => !ShipArea.Except(_hits).Any();

        public bool Hit(Coordinates hitCoordinates)
        {
            if (ShipArea.Contains(hitCoordinates))
            {
                _hits.Add(hitCoordinates);
                return true;
            }

            return false;
        }
    }
}
