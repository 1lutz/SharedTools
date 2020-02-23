using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedTools
{
    static class AsyncExtensions
    {
        public static Task WaitOneAsync(WaitHandle handle)
        {
            if (handle == null) throw new ArgumentNullException("handle");
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            RegisteredWaitHandle waiter = ThreadPool.RegisterWaitForSingleObject(handle, delegate { tcs.SetResult(true); }, null, Timeout.Infinite, true);
            Task t = tcs.Task;
            t.ContinueWith(_ => waiter.Unregister(null));
            return t;
        }
    }
}
