using Core.Extensions;
using FluentAssertions.Execution;
using Tests.Utils;
using Ui.Elements;

namespace Tests.Steps.Ui
{
    [Binding]
    [Parallelizable(ParallelScope.All)]
    public class InteractionSteps(ScenarioContext scenarioContext) : BaseUiSteps(scenarioContext)
    {
        [When(@"I (click|click on) the '(.*)' (button|link|label|header|input|dropdown|checkbox)")]
        public void WhenIClickThe(string clickType, string name, string type)
        {
            UiElement element = GetElementByType(name, type);

            if (clickType == "click")
            {
                element.Click();
            }
            else
            {
                element.ClickOn();
            }
        }

        [When(@"I (check|uncheck) the '(.*)' checkbox")]
        public void WhenICheckUncheckTheCheckbox(string check, string name)
        {
            switch (check)
            {
                case "check":
                    _basePage.GetCheckbox(name).Check();
                    break;
                default:
                    _basePage.GetCheckbox(name).Uncheck();
                    break;
            }
        }

        [Then(@"I should see the '(.*)' (button|link|label|checkbox|dropdown|input|radiobutton)")]
        public void ThenIShouldSee(string name, string type)
        {
            UiElement element = GetElementByType(name.ReplaceDynamicValues(), type);
            Assert.That(element.IsDisplayed());
        }

        [Then(@"I should see the following elements")]
        public void ThenIShouldSeeTheFollowingElements(Table table)
        {
            using (new AssertionScope())
            {
                foreach (var row in table.Rows)
                {
                    var type = row["Type"];
                    var name = row["Name"];

                    UiElement element = GetElementByType(name, type);
                    Assert.That(element.IsDisplayed(), $"Expected {type} '{name}' to be displayed.");
                }
            }
        }

        [When(@"I enter '(.*)' in the input '(.*)'")]
        public void WhenIEnterInThe(string text, string name)
        {
            var input = _basePage.GetInput(name);
            input.EnterText(text);
        }

        [When(@"I select the '(.*)' option in the dropdown '(.*)'")]
        public void WhenISelectTheOptionInTeDropdown(string option, string name)
        {
            var dropdown = _basePage.GetNamedDropdown(name);
            dropdown.SearchAndSelect(option);
        }

        [When(@"I enter the following data in the inputs")]
        public void WhenIEnterTheFollowingDataInTheInputs(Table table)
        {
            table.FillAppSettingsFields();
            table.FillRandomFields();
            foreach (var row in table.Rows)
            {
                var fieldName = row["Field"];
                var fieldValue = row["Value"];

                var input = _basePage.GetInput(fieldName);
                input.EnterText(fieldValue);
            }
        }

        [Then(@"the input '(.*)' should have color '(.*)'")]
        public void ThenTheInputShouldHaveColor(string name, string expectedColor)
        {
            //var input = _basePage.GetInput(name);
            //input.WaitForInputColor(expectedColor);

            var input = _basePage.GetInput(name);
            var actualColor = input.GetInputColor();

            Assert.That(actualColor, Is.EqualTo(expectedColor), $"Expected color for input '{name}' to be '{expectedColor}', but was '{actualColor}'.");
        }

        [Then(@"the input '(.*)' should be (valid|invalid)")]
        public void ThenTheInputShouldBeValidInvalid(string name, string valid)
        {
            var input = _basePage.GetInput(name);
            if (valid == "valid")
            {
                Assert.That(input.IsValid(), $"Expected input '{name}' to be valid, but it was invalid.");
            }
            else 
            {
                Assert.That(input.IsNotValid(), $"Expected input '{name}' to be invalid, but it was valid.");
            }
        }

        [Then(@"the button '(.*)' should be (enabled|disabled)")]
        public void ThenTheButtonShouldBeEnabledDisabled(string name, string enabled)
        {
            var button = _basePage.GetButton(name);
            if (enabled == "enabled")
            {
                Assert.That(button.IsEnabled(), $"Expected button '{name}' to be enabled, but it was disabled.");
            }
            else
            {
                Assert.That(button.IsDisabled(), $"Expected button '{name}' to be disabled, but it was enabled.");
            }
        }

        [Then(@"I should see a (successful|unsuccessful) modal with header '(.*)' and description '(.*)'")]
        public void ThenIShouldSeeAModalWithHeaderAndDescription(string status, string expectedHeader, string expectedDescription)
        {
            var modal = _basePage.GetModal();

            bool isSuccessModal = modal.IsSuccessModal();
            using (new AssertionScope())
            {
                if (status == "successful")
                {
                    Assert.That(isSuccessModal, Is.True, "Expected a successful modal, but it was not successful.");
                }
                else
                {
                    Assert.That(isSuccessModal, Is.False, "Expected an unsuccessful modal, but it was successful.");
                }

                var closeButton = modal.GetCloseButton();
                Assert.That(closeButton.IsDisplayed(), "Expected the close button to be displayed, but it was not.");

                string actualHeader = modal.GetHeaderText();
                Assert.That(actualHeader, Is.EqualTo(expectedHeader), $"Expected modal header to be '{expectedHeader}', but was '{actualHeader}'.");

                string actualDescription = modal.GetDescriptionText();
                Assert.That(actualDescription, Is.EqualTo(expectedDescription), $"Expected modal description to be '{expectedDescription}', but was '{actualDescription}'.");
            }
        }

        [Then(@"I should see a (successful|unsuccessful) modal")]
        public void ThenIShouldSeeAModal(string status)
        {
            var modal = _basePage.GetModal();

            bool isSuccessModal = modal.IsSuccessModal();
            using (new AssertionScope())
            {
                if (status == "successful")
                {
                    Assert.That(isSuccessModal, "Expected a successful modal, but it was not successful.");
                }
                else
                {
                    Assert.That(!isSuccessModal, "Expected an unsuccessful modal, but it was successful.");
                }

                var closeButton = modal.GetCloseButton();
                Assert.That(closeButton.IsDisplayed(), "Expected the close button to be displayed, but it was not.");
            }
        }

        [When(@"I close the modal")]
        public void WhenICloseTheModal()
        {
            _basePage.GetModal().CloseModal();
        }

        [When(@"I click out of the modal")]
        public void WhenIClickOutOfTheModal()
        {
            _basePage.GetModal().ClickOutOfTheModal();
        }

        [Then(@"I should (see|not see) the modal")]
        public void ThenIShouldSeeNotSeeTheModal(string see)
        {
            _basePage.WaitForLoaderToDisappear();
            var modal = _basePage.GetModal();

            if (see == "see")
            {
                Assert.That(modal.IsDisplayed(), "Expected that modal to be displayed, but it was not.");
                var closeButton = modal.GetCloseButton();

                Assert.That(closeButton.IsDisplayed(), "Expected the close button to be displayed, but it was not.");
            }
            else
            {
                Assert.That(modal.IsNotDisplayed(), "Expected that modal to be not displayed, but it was.");
            }
        }
    }
}
