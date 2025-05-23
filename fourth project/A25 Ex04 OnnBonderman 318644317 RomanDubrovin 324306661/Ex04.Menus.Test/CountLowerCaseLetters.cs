using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class CountLowerCaseLetters : IMenuExecute
    {
        private  readonly FunctionsForMenus r_FunctionsForMenus;

        public CountLowerCaseLetters(FunctionsForMenus i_FunctionsForMenus)
        {
            this.r_FunctionsForMenus = i_FunctionsForMenus;
        }

        public void Execute()
        {
            r_FunctionsForMenus.CountLowerCaseLetters();
        }
    }
}
