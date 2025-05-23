
namespace CheckersGame
{
    public class Player
    {
        private readonly string r_Name;
        private char m_Symbol;
        private int m_Score;
       
        public Player(string i_Name, char i_Symbol)
        {
            this.r_Name = i_Name;
            this.m_Symbol = i_Symbol;
            this.m_Score = 0; 
        }

        public char KingSymbol
        {
            get { return m_Symbol == 'O' ? 'U' : 'K'; }
        }

        public string Name
        {
            get { return this.r_Name; }
        }

        public char Symbol
        {
            get { return this.m_Symbol; }
            set { this.m_Symbol = value; }
        }

        public int Score
        {
            get { return this.m_Score; }
            set { this.m_Score = value; }
        }

        public void UpdateScore(int i_ScoreToAdd)
        {
            m_Score += i_ScoreToAdd;
        }
    }
}
