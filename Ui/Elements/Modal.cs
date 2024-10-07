using NUnit.Framework;
using OpenQA.Selenium;

namespace Ui.Elements
{
    public class Modal : UiElement
    {
        private static readonly By _modalLocator = By.CssSelector("pet-world-modal-wrapper");
        private static readonly By _closeButtonLocator = By.CssSelector("button.close-button");
        private static readonly By _headerLocator = By.CssSelector("h2.modal-header");
        private static readonly By _descriptionLocator = By.CssSelector("p.modal-description");
        private static readonly string _successClass = "confirm--success";

        public Modal() : base(_modalLocator, "Modal") { }

        public Button GetCloseButton()
        {
            return new Button(GetElement().FindElement(_closeButtonLocator));
        }

        public Header GetModalHeader()
        {
            return new Header(GetElement().FindElement(_headerLocator));
        }

        public string GetHeaderText()
        {
            return GetModalHeader().GetText();
        }

        public Label GetModalDescription()
        {
            return new Label(GetElement().FindElement(_descriptionLocator));
        }

        public string GetDescriptionText()
        {
            return GetModalDescription().GetText();
        }

        public bool IsSuccessModal()
        {
            try
            {
                var element = GetElement();
                var classAttribute = element.GetAttribute("class");
                return classAttribute.Contains(_successClass);
            }
            catch (Exception ex)
            {
                throw new AssertionException(ex.Message);
            }
        }

        public void CloseModal()
        {
            GetCloseButton().Click();
        }

        public void ClickOutOfTheModal()
        {
            GetCloseButton().MoveAndClick(50, -50);
        }
    }
}
