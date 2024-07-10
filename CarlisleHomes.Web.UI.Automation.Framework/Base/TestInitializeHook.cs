using CarlisleHomes.Web.UI.Automation.Framework.Config;
using CarlisleHomes.Web.UI.Automation.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;

namespace CarlisleHomes.Web.UI.Automation.Framework.Base
{
    public abstract class TestInitializeHook : Base
    {
        public void InitializeSettings()
        {
            //Set all the settings for framework
            ConfigReader.SetFrameworkSettings();

            //Converting string to enum
            BrowserType browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Settings.Browser, true);

            //Open Browser
            OpenBrowser(browserType);

            LogHelper.SerilogWrite("Initialized Framework !!!");
            //Maximize the window
            DriverContext.Driver.Manage().Window.Maximize();
            //Page Loag timeouts
            DriverContext.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Settings.PageLoadTimeout);
            DriverContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Settings.ImplicitWaitTimeout);
        }

        //Defining different types of Drivers
        private void OpenBrowser(BrowserType browserType)
        {
            String buildName = Environment.GetEnvironmentVariable("BROWSERSTACK_BUILD_NAME", EnvironmentVariableTarget.Process);
            if (Settings.BrowserStack)
            {
                Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
                browserstackOptions.Add("platformName", "windows");
                browserstackOptions.Add("local", "true");
                switch (browserType)
                {
                    case BrowserType.Chrome: //If browser is Chrome, following capabilities will be passed to 'executetestwithcaps' function
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.BrowserVersion = "126.0";
                        Dictionary<string, object> browserstackOptionsChrome = new Dictionary<string, object>();
                        browserstackOptionsChrome.Add("os", "Windows");
                        browserstackOptionsChrome.Add("osVersion", "11");
                        browserstackOptionsChrome.Add("local", "false");
                        browserstackOptionsChrome.Add("buildName", buildName);
                        browserstackOptionsChrome.Add("userName", Settings.BrowserStackUsername);
                        browserstackOptionsChrome.Add("accessKey", Settings.BrowserStackAccessKey);
                        browserstackOptionsChrome.Add("networkLogs", "true");
                        chromeOptions.AddAdditionalOption("bstack:options", browserstackOptionsChrome);
                        IWebDriver chromedriver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), chromeOptions);
                        DriverContext.Driver = chromedriver;
                        DriverContext.Browser = new Browser(chromedriver);
                        break;

                    case BrowserType.FireFox: //If browser is Firefox, following capabilities will be passed to 'executetestwithcaps' function
                        FirefoxOptions firefoxOptions = new FirefoxOptions();
                        firefoxOptions.BrowserVersion = "120.0";
                        Dictionary<string, object> browserstackOptionsFirefox = new Dictionary<string, object>();
                        browserstackOptionsFirefox.Add("os", "Windows");
                        browserstackOptionsFirefox.Add("osVersion", "11");
                        browserstackOptionsFirefox.Add("local", "false");
                        browserstackOptionsFirefox.Add("buildName", buildName);
                        browserstackOptionsFirefox.Add("userName", Settings.BrowserStackUsername);
                        browserstackOptionsFirefox.Add("accessKey", Settings.BrowserStackAccessKey);
                        browserstackOptionsFirefox.Add("networkLogs", "true");
                        firefoxOptions.AddAdditionalOption("bstack:options", browserstackOptionsFirefox);
                        IWebDriver firefoxdriver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), firefoxOptions);
                        DriverContext.Driver = firefoxdriver;
                        DriverContext.Browser = new Browser(firefoxdriver);
                        break;

                    case BrowserType.Edge: //If browser is Edge, following capabilities will be passed to 'executetestwithcaps' function
                        EdgeOptions edgeOptions = new EdgeOptions();
                        edgeOptions.BrowserVersion = "126.0";
                        Dictionary<string, object> browserstackOptionsEdge = new Dictionary<string, object>();
                        browserstackOptionsEdge.Add("os", "Windows");
                        browserstackOptionsEdge.Add("osVersion", "11");
                        browserstackOptionsEdge.Add("local", "false");
                        browserstackOptionsEdge.Add("buildName", buildName);
                        browserstackOptionsEdge.Add("userName", Settings.BrowserStackUsername);
                        browserstackOptionsEdge.Add("accessKey", Settings.BrowserStackAccessKey);
                        browserstackOptionsEdge.Add("networkLogs", "true");
                        edgeOptions.AddAdditionalOption("bstack:options", browserstackOptionsEdge);
                        IWebDriver edgedriver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), edgeOptions);
                        DriverContext.Driver = edgedriver;
                        DriverContext.Browser = new Browser(edgedriver);
                        break;

                    case BrowserType.Safari: //If browser is Safari, following capabilities will be passed to 'executetestwithcaps' function
                        SafariOptions safariOptions = new SafariOptions();
                        safariOptions.BrowserVersion = "16.5";
                        Dictionary<string, object> browserstackOptionsSafari = new Dictionary<string, object>();
                        browserstackOptionsSafari.Add("os", "OS X");
                        browserstackOptionsSafari.Add("osVersion", "Ventura");
                        browserstackOptionsSafari.Add("local", "false");
                        browserstackOptionsSafari.Add("buildName", buildName);
                        browserstackOptionsSafari.Add("userName", Settings.BrowserStackUsername);
                        browserstackOptionsSafari.Add("accessKey", Settings.BrowserStackAccessKey);
                        browserstackOptionsSafari.Add("networkLogs", "true");
                        safariOptions.AddAdditionalOption("bstack:options", browserstackOptionsSafari);
                        IWebDriver safaridriver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), safariOptions);
                        DriverContext.Driver = safaridriver;
                        DriverContext.Browser = new Browser(safaridriver);
                        break;

                    default: //With default browser as Chrome, following capabilities will be passed to 'executetestwithcaps' function
                        ChromeOptions chromeOptions1 = new ChromeOptions();
                        chromeOptions1.BrowserVersion = "126.0";
                        Dictionary<string, object> browserstackOptionsDefault = new Dictionary<string, object>();
                        browserstackOptionsDefault.Add("os", "Windows");
                        browserstackOptionsDefault.Add("osVersion", "11");
                        browserstackOptionsDefault.Add("local", "false");
                        browserstackOptionsDefault.Add("buildName", buildName);
                        browserstackOptionsDefault.Add("userName", Settings.BrowserStackUsername);
                        browserstackOptionsDefault.Add("accessKey", Settings.BrowserStackAccessKey);
                        chromeOptions1.AddAdditionalOption("bstack:options", browserstackOptionsDefault);
                        IWebDriver chromedriver1 = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), chromeOptions1);
                        DriverContext.Driver = new ChromeDriver();
                        DriverContext.Browser = new Browser(chromedriver1);
                        break;
                }
            }
            else
            {
                switch (browserType)
                {
                    case BrowserType.Chrome:
                        DriverContext.Driver = new ChromeDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        break;

                    case BrowserType.FireFox:
                        DriverContext.Driver = new FirefoxDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        break;

                    case BrowserType.Edge:
                        DriverContext.Driver = new EdgeDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        break;

                    case BrowserType.Safari:
                        DriverContext.Driver = new SafariDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        break;

                    default:
                        DriverContext.Driver = new ChromeDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        break;
                }
            }
        }

        private static void executetestwithcaps(DriverOptions capability)
        {
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>();
            // Starts the Local instance with the required arguments
            bsLocalArgs.Add(new KeyValuePair<string, string>("key", Settings.BrowserStackAccessKey));

            IWebDriver driver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), capability);
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://mcmillanllptrial-tst.outsystemsenterprise.com/SPOnlineSetupMaxxia/startapplication.aspx?issagov=false");
                driver.FindElement(By.LinkText("Login")).Click();
                driver.FindElement(By.Id("DigitalMaxxia_Theme_wt38_block_wtMainContent_wt24_wtLoginLayoutInput_wt7_wtInput_wtInteractiveInputBox")).SendKeys("test2@mmsg.com.au");
                driver.FindElement(By.Id("DigitalMaxxia_Theme_wt38_block_wtMainContent_wt24_wtLoginLayoutInput_wt32_wtInput_wtInteractivePasswordTextbox")).SendKeys("Password123");
                driver.FindElement(By.Id("DigitalMaxxia_Theme_wt38_block_wtMainContent_wt24_wtLoginLayoutButton_wtLoginButton")).Click();
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \" Some elements failed to load.\"}}");
            }
            CloseBrowser();
        }

        //Close the Browser
        public static void CloseBrowser()
        {
            DriverContext.Driver.Quit();
        }
    }
}