using System;
using System.Text;

namespace Ex04.Menus.Test
{
    // $G$ DSN-999 (-5) Bad encapsulation. These methods should have been internal.
    public class FunctionsForMenus 
    {
        public void ShowVersion()
        {
            Console.WriteLine();
            string appVersion = "App Version: 25.1.4.5480";
            Console.WriteLine("Version number is: {0}", appVersion);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine();
        }

        public void CountLowerCaseLetters()
        {
            Console.WriteLine();
            int countLowerCaseLetters = 0;
            string userInputSentence;
            Console.WriteLine("Please enter a sentence and I will count lowercase letters in it");
            userInputSentence = Console.ReadLine();
            while(string.IsNullOrEmpty(userInputSentence))
            {
                Console.Clear();
                Console.WriteLine("Invalid input, please enter only letters");
                userInputSentence = Console.ReadLine();
            }

            foreach(char currentLetter in userInputSentence)
            {
                if(char.IsLower(currentLetter))
                {
                    countLowerCaseLetters++;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("The sentence you entered is | {0} | and it contains {1} lowercase letters",
                                        userInputSentence, countLowerCaseLetters);
            Console.WriteLine(stringBuilder);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void PrintCurrentTime()
        {
            Console.WriteLine();
            DateTime now = DateTime.Now;
            Console.WriteLine($"Current Time: {now:HH:mm:ss}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine();
        }

        public void PrintCurrentDate()
        {
            Console.WriteLine();
            DateTime now = DateTime.Now;
            Console.WriteLine($"Current Date: {now:dd/MM/yyyy}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine();
        }

        public void PrintHeaderForImplemationsOptions(string i_FirstOrSecond, string i_OptionType)
        {
            Console.WriteLine("{0} implemantion - {1}", i_FirstOrSecond, i_OptionType);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
