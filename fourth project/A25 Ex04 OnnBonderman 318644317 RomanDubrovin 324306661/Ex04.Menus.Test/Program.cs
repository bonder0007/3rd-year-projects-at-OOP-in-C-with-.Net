using System;
// $G$ SFN-002 (-3) Selecting an action item should clear the screen.

namespace Ex04.Menus.Test
{
    public class Program 
    {
        // $G$ DSN-003 (-3) The Main method should only invoke the main object of the program.
        public static void Main()
        {
            FunctionsForMenus functionsForMenus = new FunctionsForMenus();
            functionsForMenus.PrintHeaderForImplemationsOptions("First", "Interfaces");
            InterfaceManager interfaceManager = new InterfaceManager();
            interfaceManager.StartInterfaceMenu();
            functionsForMenus.PrintHeaderForImplemationsOptions("Second", "Delegates");
            EventsManager eventsManager = new EventsManager();
            eventsManager.StartEventMenu();
        }
    }
}
