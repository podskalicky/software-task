using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Input
{
    /// <summary>
    /// String implementation of IInputQueue.
    /// Uses ConcurrentQueue implementation.
    /// </summary>
    public class StringInputQueue : IInputQueue<string>
    {
        /// <summary>
        /// Queue implementation.
        /// </summary>
        private ConcurrentQueue<string> StringQueue { get; set; }

        public StringInputQueue()
        {
            StringQueue = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// Enqueue input string to queue.
        /// Does not allow null or empty strings.
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                StringQueue.Enqueue(item);
            }
        }

        public string Dequeue()
        {
            string result;
            StringQueue.TryDequeue(out result);

            return result;
        }

        public int GetSize()
        {
            return StringQueue.Count;
        }
    }
}
