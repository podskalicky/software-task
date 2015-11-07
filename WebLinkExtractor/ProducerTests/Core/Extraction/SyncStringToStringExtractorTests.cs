using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Producer.Core.Input;
using Producer.Core.Output;
using Producer.Core.Extraction;
using Producer.Core.Extraction.Client;

namespace ProducerTests.Core.Extraction
{
    [TestClass]
    public class SyncStringToStringExtractorTests
    {
        [TestMethod]
        public void Extracting_ValidInput_ValidOutput()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();
            IOutputQueue<string> outputQueue = new StringOutputQueue();
            inputQueue.Enqueue("https://www.google.sk/");

            IExtractor<string, string> extractor = new SyncStringToStringExtractor(new MockWebClient());
            bool finished = false;

            // Act
            extractor.Extract(inputQueue, outputQueue, () => finished = true);

            // Assert
            string output = outputQueue.Dequeue();
            Assert.IsNotNull(output);
            Assert.IsTrue(finished);
        }
    }
}
