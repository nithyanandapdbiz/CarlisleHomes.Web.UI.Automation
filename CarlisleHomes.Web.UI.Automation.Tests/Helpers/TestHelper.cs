using CarlisleHomes.Web.UI.Automation.Framework.Base;
using CarlisleHomes.Web.UI.Automation.Framework.Extensions;
using CarlisleHomes.Web.UI.Automation.Framework.Config;
using CarlisleHomes.Web.UI.Automation.Tests.Data.DTOs;
using CarlisleHomes.Web.UI.Automation.Tests.Hooks;
using CsvHelper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using System.Globalization;
using TechTalk.SpecFlow;

namespace CarlisleHomes.Web.UI.Automation.Tests.Helpers
{
    public static class TestHelper
    {
        private static IWebElement uploadtoFilePath => DriverContext.Driver.FindElement(By.XPath("//input[contains(@id, 'FileUploadInput')]"));

        private static readonly int _waitTimeOut = 50;
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;

        private static Stopwatch stopwatch;
        private static List<ElapsedTimeData> elapsedTimesList = new List<ElapsedTimeData>();

        public static void ScrollIntoView(IWebElement webElement)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)DriverContext.Driver;
            je.ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
        }

        // This method is used perform click functionality using java script
        public static void ClickByJavaScriptExecutor(IWebElement iWebElement)
        {
            ((IJavaScriptExecutor)DriverContext.Driver).ExecuteScript("arguments[0].click();", iWebElement);
        }

        /// <summary>
        /// Waits for finding the element.
        /// </summary>
        /// <param name="element">This is HTML element locating mechanism to use.</param>
        /// <param name="timeOut">This is the time to wait for HTML element to appear on the page.</param>
        /// <exception cref="ElementNotVisibleException">If the element not able to visible on the page.</exception>
        /// <exception cref="WebDriverTimeoutException">If the element not able to find in the specified time.</exception>
        /// <returns>True if the element is visible otherwise throws the exception.</returns>
        public static void WaitForElement(IWebElement element, int timeOut = -1)
        {
            bool flag1 = false;
            if (timeOut == -1)
            {
                timeOut = _waitTimeOut;
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeOut));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.Until<IWebElement>((d) =>
                {
                    if (element.Displayed && element.Enabled)
                    {
                        return element;
                    }
                    return null;
                }
                    );
                IsElementDisplayedInPage(element, flag1);
            }
            //Exception Handling
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Check whether element is displayed in web page or not.
        /// </summary>
        /// <param name="element">Element name.</param>
        /// <param name="timeOut">Time out value.</param>
        /// <returns>Status of the element display.</returns>
        public static bool IsElementDisplayedInPage(IWebElement element, bool flag, int timeOut = -1)
        {
            Stopwatch stopWatch = new Stopwatch();
            bool isElementDisplayedInPage = false;

            if (timeOut == -1)
            {
                timeOut = _waitTimeOut;
            }
            if (!flag)
            {
                stopWatch.Start();
            }

            try
            {
                while (stopWatch.Elapsed.TotalSeconds < timeOut)
                {
                    isElementDisplayedInPage = element.Displayed;
                    if (isElementDisplayedInPage)
                    {
                        isElementDisplayedInPage = true;
                        break;
                    }
                }
            }
            //Exception Handling
            catch (NoSuchElementException)
            {
                bool flag2 = true;
                IsElementDisplayedInPage(element, flag2);
            }
            stopWatch.Stop();
            return isElementDisplayedInPage;
        }

        /// <summary>
        /// Wait until the page load.
        /// </summary>
        /// <param name="expectedTitle">Expected page title.</param>
        /// <param name="timeOut">Time out value.</param>
        public static void WaitUntilPageLoadUsingPageTitle(string expectedTitle)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(_waitTimeOut));
                wait.Until(ExpectedConditions.TitleContains(expectedTitle));
            }
            catch (Exception)
            {
            }
        }

        // <summary>
        ///Select File to Upload
        /// </summary>
        /// <param name="expectedTitle">Expected page title.</param>
        /// <param name="timeOut">Time out value.</param>
        public static string GetFullFilePathToUpload(string fileName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            if (Settings.BrowserStack)
            {
                LocalFileDetector detector = new LocalFileDetector();
                var allowsDetection = DriverContext.Driver as IAllowsFileDetection;
                if (allowsDetection != null)
                {
                    allowsDetection.FileDetector = detector;
                }
            }

            return Path.Combine(projectDirectory, @".\Data\Files\" + fileName);
        }

        public static string DragNDropFileUpload(string fileName)
        {
            string fromFilePath = TestHelper.GetFullFilePathToUpload(fileName);
            string script = @"
                var target = arguments[0];
                var file = arguments[1];

                var dropEvent = new DragEvent('drop', {
                    bubbles: true,
                    cancelable: true,
                    dataTransfer: new DataTransfer()
                });

                Object.defineProperty(dropEvent, 'dataTransfer', { value: { files: [file] } });

                target.dispatchEvent(dropEvent);
             ";

            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)DriverContext.Driver;
            javaScriptExecutor.ExecuteScript(script, uploadtoFilePath, fromFilePath);

            return fromFilePath;
        }

        /// <summary>
        /// This method is used to check the element is clickable or not
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsElementClickable(IWebElement element)
        {
            bool status = false;
            String CssValue = element.GetCssValue("cursor");

            if (CssValue == "default")
            {
                status = true;
            }
            return status;
        }

        /// <summary>
        /// Wait until the page load.
        /// </summary>
        /// <param name="expectedURL">Expected URL.</param>
        /// <param name="timeOut">Time out value.</param>
        public static void WaitUntilPageLoadUsingURL(string expectedURL, int timeOut)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeOut));
                wait.Until(ExpectedConditions.UrlContains(expectedURL));
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// This method is used Scroll up
        /// </summary>
        /// <param name="pixelsToScroll">Pixels need to be scroll.</param>
        public static void ScrollUp(int pixelsToScroll)
        {
            try
            {
                int pixel = -pixelsToScroll;
                IJavaScriptExecutor js = (IJavaScriptExecutor)DriverContext.Driver;
                js.ExecuteScript($"window.scrollBy(0, {pixel});");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// This method is used Scroll down
        /// </summary>
        /// <param name="pixelsToScroll">Pixels need to be scroll.</param>
        public static void ScrollDown(int pixelsToScroll)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)DriverContext.Driver;
                js.ExecuteScript($"window.scrollBy(0, {pixelsToScroll});");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// This method is used Clear  value of the field
        /// </summary>
        /// <param name="element">Element of the field.</param>
        public static void ClearValueUsingBackspace(IWebElement element)
        {
            try
            {
                string fieldValue = element.GetAttribute("value");
                for (int i = 0; i < fieldValue.Length + 3; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// This method is used wait untill page load
        /// </summary>
        public static void WaitUntilPageTitleAppears()
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(_waitTimeOut));
            wait.Until(driver =>

            {
                string pageTitle = driver.Title;
                return !string.IsNullOrEmpty(pageTitle);
            });
        }

        /// <summary>
        /// This method is used to get the Attribute value of the field
        /// </summary>
        /// <param name="element">Element of the field.</param>
        /// <param name="attributeName">Name of the Attribute.</param>
        public static string GetAttributeValue(IWebElement element, string attributeName)
        {
            // Use the GetAttribute method to retrieve the attribute value
            return element.GetAttribute(attributeName);
        }

        /// <summary>
        /// This method is used to click on element with retry after exception occurs
        /// </summary>
        /// <param name="element">Element of the field.</param>
        public static void ClickWithRetry(IWebElement element)
        {
            int maxRetries = 3;
            int retries = 0;

            while (retries < maxRetries)
            {
                try
                {
                    // Try to interact with the element
                    Thread.Sleep(2000);
                    WaitForElement(element);
                    ScrollIntoView(element);
                    element.ClickWithRetry();
                    Thread.Sleep(5000);
                    break; // If successful, exit the loop
                }
                catch (NoSuchElementException)
                {
                    // Handle the exception
                    retries++;
                    Thread.Sleep(5000); // Wait for a moment before retrying
                }
                catch (ElementClickInterceptedException)
                {
                    // Handle the exception
                    retries++;
                    Thread.Sleep(5000); // Wait for a moment before retrying
                }
                catch (StaleElementReferenceException)
                {
                    // Handle the exception
                    retries++;
                    Thread.Sleep(5000); // Wait for a moment before retrying
                }
            }
        }

        /// <summary>
        /// This method is used to perform mouse click on element with retry after exception occurs
        /// </summary>
        /// <param name="element">Element of the field.</param>
        public static void PerformMouseClick(IWebElement element)
        {
            // Create an instance of the Actions class
            Actions actions = new Actions(DriverContext.Driver);

            // Perform the mouse click operation
            actions.Click(element).Build().Perform();
        }

        /// <summary>
        /// This method is used to verify the page title
        /// </summary>
        public static void VerifyingPageTitle(string pageName)
        {
            Thread.Sleep(4000);
            WaitUntilPageLoadUsingPageTitle(pageName);
            if (DriverContext.Driver.Title.Equals(pageName))
            {
                Assert.That(true, pageName + " page displayed");
            }
            else
            {
                Assert.That(false, pageName + " page is not displayed");
            }
        }

        /// <summary>
        /// This is Unique Folder Name
        /// </summary>
        private static string UniqueFolderName;

        /// <summary>
        /// This method is used to generate the folder name
        /// </summary>
        private static string GenerateUniqueFolderName()
        {
            // Generate a unique folder name only if it hasn't been generated yet
            if (string.IsNullOrEmpty(UniqueFolderName))
            {
                // Get the current timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                // Generate a random number (you may need a more sophisticated random number generator)
                Random random = new Random();
                int randomNumber = random.Next(1000, 9999); // Change the range as needed

                // Combine timestamp and random number to create a unique folder name
                UniqueFolderName = $"TestRun_{timestamp}_{randomNumber}";
            }

            return UniqueFolderName;
        }

        /// <summary>
        /// This method is used to generate the excel file with .csv extension
        /// </summary>
        public static string CreatingExcelFile()
        {
            // Write elapsed time data row by row to CSV file in the unique folder
            string uniqueFolder = GenerateUniqueFolderName();
            // Get the current timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd");
            // Get the current working directory as the base path
            string basePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            // Combine the base directory path with the "TestResults" folder name
            string testResultsFolderPath = Path.Combine(basePath, "TestResults");
            if (!Directory.Exists(testResultsFolderPath))
            {
                Directory.CreateDirectory(testResultsFolderPath);
            }
            string folderPath = Path.Combine(testResultsFolderPath, uniqueFolder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string csvFilePath = Path.Combine(folderPath, "ElapsedTimes" + timestamp + ".csv");
            return csvFilePath;
        }

        /// <summary>
        /// This method is used to measure the time taken by element to get display
        /// </summary>
        public static void MeasureTimeForElementToBecomeVisible(IWebElement element)
        {
            string currentStepName = ScenarioContext.Current.StepContext.StepInfo.Text;
            // Start the stopwatch to measure time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Wait until the target element becomes visible
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(_waitTimeOut));
            try
            {
                WaitForElement(element);
                wait.Until((d) =>
                {
                    if (element.Displayed && element.Enabled)
                    {
                        stopwatch.Stop();
                        return true; // Stop waiting if element is displayed and enabled
                    }
                    return false; // Continue waiting
                });
            }
            catch (WebDriverTimeoutException)
            {
                // WebDriverTimeoutException will be caught when the element doesn't become displayed and enabled within the specified timeout
            }

            // Calculate the time taken
            TimeSpan timeTaken = stopwatch.Elapsed;
            // Print the time taken
            HookInitialize._specFlowOutputHelper.WriteLine("Time taken to display element in milliseconds: " + timeTaken.TotalMilliseconds);
            HookInitialize._specFlowOutputHelper.WriteLine("Time taken to display element in seconds: " + timeTaken.TotalSeconds);
            WriteElapsedTimeToCsv(CreatingExcelFile(), new List<ElapsedTimeData>
        {
            new ElapsedTimeData
            {
                StepName = currentStepName,
                ElapsedTimeInSeconds = timeTaken.TotalSeconds.ToString(),
                ElapsedTimeInMilliseconds=timeTaken.TotalMilliseconds.ToString()
            }
        });

            Console.WriteLine($"Unique folder created: {CreatingExcelFile()}");
        }

        /// <summary>
        /// This method is used to write the data to excel.csv file
        /// </summary>
        public static void WriteElapsedTimeToCsv(string csvFilePath, List<ElapsedTimeData> dataList)
        {
            // Check if the CSV file exists, if not create it and write the header row
            bool isFileExists = File.Exists(csvFilePath);
            using (var writer = new StreamWriter(csvFilePath, true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (!isFileExists)
                {
                    // Write the header row with column names
                    csv.WriteField("StepName");
                    csv.WriteField("ElapsedTimeInMilliseconds");
                    csv.WriteField("ElapsedTimeInSeconds");
                    csv.NextRecord();
                }
                // Write elapsed time data for each data item
                foreach (var data in dataList)
                {
                    csv.WriteField(data.StepName);
                    csv.WriteField(data.ElapsedTimeInMilliseconds.ToString());
                    csv.WriteField(data.ElapsedTimeInSeconds.ToString());
                    csv.NextRecord();
                }
            }
        }
    }
}