using Codecool.EinsenhowerMatrix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Codecool.EinsenhowerMatrix
{
    /// <summary>
    /// Top level class for Matrix
    /// </summary>
    public class TodoMatrix
    {
        /// <summary>
        /// Gets or sets dictionary with quarters
        /// </summary>
        public Dictionary<string, TodoQuarter> Quarters { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoMatrix"/> class.
        /// </summary>
        public TodoMatrix()
        {
            Quarters = new Dictionary<string, TodoQuarter>();
            CreateQuarters();
        }

        /// <summary>
        /// Returns quarters
        /// </summary>
        /// <returns>Quarters</returns>
        public Dictionary<string, TodoQuarter> GetQuarters()
        {
            return Quarters;
        }

        /// <summary>
        /// Returns quarter by specified status
        /// </summary>
        /// <param name="status">Quarter status</param>
        /// <returns>Quarter</returns>
        public TodoQuarter GetQuarter(string status) 
        { 
            return Quarters[status];
        }

        /// <summary>
        /// Creates new item based on given parameters
        /// </summary>
        /// <param name="title">title for new task</param>
        /// <param name="date">deadline for new task</param>
        /// <param name="isImportant">boolean value that indicates whenever task is important or not</param>
        public void AddItem(string title, DateTime date, bool isImportant = false)
        {
            if (date < DateTime.Now)
            {
                throw new ArgumentException("Deadline is in the past");
            }
            string key = (date - DateTime.Now).TotalHours <= 72 ? "NU" : "NN";
            if (isImportant)
            {
                key = (date - DateTime.Now).TotalHours <= 72 ? "IU" : "IN";
            }
            Quarters[key].AddItem(title, date, isImportant);
        }

        /// <summary>
        /// Deletes all items that are marked as done
        /// </summary>
        public void ArchiveItems()
        {
            foreach (KeyValuePair<string, TodoQuarter> quarterElement in Quarters)
            {
                quarterElement.Value.ArchiveItems();
            }
        }

        /// <summary>
        /// Reads the content from given file, creates and add item to given quarter
        /// </summary>
        /// <param name="filePath">string with path leading to source file</param>
        public void AddItemsFromFile(string filePath)
        {
            try
            {
                List<string> fileLines = File.ReadAllLines(filePath).ToList();
                foreach (string line in fileLines)
                {
                    string[] splitValues = line.Split('|');
                    if (splitValues.Length == 3)
                    {
                        AddItem(splitValues[0], ConvertToDateFrom(splitValues[1]), splitValues[2] == "true");
                    }
                    if (splitValues.Length == 2)
                    {
                        AddItem(splitValues[0], ConvertToDateFrom(splitValues[1]));
                    }
                }
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// Saves current matrix content to file
        /// </summary>
        /// <param name="filePath">file path under all task will be saved</param>
        public void SaveItemsToFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                { 
                    File.Delete(filePath); 
                }
                foreach (KeyValuePair<string, TodoQuarter> quarterElement in Quarters)
                {
                    foreach (TodoItem item in quarterElement.Value.Items)
                    {
                        File.AppendAllText(filePath, $"{ item.Title }|{ item.Deadline.ToString("d-M", CultureInfo.InvariantCulture) }|{ (item.IsImportant ? "true" : "") }{ Environment.NewLine }");
                    }
                }
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public void SaveItemsToDb()
        {
            ArchiveItems();
            using (EisMatrixContext context = new EisMatrixContext())
            {
                context.Database.ExecuteSql(FormattableStringFactory.Create("TRUNCATE TABLE todoitems"));
                foreach (KeyValuePair<string, TodoQuarter> quarterElement in Quarters)
                {
                    foreach (TodoItem item in quarterElement.Value.Items)
                    {
                        context.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddItemsFromDb()
        {
            using (EisMatrixContext context = new EisMatrixContext())
            {
                foreach (TodoItem item in context.Todoitems)
                {
                    AddItem(item.Title, item.Deadline, item.IsImportant);
                }
            }

        }

        /// <summary>
        /// Returns human readable representation for matrix
        /// </summary>
        /// <returns>string with all quarters and associated items</returns>
        public override string ToString()
        {
            //string matrix = "";
            //foreach (var quarter in Quarters)
            //{
            //    matrix += $"{quarter.Key}\n{quarter.Value.ToString()}\n\n";

            //}
            //return matrix;
            return GetMatrix();
        }

        private DateTime ConvertToDateFrom(string representation)
        {
            return DateTime.ParseExact(representation, "d-M", null);
        }

        private void CreateQuarters()
        {
            TodoQuarter iuquarter = new TodoQuarter();
            TodoQuarter inquarter = new TodoQuarter();
            TodoQuarter nuquarter = new TodoQuarter();
            TodoQuarter nnquarter = new TodoQuarter();
            Quarters.Add("IU", iuquarter);
            Quarters.Add("IN", inquarter);
            Quarters.Add("NU", nuquarter);
            Quarters.Add("NN", nnquarter);
        }

        private string GetMatrix()
        {
            const string splitter = "--|--------------------------------|--------------------------------|--";
            string important = "  IMPORTANT  ";
            string notimportant = "NOT IMPORTANT";

            List<string> iuFormattedList = GetFormattedToDoItems(Quarters["IU"].Items);
            List<string> inFormattedList = GetFormattedToDoItems(Quarters["IN"].Items);
            List<string> nuFormattedList = GetFormattedToDoItems(Quarters["NU"].Items);
            List<string> nnFormattedList = GetFormattedToDoItems(Quarters["NN"].Items);

            int importantctr = new int[] { iuFormattedList.Count, inFormattedList.Count, important.Length}.Max();
            int notimportantctr = new int[] { nuFormattedList.Count, nnFormattedList.Count, notimportant.Length }.Max();

            if (importantctr > important.Length)
            {
                double spaces = (Math.Round(importantctr - important.Length / 2d, MidpointRounding.AwayFromZero) * 2) / 2;
                important.PadLeft(important.Length + Convert.ToInt32(spaces));
                important.PadRight(important.Length + Convert.ToInt32(spaces));
            }

            if (notimportantctr > notimportant.Length)
            {
                double spaces = (Math.Round(notimportantctr - notimportant.Length / 2d, MidpointRounding.AwayFromZero) * 2) / 2;
                notimportant.PadLeft(notimportant.Length + Convert.ToInt32(spaces));
                notimportant.PadRight(notimportant.Length + Convert.ToInt32(spaces));
            }

            string matrix = "  |             URGENT             |           NOT URGENT           |  " + Environment.NewLine;
            matrix += splitter + Environment.NewLine;

            for (int i = 0; i < importantctr; i++)
            {
                string output;
                
                string iuelementwithspaces;
                string inelementwithspaces;

                iuelementwithspaces = i < iuFormattedList.Count ? iuFormattedList[i].PadRight(32) : "                                ";
                inelementwithspaces = i < inFormattedList.Count ? inFormattedList[i].PadRight(32) : "                                ";

                output = $"{ important[i] } |{ iuelementwithspaces }|{ inelementwithspaces }|  { Environment.NewLine }";
                matrix += output;
            }
            matrix += splitter + Environment.NewLine;

            for (int i = 0; i < notimportantctr; i++)
            {
                string output;

                string nuelementwithspaces;
                string nnelementwithspaces;

                nuelementwithspaces = i < nuFormattedList.Count ? nuFormattedList[i].PadRight(32) : "                                ";
                nnelementwithspaces = i < nnFormattedList.Count ? nnFormattedList[i].PadRight(32) : "                                ";

                output = $"{ notimportant[i] } |{ nuelementwithspaces }|{ nnelementwithspaces }|  {Environment.NewLine}";
                matrix += output;
            }
            matrix += splitter + Environment.NewLine;

            return matrix;
        }

        private List<string> GetFormattedToDoItems(List<TodoItem> items)
        {
            int index = 1;
            List<string> outlist = new List<string>();
            foreach (TodoItem item in items)
            {
                string indexedstring = index.ToString() + ". " + item.ToString().PadRight(32);
                List<string> splited = ChunkString(indexedstring, 32);
                splited = splited.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                outlist.AddRange(splited);
                index++;
            }
            return outlist;
        }

        private List<string> ChunkString(string str, int chunkSize)
        {
            StringBuilder b = new StringBuilder();
            int stringLength = str.Length;
            for (Int32 i = 0; i < stringLength; i += chunkSize)
            {
                if (i + chunkSize > stringLength) chunkSize = stringLength - i;
                b.Append(str.Substring(i, chunkSize));
                if (i + chunkSize != stringLength)
                    b.Append("|");
            }
            return b.ToString().Split("|").ToList();
        }

    }
}