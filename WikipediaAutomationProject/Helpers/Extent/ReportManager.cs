using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace WikipediaAutomationProject.Helpers.Extent
{
    public static class ReportManager
    {
        private static ExtentReports extent;

        public static ExtentReports GetReporter()
        {
            if (extent == null)
            {
                var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName;
                var reportsDir = Path.Combine(projectDir, "Reports");
                Directory.CreateDirectory(reportsDir);

                var reportPath = Path.Combine(reportsDir, $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.html");
                var spark = new ExtentSparkReporter(reportPath);

                spark.Config.DocumentTitle = "Wikipedia Automation Report";
                spark.Config.ReportName = "Wikipedia Assignment Execution Report";

                extent = new ExtentReports();
                extent.AttachReporter(spark);
            }

            return extent;
        }

        public static void Flush()
        {
            if (extent == null)
                return;

            extent.Flush();
        }
    }
}
