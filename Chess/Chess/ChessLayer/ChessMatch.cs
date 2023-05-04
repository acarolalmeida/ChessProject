using BoardLayer;

namespace ChessLayer
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool Finished { get; private set; }
        public int Turn { get; private set; }
        public Color NextPlayer { get; private set; }
        public bool Check { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            NextPlayer = Color.White;
            Finished = false;
            Check = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            IncludePieces();
        }

        public Piece ExecuteMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovesCounter();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.IncludePiece(p, destination);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecreaseMovesCounter();
            if (capturedPiece != null)
            {
                Board.IncludePiece(capturedPiece, destination);
                capturedPieces.Remove(capturedPiece);
            }

            Board.IncludePiece(p, origin);
        }

        public void PerformPlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMove(origin, destination);
            if (IsInCheck(NextPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Check = IsInCheck(Opponent(NextPlayer)) ? true : false;
            if (TestCheckmate(Opponent(NextPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
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
            if (!Board.Piece(origin).PossibleMovement(destination))
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

        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.PossibleMoves();
                for (int i = 0; i < Board.Row; i++)
                {
                    for (int j = 0; j < Board.Column; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, target);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece p in PiecesInGame(color))
            {
                if (p is King)
                {
                    return p;
                }
            }

            return null;
        }

        public bool IsInCheck(Color color)
        {
            foreach (Piece p in PiecesInGame(Opponent(color)))
            {
                Piece K = King(color);
                if (K == null)
                {
                    throw new BoardException($"There is no {color} king in the board!");
                }

                bool[,] mat = p.PossibleMoves();
                if (mat[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public void IncludeNewPiece(char column, int row, Piece piece)
        {
            Board.IncludePiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        private void IncludePieces()
        {
            IncludeNewPiece('a', 1, new Rook(Color.White, Board));
            IncludeNewPiece('b', 1, new Knight(Color.White, Board));
            IncludeNewPiece('c', 1, new Bishop(Color.White, Board));
            IncludeNewPiece('d', 1, new Queen(Color.White, Board));
            IncludeNewPiece('e', 1, new King(Color.White, Board));
            IncludeNewPiece('f', 1, new Bishop(Color.White, Board));
            IncludeNewPiece('g', 1, new Knight(Color.White, Board));
            IncludeNewPiece('h', 1, new Rook(Color.White, Board));
            IncludeNewPiece('a', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('b', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('c', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('d', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('e', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('f', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('g', 2, new Pawn(Color.White, Board));
            IncludeNewPiece('h', 2, new Pawn(Color.White, Board));

            IncludeNewPiece('a', 8, new Rook(Color.Black, Board));
            IncludeNewPiece('b', 8, new Knight(Color.Black, Board));
            IncludeNewPiece('c', 8, new Bishop(Color.Black, Board));
            IncludeNewPiece('d', 8, new Queen(Color.Black, Board));
            IncludeNewPiece('e', 8, new King(Color.Black, Board));
            IncludeNewPiece('f', 8, new Bishop(Color.Black, Board));
            IncludeNewPiece('g', 8, new Knight(Color.Black, Board));
            IncludeNewPiece('h', 8, new Rook(Color.Black, Board));
            IncludeNewPiece('a', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('b', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('c', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('d', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('e', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('f', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('g', 7, new Pawn(Color.Black, Board));
            IncludeNewPiece('h', 7, new Pawn(Color.Black, Board));
        }
    }
}
