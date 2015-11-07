using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorCommon
{
    /// <summary>
    /// Definition of consumer unit responsible for processing
    /// input HTML and parsing all hyperlinks.
    /// </summary>
    public interface IConsumerUnit
    {
        /// <summary>
        /// Notifiies consumer unit that there are new items
        /// in producer output queue.
        /// </summary>
        void StartConsuming(IProducerUnit producer);

        /// <summary>
        /// Returns results of the hyperlink extraction process.
        /// </summary>
        /// <returns></returns>
        List<string> GetResults();
    }
}
