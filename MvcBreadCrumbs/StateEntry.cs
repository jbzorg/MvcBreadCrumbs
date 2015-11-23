using System;

namespace MvcBreadCrumbs
{
    interface IStateEntry
    {
        StateEntry ToStateEntry();
    }

    struct StateEntry : IStateEntry
    {
        int hash;

        public string Label { get; }
        public string Url { get; }

        public StateEntry(string url, string label)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(label)) throw new ArgumentNullException(nameof(label));

            Label = label;
            Url = url;
            hash = Url.ToLowerInvariant().GetHashCode();
        }

        public override int GetHashCode()
        {
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            IStateEntry _obj = obj as IStateEntry;
            if (_obj == null) return false;
            return Equals(_obj.ToStateEntry());
        }

        public bool Equals(StateEntry obj)
        {
            if (GetHashCode() != obj.GetHashCode()) return false;
            return Url.Equals(obj.Url, StringComparison.InvariantCultureIgnoreCase);
        }

        public StateEntry ToStateEntry()
        {
            return this;
        }
    }
}
