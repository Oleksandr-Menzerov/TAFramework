using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace Ui.Elements
{
    public partial class Input(string inputName) : UiElement(By.XPath($"//pet-world-input[.//span[contains(text(), '{inputName}')]]"), "Input")
    {
        private readonly By _inputLocator = By.XPath(".//input");

        private IWebElement GetInputElement()
        {
            var parentElement = GetElement();
            var inputElement = parentElement.FindElement(_inputLocator);
            return inputElement;
        }

        public void EnterText(string text)
        {
            var inputElement = GetInputElement();
            inputElement.Clear();
            inputElement.SendKeys(text);
        }

        public string GetPlaceholder()
        {
            return GetInputElement().GetAttribute("placeholder");
        }

        public string GetInputType()
        {
            return GetInputElement().GetAttribute("type");
        }

        public bool IsStared()
        {
            var parentElement = GetElement();
            try
            {
                _ = parentElement.FindElement(By.XPath(".//span[@class='sterisk']"));
                return true;
            }
            catch(NoSuchElementException)
            { return false; }
        }

        public bool IsValid()
        {
            var inputElement = GetInputElement();
            return inputElement.GetAttribute("class").Contains("ng-valid");
        }

        public bool IsNotValid()
        {
            return !IsValid();
        }

        public string GetInputColor()
        {
            var parentElement = GetElement();
            var styleAttribute = parentElement.FindElement(By.XPath($".//span[contains(text(), '{inputName}')]")).GetAttribute("style");

            var colorMatch = GetColor().Match(styleAttribute);

            if (colorMatch.Success)
            {
                return colorMatch.Groups[1].Value;
            }

            return "Color not found";
        }

        //public void WaitForInputColor(string expectedColor)
        //{
        //    try
        //    {
        //        Wait.Until(driver =>
        //        {
        //            var actualColor = GetInputColor();
        //            return actualColor.Equals(expectedColor);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AssertionException($"Expected color {expectedColor} but actual was {GetInputColor()}. Error message: {ex.Message}");
        //    }
        //}

        [GeneratedRegex(@"(?<!background-)color:\s*(rgb\([\d\s,]+\));")]
        private static partial Regex GetColor();
    }
}
