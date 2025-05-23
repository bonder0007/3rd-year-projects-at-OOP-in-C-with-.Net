namespace ConsoleUI
{
    // $G$ SFN-006 (-3) The maximum air pressure for car and motorcycle wheels does not match the assignment requirements(car should be 34 and motorcycle 32).
    // $G$ SFN-009 (-3) Not all vehicle details are calculated and printed. What about energy percentage? 
    // $G$ RUL-005 (-20) Wrong zip folder structure, the zip file should contain a single folder. 

    public class Program
    {
        public static void Main()
        {
            ConsoleUIManager userInterface = new ConsoleUIManager();
            userInterface.RunUI();
        }
    }
}
