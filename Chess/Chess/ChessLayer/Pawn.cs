using BoardLayer;

namespace ChessLayer
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board) { }

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
            }
            
            return mat;
        }
    }
}
