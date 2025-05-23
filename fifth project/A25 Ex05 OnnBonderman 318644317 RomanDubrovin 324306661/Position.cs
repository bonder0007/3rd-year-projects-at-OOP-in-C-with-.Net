namespace CheckersGame
{
    public class Position
    {
        private int m_Row;
        private int m_Column;

        public Position(int i_Row, int i_Column)
        {
            this.m_Row = i_Row;
            this.m_Column = i_Column;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public bool Equals(Position i_OtherPosition)
        {
            return m_Row == i_OtherPosition.Row && m_Column == i_OtherPosition.Column;
        }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }
    }
}
