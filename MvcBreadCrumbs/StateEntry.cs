using System;

namespace MvcBreadCrumbs
{
    /// <summary>
    /// Interface for working with value type like ref type
    /// </summary>
    interface IStateEntry
    {
        /// <summary>
        /// Convert to StateEntry
        /// </summary>
        /// <returns></returns>
        StateEntry ToStateEntry();
    }

    /// <summary>
    /// One crumb
    /// </summary>
    struct StateEntry : IStateEntry
    {
        int hash;

        /// <summary>
        /// Name of crumb
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Group of crumb
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// This crumb is top of queue
        /// </summary>
        public bool Head { get; }

        /// <summary>
        /// Case insensitive hash of URL
        /// </summary>
        public int UrlHash { get; }

        /// <summary>
        /// This crumb is link or simple text
        /// </summary>
        public bool Link { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="label">name of crumb</param>
        /// <param name="level">group of crumb</param>
        /// <param name="head">this crumb is top of queue</param>
        /// <param name="link">this crumb is link or simple text</param>
        public StateEntry(string url, string label, int level = -1, bool head = false, bool link = true)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(label)) throw new ArgumentNullException(nameof(label));

            Label = label;
            Url = url;
            Level = level;
            Head = head;
            Link = link;
            UrlHash = Url.ToLowerInvariant().GetHashCode();

            hash = Label.GetHashCode() ^ Url.GetHashCode() ^ Level.GetHashCode() ^ Head.GetHashCode() ^ Link.GetHashCode();
        }

        /// <summary>
        /// Get hash of StateEntry
        /// </summary>
        /// <returns></returns>
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
