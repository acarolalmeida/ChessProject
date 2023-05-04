using BoardLayer;

namespace ChessLayer
{
    class Bishop : Piece
    {
        public Bishop(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position position)
        {
            Piece p = Board.Piece(position);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Row, Board.Column];
            Position pos = new Position(0, 0);

            //northwest
            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) 
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Row - 1, Position.Column - 1);
            }

            //northeast
            pos.DefineValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Row - 1, Position.Column + 1);
            }

            //southeast 
            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Row + 1, Position.Column + 1);
            }

            //southwest
            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Row + 1, Position.Column - 1);
            }

            return mat;
        }
    }
}
