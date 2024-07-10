using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlisleHomes.Web.UI.Automation.Framework.Config
{
    [JsonObject("testSettings")]
    public class TestSettings
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("aut")]
        public string AUT { get; set; }

        [JsonProperty("testType")]
        public string TestType { get; set; }

        [JsonProperty("isLog")]
        public string IsLog { get; set; }

        [JsonProperty("Browser")]
        public string Browser { get; set; }

        [JsonProperty("pageLoadTimeout")]
        public double PageLoadTimeout { get; set; }

        [JsonProperty("implicitWaitTimeout")]
        public double ImplicitWaitTimeout { get; set; }

        [JsonProperty("explicitWaitTimeout")]
        public double ExplicitWaitTimeout { get; set; }

        [JsonProperty("fluentWaitTimeout")]
        public double FluentWaitTimeout { get; set; }

        [JsonProperty("browserStack")]
        public Boolean BrowserStack { get; set; }

        [JsonProperty("browserStackUsername")]
        public string BrowserStackUsername { get; set; }

        [JsonProperty("browserStackAccessKey")]
        public string BrowserStackAccessKey { get; set; }
    }
}