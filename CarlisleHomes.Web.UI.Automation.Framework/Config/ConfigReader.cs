using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlisleHomes.Web.UI.Automation.Framework.Config
{
    public class ConfigReader
    {
        private static double default_pagetimeout = 20;
        private static double default_implicittimeout = 10;
        private static double default_explicittimeout = 10;
        private static double default_fluenttimeout = 10;
        private static bool default_browserstack = false;

        public static void SetFrameworkSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = builder.Build();
            //AUT: Application Under Test
            Settings.AUT = (Environment.GetEnvironmentVariable("AUT", EnvironmentVariableTarget.Process) != null) ? Convert.ToString(Environment.GetEnvironmentVariable("AUT", EnvironmentVariableTarget.Process)) : Convert.ToString(configurationRoot.GetSection("testSettings").Get<TestSettings>().AUT);

            //TestType: Type of testing
            Settings.TestType = configurationRoot.GetSection("testSettings").Get<TestSettings>().TestType;
            //IsLog: Logging to be enabled or disabled
            Settings.IsLog = configurationRoot.GetSection("testSettings").Get<TestSettings>().IsLog;
            //Browser: Browser type to be defined
            Settings.Browser = (Environment.GetEnvironmentVariable("Browser", EnvironmentVariableTarget.Process) != null) ? (Environment.GetEnvironmentVariable("Browser", EnvironmentVariableTarget.Process)) : configurationRoot.GetSection("testSettings").Get<TestSettings>().Browser;
            //PageLoadTimeout: Takes a default value if not defined in the test project
            Settings.PageLoadTimeout = configurationRoot.GetSection("testSettings").Get<TestSettings>().PageLoadTimeout != null ? configurationRoot.GetSection("testSettings").Get<TestSettings>().PageLoadTimeout : default_pagetimeout;
            //ImplicitWaitTimeout: Takes a default value if not defined in the test project
            Settings.ImplicitWaitTimeout = configurationRoot.GetSection("testSettings").Get<TestSettings>().ImplicitWaitTimeout != null ? configurationRoot.GetSection("testSettings").Get<TestSettings>().ImplicitWaitTimeout : default_implicittimeout;
            //ExplicitWaitTimeout: Takes a default value if not defined in the test project
            Settings.ExplicitWaitTimeout = configurationRoot.GetSection("testSettings").Get<TestSettings>().ExplicitWaitTimeout != null ? configurationRoot.GetSection("testSettings").Get<TestSettings>().ExplicitWaitTimeout : default_explicittimeout;
            //FluentWaitTimeout: Takes a default value if not defined in the test project
            Settings.FluentWaitTimeout = configurationRoot.GetSection("testSettings").Get<TestSettings>().FluentWaitTimeout != null ? configurationRoot.GetSection("testSettings").Get<TestSettings>().FluentWaitTimeout : default_fluenttimeout;
            //BrowserStack: Takes boolean value to define whether to use BrowserStack platform for script execution or not
            Settings.BrowserStack = configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserStack != null ? configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserStack : default_browserstack;
            //BrowserStackUsername and BrowserStackAccessKey: BrowserStack login credentials
            Settings.BrowserStackUsername = Settings.BrowserStack ? configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserStackUsername : "";
            Settings.BrowserStackAccessKey = Settings.BrowserStack ? configurationRoot.GetSection("testSettings").Get<TestSettings>().BrowserStackAccessKey : "";
        }
    }
}