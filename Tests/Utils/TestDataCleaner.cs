namespace Tests.Utils
{
    /// <summary>
    /// Manages and executes cleanup actions for test scenarios.
    /// Initializes a new instance of the <see cref="TestDataCleaner"/> class.
    /// </summary>
    /// <param name="scenarioContext">The <see cref="ScenarioContext"/> for storing and retrieving cleanup actions.</param>

    public class TestDataCleaner(ScenarioContext scenarioContext)
    {
        private const string CleanupActionsKey = "CleanupActions";
        private readonly ScenarioContext _scenarioContext = scenarioContext;

        /// <summary>
        /// Adds a cleanup action to the list of actions to be executed.
        /// </summary>
        /// <param name="action">The cleanup action to add.</param>
        /// <param name="identifier">A unique identifier for the action, used to remove it later if needed.</param>
        public void AddCleanupAction(Func<Task> action, string identifier)
        {
            var cleanupActions = GetCleanupActions();
            cleanupActions.Add((action, identifier));
            _scenarioContext[CleanupActionsKey] = cleanupActions;
        }

        /// <summary>
        /// Removes a specific cleanup action from the list based on its identifier.
        /// </summary>
        /// <param name="identifier">The unique identifier of the cleanup action to remove.</param>
        public void RemoveCleanupAction(string identifier)
        {
            var cleanupActions = GetCleanupActions();
            cleanupActions.RemoveAll(x => x.Identifier == identifier);
            _scenarioContext[CleanupActionsKey] = cleanupActions;
        }

        /// <summary>
        /// Executes all registered cleanup actions and returns a list of any errors encountered during execution.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is a list of error messages encountered during cleanup.</returns>
        public async Task<List<string>> CleanUpAsync()
        {
            var errors = new List<string>();
            var cleanupActions = GetCleanupActions();

            foreach (var (action, identifier) in cleanupActions)
            {
                try
                {
                    await action();
                }
                catch (Exception ex)
                {
                    errors.Add($"Error during cleanup for {identifier}: {ex.Message}");
                }
            }

            // Clear the list of cleanup actions after execution.
            _scenarioContext[CleanupActionsKey] = new List<(Func<Task> Action, string Identifier)>();
            return errors;
        }

        /// <summary>
        /// Retrieves the list of cleanup actions from the <see cref="ScenarioContext"/>.
        /// If no actions are found, an empty list is created and stored.
        /// </summary>
        /// <returns>The list of cleanup actions.</returns>
        private List<(Func<Task> Action, string Identifier)> GetCleanupActions()
        {
            if (!_scenarioContext.TryGetValue(CleanupActionsKey, out List<(Func<Task> Action, string Identifier)> cleanupActions))
            {
                cleanupActions = [];
                _scenarioContext[CleanupActionsKey] = cleanupActions;
            }
            return cleanupActions;
        }
    }
}
