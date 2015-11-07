using Consumer;
using Consumer.Core.Parsing;
using ExtractorCommon;
using Producer;
using Producer.Core.Extraction;
using Producer.Core.Extraction.Client;
using Producer.Core.Input;
using Producer.Core.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebLinkExtractorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use some IoC to initialize
            IParser<string> parser = new SyncStringToStringParser();
            IConsumerUnit consumer = new ConsumerUnit(parser, Program.PrintResults);

            IInputQueue<string> inputQueue = new StringInputQueue();
            IOutputQueue<string> outputQueue = new StringOutputQueue();

            IExtractor<string, string> extractor = new AsyncStringToStringExtractor(5, new RealWebClient());
            IProducerUnit producer = new ProducerUnit(inputQueue, outputQueue, extractor);
            producer.Consumer = consumer;

            // Retrieve input from file
            string relative = "Input/InputFile.txt";
            string absolute = Path.GetFullPath(relative);
            string[] lines = File.ReadAllLines(absolute);

            // Add input to producer
            producer.AddInput(lines);

            // Start processing
            producer.StartProcessing();
            Console.WriteLine("Processing started.");

            Console.ReadLine();
        }

        /// <summary>
        /// Method to be invoked after all the inputs have been processed.
        /// </summary>
        /// <param name="results">Results</param>
        private static void PrintResults(IEnumerable<string> results)
        {
            string relative = "Output/OutputFile.txt";
            string absolute = Path.GetFullPath(relative);

            File.WriteAllLines(absolute, results);
            Console.WriteLine("Results printed to file.");
        }
    }
}