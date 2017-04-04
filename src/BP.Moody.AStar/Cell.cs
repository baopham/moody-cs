namespace BP.Moody.AStar
{
    public struct Cell
    {
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Is(Cell? other)
        {
            return Equals(other);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}