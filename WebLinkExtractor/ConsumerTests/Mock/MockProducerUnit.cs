using ExtractorCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTests.Mock
{
    public class MockProducerUnit : IProducerUnit
    {
        public int Index { get; set; }

        public string[] Output { get; set; }

        public IConsumerUnit Consumer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public MockProducerUnit(string[] output)
        {
            Output = output;
            Index = 0;
        }

        public void AddInput(IEnumerable<string> input)
        {
            throw new NotImplementedException();
        }

        public int GetInputQueueSize()
        {
            return Output.Length - Index;
        }

        public string GetNextOutputItem()
        {
            if (Index < Output.Length)
            {
                string output = Output[Index];
                Index++;
                return output;
            }
            else
            {
                return null;
            }
        }

        public bool IsProcessing()
        {
            return Index < Output.Length;
        }

        public void StartProcessing()
        {
            throw new NotImplementedException();
        }

        public void TrimInputQueue(int count)
        {
            throw new NotImplementedException();
        }
    }
}
