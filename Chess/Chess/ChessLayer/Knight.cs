using BoardLayer;

namespace ChessLayer
{
    class Knight : Piece
    {
        public Knight(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "H";
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

            pos.DefineValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) 
            {
                mat[pos.Row, pos.Column] = true;
            }
 
            pos.DefineValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row - 1, Position.Column + 2);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column - 2);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}
