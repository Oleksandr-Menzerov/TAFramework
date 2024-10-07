namespace UI.Driver.DriverFactories
{
    public static class DriverFactorySelector
    {
        public static IDriverFactory GetDriverFactory()
        {
            return new ChromeDriverFactory();
        }
    }
}
