namespace CarlisleHomes.Web.UI.Automation.Framework.Config
{
    public class Settings
    {
        public static string TestType { get; set; }
        public static string AUT { get; set; }
        public static string IsLog { get; set; }
        public static string Browser { get; set; }
        public static double PageLoadTimeout { get; set; }
        public static double ImplicitWaitTimeout { get; set; }
        public static double ExplicitWaitTimeout { get; set; }
        public static double FluentWaitTimeout { get; set; }
        public static Boolean BrowserStack { get; set; }
        public static string BrowserStackUsername { get; set; }
        public static string BrowserStackAccessKey { get; set; }
    }
}