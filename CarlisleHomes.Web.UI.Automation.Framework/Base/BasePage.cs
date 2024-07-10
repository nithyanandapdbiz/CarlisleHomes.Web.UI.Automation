using OpenQA.Selenium;

namespace CarlisleHomes.Web.UI.Automation.Framework.Base
{
    public abstract class BasePage : Base
    {
        //Initialize Page
        public IWebDriver Driver { get; set; }
    }
}