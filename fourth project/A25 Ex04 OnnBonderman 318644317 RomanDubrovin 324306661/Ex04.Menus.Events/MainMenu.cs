namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private SubMenuItem m_SubMenu = null;

        public MainMenu()
        {

        }


        public void Show()
        {
            m_SubMenu.ExecuteMenuItem();
        }

        public void Add(SubMenuItem i_MenuItem)
        {
            m_SubMenu = i_MenuItem;
        }
    }
}