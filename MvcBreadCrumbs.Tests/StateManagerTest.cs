using NUnit.Framework;

namespace MvcBreadCrumbs.Tests
{
    [TestFixture]
    public class StateManagerTest
    {
        [Test]
        public void GetRemoveTest()
        {
            State state = StateManager.GetState("01");
            Assert.AreSame(state, StateManager.GetState("01"));
            StateManager.RemoveState("01");
            Assert.AreNotSame(state, StateManager.GetState("01"));
        }
    }
}
