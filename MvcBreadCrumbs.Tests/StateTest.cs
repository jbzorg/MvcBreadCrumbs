using NUnit.Framework;
using System.Linq;

namespace MvcBreadCrumbs.Tests
{
    [TestFixture]
    public class StateTest
    {
        [Test]
        public void AddTest()
        {
            State state = new State();

            state.Add("url1", "label1", 1);
            Assert.AreEqual(state.Count(), 1);
            Assert.AreEqual(state.ElementAt(0).Key, 1);
            Assert.AreEqual(state.ElementAt(0).Value.Label, "label1");
            Assert.AreEqual(state.ElementAt(0).Value.Url, "url1");

            state.Add("url2", "label2", 2);
            Assert.AreEqual(state.Count(), 2);
            Assert.AreEqual(state.ElementAt(1).Key, 2);
            Assert.AreEqual(state.ElementAt(1).Value.Label, "label2");
            Assert.AreEqual(state.ElementAt(1).Value.Url, "url2");

            state.Add("url3", "label3", 3);
            Assert.AreEqual(state.Count(), 3);
            Assert.AreEqual(state.ElementAt(2).Key, 3);
            Assert.AreEqual(state.ElementAt(2).Value.Label, "label3");
            Assert.AreEqual(state.ElementAt(2).Value.Url, "url3");

            state.Add("url2", "label2_new", 4); //back to url2
            Assert.AreEqual(state.Count(), 2);
            Assert.AreEqual(state.ElementAt(1).Key, 4);
            Assert.AreEqual(state.ElementAt(1).Value.Label, "label2_new");
            Assert.AreEqual(state.ElementAt(1).Value.Url, "url2");

            state.Add("url6", "label6", 6);
            Assert.AreEqual(state.Count(), 3);
            Assert.AreEqual(state.ElementAt(2).Key, 6);
            Assert.AreEqual(state.ElementAt(2).Value.Label, "label6");
            Assert.AreEqual(state.ElementAt(2).Value.Url, "url6");

            state.Add("url5", "label5", 5); //insert url5 before url6
            Assert.AreEqual(state.Count(), 4);
            Assert.AreEqual(state.ElementAt(2).Key, 5);
            Assert.AreEqual(state.ElementAt(2).Value.Label, "label5");
            Assert.AreEqual(state.ElementAt(2).Value.Url, "url5");
            Assert.AreEqual(state.ElementAt(3).Key, 6);
            Assert.AreEqual(state.ElementAt(3).Value.Label, "label6");
            Assert.AreEqual(state.ElementAt(3).Value.Url, "url6");

            state.Add("url5", "label5_new", 5, true);
            Assert.AreEqual(state.Count(), 4);
            Assert.AreEqual(state.ElementAt(2).Key, 5);
            Assert.AreEqual(state.ElementAt(2).Value.Label, "label5");
            Assert.AreEqual(state.ElementAt(2).Value.Url, "url5");

            state.Add("url5", "label5_new", 5);
            Assert.AreEqual(state.Count(), 3);
            Assert.AreEqual(state.ElementAt(2).Key, 5);
            Assert.AreEqual(state.ElementAt(2).Value.Label, "label5_new");
            Assert.AreEqual(state.ElementAt(2).Value.Url, "url5");

            state.Add("url2_new", "label2_new", 2);
            Assert.AreEqual(state.Count(), 4);
            Assert.AreEqual(state.ElementAt(1).Key, 2);
            Assert.AreEqual(state.ElementAt(1).Value.Label, "label2_new");
            Assert.AreEqual(state.ElementAt(1).Value.Url, "url2_new");
        }
    }
}
