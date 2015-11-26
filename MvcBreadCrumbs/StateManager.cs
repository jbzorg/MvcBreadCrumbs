using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MvcBreadCrumbs
{
    /// <summary>
    /// Queues manager
    /// </summary>
    static class StateManager
    {
        static readonly ConcurrentDictionary<string, State> States = new ConcurrentDictionary<string, State>();

        /// <summary>
        /// Get queue
        /// </summary>
        /// <param name="id">id queue</param>
        /// <param name="timeOut">self destory timeout</param>
        /// <returns>queue</returns>
        public static State GetState(string id, int timeOut = 60000)
        {
            if (timeOut < 10) timeOut = 10;

            State state = States.GetOrAdd(id, new State());
            if (state != null)
            {
                lock (state)
                {
                    state.CancellationTokenSource?.Cancel(); //cancel previous self destroy task
                    state.CancellationTokenSource?.Dispose();
                    state.CancellationTokenSource = new CancellationTokenSource();
                    state.Task = Task.Run(async () => //start new self destroy task
                    {
                        await Task.Delay(timeOut, state.CancellationTokenSource.Token);
                        if (!state.CancellationTokenSource.Token.IsCancellationRequested)
                            StateManager.RemoveState(id);
                    }, state.CancellationTokenSource.Token);
                }
            }
            return state;
        }

        /// <summary>
        /// Remove queue
        /// </summary>
        /// <param name="id">id queue</param>
        public static void RemoveState(string id)
        {
            State state = null;
            States.TryRemove(id, out state);
            if (state != null)
            {
                lock (state)
                {
                    state.CancellationTokenSource?.Cancel(); //cancel self destroy task
                    state.CancellationTokenSource?.Dispose();
                }
            }
        }
    }
}