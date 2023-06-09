﻿using BoardLayer;
using ChessLayer;

namespace Chess
{
    class Program
    {
        static void Main()
        {
            try
            {
                
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try 
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOrigin(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.Write("\nDestination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestination(origin, destination);

                        match.PerformPlay(origin, destination);
                    }
                    catch (BoardException ex)
                    { 
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch(BoardException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}