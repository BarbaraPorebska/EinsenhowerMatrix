using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.EinsenhowerMatrix.Tests
{
    [TestFixture]
    public class TodoItemsTests
    {

        [Test]
        public void AddItem()
        {
            TodoQuarter todoQuarter = new TodoQuarter();
            todoQuarter.AddItem("Test", DateTime.Now.AddDays(1));
            Assert.That(1, Is.EqualTo(todoQuarter.Items.Count));
        }

        [Test]
        public void RemoveItem()
        {
            TodoQuarter todoQuarter = new TodoQuarter();
            todoQuarter.AddItem("Test", DateTime.Now.AddDays(1));
            Assert.That(1, Is.EqualTo(todoQuarter.Items.Count));
            todoQuarter.RemoveItem(1);
            Assert.That(0, Is.EqualTo(todoQuarter.Items.Count));
        }

        [Test]
        public void RemoveATaskAtOutOfRangeIndex()
        {
            TodoQuarter todoQuarter = new TodoQuarter();
            todoQuarter.AddItem("Test", DateTime.Now.AddDays(1));
            todoQuarter.AddItem("Test2", DateTime.Now.AddDays(1));
            todoQuarter.AddItem("Test3", DateTime.Now.AddDays(1));
            todoQuarter.AddItem("Test4", DateTime.Now.AddDays(1));
            Assert.That(4, Is.EqualTo(todoQuarter.Items.Count));
            Assert.Throws<IndexOutOfRangeException>(() => todoQuarter.RemoveItem(5));
        }

        [Test]
        public void GetATaskAtOutOfRangeIndex()
        {
            TodoQuarter todoQuarter = new TodoQuarter();
            todoQuarter.AddItem("Test", DateTime.Now.AddDays(1));
            todoQuarter.AddItem("Test2", DateTime.Now.AddDays(1));
            todoQuarter.AddItem("Test3", DateTime.Now.AddDays(1));
            todoQuarter.AddItem("Test4", DateTime.Now.AddDays(1));
            Assert.That(4, Is.EqualTo(todoQuarter.Items.Count));
            Assert.Throws<IndexOutOfRangeException>(() => todoQuarter.GetItem(5));
        }
    }
}
