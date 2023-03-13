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
		public void Initialize()
		{
			helper = new DrawingHelper();
		}
		[TestMethod]
		public void TestImagesAreCreated()
		{
			var image1 = helper.GetStationImage();
			Assert.IsNotNull(image1);
			Assert.IsNotNull(image1.Drawing);

			var image2 = helper.FillPark(StationStructure.Parks[0], Brushes.Black);
			Assert.IsNotNull(image2);
			Assert.IsNotNull(image2.Drawing);

			var image3 = helper.HighlightShortestWay(new System.Collections.Generic.List<int> { 0, 1, 2 });
			Assert.IsNotNull(image3);
			Assert.IsNotNull(image3.Drawing);
		}
	}
}
