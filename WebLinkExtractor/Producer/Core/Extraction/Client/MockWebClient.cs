using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Extraction.Client
{
    /// <summary>
    /// Mock web client for testing purposes.
    /// </summary>
    public class MockWebClient : IWebClient
    {
        public string DownloadString(string address)
        {
            string html = string.Format("<html>Mock for: {0}</html>", address);
            return html;
        }
    }
}
