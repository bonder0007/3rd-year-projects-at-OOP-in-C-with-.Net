using CheckersGame;
using System;
using System.Collections.Generic;

public class GameManager
{
    private int m_CurrentPlayerIndex;
    private bool m_IsGameOver;
    private static bool s_IsVsComputer;
    private Position m_LastEaterPosition = null;
    private static Board s_Board;
    private static Player[] s_Players;
    private static Random s_Random = new Random();
    public event Action<string> GameOverEvent;

    public int CurrentPlayerIndex
    {
        get { return m_CurrentPlayerIndex; }
        set { m_CurrentPlayerIndex = value; }
    }

    public Player CurrentPlayer
    {
        get { return s_Players[m_CurrentPlayerIndex]; }
        set { s_Players[m_CurrentPlayerIndex] = value; }
    }

    public static Player[] GetPlayers()
    {
        return s_Players;
    }

    public Board Board
    {
        get { return s_Board; }
    }

    public void SwitchToNextPlayer()
    {
        m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % s_Players.Length;
    }

    public void StartGameLogical(int i_BoardSize, Player[] i_Players, bool i_IsVsComputer)
    {
        s_Board = new Board(i_BoardSize);
        s_Players = i_Players;
        this.m_CurrentPlayerIndex = 0;
        this.m_IsGameOver = false;
        s_IsVsComputer = i_IsVsComputer;
        s_Board.Initialize();
        bool IsThereAnyMovesLeft;
        List<Move> PossibleEatingMoves;
    }
 
    public static Move StartComputerMove()
    {
        List<Move> eatingMovesForComputer = new List<Move>();
        List<Move> legalMovesForComputer = new List<Move>();

        return collectComputerMoves(eatingMovesForComputer, legalMovesForComputer);
    }

    private static Move collectComputerMoves(List<Move> i_EatingMovesForComputer, List<Move> i_LegalMovesForComputer)
    {
        char computerSymbol = s_Players[1].Symbol;
        char computerkingSymbol = (s_Players[1].Symbol == 'X') ? 'K' : 'U';
        i_EatingMovesForComputer = Board.CheckIfPossibleEatingMoves(computerSymbol);
        i_LegalMovesForComputer = new List<Move>();
        Piece[,] ezerMatrixPiece = Board.GetPieceMatrix();

        for(int row = 0; row < Board.GetSize(); row++)
        {
            for(int col = 0; col < Board.GetSize(); col++)
            {
                if ((ezerMatrixPiece[row, col].Symbol == computerSymbol) ||
                     ezerMatrixPiece[row, col].Symbol == computerkingSymbol)
                {
                    Position ezerCurrentPosToCheck = new Position(row, col);
                    List<Move> ezerLegalListMove = Board.GetLegalMovesForPosition(ezerCurrentPosToCheck);
                    if(ezerLegalListMove != null)
                    {
                        i_LegalMovesForComputer.AddRange(ezerLegalListMove);
                    }
                }
            }
        }

        return chooseComputerMove(i_EatingMovesForComputer, i_LegalMovesForComputer);
    }

    private static Move chooseComputerMove(List<Move> i_EatingMovesComputer, List<Move> i_LegalMovesComputer)
    {
        Move chosenMove = null;

        if (i_EatingMovesComputer.Count != 0)
        {
            int randomIndex = GetRandomNumber(i_EatingMovesComputer.Count);
            chosenMove = i_EatingMovesComputer[randomIndex];
        }
        else if (i_LegalMovesComputer.Count != 0)
        {
            int randomIndex = GetRandomNumber(i_LegalMovesComputer.Count);
            chosenMove = i_LegalMovesComputer[randomIndex];
        }

        return chosenMove;
    }

    internal static int GetRandomNumber(int i_MaxIndex)
    {
        return s_Random.Next(0, i_MaxIndex);
    }

    public static void CalcScore(Player[] i_Players, char i_QuitedLostPlayerSymbol)
    {
        bool isDraw = false;

        if(i_QuitedLostPlayerSymbol == '?')
        {
            isDraw = true;
        }

        if (isDraw == false)
        {
            int firstPlayerScore = 0, secondPlayerScore = 0, firstPlayerdifferenceScore = 0, secondPlayerdifferenceScore = 0;
            Piece[,] finalMatrix = Board.GetPieceMatrix();
            for (int row = 0; row < Board.GetSize(); row++)
            {
                for (int col = 0; col < Board.GetSize(); col++)
                {
                    if (finalMatrix[row, col].Symbol == i_Players[0].Symbol)
                    {
                        firstPlayerScore++;
                    }

                    if (finalMatrix[row, col].Symbol == 'K')
                    {
                        secondPlayerScore += 4;
                    }

                    if (finalMatrix[row, col].Symbol == i_Players[1].Symbol)
                    {
                        secondPlayerScore++;
                    }

                    if (finalMatrix[row, col].Symbol == 'U')
                    {
                        firstPlayerScore += 4;
                    }
                }
            }

            if (i_QuitedLostPlayerSymbol == 'O')
            {
                firstPlayerdifferenceScore = Math.Abs(firstPlayerScore - secondPlayerScore);
                i_Players[0].UpdateScore(firstPlayerdifferenceScore);
                
            }
            else
            {
                secondPlayerdifferenceScore = Math.Abs(firstPlayerScore - secondPlayerScore);
                i_Players[1].UpdateScore(secondPlayerdifferenceScore);
            }
        }
    }

    public bool CheckGameOver()
    {
        bool gameIsOver = false;
        Player[] players = GetPlayers();
        Player currentPlayer = CurrentPlayer;
        Player opponentPlayer = players[0] == currentPlayer ? players[1] : players[0];
        bool currentPlayerHasMoves = Board.CheckIfThereAreMovesLeft(currentPlayer);
        bool opponentPlayerHasMoves = Board.CheckIfThereAreMovesLeft(opponentPlayer);

        if (!currentPlayerHasMoves)
        {
            GameOverEvent?.Invoke($"{opponentPlayer.Name} Wins!");
            gameIsOver = true;

        }
        else if (!opponentPlayerHasMoves)
        {
            GameOverEvent?.Invoke($"{currentPlayer.Name} Wins!");
            gameIsOver = true;
        }

        return gameIsOver;
    }
}
