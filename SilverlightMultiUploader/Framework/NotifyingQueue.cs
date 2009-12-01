using System.Collections;
using System.Collections.Generic;
using System;

namespace SilverlightMultiUploader.Framework
{
    public class NotifyingQueue<T> : IEnumerable<T>, ICollection
    {
        public event EventHandler ItemEnqueued;
        public event EventHandler ItemDequeued;
        public event EventHandler QueueEmpty;
        private readonly Queue<T> _queue = new Queue<T>();
        protected void OnItemEnqueued()
        {
            if (ItemEnqueued != null)
                ItemEnqueued(this, EventArgs.Empty);
        }
        protected void OnItemDequeued()
        {
            if (ItemDequeued != null)
                ItemDequeued(this, EventArgs.Empty);
        }
        protected void OnQueueEmpty()
        {
            if (QueueEmpty != null)
                QueueEmpty(this, EventArgs.Empty);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)_queue).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_queue).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_queue).CopyTo(array, index);
        }

        public void Clear()
        {
            _queue.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _queue.CopyTo(array, arrayIndex);
        }

        public void Enqueue(T item)
        {
            OnItemEnqueued();
            _queue.Enqueue(item);
        }

        public T Dequeue()
        {
            var retVal = _queue.Dequeue();
            OnItemDequeued();
            if (_queue.Count < 1)
                OnQueueEmpty();
            return retVal; 
        }

        public T Peek()
        {
            return _queue.Peek();
        }

        public bool Contains(T item)
        {
            return _queue.Contains(item);
        }

        public T[] ToArray()
        {
            return _queue.ToArray();
        }

        public void TrimExcess()
        {
            _queue.TrimExcess();
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        public object SyncRoot
        {
            get { return ((ICollection)_queue).SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return ((ICollection)_queue).IsSynchronized; }
        }

    }
}