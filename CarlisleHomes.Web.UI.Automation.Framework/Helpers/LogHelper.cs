using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

namespace CarlisleHomes.Web.UI.Automation.Framework.Helpers
{
    public class LogHelper
    {
        public static void SerilogWrite(string logMessage)
        {
            using var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("file.txt")
                   .WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = "753c20ff-5cb0-41fd-93ef-fc9993a0b30e" }, TelemetryConverter.Traces)
                   .CreateLogger();
            log.Information(logMessage);
        }
    }
}