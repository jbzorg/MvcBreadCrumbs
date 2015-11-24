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
        public int Level { get; }
        public bool Head { get; }
        public int UrlHash { get; }

        public StateEntry(string url, string label, int level = -1, bool head = false)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(label)) throw new ArgumentNullException(nameof(label));

            Label = label;
            Url = url;
            Level = level;
            Head = head;
            UrlHash = Url.ToLowerInvariant().GetHashCode();

            hash = Label.GetHashCode() ^ Url.GetHashCode() ^ Level.GetHashCode() ^ Head.GetHashCode();
        }

        public override int GetHashCode() => hash;

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
            return Label.Equals(obj.Label) &&
                Url.Equals(obj.Url) &&
                Level == obj.Level &&
                Head == obj.Head;
        }

        public StateEntry ToStateEntry() => this;

    }
}
