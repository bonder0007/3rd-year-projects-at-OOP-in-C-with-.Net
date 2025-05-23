using System;
using Ex04.Menus.Events;

namespace Ex04.Menus.Test
{
    public class EventsManager 
    {
        private readonly MainMenu r_MainMenu = new MainMenu();
        private readonly FunctionsForMenus r_FunctionsForMenus = new FunctionsForMenus();

        public void StartEventMenu()
        {
            FunctionsForMenus functionsForMenus = new FunctionsForMenus();
            SubMenuItem startMenuEventsDelegates = new SubMenuItem("Delegates Main Menu", 0, "Exit", "Wrong input, try again");
            SubMenuItem versionAndLowerCaseLettersMenuDelegates = 
                                                 new SubMenuItem("Letters and Version", startMenuEventsDelegates.CurrentLevel + 1,
                                                                  "Back", "Wrong input, try again");
            SubMenuItem showDateAndTimeMenuDelegates = new SubMenuItem("Show Current Date/Time", startMenuEventsDelegates.CurrentLevel + 1,
                                                                      "Back", "Wrong input, try again");
            ExecutableItem showVersionApp = new ExecutableItem("Show Version");
            ExecutableItem countLoverCaseLetters = new ExecutableItem("Count Lowercase Letters");
            ExecutableItem showCurrentTime = new ExecutableItem("Show Current Time");
            ExecutableItem showCurrentDate = new ExecutableItem("Show Current Date");
            startMenuEventsDelegates.AddMenuItem(versionAndLowerCaseLettersMenuDelegates);
            startMenuEventsDelegates.AddMenuItem(showDateAndTimeMenuDelegates);
            showVersionApp.SubMenuSelected += subMenuSelected_executeVersionApp;
            countLoverCaseLetters.SubMenuSelected += showVersionApp_executeCounterLowerCaseLetters;
            showCurrentTime.SubMenuSelected += showCurrentTime_executeShowTime;
            showCurrentDate.SubMenuSelected += showCurrentDate_executeShowDate;
            versionAndLowerCaseLettersMenuDelegates.AddMenuItem(showVersionApp);
            versionAndLowerCaseLettersMenuDelegates.AddMenuItem(countLoverCaseLetters);
            showDateAndTimeMenuDelegates.AddMenuItem(showCurrentTime);
            showDateAndTimeMenuDelegates.AddMenuItem(showCurrentDate);
            r_MainMenu.Add(startMenuEventsDelegates);
            try
            {
                r_MainMenu.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void showCurrentDate_executeShowDate()
        {
            r_FunctionsForMenus.PrintCurrentDate();
        }

        private void showCurrentTime_executeShowTime()
        {
            r_FunctionsForMenus.PrintCurrentTime();
        }

        private void subMenuSelected_executeVersionApp()
        {
            r_FunctionsForMenus.ShowVersion();
        }

        private void showVersionApp_executeCounterLowerCaseLetters()
        {
            r_FunctionsForMenus.CountLowerCaseLetters();
        }
    }
}
