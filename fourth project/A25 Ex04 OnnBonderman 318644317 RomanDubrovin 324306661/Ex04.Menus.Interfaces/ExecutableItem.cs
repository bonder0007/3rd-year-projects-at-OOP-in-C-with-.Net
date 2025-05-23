namespace Ex04.Menus.Interfaces
{
    public class ExecutableItem : MenuItem
    {
        private readonly IMenuExecute r_MenuExecute;

        public ExecutableItem(string i_Title, IMenuExecute i_CurrentMenuExecute) : base(i_Title)
        {
            this.r_MenuExecute = i_CurrentMenuExecute;
        }

        public override void ExecuteMenuItem()
        {
            r_MenuExecute.Execute();
        }
    }
}
