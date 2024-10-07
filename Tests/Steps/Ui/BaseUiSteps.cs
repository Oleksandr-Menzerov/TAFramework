using Ui.Elements;
using Ui.Pages;

namespace Tests.Steps.Ui
{
    public class BaseUiSteps(ScenarioContext scenarioContext) : BaseSteps(scenarioContext)
    {
        internal readonly BasePage _basePage = new();

        public UiElement GetElementByType(string name, string type)
        {
            return type switch
            {
                "button" => _basePage.GetButton(name),
                "header" => _basePage.GetHeader(name),
                "link" => _basePage.GetLink(name),
                "label" => _basePage.GetLabel(name),
                "checkbox" => _basePage.GetCheckbox(name),
                "dropdown" => _basePage.GetNamedDropdown(name),
                "input" => _basePage.GetInput(name),
                "radiobutton" => _basePage.GetRadioButton(name),
                _ => throw new ArgumentException($"Unknown element type: {type}")
            };
        }
    }
}
