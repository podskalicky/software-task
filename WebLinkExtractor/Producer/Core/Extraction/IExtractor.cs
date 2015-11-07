using Producer.Core.Input;
using Producer.Core.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Extraction
{
    public interface IExtractor<I,O>
    {
        /// <summary>
        /// Starts extraction process.
        /// </summary>
        /// <param name="input">Input queue</param>
        /// <param name="output">Output queue</param>
        /// <param name="itemProcessed">Action invoked after the processed has finished</param>
        void Extract(IInputQueue<I> input, IOutputQueue<O> output, Action itemProcessed);

        /// <summary>
        /// Gets state of extraction process.
        /// </summary>
        /// <returns>True if there are inputs being processed</returns>
        bool Processing();
    }
}
