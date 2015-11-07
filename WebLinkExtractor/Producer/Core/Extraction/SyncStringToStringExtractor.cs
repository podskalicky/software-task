using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producer.Core.Input;
using Producer.Core.Output;
using System.Net;
using System.Threading;
using Producer.Core.Extraction.Client;

namespace Producer.Core.Extraction
{
    /// <summary>
    /// Synchronous (single thread) implementation of IExtractor.
    /// Inputs and outputs are string.
    /// </summary>
    public class SyncStringToStringExtractor : IExtractor<string, string>
    {
        /// <summary>
        /// Determines if the extractor is running.
        /// </summary>
        private bool IsProcessing { get; set; }

        /// <summary>
        /// Client provider is used for creating WebClient requests.
        /// </summary>
        private IWebClient WebClientImplementation { get; set; }

        /// <summary>
        /// Constructor.
        /// Sets IsProcessing value to false.
        /// </summary>
        /// <param name="webClient">Client provider used for creating WebClient instances</param>
        public SyncStringToStringExtractor(IWebClient webClient)
        {
            // Extractor is not running yet
            IsProcessing = false;
            WebClientImplementation = webClient;
        }

        /// <summary>
        /// Creates web request and retrieves HTML content.
        /// Takes URLs from input queue and puts results to output queue.
        /// Invokes action after each item processed.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="itemProcessed"></param>
        public void Extract(IInputQueue<string> input, IOutputQueue<string> output, Action itemProcessed)
        {
            // Set the processing flag to true
            IsProcessing = true;

            string url = input.Dequeue();
            string html;

            while (url != null)
            {
                try
                {
                    html = WebClientImplementation.DownloadString(url);
                    output.Enqueue(html);
                    itemProcessed.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // TODO Log error
                }

                // Get next item from input queue
                url = input.Dequeue();
            }

            // Set the processing flag to false
            IsProcessing = false;
        }

        public bool Processing()
        {
            return IsProcessing;
        }
    }
}
