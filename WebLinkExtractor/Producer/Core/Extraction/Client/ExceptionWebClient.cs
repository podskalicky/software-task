using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Extraction.Client
{
    /// <summary>
    /// Mock web client implementation that throws exception every time.
    /// For testing purposes.
    /// </summary>
    public class ExceptionWebClient : IWebClient
    {
        public string DownloadString(string address)
        {
            throw new WebException("ExceptionWebClient threw exception.");
        }
    }
}
