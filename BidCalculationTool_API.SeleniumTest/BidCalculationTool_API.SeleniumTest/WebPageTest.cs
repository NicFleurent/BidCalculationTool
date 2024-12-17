using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BidCalculationTool_API.SeleniumTest
{
    /* I made this separate projet because I didn't figure out how to make
     * the API and WebPage run automatically during the test. I know this
     * is not the best possible practice but, to run those test, you
     * need to start the vue.js page and asp.net API manually beforehand. 
     */
    public class WebPageTest : IClassFixture<GoogleChromeFixture>
    {
        private readonly GoogleChromeFixture _fixture;
        public WebPageTest(GoogleChromeFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("398", "common", "39.80", "7.96", "5", "100", "550.76")]
        [InlineData("501", "common", "50", "10.02", "10", "100", "671.02")]
        [InlineData("57", "common", "10", "1.14", "5", "100", "173.14")]
        [InlineData("1100", "common","50" , "22", "15", "100", "1287")]
        [InlineData("1800", "luxury", "180", "72", "15", "100", "2167")]
        [InlineData("1000000", "luxury", "200", "40000", "20", "100", "1040320")]
        public void GivenValidEntry_ShowFeesAndPrices
            (
                string basePrice,
                string bidType,
                string basicFee,
                string specialFee,
                string associationFee,
                string storageFee,
                string totalPrice
            )
        {
            //Arrange
            _fixture.driver.Navigate().GoToUrl("http://localhost:5173/");

            var basePriceElement = _fixture.driver.FindElement(By.Id("basePrice"));
            var bidTypeElement = _fixture.driver.FindElement(By.Id("bidType"));

            //Act
            string basePriceString = basePrice;
            foreach (var key in basePriceString)
            {
                basePriceElement.SendKeys(key.ToString());
            }
            bidTypeElement.SendKeys(bidType);

            //Assert
            var basePriceResult = _fixture.driver.FindElement(By.Id("basePriceResult"));
            var basicFeeResult = _fixture.driver.FindElement(By.Id("basicFeeResult"));
            var specialFeeResult = _fixture.driver.FindElement(By.Id("specialFeeResult"));
            var associationFeeResult = _fixture.driver.FindElement(By.Id("associationFeeResult"));
            var storageFeeResult = _fixture.driver.FindElement(By.Id("storageFeeResult"));
            var TotalPriceResult = _fixture.driver.FindElement(By.Id("TotalPriceResult"));
            Assert.Contains(basePrice, basePriceResult.Text);
            Assert.Contains(basicFee, basicFeeResult.Text);
            Assert.Contains(specialFee, specialFeeResult.Text);
            Assert.Contains(associationFee, associationFeeResult.Text);
            Assert.Contains(storageFee, storageFeeResult.Text);
            Assert.Contains(totalPrice, TotalPriceResult.Text);
        }

        [Fact]
        public void GivenInvalidEntry_ShowError()
        {
            //Arrange
            _fixture.driver.Navigate().GoToUrl("http://localhost:5173/");

            var basePriceElement = _fixture.driver.FindElement(By.Id("basePrice"));

            //Act
            string basePriceString = "0.5";
            foreach (var key in basePriceString)
            {
                basePriceElement.SendKeys(key.ToString());
            }

            //Assert
            var errorResult = _fixture.driver.FindElement(By.Id("errorResult"));
            Assert.Contains("Error: HTTP error! Status: 400", errorResult.Text);
        }
    }

    public class GoogleChromeFixture : IDisposable
    {
        public IWebDriver driver { get; }

        public GoogleChromeFixture()
        {
            driver = new ChromeDriver();
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
