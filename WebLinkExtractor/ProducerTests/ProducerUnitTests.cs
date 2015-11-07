using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Producer;
using System.Collections.Generic;
using Producer.Core.Extraction;
using Producer.Core.Input;
using Producer.Core.Output;
using ExtractorCommon;
using Producer.Core.Extraction.Client;

namespace ProducerTests
{
    [TestClass]
    public class ProducerUnitTests
    {
        [TestMethod]
        public void AddInput_ValidInput_GetSizeCorrectValue()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();
            IProducerUnit producer = new ProducerUnit(inputQueue, null, null);

            List<string> input = new List<string>
            {
                "https://en.wikipedia.org/wiki/Web_scraping",
                "https://en.wikipedia.org/wiki/Firefox",
                "https://en.wikipedia.org/wiki/Foxfire"
            };

            // Act
            producer.AddInput(input);
            int inputQueueSize = producer.GetInputQueueSize();

            // Assert
            Assert.AreEqual(3, inputQueueSize);
        }

        [TestMethod]
        public void StartProcessing_ValidInput_GetCorrectOutput()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();
            inputQueue.Enqueue("https://en.wikipedia.org/wiki/Web_scraping");
            inputQueue.Enqueue("https://en.wikipedia.org/wiki/Firefox");
            inputQueue.Enqueue("https://en.wikipedia.org/wiki/Foxfire");

            IOutputQueue<string> outputQueue = new StringOutputQueue();
            IExtractor<string, string> extractor = new SyncStringToStringExtractor(new MockWebClient());
            IProducerUnit producer = new ProducerUnit(inputQueue, outputQueue, extractor);

            // Act
            producer.StartProcessing();

            // Assert
            int outputQueueLength = 0;
            while (outputQueue.Dequeue() != null)
            {
                outputQueueLength++;
            }
            Assert.AreEqual(3, outputQueueLength);
        }

        [TestMethod]
        public void StartProcessing_WebException_DoNotPropagateException()
        {
            // Arrange
            IInputQueue<string> inputQueue = new StringInputQueue();
            inputQueue.Enqueue("https://en.wikipedia.org/wiki/Web_scraping");
            inputQueue.Enqueue("https://en.wikipedia.org/wiki/Foxfire");

            IOutputQueue<string> outputQueue = new StringOutputQueue();
            // Use ExceptionWebClient to simulate Web Exception
            IExtractor<string, string> extractor = new SyncStringToStringExtractor(new ExceptionWebClient());
            IProducerUnit producer = new ProducerUnit(inputQueue, outputQueue, extractor);

            // Act
            producer.StartProcessing();

            // Assert
            int outputQueueLength = 0;
            while (outputQueue.Dequeue() != null)
            {
                outputQueueLength++;
            }
            Assert.AreEqual(0, outputQueueLength);
        }
    }
}
