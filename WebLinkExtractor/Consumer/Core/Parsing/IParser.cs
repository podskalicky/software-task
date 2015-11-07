using ExtractorCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Core.Parsing
{
    public interface IParser<O>
    {
        void Parse(IProducerUnit producer, IList<O> results);
    }
}
