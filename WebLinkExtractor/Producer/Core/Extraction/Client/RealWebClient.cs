using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Extraction.Client
{
    /// <summary>
    /// Provides DownloadString method using System.Net.WebClient instance.
    /// </summary>
    public class RealWebClient : IWebClient
    {
        public string DownloadString(string address)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(address);
            }
        }
    }
}
