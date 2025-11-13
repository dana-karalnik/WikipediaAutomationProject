using AventStack.ExtentReports;
using System;
using System.Threading.Tasks;

namespace WikipediaAutomationProject.Helpers
{
    public static class ReportHelper
    {

        /// <summary>
        /// Log step to report
        /// </summary>
        /// <param name="test"></param>
        /// <param name="actionDescription"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static async Task LogStep(ExtentTest test, string actionDescription, Func<Task<bool>> step)
        {
            test.Info($"Starting: {actionDescription}");
            try
            {
                bool result = await step();

                if (result)
                    test.Pass($"{actionDescription} - succeeded");
                else
                    test.Fail($"{actionDescription} - failed");
            }
            catch (Exception ex)
            {
                test.Fail($"{actionDescription} - exception: {ex.Message}");
            }
        }
    }
}
