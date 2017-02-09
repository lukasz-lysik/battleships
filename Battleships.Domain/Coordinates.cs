namespace Battleships.Domain
{
    public struct Coordinates
    {
        public static Coordinates? FromString(string input)
        {
            if (input.Length != 2)
                return null;

            var column = input[0];
            var row = input[1];

            if (column < 'A' || column > 'Z')
                return null;

            if (row < '0' || row > '9')
                return null;

            return new Coordinates(column - 'A', row - '0');
        }

        public Coordinates(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; }
        public int Row { get; }

        public override string ToString()
        {
            return $"({Column},{Row})";
        }
    }
}
