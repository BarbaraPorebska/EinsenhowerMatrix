using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codecool.EinsenhowerMatrix
{
    /// <summary>
    /// Class that represents quarter for items from different categories
    /// </summary>
    public class TodoQuarter
    {
        /// <summary>
        /// Gets or sets list of items
        /// </summary>
        public List<TodoItem> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoQuarter"/> class.
        /// </summary>
        public TodoQuarter()
        {
            Items = new List<TodoItem>();
        }

        /// <summary>
        /// Add item to list
        /// </summary>
        /// <param name="title">title of item</param>
        /// <param name="deadline">deadline of item</param>
        public void AddItem(string title, DateTime deadline)
        {
            if (title == null)
            {
                throw new ArgumentNullException("Title cannot be null");
            }
            if (deadline < DateTime.Now)
            {
                throw new ArgumentException("Deadline is in the past");
            }
            Items.Add(new TodoItem(title, deadline));
            SortToDoItems();
        }

        /// <summary>
        /// Add item to list
        /// </summary>
        /// <param name="title">title of item</param>
        /// <param name="deadline">deadline of item</param>
        /// <param name="isImportant">boolean that indicates whenever item is important or not</param>
        public void AddItem(string title, DateTime deadline, bool isImportant)
        {
            if (title == null)
            {
                throw new ArgumentNullException("Title cannot be null");
            }
            if (deadline < DateTime.Now)
            {
                throw new ArgumentException("Deadline is in the past");
            }
            Items.Add(new TodoItem(title, deadline, isImportant));
            SortToDoItems();
        }

        /// <summary>
        /// Removes item instance under given index
        /// </summary>
        /// <param name="index">index of </param>
        public void RemoveItem(int index)
        {
            if (index > Items.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
            Items.RemoveAt(index - 1);
        }

        /// <summary>
        /// Destroys all done item
        /// </summary>
        public void ArchiveItems()
        {
            for (int i = Items.Count - 1; i > 0; i--)
            {
                if (Items[i].IsDone)
                {
                    Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Returns ToDoItem at index
        /// </summary>
        /// <param name="index">Index of ToDoItem</param>
        /// <returns>ToDoItem</returns>
        public TodoItem GetItem(int index)
        {
            if (index > Items.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
            return Items[index - 1];
        }

        /// <summary>
        /// Returns private field todoItems.
        /// </summary>
        /// <returns>todoItems</returns>
        public List<TodoItem> GetItems()
        {
            return Items;
        }

        /// <summary>
        /// Returns human readable representation of quarter
        /// </summary>
        /// <returns>string with all nested items</returns>
        public override string ToString()
        {
            string res = String.Empty;
            for (int i = 0; i < Items.Count; i++)
            {
                res += $"{ i + 1 }. { Items[i] }{ Environment.NewLine }"; 
            }
            return res;
        }

        private void SortToDoItems()
        {
            Items = Items.OrderBy(x => x.Deadline).ToList();
        }
    }
}