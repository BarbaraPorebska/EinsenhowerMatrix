using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Codecool.EinsenhowerMatrix
{
    /// <summary>
    /// Entry point for app
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry method for app
        /// </summary>
        /// <param name="args">command line args</param>
        public static void Main(string[] args)
        {
            //new EisenhowerMain().Run();

            //var quarter = new TodoQuarter();
            //quarter.AddItem("Test1", DateTime.Parse("2024-11-16"), true);
            //quarter.AddItem("Test2", DateTime.Parse("2024-01-27"));
            //quarter.AddItem("Test3", DateTime.Parse("2024-01-29"));

            //TodoItem item = quarter.GetItem(4);

            //Console.WriteLine(quarter.ToString());

            //quarter.Items[1].Mark();

            //Console.WriteLine(quarter.ToString());

            //TodoMatrix todomatrix = new TodoMatrix();
            //todomatrix.AddItem("Test1", DateTime.Parse("2024-01-22"));
            //todomatrix.AddItem("Test2", DateTime.Parse("2024-01-29"));
            //todomatrix.AddItem("Test3", DateTime.Parse("2024-01-28"), true);
            //todomatrix.AddItem("Test4", DateTime.Parse("2024-01-22"), true);
            //todomatrix.AddItem("Test2", DateTime.Parse("2024-01-27"));
            //todomatrix.AddItem("TestAFARAAIIIIII YES AI AI AI MASSIVE MASSIVE", DateTime.Parse("2024-01-27"));
            //todomatrix.AddItemsFromFile(@"D:\OneDrive\Pulpit\EGZAMIN\PRAKTYKA\basia_start\start\TasksSrc\tasks.csv");

            //todomatrix.SaveItemsToFile(@"D:\OneDrive\Pulpit\EGZAMIN\PRAKTYKA\basia_start\start\TasksSrc\tasksOut.csv");
            //todomatrix.AddItemsFromDb();
            //Console.WriteLine(todomatrix.ToString());
            //todomatrix.SaveItemsToDb();

            //Console.WriteLine(todomatrix.GetMatrix());
            //var linie = File.ReadAllLines(@"D:\OneDrive\Pulpit\EGZAMIN\PRAKTYKA\basia_start\start\TasksSrc\tasks.csv").ToList();
            //Console.WriteLine(linie);
            //Console.ReadLine();
            EisenhowerMain main = new EisenhowerMain();
            main.Run();


            //TO DO:
            //UI - "as a user..."
            //Code refactor - przerobić vary, ify na inline
            //nUnit - testy jednostkowe
            //Baza danych
            
            //+-:
            //kolorki
            //plansza

        }
    }
}
