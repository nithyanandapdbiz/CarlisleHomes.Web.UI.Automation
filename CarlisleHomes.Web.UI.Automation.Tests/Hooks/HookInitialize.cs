using CarlisleHomes.Web.UI.Automation.Framework.Base;
using CarlisleHomes.Web.UI.Automation.Framework.Config;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow;

namespace CarlisleHomes.Web.UI.Automation.Tests.Hooks
{
    [Binding]
    public class HookInitialize : TestInitializeHook
    {
        private readonly ScenarioContext _scenarioContext;
        public static ISpecFlowOutputHelper? _specFlowOutputHelper;

        public HookInitialize(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = outputHelper;
        }

        [AfterFeature]
        public static void CleanUp()
        {
            CloseBrowser();
        }

        [BeforeScenario]
        public void Initialize()
        {
            InitializeSettings();
            if (Settings.BrowserStack)
            {
                string scenarioTitle = _scenarioContext.ScenarioInfo.Title;
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DriverContext.Driver;
                JsonObject executorObject = new JsonObject();
                JsonObject argumentsObject = new JsonObject();
                argumentsObject.Add("name", scenarioTitle);
                executorObject.Add("action", "setSessionName");
                executorObject.Add("arguments", argumentsObject);
                jse.ExecuteScript("browserstack_executor: " + executorObject.ToString());
            }
        }

        [AfterScenario]
        public void TestStop()
        {
            //To Close the Browser
            DriverContext.Driver.Quit();
        }
    }
}