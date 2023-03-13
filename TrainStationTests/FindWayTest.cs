using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainStation.Helpers;

namespace TrainStationTests
{
    [TestClass]
    public class FindWayTest
    {
        private FindWayHelper helper;
        [TestInitialize]
        public void Initialize() {
            helper = new FindWayHelper();
        }

        [TestMethod]
        public void TestModelCreatedSuccessfully() {
            Assert.IsNotNull(helper.Graph);
            Assert.IsTrue(helper.Graph.Vertices.Count == 11);
        }

        [TestMethod]
        public void TestShortestPathCalculatedCorrectly() {
            var path = helper.FindShortestWay(0, 10);
            Assert.IsNotNull(path);
            Assert.IsTrue(path.Contains(5));
            Assert.IsTrue(path.Contains(4));
            Assert.IsTrue(path.Contains(10));
            Assert.IsTrue(path.Contains(0));
        }
    }
}
