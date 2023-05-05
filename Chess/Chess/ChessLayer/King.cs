using BoardLayer;

namespace ChessLayer
{
    class King : Piece
    {
        private ChessMatch match;
        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Piece p = Board.Piece(position);
            return p == null || p.Color != Color;
        }

        private bool TestingRookCastling(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.MovesCounter == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Row, Board.Column];
            Position pos = new Position(0, 0);

            //above
            pos.DefineValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //up right diagonal
            pos.DefineValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //right
            pos.DefineValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //down right diagonal
            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //below
            pos.DefineValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //down left diagonal
            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //left
            pos.DefineValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //up left diagonal
            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //SPECIAL MOVE - castling 
            if (MovesCounter == 0 && !match.Check)
            {
                // castling short
                Position positionRookS = new Position(Position.Row, Position.Column + 3);
                if (TestingRookCastling(positionRookS))
                {
                    Position pos1 = new Position(Position.Row, Position.Column + 1);
                    Position pos2 = new Position(Position.Row, Position.Column + 2);

                    if (Board.Piece(pos1) == null && Board.Piece(pos2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }

                // castling long
                Position positionRookL = new Position(Position.Row, Position.Column - 4);
                if (TestingRookCastling(positionRookL))
                {
                    Position pos1 = new Position(Position.Row, Position.Column - 1);
                    Position pos2 = new Position(Position.Row, Position.Column - 2);
                    Position pos3 = new Position(Position.Row, Position.Column - 3);

                    if (Board.Piece(pos1) == null && Board.Piece(pos2) == null && Board.Piece(pos3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
