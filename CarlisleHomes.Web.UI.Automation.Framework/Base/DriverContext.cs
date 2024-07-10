using OpenQA.Selenium;

namespace CarlisleHomes.Web.UI.Automation.Framework.Base
{
    public static class DriverContext
    {
        //Driver instantiation
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get { return _driver; }
            set { _driver = value; }
        }

        public static Browser Browser { get; set; }
    }
}