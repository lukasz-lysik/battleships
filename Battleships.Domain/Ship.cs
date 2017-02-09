namespace Battleships.Domain
{
    public struct Ship
    {
        public Ship(int length)
        {
            Length = length;
        }

        public int Length { get; private set; }
    }
}
