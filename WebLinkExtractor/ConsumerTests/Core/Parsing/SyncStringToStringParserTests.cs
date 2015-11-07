using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtractorCommon;
using System.Collections.Generic;
using Consumer.Core.Parsing;
using ConsumerTests.Mock;

namespace ConsumerTests.Core.Parsing
{
    [TestClass]
    public class SyncStringToStringParserTests
    {
        [TestMethod]
        public void Parse_ValidInput_ValidOutput()
        {
            // Arrange
            string[] output = new string[]
            {
                "html code <a href=\"http://www.google1.com\">link to google</a>",
                "html code <a href=\"http://www.google2.com\">link to google</a> html code <a href=\"http://www.google3.com\">link to google</a> html code",
                "html code no link",
                ""
            };

            IProducerUnit producer = new MockProducerUnit(output);
            IParser<string> parser = new SyncStringToStringParser();
            List<string> results = new List<string>();

            // Act
            parser.Parse(producer, results);

            // Assert
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void Parse_InvalidInput_FinishesIncorrectly()
        {
            // Arrange
            string[] output = new string[]
            {
                "html code <a href=\"http://www.google1.com\">link to google</a>",
                null,
                "html code <a href=\"http://www.google2.com\">link to google</a>",
                "html code <a href=\"http://www.google3.com\">link to google</a> html code",
            };

            IProducerUnit producer = new MockProducerUnit(output);
            IParser<string> parser = new SyncStringToStringParser();
            List<string> results = new List<string>();

            // Act
            parser.Parse(producer, results);

            // Assert
            Assert.AreEqual(1, results.Count);
        }
    }
}
