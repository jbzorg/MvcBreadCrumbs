using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MvcBreadCrumbs
{
    static class StateManager
    {
        static readonly ConcurrentDictionary<string, State> States = new ConcurrentDictionary<string, State>();

        public static State GetState(string id, int timeOut = 60000)
        {
            if (timeOut < 10) timeOut = 10;

            State state = States.GetOrAdd(id, new State());
            if (state != null)
            {
                lock (state)
                {
                    state.CancellationTokenSource?.Cancel();
                    state.CancellationTokenSource?.Dispose();
                    state.CancellationTokenSource = new CancellationTokenSource();
                    state.Task = Task.Run(async () =>
                    {
                        await Task.Delay(timeOut, state.CancellationTokenSource.Token);
                        if (!state.CancellationTokenSource.Token.IsCancellationRequested)
                            StateManager.RemoveState(id);
                    }, state.CancellationTokenSource.Token);
                }
            }
            return state;
        }

        public static void RemoveState(string id)
        {
            State state = null;
            States.TryRemove(id, out state);
            if (state != null)
            {
                lock (state)
                {
                    state.CancellationTokenSource?.Cancel();
                    state.CancellationTokenSource?.Dispose();
                }
            }
        }
    }
}