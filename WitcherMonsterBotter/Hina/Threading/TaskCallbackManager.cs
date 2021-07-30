using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Hina.Threading
{
    public class TaskCallbackManager<TKey, TValue>
    {
        readonly ConcurrentDictionary<TKey, TaskCompletionSource<TValue>> callbacks = new ConcurrentDictionary<TKey, TaskCompletionSource<TValue>>();
        public static T NotNull<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException("parameter-0", "one of the passed-in parameters is null");

            return obj;
        }

        public Task<TValue> Create(TKey key)
        {
            NotNull(key);

            return callbacks
                .GetOrAdd(key, k => new TaskCompletionSource<TValue>(TaskCreationOptions.RunContinuationsAsynchronously))
                .Task;
        }

        public bool Remove(TKey key)
        {
            NotNull(key);

            return callbacks.TryRemove(key, out var callback);
        }

        public bool SetResult(TKey key, TValue result)
        {
            NotNull(key);

            if (callbacks.TryRemove(key, out var callback))
            {
                callback.TrySetResult(result);
                return true;
            }

            return false;
        }

        public void SetException(TKey key, Exception exception)
        {
            NotNull(key);

            if (callbacks.TryRemove(key, out var callback))
                callback.TrySetException(exception);
        }

        public void Clear()
        {
            callbacks.Clear();
        }
    }
}
