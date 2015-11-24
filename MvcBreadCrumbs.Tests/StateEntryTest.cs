using NUnit.Framework;
using System;

namespace MvcBreadCrumbs.Tests
{
    [TestFixture]
    public class StateEntryTest
    {
        [Test]
        public void ConstructTest()
        {
            StateEntry stateEntry = new StateEntry();
            Assert.AreEqual(stateEntry.Url, null);
            Assert.AreEqual(stateEntry.Label, null);
            Assert.AreEqual(stateEntry.Level, 0);
            Assert.AreEqual(stateEntry.Head, false);
            Assert.AreEqual(stateEntry.UrlHash, 0);

            stateEntry = new StateEntry("Url", "Label", 42, true);
            Assert.AreEqual(stateEntry.Url, "Url");
            Assert.AreEqual(stateEntry.Label, "Label");
            Assert.AreEqual(stateEntry.Level, 42);
            Assert.AreEqual(stateEntry.Head, true);
            Assert.AreEqual(stateEntry.UrlHash, "Url".ToLowerInvariant().GetHashCode());

            Assert.Catch<ArgumentNullException>(() => { new StateEntry(null as string, "Label"); });
            Assert.Catch<ArgumentNullException>(() => { new StateEntry("Url", null); });
        }
    }
}
