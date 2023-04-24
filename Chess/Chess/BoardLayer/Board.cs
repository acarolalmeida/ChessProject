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

        public void IncludePiece(Piece piece, Position position)
        {
            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }
    }
}
