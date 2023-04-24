using CustomWeatherClientTool.Models;

namespace CustomWeatherClientToolUnitTest
{
    [TestClass]
    public class TestCityWeather
    {
        [TestMethod]
        public void TestMethod1()
        {
            CityResponse cityResponse = Program.GetCityResponse((decimal)18.9667, (decimal)72.8333);
            Assert.IsNotNull(cityResponse);
        }

        [TestMethod]
        public void TestMethod2()
        {
            CityResponse cityResponse = Program.GetCityResponse(0, 0);
            Assert.AreEqual(cityResponse.timezone, "GMT");
        }
    }
}