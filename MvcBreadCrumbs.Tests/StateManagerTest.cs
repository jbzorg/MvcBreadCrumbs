using NUnit.Framework;
using System.Threading.Tasks;

namespace MvcBreadCrumbs.Tests
{
    [TestFixture]
    public class StateManagerTest
    {
        [Test]
        public async Task GetRemoveTest()
        {
            State state = null;

            state = StateManager.GetState("01");
            Assert.AreSame(state, StateManager.GetState("01"));
            StateManager.RemoveState("01");
            Assert.AreNotSame(state, StateManager.GetState("01"));

            state = StateManager.GetState("02");
            Assert.AreSame(state, StateManager.GetState("02", 10));
            await Task.Delay(11);
            Assert.AreNotSame(state, StateManager.GetState("02"));
        }
    }
}
