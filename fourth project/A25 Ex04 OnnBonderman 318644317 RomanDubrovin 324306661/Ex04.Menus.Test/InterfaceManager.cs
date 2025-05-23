using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class InterfaceManager 
    {
        private MainMenu m_InterfaceMainMenu = new MainMenu();

        public void StartInterfaceMenu()
        {
            FunctionsForMenus functionsForMenus = new FunctionsForMenus();
            SubMenuItem startMenu = new SubMenuItem("Delegates Main Menu", 0, "Exit", "Wrong input, try again");
            SubMenuItem versionAndLowerCaseLettersMenu = new SubMenuItem("Letters and Version", startMenu.CurrentLevel + 1,
                                                                     "Back", "Wrong input, try again");
            SubMenuItem showDateAndTimeMenu = new SubMenuItem("Show Current Date/Time", startMenu.CurrentLevel + 1,
                                                                     "Back", "Wrong input, try again");
            startMenu.AddMenuItem(versionAndLowerCaseLettersMenu);
            startMenu.AddMenuItem(showDateAndTimeMenu);
            versionAndLowerCaseLettersMenu.AddMenuItem(new ExecutableItem("Show app version", new ExecuteShowVersion(functionsForMenus)));
            versionAndLowerCaseLettersMenu.AddMenuItem(new ExecutableItem("Count Lowercases Letters", new CountLowerCaseLetters(functionsForMenus)));
            showDateAndTimeMenu.AddMenuItem(new ExecutableItem("Show Current Time", new AnalyzeCurrentTime(functionsForMenus)));
            showDateAndTimeMenu.AddMenuItem(new ExecutableItem("Show Current Date", new AnalyzeCurrentDate(functionsForMenus)));
            m_InterfaceMainMenu.AddMenu(startMenu);
            try
            {
                m_InterfaceMainMenu.ShowMenu();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
