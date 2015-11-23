using System.Collections.Concurrent;

namespace MvcBreadCrumbs
{
    static class StateManager
    {
        static readonly ConcurrentDictionary<string, State> States = new ConcurrentDictionary<string, State>();

        public static State GetState(string id)
        {
            return States.GetOrAdd(id, new State());
        }

        public static void RemoveState(string id)
        {
            State state = null;
            States.TryRemove(id, out state);
        }
    }
}