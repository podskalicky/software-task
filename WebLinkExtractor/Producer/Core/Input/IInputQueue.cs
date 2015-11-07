using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Core.Input
{
    public interface IInputQueue<T>
    {
        /// <summary>
        /// Add next item to the queue.
        /// </summary>
        /// <param name="item">Item</param>
        void Enqueue(T item);

        /// <summary>
        /// Removes first item from the queue and returns it.
        /// </summary>
        /// <returns>First item</returns>
        T Dequeue();

        /// <summary>
        /// Get size of the queue.
        /// </summary>
        /// <returns>Number of items</returns>
        int GetSize();
    }
}
