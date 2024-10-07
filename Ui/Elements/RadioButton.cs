using OpenQA.Selenium;

namespace Ui.Elements
{
    public class RadioButton(string labelName) : UiElement(By.XPath($"//pet-world-radio-button//label[contains(@class, 'petworldlabel') and contains(text(), '{labelName}')]"), "RadioButton")
    {
        public void SelectOption(string optionText)
        {
            var optionLocator = By.XPath($"//mat-radio-button[contains(text(), '{optionText}')]");

            var option = Driver.FindElement(optionLocator);
            if (!option.GetAttribute("class").Contains("checked"))
            {
                option.Click();
            }
        }

        public bool IsOptionSelected(string optionText)
        {
            var optionLocator = By.XPath($"//mat-radio-button[contains(text(), '{optionText}') and contains(@class, 'checked')]");

            try
            {
                var option = GetElement().FindElement(optionLocator);
                return option != null;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }
    }
}
