using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace CarlisleHomes.Web.UI.Automation.Framework.Extensions
{
    public static class ActionsExtensions
    {
        //Handles the exceptions encountered during a double click on an element
        public static void DoubleClickOnElement(this Actions actions, IWebElement element, int retryCount = 3)
        {
            var i = 0;
            while (i <= retryCount)
            {
                try
                {
                    actions.DoubleClick(element).Build().Perform();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    i++;
                }
                catch (ElementClickInterceptedException)
                {
                    i++;
                }
                catch (Exception)
                {
                    i++;
                }
            }
        }
    }
}