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
            Assert.AreEqual(stateEntry.Label, null);
            Assert.AreEqual(stateEntry.Url, null);

            stateEntry = new StateEntry("Url", "Label");
            Assert.AreEqual(stateEntry.Label, "Label");
            Assert.AreEqual(stateEntry.Url, "Url");

            Assert.Catch<ArgumentNullException>(() => { new StateEntry(null as string, "Label"); });
            Assert.Catch<ArgumentNullException>(() => { new StateEntry("Url", null); });
        }
    }
}
