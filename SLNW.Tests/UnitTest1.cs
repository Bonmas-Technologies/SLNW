using Microsoft.VisualStudio.TestTools.UnitTesting;
using SLNW;
using SLNW.Core;
using SLNW.Standart;

namespace SLNW.Tests
{
    [TestClass]
    public class Fabric
    {
        [TestMethod]
        public void FabricTest()
        {
            int[] maket = { 4, 8, 4 };

            var networkA = NetworkFabric.CreateNetwork(maket, new Sigmoid(), 0.5);
            var networkB = NetworkFabric.CreateNetwork(maket, new Sigmoid(), 0.5);

            Assert.AreNotSame(networkA, networkB); // idiotic test. i delete this 
        }
    }
}
