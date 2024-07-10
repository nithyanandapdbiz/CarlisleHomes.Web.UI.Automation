using OpenQA.Selenium;

namespace CarlisleHomes.Web.UI.Automation.Framework.Base
{
    public class Browser
    {
        private readonly IWebDriver _driver;

        public Browser(IWebDriver driver)
        {
            _driver = driver;
        }

        public BrowserType Type { get; set; }

        public void GoToUrl(string url)
        {
            DriverContext.Driver.Url = url;
        }
    }

    //Supported BrowserTypes
    public enum BrowserType
    {
        Edge,
        FireFox,
        Chrome,
        Safari
    }
}