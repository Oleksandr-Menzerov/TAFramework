using System.Collections;
using System.Collections.Specialized;

namespace Tests.Utils
{
    public static class RetryCounter
    {
        [ThreadStatic] // Each thread has unique dictionary
        private static Dictionary<string, int>? _testRetries;

        public static void IncreaseRetryCount(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var identifier = FormatTestIdentifier(featureContext, scenarioContext);
            _testRetries ??= [];
            if (_testRetries!.TryGetValue(identifier, out int value))
            {
                _testRetries[identifier] = value + 1;
            }
            else
            {
                _testRetries[identifier] = 1;
            }
        }

        //private static ConcurrentDictionary<string, int>? _testRetries; // One dictionary for all threads
        //public static void IncreaseRetryCount(FeatureContext featureContext, ScenarioContext scenarioContext)
        //{
        //    var identifier = FormatTestIdentifier(featureContext, scenarioContext);
        //    _testRetries ??= new();
        //    _testRetries.AddOrUpdate(identifier, 1, (key, value) => value + 1);
        //}

        public static int GetRetryCount(string identifier)
        {
            if (_testRetries == null)
            {
                throw new InvalidOperationException("Test retries dictionary was not initialised properly.");
            }
            _testRetries.TryGetValue(identifier, out var retryCount);
            return retryCount;
        }

        public static string FormatTestIdentifier(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            var testName = scenarioContext.ScenarioInfo.Title;
            var arguments = FormatArguments(scenarioContext.ScenarioInfo.Arguments);

            if (!string.IsNullOrEmpty(arguments))
            {
                return $"{featureTitle}::{testName}({arguments})";
            }
            else
            {
                return $"{featureTitle}::{testName}";
            }
        }

        private static string FormatArguments(IOrderedDictionary arguments)
        {
            return string.Join(", ", arguments.Cast<DictionaryEntry>()
                .Select(entry => $"{entry.Key}: {entry.Value}"));
        }

        internal static string GetRetryCountReport(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var identifier = FormatTestIdentifier(featureContext, scenarioContext);
            var retryCount = GetRetryCount(identifier);

            if (retryCount > 1)
            {
                return $"Test '{identifier}' was retried {retryCount} times.";
            }
            return string.Empty;
        }
    }
}
