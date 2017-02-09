namespace Battleships.Domain
{
    public struct Direction
    {
        public Direction(int columnIncrement, int rowIncrement)
        {
            ColumnIncrement = columnIncrement;
            RowIncrement = rowIncrement;
        }

        public int ColumnIncrement { get; private set; }
        public int RowIncrement { get; private set; }
    }
}
