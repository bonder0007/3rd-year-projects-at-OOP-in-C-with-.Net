using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class AnalyzeCurrentDate : IMenuExecute
    {
        private readonly FunctionsForMenus r_FunctionsForMenus;

        public AnalyzeCurrentDate(FunctionsForMenus i_FunctionsForMenus)
        {
            this.r_FunctionsForMenus = i_FunctionsForMenus;
        }

        public void Execute()
        {
            r_FunctionsForMenus.PrintCurrentDate();
        }
    }
}
