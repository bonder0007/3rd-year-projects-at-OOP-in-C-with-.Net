using System;
using System.Linq;
using System.Drawing;      
using System.Windows.Forms;

namespace CheckersGame
{
    public partial class SettingsForm : Form
    {
        private Label m_BoardSizeLable;
        private RadioButton m_RadioButtonSixOnSix;
        private RadioButton m_RadioButtonEightOnEight;
        private RadioButton m_RadioButtonTenOnTen;
        private Label m_PlayersLabel;
        private Label m_FirstPlayerLabel;
        private Label m_SecondPlayerLabel;
        private TextBox m_TextBoxForFirstPlayer;
        private CheckBox m_IsNotVsComputer;
        private TextBox m_TextBoxForSecondPlayer;
        private Button m_ButtonDone;

        public string Player1Name { get; private set; }

        public string Player2Name { get; private set; }

        public int BoardSize { get; private set; }

        public bool IsNotVsComputer { get; private set; }

        public SettingsForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            initializeSettingWindow();
        }

        private void initializeSettingWindow()
        {
            this.Text = "Game Settings";
            this.Size = new System.Drawing.Size(250, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            m_BoardSizeLable = new Label();
            m_BoardSizeLable.Text = "Board Size:";
            m_BoardSizeLable.Location = new Point(20, 20);
            this.Controls.Add(m_BoardSizeLable);
            this.BackgroundImage = Image.FromFile(@"Images\SettingsWindowBackGround.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            m_RadioButtonSixOnSix = new RadioButton()
            {
                Text = "6X6",
                AutoSize = true,
                Checked = true,
                Location = new Point(m_BoardSizeLable.Top + 10, m_BoardSizeLable.Top + m_BoardSizeLable.Height),
            };

            this.Controls.Add(m_RadioButtonSixOnSix);

            m_RadioButtonEightOnEight = new RadioButton()
            {
                Text = "8X8",
                Checked = false,
                AutoSize = true,
                Location = new Point(m_BoardSizeLable.Top + 80,
                                     m_BoardSizeLable.Top + m_BoardSizeLable.Height),
            };

            this.Controls.Add(m_RadioButtonEightOnEight);

            m_RadioButtonTenOnTen = new RadioButton()
            {
                Text = "10X10",
                Checked = false,
                AutoSize = true,
                Location = new Point(m_BoardSizeLable.Top + 150,
                                    m_BoardSizeLable.Top + m_BoardSizeLable.Height),
            };

            this.Controls.Add(m_RadioButtonTenOnTen);

            m_PlayersLabel = new Label();
            {
                m_PlayersLabel.Text = "Players:";
                m_PlayersLabel.AutoSize = true;
                m_PlayersLabel.Location = new Point(20, 70);
            };

            this.Controls.Add(m_PlayersLabel);

            m_FirstPlayerLabel = new Label();
            {
                m_FirstPlayerLabel.Text = "Player 1:";
                m_FirstPlayerLabel.AutoSize = true;
                m_FirstPlayerLabel.Location = new Point(m_BoardSizeLable.Top + 10, m_PlayersLabel.Top + m_PlayersLabel.Height + 2);
            };

            this.Controls.Add(m_FirstPlayerLabel);

            m_TextBoxForFirstPlayer = new TextBox()
            {
                Width = 110,
                AutoSize = true,
                Location = new Point(m_BoardSizeLable.Top + 90, m_PlayersLabel.Top + m_PlayersLabel.Height - 2),
            };

            this.Controls.Add(m_TextBoxForFirstPlayer);

            m_IsNotVsComputer = new CheckBox()
            {
                Checked = false,
                AutoSize = true,
                Width = 100,
                Height = 100,
                Location = new Point(m_BoardSizeLable.Top + 10, m_PlayersLabel.Top + m_PlayersLabel.Height + 30),
            };

            this.Controls.Add(m_IsNotVsComputer);

            m_SecondPlayerLabel = new Label();
            {
                m_SecondPlayerLabel.Text = "Player 2:";
                m_SecondPlayerLabel.AutoSize = true;
                m_SecondPlayerLabel.Location = new Point(m_BoardSizeLable.Top + 30, m_PlayersLabel.Top + m_PlayersLabel.Height + 30);
            };

            this.Controls.Add(m_SecondPlayerLabel);

            m_TextBoxForSecondPlayer = new TextBox()
            {
                Text = "[Computer]",
                Width = 110,
                Enabled = false,
                AutoSize = true,
                Location = new Point(m_BoardSizeLable.Top + 90, m_PlayersLabel.Top + m_PlayersLabel.Height + 30),
            };

            this.Controls.Add(m_TextBoxForSecondPlayer);

            m_ButtonDone = new Button()
            {
                Text = "Done",
                AutoSize = true,
                Location = new Point (m_BoardSizeLable.Top + 120, m_PlayersLabel.Top + m_PlayersLabel.Height + 70),
            };

            this.Controls.Add(m_ButtonDone);
            m_ButtonDone.Click += onDoneButtonClick;
            m_IsNotVsComputer.CheckedChanged += isVsComputerChecked;
            m_TextBoxForFirstPlayer.TextChanged += onPlayerNameTextChanged;
            m_TextBoxForSecondPlayer.TextChanged += onPlayerNameTextChanged;
        }

        private int getSelectedBoardSize()
        {
            int boardSize = 6; 

            if (m_RadioButtonEightOnEight.Checked)
            {
                boardSize = 8;
            }
            else if (m_RadioButtonTenOnTen.Checked)
            {
                boardSize = 10;
            }

            return boardSize;
        }

        private void saveSettings()
        {
            Player1Name = m_TextBoxForFirstPlayer.Text;
            Player2Name = m_IsNotVsComputer.Checked ?  m_TextBoxForSecondPlayer.Text  : "Computer";
            BoardSize = getSelectedBoardSize();
            IsNotVsComputer = m_IsNotVsComputer.Checked;

            MessageBox.Show("Settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void isVsComputerChecked(object sender, EventArgs e)
        {
            if (m_IsNotVsComputer.Checked)
            {
                m_TextBoxForSecondPlayer.Text = string.Empty;
                m_TextBoxForSecondPlayer.Enabled = true;
                m_TextBoxForSecondPlayer.ForeColor = Color.Black;
            }
            else
            {
                m_TextBoxForSecondPlayer.Text = "[Computer]";
                m_TextBoxForSecondPlayer.Enabled = false;
                m_TextBoxForSecondPlayer.ForeColor = Color.Gray;
            }
        }

        private void onDoneButtonClick(object sender, EventArgs e)
        {
            if (validationPlayersName())
            {
                saveSettings();
                Player[] playersList = new Player[2];
                playersList[0] = new Player(Player1Name, 'O'); 
                playersList[1] = new Player(Player2Name, 'X');
                this.Hide();
                GameWindow gameWindow = new GameWindow(Player1Name, Player2Name, BoardSize, IsNotVsComputer);
                gameWindow.ShowDialog();              
                this.Close();
            }
        }

        // $G$ DSN-004 (-5) Redundant code duplication.
        private bool validationPlayersName()
        {
            bool isValid = true;
            string errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(m_TextBoxForFirstPlayer.Text))
            {
                errorMessage = "Player 1 name cannot be empty";
            }
            else if (m_TextBoxForFirstPlayer.Text.Contains(" "))
            {
                errorMessage = "Player 1 name cannot contain spaces";
            }
            else if (m_TextBoxForFirstPlayer.Text.Length > 20)
            {
                errorMessage = "Player 1 name can be under 20 characters only";
            }
            else if (!onlyLetters(m_TextBoxForFirstPlayer.Text))
            {
                errorMessage = "Player 1 name must contain only letters";
            }

            if (string.IsNullOrEmpty(errorMessage) && m_IsNotVsComputer.Checked)
            {
                if (string.IsNullOrWhiteSpace(m_TextBoxForSecondPlayer.Text))
                {
                    errorMessage = "Player 2 name cannot be empty";
                }
                else if (m_TextBoxForSecondPlayer.Text.Contains(" "))
                {
                    errorMessage = "Player 2 name cannot contain spaces";
                }
                else if (m_TextBoxForSecondPlayer.Text.Length > 20)
                {
                    errorMessage = "Player 2 name can be under 20 characters only";
                }
                else if (!onlyLetters(m_TextBoxForSecondPlayer.Text))
                {
                    errorMessage = "Player 2 name must contain only letters";
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                isValid = false;

                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }

        private bool onlyLetters(string i_Text)
        {
            return i_Text.All(char.IsLetter);
        }

        private void onPlayerNameTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null && textBox.Text.Length > 20)
            {
                MessageBox.Show("Player name can be under 20 characters.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Text = textBox.Text.Substring(0, 20);
                textBox.SelectionStart = textBox.Text.Length; 
            }
        }
    }
}


