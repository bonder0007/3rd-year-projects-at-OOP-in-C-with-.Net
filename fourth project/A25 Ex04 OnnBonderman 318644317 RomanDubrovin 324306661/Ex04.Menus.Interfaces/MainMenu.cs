namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        private SubMenuItem m_SubMenuItem = null;

        public void ShowMenu()
        {
            m_SubMenuItem.ExecuteMenuItem();
        }

        public void AddMenu(SubMenuItem i_SubMenuItem)
        {
            m_SubMenuItem = i_SubMenuItem;
        }
    }
}
