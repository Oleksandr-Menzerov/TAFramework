using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Chips(string chipsName) : UiElement(By.XPath($"//div[contains(@class, 'chips')]//h3[contains(text(), '{chipsName}')]"), "Chips")
    {
        public void ClickButton(string buttonText)
        {
            var buttonLocator = By.XPath($"//button[contains(@class, 'chips') and contains(text(), '{buttonText}')]");
            var button = Driver.FindElement(buttonLocator);
            button.Click();
        }

        public bool IsChipActive(string buttonText)
        {
            var activeButtonLocator = By.XPath($"//button[contains(@class, 'chips active') and contains(text(), '{buttonText}')]");
            return Driver.FindElements(activeButtonLocator).Count != 0;
        }

        public void EnableChip(string buttonText)
        {
            if (!IsChipActive(buttonText))
            {
                ClickButton(buttonText);
            }
        }

        public void DisableChip(string buttonText)
        {
            if (IsChipActive(buttonText))
            {
                ClickButton(buttonText);
            }
        }

        public List<string> GetActiveChips()
        {
            var activeChipsLocator = By.XPath("//button[contains(@class, 'chips active')]");
            var activeChips = Driver.FindElements(activeChipsLocator);
            return activeChips.Select(chip => chip.Text).ToList();
        }

        public List<string> GetAllChips()
        {
            var allChipsLocator = By.XPath("//button[contains(@class, 'chips')]");
            var allChips = Driver.FindElements(allChipsLocator);
            return allChips.Select(chip => chip.Text).ToList();
        }
    }
}
