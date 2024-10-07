using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Checkbox(string checkboxName) : UiElement(By.XPath($"//pet-world-checkbox[contains(., '{checkboxName}')]"), "Checkbox")
    {
        private readonly By _inputLocator = By.XPath(".//input");
        private readonly By _labelLocator = By.XPath(".//label[@for='checkbox']");

        private IWebElement GetInputElement()
        {
            var parentElement = GetElement();
            var inputElement = parentElement.FindElement(_inputLocator);
            return inputElement;
        }

        public Label GetLabel()
        {
            return new Label(GetElement().FindElement(_labelLocator));
        }

        public bool IsChecked()
        {
            var inputElement = GetInputElement();
            return inputElement.Selected;
        }

        public void Check()
        {
            if (!IsChecked())
            {
                Click();
            }
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                Click();
            }
        }

        public bool IsValid()
        {
            var inputElement = GetInputElement();
            return inputElement.GetAttribute("class").Contains("ng-valid");
        }

        public bool IsTouched()
        {
            var inputElement = GetInputElement();
            return inputElement.GetAttribute("class").Contains("ng-touched");
        }

        public override void Click()
        {
            ClickOn();
        }

        public override void ClickOn()
        {
            GetLabel().ClickOn();
        }
    }
}
