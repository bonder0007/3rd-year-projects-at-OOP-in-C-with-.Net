using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersGame
{
    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should start with Form.
    public class GameWindow : Form
    {
        private Label m_LabelPlayer1Name;
        private Label m_LabelPlayer2Name;
        private Label m_LabelCurrentTurn;
        private Panel m_BoardGame;
        private Position m_SelectedPosition = null;
        private static Player[] m_Players;
        private GameManager m_GameManager = new GameManager();
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly int r_BoardSize;
        private int m_TileSize = 50;
        private readonly bool r_IsNotVsComputer;

        // $G$ DSN-999 (-5) Too much logic in the Constructor. 
        public GameWindow(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsNotVsComputer)
        {
            this.Text = "Damka";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; 
            this.MinimizeBox = false;
            this.r_IsNotVsComputer = i_IsNotVsComputer;
            this.r_Player1Name = i_Player1Name;
            this.r_Player2Name = i_Player2Name;
            this.r_BoardSize = i_BoardSize;
            Player player1 = new Player(i_Player1Name, 'O');
            Player player2 = new Player(i_Player2Name, 'X');
            Player[] players = { player1, player2 };
            m_Players = players;
            int windowWidth = r_BoardSize * m_TileSize + 100;
            int windowHeight = r_BoardSize * m_TileSize + 150;
            this.ClientSize = new Size(windowWidth, windowHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
            initializeGameWindow();
            buildGameBoard();                                  
            m_GameManager.StartGameLogical(i_BoardSize, players, !i_IsNotVsComputer);
            m_GameManager.GameOverEvent += showGameOverMessage;
        }

        public Player[] Players
        {
            get { return m_Players; }
        }

        private void initializeGameWindow()
        {
            TableLayoutPanel topPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.LightGray
            };

            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f)); 
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            this.Controls.Add(topPanel);

            m_LabelPlayer1Name = new Label
            {
                Text = $"{r_Player1Name}: {m_Players[0].Score}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            topPanel.Controls.Add(m_LabelPlayer1Name, 0, 0); 

            m_LabelPlayer2Name = new Label
            {
                Text = $"{r_Player2Name}: {m_Players[1].Score}",
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            topPanel.Controls.Add(m_LabelPlayer2Name, 1, 0); 

            m_LabelCurrentTurn = new Label
            {
                Text = $"{r_Player1Name}'s Turn",
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            topPanel.Controls.Add(m_LabelCurrentTurn, 2, 0); 

            m_BoardGame = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Beige
            };

            this.Controls.Add(m_BoardGame);
        }

        private void highlightTile(Button i_Tile, bool i_Highlight)
        {
            if (i_Highlight)
            {
                i_Tile.FlatAppearance.BorderColor = Color.Navy;
                i_Tile.FlatAppearance.BorderSize = 3;
            }
            else
            {
                i_Tile.FlatAppearance.BorderColor = Color.Empty;
                i_Tile.FlatAppearance.BorderSize = 1;
            }
        }

        private void updateGameBoard()
        {
            foreach (Control control in m_BoardGame.Controls)
            {
                if (control is Button tile)
                {
                    int row = tile.Location.Y / tile.Height;
                    int col = tile.Location.X / tile.Width;
                    Piece piece = m_GameManager.Board.GetPieceAt(new Position(row, col));

                    if (piece != null && piece.Symbol != ' ')
                    {
                        switch (piece.Symbol)
                        {
                            case 'O': 
                                tile.BackgroundImage = Image.FromFile(@"Images\BlackPawn.png");
                                break;

                            case 'X': 
                                tile.BackgroundImage = Image.FromFile(@"Images\WhitePawn.png");
                                break;

                            case 'K': 
                                tile.BackgroundImage = Image.FromFile(@"Images\WhiteKing.png");
                                break;

                            case 'U': 
                                tile.BackgroundImage = Image.FromFile(@"Images\BlackQueen.jpg");
                                break;

                            default:
                                tile.BackgroundImage = null; 
                                break;
                        }

                        tile.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        tile.BackgroundImage = null; 
                    }

                    tile.FlatAppearance.BorderColor = Color.Black;
                    tile.FlatAppearance.BorderSize = 1;
                }
            }
        }

        private void buildGameBoard()
        {
            int tileSize = Math.Min(m_BoardGame.ClientSize.Width / r_BoardSize, (m_BoardGame.ClientSize.Height - 50) / r_BoardSize);
            m_BoardGame.Controls.Clear(); 

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    Button buttonPiece = new Button
                    {
                        Size = new Size(tileSize, tileSize), 
                        Location = new Point(col * tileSize, row * tileSize + 50),
                        BackColor = (row + col) % 2 == 0 ? Color.Black : Color.White, 
                        FlatStyle = FlatStyle.Flat,
                        Margin = Padding.Empty,
                        Padding = Padding.Empty,
                        Enabled = (row + col) % 2 != 0 
                    };

                    if (buttonPiece.BackColor == Color.White)
                    {
                        if (row < (r_BoardSize / 2) - 1)
                        {
                            buttonPiece.BackgroundImage = Image.FromFile(@"Images\BlackPawn.png");
                            buttonPiece.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (row > r_BoardSize / 2)
                        {
                            buttonPiece.BackgroundImage = Image.FromFile(@"Images\WhitePawn.png");
                            buttonPiece.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }

                    m_BoardGame.Controls.Add(buttonPiece);
                }
            }

            foreach (Control control in m_BoardGame.Controls)
            {
                if (control is Button tile)
                {
                    tile.Click += new EventHandler(tile_Click);
                }
            }
        }

        private void showAllPossibleEatingMoves()
        {
            List<Move> eatingMoves = m_GameManager.Board.GetAllEatingMoves(m_GameManager.CurrentPlayer);

            if (eatingMoves.Count > 0)
            {
                string currentPlayerName = m_GameManager.CurrentPlayer.Symbol == 'X' ? r_Player2Name : r_Player1Name;
                string movesText = string.Join(Environment.NewLine, eatingMoves.Select(move => $"{move.FromPosition} -> {move.ToPosition}"));

                MessageBox.Show($"{currentPlayerName}, You must choose one of the following eating moves," +
                                $" start count the row&&column with '0' to find the tile that you need to play with:" +
                                $"{Environment.NewLine}{movesText}",
                                "Mandatory Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // $G$ DSN-003 (-3) This method is too long. Should be split into several methods.
        // $G$ CSS-028 (-5) A method should not include more than one return statement.
        private void tile_Click(object sender, EventArgs e)
        {
            Move move = null;
            Button clickedTile = sender as Button;
            int row = clickedTile.Location.Y / clickedTile.Height;
            int col = clickedTile.Location.X / clickedTile.Width;
            Position clickedPosition = new Position(row, col);
            Player[] currentPlayerList = Players;
            Player currentPlayer = currentPlayerList[m_GameManager.CurrentPlayerIndex];

            if (m_SelectedPosition == null) 
            {
                m_SelectedPosition = clickedPosition;
                highlightTile(clickedTile, true);

                return;
            }

            if (m_SelectedPosition.Equals(clickedPosition)) 
            {
                highlightTile(clickedTile, false);
                m_SelectedPosition = null;

                return;
            }

            try
            {
                move = new Move(m_SelectedPosition, clickedPosition);
                move = move.ProcessMove(m_SelectedPosition, clickedPosition, m_GameManager.CurrentPlayer, m_GameManager.Board, move.LastEatPosition);
                Board.MovePiece(move.FromPosition, move.ToPosition, currentPlayer);
                updateGameBoard();
                if (move.LastEatPosition != null)
                {
                    List<Move> eatingMoves = Board.CheckDirectionsForEating(move.LastEatPosition,
                                                                            m_GameManager.CurrentPlayer.Symbol,
                                                                            m_GameManager.CurrentPlayer.KingSymbol);
                    if (eatingMoves.Count > 0)
                    {
                        MessageBox.Show($"You have another move to play from ({move.LastEatPosition.Row},{move.LastEatPosition.Column})",
                                        "Mandatory Eating Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_SelectedPosition = move.LastEatPosition;

                        return;
                    }
                }

                if (m_GameManager.CheckGameOver())
                {
                    return; 
                }

                move.LastEatPosition = null; 
                m_GameManager.SwitchToNextPlayer();
                updateCurrentTurnLabel();
                showAllPossibleEatingMoves();
                m_SelectedPosition = null; 

                if (m_GameManager.CurrentPlayerIndex == 1 && !r_IsNotVsComputer)
                {
                    performComputerMove();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_SelectedPosition = null;  
                updateGameBoard();  
            }
        }

        private void performComputerMove()
        {
            Move computerMove = GameManager.StartComputerMove();

            if (computerMove != null)
            {
                computerMove = computerMove.ProcessMove(computerMove.FromPosition, computerMove.ToPosition,
                                                        m_GameManager.CurrentPlayer, m_GameManager.Board, computerMove.LastEatPosition);

                Board.MovePiece(computerMove.FromPosition, computerMove.ToPosition, m_GameManager.CurrentPlayer);
                updateGameBoard();
                if (computerMove.LastEatPosition != null)
                {
                    List<Move> eatingMoves = Board.CheckDirectionsForEating(computerMove.LastEatPosition,
                                                                            m_GameManager.CurrentPlayer.Symbol,
                                                                            m_GameManager.CurrentPlayer.KingSymbol);
                    if (eatingMoves.Count > 0)
                    {
                        performComputerMove();

                        return;
                    }            
                }

                if (m_GameManager.CheckGameOver())
                {
                    return;
                }

                computerMove.LastEatPosition = null;
                m_GameManager.SwitchToNextPlayer();
                updateCurrentTurnLabel();
                showAllPossibleEatingMoves();
            }
        }

        private void updateCurrentTurnLabel()
        {
            string currentPlayerName = m_GameManager.CurrentPlayer.Symbol == 'X' ? r_Player2Name : r_Player1Name;
            m_LabelCurrentTurn.Text = $"{currentPlayerName}'s Turn";
        }

        // $G$ NTT-999 (-10) You should use Environment.NewLine instead of ' \n '.
        private void showGameOverMessage(string message)
        {
            DialogResult result = MessageBox.Show(
            $"{message}\n\nDo you want to play again?",
            "Damka",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                Application.Exit();
            }
            else
            {
                resetGame();
            }
        }

        private void resetGame()
        {
            updateScoreLabel();
            m_GameManager = new GameManager(); 
            m_GameManager.StartGameLogical(r_BoardSize, m_Players, !r_IsNotVsComputer); 
            m_SelectedPosition = null; 
            m_GameManager.CurrentPlayerIndex = 0;
            updateGameBoard(); 
            updateCurrentTurnLabel();
            m_GameManager.GameOverEvent += showGameOverMessage;
        }

        private void updateScoreLabel()
        {
            char playerWinnerSymbol = (m_GameManager.CurrentPlayerIndex == 0) ? 'O' : 'X';
            GameManager.CalcScore(m_Players, playerWinnerSymbol);
            m_LabelPlayer2Name.Text = $"{r_Player2Name}: {m_Players[1].Score}";
            m_LabelPlayer1Name.Text = $"{r_Player1Name}: {m_Players[0].Score}";
        }
    }
}


