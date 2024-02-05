using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Codecool.EinsenhowerMatrix
{
    /// <summary>
    /// Base class that represents task
    /// </summary>
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idtodoitem { get; private set; }
        /// <summary>
        /// Gets or sets Task's title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets Task's deadline
        /// </summary>
        public DateTime Deadline { get; private set; } 

        /// <summary>
        /// Gets or sets a value indicating whether task is complete or not
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether task is important or not
        /// </summary>
        public bool IsImportant { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItem"/> class.
        /// </summary>
        /// <param name="title">string representation for title</param>
        /// <param name="deadline">deadline for title</param>
        public TodoItem(string title, DateTime deadline)
        {
            Title = title;
            Deadline = new DateTime(2024, deadline.Month, deadline.Day);    
            IsDone = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItem"/> class.
        /// </summary>
        /// <param name="title">string representation for title</param>
        /// <param name="deadline">deadline for title</param>
        /// <param name="isImportant">value indicating whether task is important or not</param>
        public TodoItem(string title, DateTime deadline, bool isImportant)
            : this(title, deadline)
        {
            IsImportant = isImportant;  
        }

        /// <summary>
        /// Marks item as done
        /// </summary>
        public void Mark()
        {
            IsDone = true;
        }

        /// <summary>
        /// Marks item as undone
        /// </summary>
        public void UnMark()
        {
            IsDone = false;
        }

        /// <summary>
        /// Gets title
        /// </summary>
        /// <returns>string containing title of item</returns>
        public string GetTitle()
        {
            return Title;
        }

        /// <summary>
        /// Gets deadline
        /// </summary>
        /// <returns>DateTime containing deadline of item</returns>
        public DateTime GetDeadline()
        { 
            return Deadline; 
        }

        /// <summary>
        /// Returns a human readable representation for task
        /// </summary>
        /// <returns>string containing instance values</returns>
        public override string ToString()
        {
            return $"[{ (IsDone ? "x" : " ") }] { Deadline.ToString("d-M", CultureInfo.InvariantCulture) } { Title }";
        }
    }
}