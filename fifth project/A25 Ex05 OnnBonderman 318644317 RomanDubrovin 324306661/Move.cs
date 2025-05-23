using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersGame
{
    public class Move
    {
        private static Position s_LastEatPosition;
        private Position m_From;
        private Position m_To;

        public Position LastEatPosition
        {
            get { return s_LastEatPosition; }
            set { s_LastEatPosition = value; }
        }
     
        public Position FromPosition
        {
            get { return this.m_From; }
            set { this.m_From = value; }
        }

        public Position ToPosition
        {
            get { return this.m_To; }
            set { this.m_To = value; }
        }

        public Move(Position i_From, Position i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        public Move ProcessMove(Position i_StartPos, Position i_EndPos, Player i_Player, Board i_Board, Position i_LastEaterPosition)
        {
            char playerKingSymbol = (i_Player.Symbol == 'O') ? 'U' : 'K';
            Piece[,] ezerMatrix = Board.GetPieceMatrix();
            if (ezerMatrix[i_StartPos.Row, i_StartPos.Column].Symbol == i_Player.Symbol ||
                ezerMatrix[i_StartPos.Row, i_StartPos.Column].Symbol == playerKingSymbol)
            {
                List<Move> eatingMoves = i_Board.GetAllEatingMoves(i_Player);
                if (eatingMoves.Count > 0)
                {
                    bool isValidEatingMove = eatingMoves.Any(move => move.FromPosition.Equals(i_StartPos) && move.ToPosition.Equals(i_EndPos));
                    if (!isValidEatingMove)
                    {
                        throw new ArgumentException("You must choose a valid eating move.");
                    }
                }

                if (ezerMatrix[i_EndPos.Row, i_EndPos.Column].Symbol != ' ')
                {
                    throw new ArgumentException("End Target isn't available.");
                }

                if (i_LastEaterPosition != null && !i_StartPos.Equals(i_LastEaterPosition))
                {
                    throw new ArgumentException("You must continue eating with the same piece.");
                }

                if (CheckforBackwards(i_StartPos, i_EndPos, ezerMatrix[i_StartPos.Row, i_StartPos.Column].Symbol))
                {
                    throw new ArgumentException("Illegal Move! Cannot move backwards.");
                }

                if (CheckMoveEatable(i_StartPos, i_EndPos, i_Board))
                {
                    Board.UpdateBoardForEatingMove(i_StartPos, i_EndPos);
                    if (Board.CanContinueEatingFrom(i_EndPos, i_Player.Symbol, playerKingSymbol))
                    {
                        s_LastEatPosition = i_LastEaterPosition = i_EndPos;
                    }
                    else
                    {
                        s_LastEatPosition = i_LastEaterPosition = null;
                    }
                }
                else if (checkMoveTooFar(i_StartPos, i_EndPos))
                {
                    throw new ArgumentException("Move too far.");
                }
                else if (!checkMoveIsEmpty(i_EndPos))
                {
                    throw new ArgumentException("End position is not free.");
                }
                else if (!checkMoveDiagonal(i_StartPos, i_EndPos))
                {
                    throw new ArgumentException("Move is not diagonal.");
                }
            }
            else
            {
                throw new ArgumentException("Incorrect player selection. Please choose one of your own pieces.");
            }

            return new Move(i_StartPos, i_EndPos);
        }
       
        public static bool CheckMoveEatable(Position i_StartPos, Position i_EndPos, Board i_Board) 
        {
            bool moveEatable = false;
            if (checkMoveforEatable(i_StartPos, i_EndPos))
            {
                Piece[,] boardPieces = Board.GetPieceMatrix();
                char startSymbol = boardPieces[i_StartPos.Row, i_StartPos.Column].Symbol;
                int middleRow = (i_StartPos.Row + i_EndPos.Row) / 2;
                int middleCol = (i_StartPos.Column + i_EndPos.Column) / 2;

                int ezerEndRow = 0, ezerEndCol = 0;
                ezerEndRow = i_EndPos.Row;
                ezerEndCol = i_EndPos.Column;
                Piece middlePiece = boardPieces[middleRow, middleCol];
                Piece targetPiece = boardPieces[ezerEndRow, ezerEndCol];

                if ("XOKU".Contains(boardPieces[i_StartPos.Row, i_StartPos.Column].Symbol))
                {
                    char middleSymbol = middlePiece.Symbol;
                    char targetSymbol = targetPiece.Symbol;

                    if (targetSymbol == ' ' &&
                       ((middleSymbol == 'O' && startSymbol != 'U') ||
                        (middleSymbol == 'X' && startSymbol != 'K') ||
                        (middleSymbol == 'K' && startSymbol != 'X') ||
                        (middleSymbol == 'U' && startSymbol != 'O')))
                    {
                        moveEatable = true;

                        return moveEatable;
                    }
                }
            }
          
            return moveEatable;
        }

        private static bool checkMoveDiagonal(Position i_StartPos, Position i_EndPos)
        {
            return (Math.Abs(i_StartPos.Row - i_EndPos.Row) == 1 &&
                   Math.Abs(i_StartPos.Column - i_EndPos.Column) == 1) ||
                   (Math.Abs(i_StartPos.Row - i_EndPos.Row) == 2 &&
                   Math.Abs(i_StartPos.Column - i_EndPos.Column) == 2);
        }

        private static bool checkMoveforEatable(Position i_StartPos, Position i_EndPos)
        {
            return Math.Abs(i_StartPos.Row - i_EndPos.Row) == 2  &&
                   Math.Abs(i_StartPos.Column - i_EndPos.Column) ==2 ;
        }

        private static bool checkMoveTooFar(Position i_StartPos, Position i_EndPos)
        {
            return Math.Abs(i_StartPos.Row - i_EndPos.Row) > 1 &&
                   Math.Abs(i_StartPos.Column - i_EndPos.Column) > 1;
        }

        public static bool CheckforBackwards(Position i_StartPos, Position i_EndPos, char i_PieceSymbol)
        {
            bool isBackMove = true;

            if (i_PieceSymbol == 'K' || i_PieceSymbol == 'U')
            {
                isBackMove = false;
            }
            else if (i_PieceSymbol == 'X')
            {
                if (i_StartPos.Row > i_EndPos.Row)
                {
                    isBackMove = false;
                }
            }
            else if (i_PieceSymbol == 'O')
            {
                if (i_StartPos.Row < i_EndPos.Row)
                {
                    isBackMove = false;
                }
            }

            return isBackMove;
        }

        private static bool checkMoveIsEmpty(Position i_EndPos)
        {
            Piece[,] ezerPiece = Board.GetPieceMatrix();
            Piece targetPiece = ezerPiece[i_EndPos.Row, i_EndPos.Column];

            return targetPiece == null || targetPiece.Symbol == ' ';     
        }
    }
}
