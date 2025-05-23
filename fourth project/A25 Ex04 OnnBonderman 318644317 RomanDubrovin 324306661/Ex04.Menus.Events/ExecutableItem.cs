namespace Ex04.Menus.Events
{
    // $G$ CSS-021 (-3) Delegate name should end with the extension of "Delegate".
    public delegate void SubMenuSelected();

    public class ExecutableItem : MenuItem
    {
        public event SubMenuSelected SubMenuSelected;

        public ExecutableItem(string i_Title):base(i_Title)
        {

        }

        public override void ExecuteMenuItem()
        {
            ItemChosen();
        }

        protected virtual void ItemChosen()
        {
            SubMenuSelected?.Invoke();
        }
    }
}

