using BoardLayer;

namespace ChessLayer
{
    class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Color color, Board board, ChessMatch match) : base(color, board) 
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool OpponentExists(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool Free(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Row, Board.Column];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && MovesCounter == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && OpponentExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && OpponentExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                //SPECIAL MOVE - en passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && OpponentExists(left) && Board.Piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && OpponentExists(right) && Board.Piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.DefineValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && MovesCounter == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && OpponentExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && OpponentExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                //SPECIAL MOVE - en passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && OpponentExists(left) && Board.Piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && OpponentExists(right) && Board.Piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }
            
            return mat;
        }
    }
}
