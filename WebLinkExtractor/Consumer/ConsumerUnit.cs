using Consumer.Core.Parsing;
using ExtractorCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConsumerUnit : IConsumerUnit
    {
        /// <summary>
        /// Parsing is single thread process running in parser task.
        /// Ensures there is only one parser task per consumer unit.
        /// </summary>
        private Task ParserTask { get; set; }

        /// <summary>
        /// Parser implementation.
        /// </summary>
        private IParser<string> Parser { get; set; }

        /// <summary>
        /// List of results.
        /// </summary>
        private List<string> Results { get; set; }

        /// <summary>
        /// Action to be invoked after the processing has finished.
        /// </summary>
        private Action<IEnumerable<string>> PrintResults { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parser">Parser implementation</param>
        /// <param name="printResults">Action to be invoked after the processing has finished</param>
        public ConsumerUnit(IParser<string> parser, Action<IEnumerable<string>> printResults)
        {
            Parser = parser;
            PrintResults = printResults;

            Results = new List<string>();
        }

        /// <summary>
        /// Checks if the producer has finished.
        /// This method is called if the consumer has finished processing.
        /// </summary>
        /// <param name="producer">Producer unit</param>
        private void ProcessingFinished(IProducerUnit producer)
        {
            if ((producer.GetInputQueueSize() < 1) &&
                (!producer.IsProcessing()))
            {
                if (PrintResults != null)
                {
                    PrintResults.Invoke(GetResults());
                }
            }
        }

        public void StartConsuming(IProducerUnit producer)
        {
            if (ParserTask == null || ParserTask.IsCompleted)
            {
                ParserTask = new Task(() => Parser.Parse(producer, Results));
                ParserTask.ContinueWith(finished => ProcessingFinished(producer));
                ParserTask.Start();
            }
        }

        public List<string> GetResults()
        {
            lock (Results)
            {
                List<string> results = Results.ToList();
                Results = new List<string>();

                return results;
            }
        }
    }
}
