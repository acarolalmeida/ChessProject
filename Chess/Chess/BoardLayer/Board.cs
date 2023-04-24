namespace BoardLayer
{
    class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        private Piece[,] pieces;

        public Board(int row, int column)
        {
            Row = row;
            Column = column;
            pieces = new Piece[row, column];
        }

        public Piece Piece(int row, int column)
        {
            return pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return pieces[position.Row, position.Column];
        }

        public bool ExistingPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void IncludePiece(Piece piece, Position position)
        {
            if (ExistingPiece(position))
            {
                throw new BoardException("There is already a piece in this position!");
            }
            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Row || position.Column < 0 || position.Column >= Column)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
