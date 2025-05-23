using System.Collections.Generic;

namespace CheckersGame
{
    public class Board
    {
        private static int s_Size;
        private static Piece[,] s_Pieces;

        public Board(int i_Size)
        {
            s_Size = i_Size;
            s_Pieces = new Piece[i_Size, i_Size];
        }

        public static int GetSize()
        {
            return s_Size;
        }

        public Piece GetPieceAt(Position i_Position)
        {
            return s_Pieces[i_Position.Row, i_Position.Column];
        }

        public void Initialize()
        {
            initializePieces();
        }

        public static bool CheckIfThereAreMovesLeft(Player i_CurrentPlayer)
        {
            char playerKingSymbol = (i_CurrentPlayer.Symbol == 'O') ? 'U' : 'K';
            int counterPiecePerPlayer = 0;
            bool noOtherMove = false;

            for (int row = 0; row < s_Size; row++)
            {
                for (int col = 0; col < s_Size; col++)
                {
                    if ((s_Pieces[row, col].Symbol == i_CurrentPlayer.Symbol ||
                        s_Pieces[row, col].Symbol == playerKingSymbol))
                    {
                        counterPiecePerPlayer++;
                        Position currentPosition = new Position(row, col);
                        List<Move> directionsToEat =
                            CheckDirectionsForEating(currentPosition, i_CurrentPlayer.Symbol, playerKingSymbol);
                        List<Move> isThereLegalMoves = GetLegalMovesForPosition(currentPosition);

                        if (directionsToEat.Count != 0 || isThereLegalMoves.Count != 0)
                        {
                            noOtherMove = true;
                            break;
                        }
                    }
                }

                if (noOtherMove)
                {
                    break;
                }
            }

            if (counterPiecePerPlayer == 0)
            {
                noOtherMove = false;
            }

            return noOtherMove;
        }

        public static List<Move> GetLegalMovesForPosition(Position i_CurrentPosition)
        {
            List<Move> legalMoves = new List<Move>();
            int currentRow = i_CurrentPosition.Row;
            int currentCol = i_CurrentPosition.Column;
            int endLimitDueToSymbol = 0;
            int startLimit = 0;
            int[] rowOffsets = { -1, -1, 1, 1 }; 
            int[] colOffsets = { -1, 1, -1, 1 };
            if (s_Pieces[currentRow, currentCol].Symbol == 'K' ||
                s_Pieces[currentRow, currentCol].Symbol == 'U')
            {
                endLimitDueToSymbol = 4;
            }
            else if (s_Pieces[currentRow, currentCol].Symbol == 'X')
            {
                endLimitDueToSymbol = 2;
            }
            else
            {
                endLimitDueToSymbol = 4;
                startLimit = 2;
            }

            for (int index = startLimit; index < endLimitDueToSymbol; index++) 
            {
                int newRow = currentRow + rowOffsets[index];
                int newCol = currentCol + colOffsets[index];

                if (newRow >= 0 && newRow < Board.GetSize() && newCol >= 0 && newCol < Board.GetSize())
                {
                    if (s_Pieces[newRow, newCol].Symbol == ' ')
                    {
                        Position ezerCurrentPosition = new Position(currentRow, currentCol);
                        Position ezerNewPosition = new Position(newRow, newCol);
                        Move ezerNewLegalMove = new Move(ezerCurrentPosition, ezerNewPosition);
                        legalMoves.Add(ezerNewLegalMove);
                    }
                }
            }

            return legalMoves;
        }

        public static List<Move> CheckIfPossibleEatingMoves(char i_CharPlayerSmybol)
        {
            char playerKingSymbol = (i_CharPlayerSmybol == 'O') ? 'U' : 'K';
            List<Move> finalAllMovesToEat = new List<Move>();
            for (int row = 0; row < s_Size; row++)
            {
                for (int col = 0; col < s_Size; col++)
                { 
                    if ((s_Pieces[row, col].Symbol == i_CharPlayerSmybol) ||
                         s_Pieces[row, col].Symbol == playerKingSymbol)
                    {
                        Position ezerPosition = new Position(row, col);
                        if (CanContinueEatingFrom(ezerPosition, i_CharPlayerSmybol,playerKingSymbol))
                        {
                            List<Move> possibleMovesToEat = CheckDirectionsForEating(ezerPosition, i_CharPlayerSmybol, playerKingSymbol);
                            for (int index = 0; index < possibleMovesToEat.Count; index++)
                            {
                                finalAllMovesToEat.Add(possibleMovesToEat[index]);
                            }
                        }
                    }
                }
            }

            return finalAllMovesToEat;
        }

        public static List<Move> CheckDirectionsForEating(Position i_CheckPositionToEating, char i_PlayerSymbol, char i_PlayerKingSymbol)
        {
            int[][] directions = null;
            Piece[,] ezerMatrixPiece = Board.GetPieceMatrix();
            char currentPieceSymbol = ezerMatrixPiece[i_CheckPositionToEating.Row, i_CheckPositionToEating.Column].Symbol;
            List<Move> listOfEatingMoves = new List<Move>();
            if (currentPieceSymbol == 'K' || currentPieceSymbol == 'U')
            {
                directions = new int[][]
                {
                    new int[] { -2, -2 },
                    new int[] { -2, 2 },
                    new int[] { 2, -2 },
                    new int[] { 2, 2 }
                };
            }
            else if (currentPieceSymbol == 'X')
            {
                directions = new int[][]
                {
                    new int[] { -2, -2 },
                    new int[] { -2, 2 }
                };
            }
            else
            {
                directions = new int[][]
                {
                    new int[] { 2, -2 },
                    new int[] { 2, 2 }
                };
            }  

            foreach (var direction in directions)
            {
                int newRow = i_CheckPositionToEating.Row + direction[0];
                int newCol = i_CheckPositionToEating.Column + direction[1];
                int middleRow = i_CheckPositionToEating.Row + direction[0] / 2;
                int middleCol = i_CheckPositionToEating.Column + direction[1] / 2;

                if (isWithinBounds(newRow, newCol))
                {
                    if (s_Pieces[middleRow, middleCol].Symbol != i_PlayerSymbol &&
                        s_Pieces[middleRow, middleCol].Symbol != i_PlayerKingSymbol &&
                        s_Pieces[middleRow, middleCol].Symbol != ' ' &&
                        s_Pieces[newRow, newCol].Symbol == ' ')
                    {
                        Position finalPosition = new Position(newRow, newCol);
                        Move ezerMove = new Move(i_CheckPositionToEating, finalPosition);
                        listOfEatingMoves.Add(ezerMove);
                    }
                }
            }

            return listOfEatingMoves;
        }

        public List<Move> GetAllEatingMoves(Player i_CurrentPlayer)
        {
            List<Move> allEatingMoves = new List<Move>();

            for (int row = 0; row < s_Size; row++)
            {
                for (int col = 0; col < s_Size; col++)
                {
                    Position position = new Position(row, col);
                    Piece piece = GetPieceAt(position);

                    if (piece != null && (piece.Symbol == i_CurrentPlayer.Symbol || piece.Symbol == i_CurrentPlayer.KingSymbol))
                    {
                        List<Move> eatingMoves = CheckDirectionsForEating(position, i_CurrentPlayer.Symbol, i_CurrentPlayer.KingSymbol);
                        allEatingMoves.AddRange(eatingMoves);
                    }
                }
            }

            return allEatingMoves;
        }

        private void initializePieces()
        {
            for (int row = 0; row < s_Size; row++)
            {
                for (int col = 0; col < s_Size; col++)
                {
                    if ((row < s_Size / 2 - 1) && (row + col) % 2 == 1)
                    {
                        s_Pieces[row, col] = new Piece('O');
                    }
                    else if ((row >= s_Size / 2 + 1) && (row + col) % 2 == 1)
                    {
                        s_Pieces[row, col] = new Piece('X');
                    }
                    else
                    {
                        s_Pieces[row, col] = new Piece(' ');
                    }
                }
            }
        }

        private static int promoteToKingIfNeeded(Position i_To, Player i_Player)
        {
            int result = 2;

            if (i_To != null)
            {
                int lastRow = (i_Player.Symbol == 'O') ? s_Size - 1 : 0;
                if (i_To.Row == lastRow)
                {
                    result = (i_Player.Symbol == 'O') ? 0 : 1;
                }
            }

            return result;
        }

        public static void MovePiece(Position i_From, Position i_To, Player i_Player)  
        {                                                             
            if (i_Player.Symbol=='X')
            {
                if (s_Pieces[i_From.Row, i_From.Column].Symbol == 'K')
                {
                    s_Pieces[i_To.Row, i_To.Column].Symbol = 'K';
                    s_Pieces[i_From.Row, i_From.Column].Symbol = ' ';
                }
                else
                {
                    s_Pieces[i_From.Row, i_From.Column].Symbol = ' ';
                    if (Board.promoteToKingIfNeeded(i_To, i_Player) == 1)
                    {
                        s_Pieces[i_To.Row, i_To.Column].Symbol = 'K';
                    }
                    else
                    {
                        s_Pieces[i_To.Row, i_To.Column].Symbol = 'X';
                    }
                }
            }
            else
            {
                if (s_Pieces[i_From.Row, i_From.Column].Symbol == 'U')
                {
                    s_Pieces[i_To.Row, i_To.Column].Symbol = 'U';
                    s_Pieces[i_From.Row, i_From.Column].Symbol = ' ';
                }
                else
                {
                    s_Pieces[i_From.Row, i_From.Column].Symbol = ' ';
                    if (Board.promoteToKingIfNeeded(i_To, i_Player) == 0)
                    {
                        s_Pieces[i_To.Row, i_To.Column].Symbol = 'U';
                    }
                    else
                    {
                        s_Pieces[i_To.Row, i_To.Column].Symbol = 'O';
                    }
                }
            }
        }

        public static void UpdateBoardForEatingMove(Position i_StartPos, Position i_EndPos)
        {
            Piece[,] ezerPiece = Board.GetPieceMatrix();
            int eatenRow = (i_StartPos.Row + i_EndPos.Row) / 2;
            int eatenCol = (i_StartPos.Column + i_EndPos.Column) / 2;
            ezerPiece[eatenRow, eatenCol].Symbol = ' ';
            ezerPiece[i_EndPos.Row, i_EndPos.Column].Symbol = ezerPiece[i_StartPos.Row, i_StartPos.Column].Symbol;

            if (i_EndPos.Row == 0)
            {
                ezerPiece[i_EndPos.Row, i_EndPos.Column].Symbol = 'K' ;
            }
            if(i_EndPos.Row == s_Size-1)
            {
                ezerPiece[i_EndPos.Row, i_EndPos.Column].Symbol = 'U';
            }
        }

        public static bool CanContinueEatingFrom(Position i_Position, char i_PlayerSymbol, char i_PlayerKingSymbol)
        {
            bool canContinueEating = false;

            if (i_Position != null)
            {
                int[][] directions;
                Piece[,] ezerMatrix = Board.GetPieceMatrix();
                if (ezerMatrix[i_Position.Row, i_Position.Column].Symbol == i_PlayerKingSymbol)
                {
                    directions = new int[][]
                    {
                        new int[] { -2, -2 },
                        new int[] { -2, 2 },
                        new int[] { 2, -2 },
                        new int[] { 2, 2 }
                    };
                }
                else if (i_PlayerSymbol == 'X')
                {
                    directions = new int[][]
                    {
                        new int[] { -2, -2 },
                        new int[] { -2, 2 }
                    };
                }
                else
                {
                    directions = new int[][]
                    {
                        new int[] { 2, -2 },
                        new int[] { 2, 2 }
                    };
                }

                foreach (var direction in directions)
                {
                    int newRow = i_Position.Row + direction[0];
                    int newCol = i_Position.Column + direction[1];
                    int middleRow = i_Position.Row + direction[0] / 2;
                    int middleCol = i_Position.Column + direction[1] / 2;

                    if (isWithinBounds(newRow, newCol) &&
                        s_Pieces[middleRow, middleCol].Symbol != i_PlayerSymbol &&
                        s_Pieces[middleRow, middleCol].Symbol != i_PlayerKingSymbol &&
                        s_Pieces[middleRow, middleCol].Symbol != ' ' &&
                        s_Pieces[newRow, newCol]?.Symbol == ' ')
                    {
                        canContinueEating = true;
                        break;
                    }
                }
            }

            return canContinueEating;
        }

        private static bool isWithinBounds(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < s_Size && i_Col >= 0 && i_Col < s_Size;
        }

        public static Piece[,] GetPieceMatrix()
        {
            return s_Pieces;
        }
    }
}
