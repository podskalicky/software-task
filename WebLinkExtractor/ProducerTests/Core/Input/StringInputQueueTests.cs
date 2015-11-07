using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Producer.Core.Input;

namespace ProducerTests.Core.Input
{
    [TestClass]
    public class StringInputQueueTests
    {
        [TestMethod]
        public void Enqueue_AddItem_CorrectSize()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();

            // Act
            int empty = inputQueue.GetSize();
            inputQueue.Enqueue("test string");
            int filled = inputQueue.GetSize();

            // Assert
            Assert.AreEqual(0, empty);
            Assert.AreEqual(1, filled);
        }

        [TestMethod]
        public void Dequeue_AddAndRemoveItem_CorrectSize()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();
            string original = "test string";

            // Act
            int empty = inputQueue.GetSize();
            inputQueue.Enqueue(original);
            int filled = inputQueue.GetSize();
            string retrieved = inputQueue.Dequeue();
            int removed = inputQueue.GetSize();

            // Assert
            Assert.AreEqual(0, empty);
            Assert.AreEqual(1, filled);
            Assert.AreEqual(0, removed);
            Assert.AreEqual(original, retrieved);
        }

        [TestMethod]
        public void Enqueue_AddNullAndEmptyString_CorrectSize()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();

            // Act
            int empty = inputQueue.GetSize();
            inputQueue.Enqueue("test string");
            inputQueue.Enqueue(null);
            inputQueue.Enqueue(string.Empty);
            inputQueue.Enqueue("test string");
            int filled = inputQueue.GetSize();

            // Assert
            Assert.AreEqual(0, empty);
            Assert.AreEqual(2, filled);
        }
    }
}
