using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Producer.Core.Output;

namespace ProducerTests.Core.Output
{
    [TestClass]
    public class StringOutputQueueTests
    {
        [TestMethod]
        public void Enqueue_AddItem_CorrectSize()
        {
            // Arrange
            IOutputQueue<string> outputQueue = new StringOutputQueue();

            // Act
            int empty = outputQueue.GetSize();
            outputQueue.Enqueue("test string");
            int filled = outputQueue.GetSize();

            // Assert
            Assert.AreEqual(0, empty);
            Assert.AreEqual(1, filled);
        }

        [TestMethod]
        public void Dequeue_AddAndRemoveItem_CorrectSize()
        {
            // Arrange
            IOutputQueue<string> outputQueue = new StringOutputQueue();
            string original = "test string";

            // Act
            int empty = outputQueue.GetSize();
            outputQueue.Enqueue(original);
            int filled = outputQueue.GetSize();
            string retrieved = outputQueue.Dequeue();
            int removed = outputQueue.GetSize();

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
            IOutputQueue<string> outputQueue = new StringOutputQueue();

            // Act
            int empty = outputQueue.GetSize();
            outputQueue.Enqueue("test string");
            outputQueue.Enqueue(null);
            outputQueue.Enqueue(string.Empty);
            outputQueue.Enqueue("test string");
            int filled = outputQueue.GetSize();

            // Assert
            Assert.AreEqual(0, empty);
            Assert.AreEqual(2, filled);
        }
    }
}
