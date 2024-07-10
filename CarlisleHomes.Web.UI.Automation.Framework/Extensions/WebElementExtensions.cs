using CarlisleHomes.Web.UI.Automation.Framework.Base;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CarlisleHomes.Web.UI.Automation.Framework.Extensions
{
    public static class WebElementExtensions
    {
        public static string GetSelectedDropDown(this IWebElement element)
        {
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions.First().ToString();
        }

        public static IList<IWebElement> GetSelectedListOptions(this IWebElement element)
        {
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions;
        }

        public static string GetLinkText(this IWebElement element)
        {
            return element.Text;
        }

        public static void SelectDropDownList(this IWebElement element, string value)
        {
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByText(value);
        }

        public static void AssertElementPresent(this IWebElement element)
        {
            if (!IsElementPresent(element))
                throw new Exception(string.Format("Element Not Present Exception"));
        }

        public static bool IsElementDisplayed(this IWebElement element)
        {
            try
            {
                bool ele = element.Displayed;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsElementPresent(this IWebElement element)
        {
            try
            {
                Size size = element.Size;
                if (size.Width > 0 || size.Height > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Handles exceptions encountered while waiting for element to be clickable
        public static void WaitTillElementClickable(this IWebElement element, int retryCount = 3)
        {
            var i = 0;
            while (i <= retryCount)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(DriverContext.Driver, new TimeSpan(0, 0, 5));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
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
                catch (ElementNotInteractableException)
                {
                    i++;
                }
                catch (Exception)
                {
                    i++;
                }
            }
        }

        //Handles exception by introducing clicks with retries
        public static void ClickWithRetry(this IWebElement element, int retryCount = 3)
        {
            var i = 0;
            while (i <= retryCount)
            {
                try
                {
                    element.Click();
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
                catch (ElementNotInteractableException)
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