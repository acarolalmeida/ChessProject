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

        public bool PossibleMovesExist()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Row; i++)
            {
                for (int j = 0; j < Board.Column; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMoves()[position.Row, position.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
