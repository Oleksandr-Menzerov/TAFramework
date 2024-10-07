using OpenQA.Selenium;
using Ui.Elements;

namespace Ui.Pages
{
    public class AuthPage : BasePage
    {
        public static Label Description => new(By.XPath(".//*[@class='page-description']"));
        public static string GetDescription() => Description.GetText();
    }
}
