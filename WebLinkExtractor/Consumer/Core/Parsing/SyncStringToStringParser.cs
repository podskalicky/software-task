using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractorCommon;
using System.Text.RegularExpressions;
using System.Threading;

namespace Consumer.Core.Parsing
{
    public class SyncStringToStringParser : IParser<string>
    {
        /// <summary>
        /// Synchronously picks items from producers output queue.
        /// </summary>
        /// <param name="producer">Producer unit that contains list of HTML content to be parsed</param>
        /// <param name="results">List of results</param>
        public void Parse(IProducerUnit producer, IList<string> results)
        {
            string html = producer.GetNextOutputItem();

            while (html != null)
            {
                Regex regex = new Regex("(?:href)=[\"|']?(.*?)[\"|'|>]+", RegexOptions.Singleline | RegexOptions.CultureInvariant);
                // TODO Additional filters might be required
                if (regex.IsMatch(html))
                {
                    foreach (Match match in regex.Matches(html))
                    {
                        string value = match.Groups[1].Value;
                        lock (results)
                        {
                            results.Add(value);
                        }
                    }
                }

                html = producer.GetNextOutputItem();
            }
        }
    }
}
