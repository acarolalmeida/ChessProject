namespace BoardLayer
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesCounter { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            MovesCounter = 0;
        }

        public void IncreaseMovesCounter()
        {
            MovesCounter++;
        }

        public abstract bool[,] PossibleMoves();
    }
}
