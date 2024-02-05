using System;

namespace Codecool.EinsenhowerMatrix
{
    /// <summary>
    /// Main class for program
    /// </summary>
    public class EisenhowerMain
    {
        /// <summary>
        /// Runs program with basic user UI
        /// </summary>
        public void Run()
        {
            TodoMatrix matrix = new TodoMatrix();
            matrix.AddItemsFromDb();
            while (true)
            {
                Console.Clear(); 

                Console.WriteLine("1. Show matrix");
                Console.WriteLine("2. Option 2");
                Console.WriteLine("3. Option 3");
                Console.WriteLine("4. Exit");

                Console.Write("Choose option (1-4): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Option 1. Press enter to continue...");
                        Console.WriteLine(matrix.ToString()); 
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("Option 2. Press enter to continue...");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("Option 3. Press enter to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.WriteLine("Exiting the program...");
                        return;

                    default:
                        Console.WriteLine("Incorrect choice. Press enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
