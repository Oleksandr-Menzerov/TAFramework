using Tests.Utils;

namespace Tests.Steps
{
    public class BaseSteps(ScenarioContext scenarioContext)
    {
        internal readonly ScenarioContext _scenarioContext = scenarioContext;
        internal readonly TestDataCleaner _testDataCleaner = new(scenarioContext);
    }
}
