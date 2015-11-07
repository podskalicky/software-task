using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtractorCommon;
using Consumer;
using ConsumerTests.Mock;
using Consumer.Core.Parsing;
using System.Collections.Generic;
using System.Threading;

namespace ConsumerTests
{
    [TestClass]
    public class ConsumerUnitTests
    {
        [TestMethod]
        public void StartConsuming_ValidInput_ValidOutput()
        {
            // Arrange
            string[] output = new string[]
            {
                "html code <a href=\"http://www.google1.com\">link to google</a> html code",
                "html code <a href=\"http://www.google2.com\">link to google</a> html code",
                "html code <a href=\"http://www.google3.com\">link to google</a> html code",
            };

            string firstResult = null;

            IProducerUnit producer = new MockProducerUnit(output);
            IParser<string> parser = new SyncStringToStringParser();
            List<string> results = new List<string>();
            IConsumerUnit consumer = new ConsumerUnit(parser,
                (res) => firstResult = new List<string>(res)[0]);

            // Act
            consumer.StartConsuming(producer);
            // TODO
            Thread.Sleep(2000);

            // Assert
            Assert.IsNotNull(firstResult);
        }
    }
}
