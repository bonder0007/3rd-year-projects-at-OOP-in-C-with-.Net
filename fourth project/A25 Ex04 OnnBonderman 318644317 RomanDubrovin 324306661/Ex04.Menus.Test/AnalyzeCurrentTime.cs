using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class AnalyzeCurrentTime : IMenuExecute
    {
        private readonly FunctionsForMenus r_FunctionsForMenus;

        public AnalyzeCurrentTime(FunctionsForMenus i_FunctionsForMenus)
        {
            this.r_FunctionsForMenus = i_FunctionsForMenus;
        }

        public void Execute()
        {
            r_FunctionsForMenus.PrintCurrentTime();
        }
    }
}
