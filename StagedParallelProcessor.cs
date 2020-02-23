using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedTools
{
    public class StagedParallelProcessor<T>
    {
        private readonly BlockingCollection<T> _queue = new BlockingCollection<T>();
        private readonly Thread[] _threads;
        private readonly Action<T> _itemProcessor;
        private int _leftItemCount = 0;
        private readonly AutoResetEvent _areStageFinished = new AutoResetEvent(false);
        private readonly Dictionary<T, Exception> _errors = new Dictionary<T, Exception>();

        public Dictionary<T, Exception> Exceptions
        {
            get
            {
                lock (_errors)
                {
                    return new Dictionary<T, Exception>(_errors);
                }
            }
        }

        public bool Success
        {
            get
            {
                lock (_errors)
                {
                    return _errors.Count == 0;
                }
            }
        }

        private void DatenThread(object source)
        {
            foreach (T item in (IEnumerable<T>)source)
            {
                try
                {
                    _itemProcessor(item);
                }
                catch (Exception ex)
                {
                    lock (_errors)
                    {
                        _errors.Add(item, ex);
                    }
                }
                finally
                {
                    if (Interlocked.Decrement(ref _leftItemCount) == 0)
                    {
                        _areStageFinished.Set();
                    }
                }
            }
        }

        public StagedParallelProcessor(Action<T> itemProcessor, int degreeOfParallelism)
        {
            if (degreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(degreeOfParallelism));
            _threads = new Thread[degreeOfParallelism];
            _itemProcessor = itemProcessor;
            Thread t;

            for (int x = 0; x < degreeOfParallelism; ++x)
            {
                t = new Thread(DatenThread);
                t.Start(_queue.GetConsumingEnumerable());
                _threads[x] = t;
            }
        }

        public StagedParallelProcessor(Action<T> itemProcessor) : this(itemProcessor, Environment.ProcessorCount)
        {
        }

        public void Add(T item)
        {
            _areStageFinished.Reset();
            Interlocked.Increment(ref _leftItemCount);
            _queue.Add(item);
        }

        public void AddRange(IEnumerable<T> source)
        {
            _areStageFinished.Reset();

            if (source is ICollection<T> coll)
            {
                Interlocked.Add(ref _leftItemCount, coll.Count);
                foreach (T item in source) _queue.Add(item);
            }
            else
            {
                foreach (T item in source)
                {
                    Interlocked.Increment(ref _leftItemCount);
                    _queue.Add(item);
                }
            }
        }

        public void JoinStage()
        {
            _areStageFinished.WaitOne();
        }

        public Task JoinStageAsync()
        {
            return AsyncExtensions.WaitOneAsync(_areStageFinished);
        }

        public void Complete()
        {
            _queue.CompleteAdding();
            foreach (Thread t in _threads) t.Join();
        }

        public Task CompleteAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            new Thread(_tcs =>
            {
                Complete();
                ((TaskCompletionSource<bool>)_tcs).SetResult(true);
            }).Start(tcs);
            return tcs.Task;
        }
    }
}
