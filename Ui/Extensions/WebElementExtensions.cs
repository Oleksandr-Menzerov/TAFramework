using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UI.Driver;

namespace Ui.Extensions
{
    /// <summary>
    /// A static class that provides extension methods for the <see cref="IWebElement"/> interface.
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        /// Clicks on the specified web element using JavaScript.
        /// </summary>
        /// <param name="webElement">The <see cref="IWebElement"/> to be clicked.</param>
        public static void ClickOn(this IWebElement webElement)
        {
            Driver.GetCurrentDriver().Scripts().ExecuteScript("arguments[0].click();", webElement);
        }

        /// <summary>
        /// Creates an <see cref="Actions"/> object to move to the specified web element.
        /// </summary>
        /// <param name="webElement">The <see cref="IWebElement"/> to move to.</param>
        /// <param name="offsetX">The X offset to move to.</param>
        /// <param name="offsetY">The Y offset to move to.</param>
        /// <returns>An <see cref="Actions"/> object configured to move to the specified web element.</returns>
        private static Actions MoveToAction(this IWebElement webElement, int offsetX = 0, int offsetY = 0)
        {
            var actions = new Actions(Driver.GetCurrentDriver());
            return actions.MoveToElement(webElement, offsetX, offsetY);
        }

        /// <summary>
        /// Moves the mouse to the specified web element with optional offsets.
        /// </summary>
        /// <param name="webElement">The <see cref="IWebElement"/> to move to.</param>
        /// <param name="offsetX">The X offset to move to.</param>
        /// <param name="offsetY">The Y offset to move to.</param>
        public static void MoveTo(this IWebElement webElement, int offsetX = 0, int offsetY = 0)
        {
            webElement.MoveToAction(offsetX, offsetY).Perform();
        }

        /// <summary>
        /// Moves to the specified web element and then clicks on it with optional offsets.
        /// </summary>
        /// <param name="webElement">The <see cref="IWebElement"/> to move to and click.</param>
        /// <param name="offsetX">The X offset to move to.</param>
        /// <param name="offsetY">The Y offset to move to.</param>
        public static void MoveAndClick(this IWebElement webElement, int offsetX = 0, int offsetY = 0)
        {
            webElement.MoveToAction(offsetX, offsetY).Click().Perform();
        }
    }
}
