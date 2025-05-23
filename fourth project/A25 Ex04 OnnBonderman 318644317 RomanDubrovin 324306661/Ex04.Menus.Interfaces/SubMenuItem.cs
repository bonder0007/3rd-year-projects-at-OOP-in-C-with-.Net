using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class SubMenuItem : MenuItem
    {
        private const int k_firstMenuLevel = 1;
        private readonly int r_CurrentMenuLevel;
        private readonly string r_BackOptionMenu;
        private readonly string r_ErrorMessage;
        private readonly List<MenuItem> r_MenuItemsList = new List<MenuItem>();
       
        public int CurrentLevel { get; }

        public SubMenuItem(string i_Title, int i_CurrentLevel, string i_BackOption, 
                           string i_ErrorString) : base(i_Title)
        {
            this.r_BackOptionMenu = i_BackOption;
            this.r_ErrorMessage = i_ErrorString;
            this.r_CurrentMenuLevel = i_CurrentLevel;
            if (i_CurrentLevel == 0)
            {
                this.r_CurrentMenuLevel = k_firstMenuLevel;
            }
            else
            {
                this.r_CurrentMenuLevel = i_CurrentLevel;
            }
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            r_MenuItemsList.Add(i_MenuItem);
        }

        public override void ExecuteMenuItem()
        {
            bool runningMenu;
            do
            {
                Console.Clear();
                printMenuTitle();
                printMenuOptions();
                Console.Write("Please enter your choice(1 - 2 or 0 to exit):");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= r_MenuItemsList.Count)
                {
                    if (choice == 0)
                    {
                        runningMenu = false;
                        Console.Clear();
                    }
                    else
                    {
                        runningMenu = true;
                        r_MenuItemsList[choice - 1].ExecuteMenuItem();
                    }
                }
                else
                {
                    runningMenu = true;
                    Console.WriteLine("Invalid input");
                    Console.Clear();
                    Console.WriteLine($"{r_ErrorMessage} Press any key to continue...");
                    Console.ReadKey();
                }
            }
            while (runningMenu);
        }

        private void printMenuTitle()
        {
            Console.WriteLine(string.Format("** {0} **", Title));
            Console.WriteLine("-----------------------------");
        }

        private void printMenuOptions()
        {
            for (int index = 0; index < r_MenuItemsList.Count; index++)
            {
                Console.WriteLine(string.Format("{0}. {1}", index + 1, r_MenuItemsList[index].Title));
            }

            Console.WriteLine(string.Format("0. {0}", r_BackOptionMenu));
        }
    }
}
    

