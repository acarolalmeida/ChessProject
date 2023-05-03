using BoardLayer;

namespace ChessLayer
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool Finished { get; private set; }
        public int Turn { get; private set; }
        public Color NextPlayer { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            NextPlayer = Color.White;
            Finished = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            IncludePieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovesCounter();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.IncludePiece(p, destination);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }
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

        public HashSet<Piece> CapturedPieces(Color color)
        { 
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in capturedPieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void IncludeNewPiece(char column, int row, Piece piece)
        {
            Board.IncludePiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        private void IncludePieces()
        {
            IncludeNewPiece('c', 1, new Rook(Color.White, Board));
            IncludeNewPiece('c', 2, new Rook(Color.White, Board));
            IncludeNewPiece('d', 2, new Rook(Color.White, Board));
            IncludeNewPiece('e', 1, new Rook(Color.White, Board));
            IncludeNewPiece('e', 2, new Rook(Color.White, Board));
            IncludeNewPiece('d', 1, new King(Color.White, Board));

            IncludeNewPiece('c', 7, new Rook(Color.Black, Board));
            IncludeNewPiece('c', 8, new Rook(Color.Black, Board));
            IncludeNewPiece('d', 7, new Rook(Color.Black, Board));
            IncludeNewPiece('e', 7, new Rook(Color.Black, Board));
            IncludeNewPiece('e', 8, new Rook(Color.Black, Board));
            IncludeNewPiece('d', 8, new King(Color.Black, Board));
        }
    }
}
