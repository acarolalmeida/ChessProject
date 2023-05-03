using BoardLayer;

namespace ChessLayer
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool Finished { get; private set; }
        public int Turn { get; private set; }
        public Color NextPlayer { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            NextPlayer = Color.White;
            Finished = false;
            IncludePieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovesCounter();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.IncludePiece(p, destination);
        }

        public void PerformPlay(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOrigin(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("The chosen origin position is empty!");
            }
            if (NextPlayer != Board.Piece(position).Color)
            {
                throw new BoardException("The chosen piece is not yours!");
            }
            if (!Board.Piece(position).PossibleMovesExist())
            {
                throw new BoardException("There are no possible moves for the chosen piece!");
            }
        }

        public void ValidateDestination(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination position!");
            }
        }

        private void ChangePlayer()
        {
            if (NextPlayer == Color.White)
            {
                NextPlayer = Color.Black;
            }
            else
            {
                NextPlayer = Color.White;
            }
        }

        private void IncludePieces()
        {
            Board.IncludePiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.IncludePiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.IncludePiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.IncludePiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.IncludePiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.IncludePiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.IncludePiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.IncludePiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.IncludePiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.IncludePiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.IncludePiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.IncludePiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}
