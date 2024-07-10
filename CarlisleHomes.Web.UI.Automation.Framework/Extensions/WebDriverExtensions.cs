using CarlisleHomes.Web.UI.Automation.Framework.Base;
using OpenQA.Selenium;
using System.Diagnostics;

namespace CarlisleHomes.Web.UI.Automation.Framework.Extensions
{
    public static class WebDriverExtensions
    {
        public static void WaitForPageLoaded(this IWebDriver driver)
        {
            driver.WaitForCondition(dri =>
            {
                string state = ((IJavaScriptExecutor)dri).ExecuteScript("return document.readyState").ToString();
                return state == "complete";
            }, 10);
        }

        public static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut)
        {
            Func<T, bool> execute =
                (arg) =>
                {
                    try
                    {
                        return condition(arg);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                };
            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < timeOut)
            {
                if (execute(obj))
                {
                    break;
                }
            }
        }

        //Passing the driver handle to different window
        public static void SwitchWindowHandle(this IWebDriver driver)
        {
            string current = DriverContext.Driver.CurrentWindowHandle;
            DriverContext.Driver.SwitchTo().Window(current);
        }
    }
}