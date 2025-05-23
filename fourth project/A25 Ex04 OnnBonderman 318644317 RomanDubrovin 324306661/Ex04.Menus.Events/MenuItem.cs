namespace Ex04.Menus.Events
{
    public abstract class MenuItem
    {

        protected MenuItem(string i_TitleMenu)
        {
            Title = i_TitleMenu;
        }

        public string Title { get; }

        public abstract void ExecuteMenuItem();
    }
}
