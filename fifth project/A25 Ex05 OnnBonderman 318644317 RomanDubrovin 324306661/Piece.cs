
namespace CheckersGame
{
    public class Piece
    {
        private char m_Symbol;

        public Piece(char i_Symbol)
        {
            this.m_Symbol = i_Symbol;
        }

        public char Symbol
        {
            get { return this.m_Symbol; }
            set { this.m_Symbol = value; }
        }
    }
}
