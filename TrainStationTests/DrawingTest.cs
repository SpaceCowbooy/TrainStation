using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using TrainStation.Helpers;
using TrainStation.Models;

namespace TrainStationTests
{
    [TestClass]
    public class DrawingTest
    {
        private DrawingHelper helper;
        [TestInitialize]
        public void Initialize() {
            helper = new DrawingHelper();
        }

        [TestMethod]
        public void TestImagesAreCreated() {
            var stationImage = helper.GetStationImage();
            Assert.IsNotNull(stationImage);
            Assert.IsNotNull(stationImage.Drawing);

            var filledParkImage = helper.FillPark(StationStructure.Parks[0], Brushes.Black);
            Assert.IsNotNull(filledParkImage);
            Assert.IsNotNull(filledParkImage.Drawing);

            var shortestWayImage = helper.HighlightShortestWay(new System.Collections.Generic.List<int> { 0, 1, 2 });
            Assert.IsNotNull(shortestWayImage);
            Assert.IsNotNull(shortestWayImage.Drawing);
        }
    }
}
