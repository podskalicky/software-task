using ExtractorCommon;
using Producer.Core.Extraction;
using Producer.Core.Input;
using Producer.Core.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    /// <summary>
    /// Implementation of producer unit.
    /// </summary>
    public class ProducerUnit : IProducerUnit
    {
        /// <summary>
        /// Input queue.
        /// </summary>
        private IInputQueue<string> InputQueue { get; set; }

        /// <summary>
        /// Output queue.
        /// </summary>
        private IOutputQueue<string> OutputQueue { get; set; }

        /// <summary>
        /// Extractor implementation.
        /// </summary>
        private IExtractor<string, string> Extractor { get; set; }

        /// <summary>
        /// Consumer field.
        /// </summary>
        private IConsumerUnit _consumer { get; set; }

        /// <summary>
        /// Consumer property to set ang get Consumer reference.
        /// </summary>
        public IConsumerUnit Consumer
        {
            get
            {
                return _consumer;
            }

            set
            {
                _consumer = value;
            }
        }

        public ProducerUnit(
            IInputQueue<string> inputQueue,
            IOutputQueue<string> outputQueue,
            IExtractor<string, string> extractor)
        {
            InputQueue = inputQueue;
            OutputQueue = outputQueue;
            Extractor = extractor;
        }

        /// <summary>
        /// Notifies the consumer unit that output is ready to be processed in output queue.
        /// Sends reference of this producer unit.
        /// </summary>
        private void ProcessingFinished()
        {
            if (_consumer != null)
            {
                _consumer.StartConsuming(this);
            }
        }

        public void AddInput(IEnumerable<string> input)
        {
            foreach (var item in input)
            {
                InputQueue.Enqueue(item);
            }
        }

        public void StartProcessing()
        {
            Extractor.Extract(InputQueue, OutputQueue, ProcessingFinished);
        }

        public string GetNextOutputItem()
        {
            return OutputQueue.Dequeue();
        }

        public int GetInputQueueSize()
        {
            return InputQueue.GetSize();
        }

        public bool IsProcessing()
        {
            return Extractor.Processing();
        }

        public void TrimInputQueue(int count)
        {
            // TODO 
            throw new NotImplementedException();
        }
    }
}
