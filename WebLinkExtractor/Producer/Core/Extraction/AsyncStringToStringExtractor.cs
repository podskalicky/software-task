using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producer.Core.Input;
using Producer.Core.Output;
using Producer.Core.Extraction.Client;

namespace Producer.Core.Extraction
{
    /// <summary>
    /// Asychronous implementation of extractor.
    /// Keeps list of synchronous extractors and runs them in separate threads.
    /// </summary>
    public class AsyncStringToStringExtractor : IExtractor<string, string>
    {
        /// <summary>
        /// Defines how many threads will be initialized for extraction.
        /// </summary>
        private int ThreadCount { get; set; }

        /// <summary>
        /// Client provider is used for creating WebClient requests.
        /// </summary>
        private IWebClient WebClientImplementation { get; set; }

        /// <summary>
        /// List of synchronous extractors used in separate threads.
        /// </summary>
        private List<IExtractor<string, string>> SyncExtractors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="threadCount">Defines how many threads should be initalized for extraction process</param>
        /// <param name="webClient">Client provider used for creating WebClient instances</param>
        public AsyncStringToStringExtractor(int threadCount, IWebClient webClient)
        {
            ThreadCount = threadCount;
            WebClientImplementation = webClient;
            SyncExtractors = new List<IExtractor<string, string>>();
        }

        /// <summary>
        /// Creates predefined number of synchronous extractors and starts them in separate threads.
        /// </summary>
        /// <param name="input">Input queue</param>
        /// <param name="output">Output queue</param>
        /// <param name="itemProcessed">Action to be invoked after each processed item</param>
        public void Extract(IInputQueue<string> input, IOutputQueue<string> output, Action itemProcessed)
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                IExtractor<string, string> synchronousExtractor = new SyncStringToStringExtractor(WebClientImplementation);
                SyncExtractors.Add(synchronousExtractor);

                Task synchronousTask = new Task(() => synchronousExtractor.Extract(input, output, itemProcessed));
                synchronousTask.Start();
            }
        }

        /// <summary>
        /// Gets state of all extractors.
        /// If there are no extractors still processing clear the list so they can be disposed.
        /// </summary>
        /// <returns>Returns true if at least one extractor is still processing</returns>
        public bool Processing()
        {
            foreach (var item in SyncExtractors)
            {
                if (item.Processing()) return true;
            }

            SyncExtractors.Clear();
            return false;
        }
    }
}
