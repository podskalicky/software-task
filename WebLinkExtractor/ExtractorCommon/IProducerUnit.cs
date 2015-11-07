using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorCommon
{
    /// <summary>
    /// Definition of Producer unit that is responsible for processing
    /// input URLs into HTML content.
    /// </summary>
    public interface IProducerUnit
    {
        /// <summary>
        /// Consumer unit reference that can be initalized to notify
        /// consumer of new output.
        /// </summary>
        IConsumerUnit Consumer { get; set; }

        /// <summary>
        /// Adds input into the input queue.
        /// </summary>
        /// <param name="links">List of input strings</param>
        void AddInput(IEnumerable<string> input);

        /// <summary>
        /// Starts processing all links in input queue.
        /// </summary>
        void StartProcessing();

        /// <summary>
        /// Gets next item from output queue.
        /// </summary>
        /// <returns>Next string from output queue</returns>
        string GetNextOutputItem();

        /// <summary>
        /// Gets current size of input queue.
        /// </summary>
        /// <returns>Size of input queue</returns>
        int GetInputQueueSize();

        /// <summary>
        /// Determines if the extractor is still running.
        /// </summary>
        /// <returns>Returns true if at least one extraction process is running</returns>
        bool IsProcessing();

        /// <summary>
        /// Reduces input queue size by removing oldest items first.
        /// </summary>
        /// <param name="count">Number of items to be removed</param>
        void TrimInputQueue(int count);
    }
}
