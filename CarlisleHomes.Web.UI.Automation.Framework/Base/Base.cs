using TechTalk.SpecFlow;

namespace CarlisleHomes.Web.UI.Automation.Framework.Base
{
    public class Base
    {
        public BasePage CurrentPage
        {
            get
            {
                return (BasePage)ScenarioContext.Current["currentPage"];
            }
            set
            {
                ScenarioContext.Current["currentPage"] = value;
            }
        }

        // Getting the current page instance while carrying out Page Navigation
        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            TPage pageInstance = new TPage()
            {
                Driver = DriverContext.Driver
            };
            return pageInstance;
        }

        public TPage As<TPage>() where TPage : BasePage
        {
            return (TPage)this;
        }
    }
}