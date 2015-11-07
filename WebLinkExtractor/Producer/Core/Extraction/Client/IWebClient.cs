using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Extraction.Client
{
    /// <summary>
    /// Provides Web Client instances.
    /// </summary>
    public interface IWebClient
    {
        string DownloadString(string address);
    }
}
