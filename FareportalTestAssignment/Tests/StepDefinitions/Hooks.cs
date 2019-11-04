using TechTalk.SpecFlow;

namespace FareportalTestAssignment.Tests.StepDefinitions
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeScenario]
        public static void BeforeScenario()
        {
            ScenarioContext.Current[SharedSteps.CURRENT_URL] = Helpers.Helpers.GetUrl();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
