using Core.Helpers;
using OpenQA.Selenium;
using Ui.Elements;
using Ui.Models;

namespace Ui.Pages
{
    public class PetDetailsPage : BasePage
    {
        public UiElement GetPetDetailsContainer() => new(By.XPath(".//pet-world-pet-details"), "PetDetailsContainer");

        public DetailedPetProposal GetPetDetails()
        {
            var priceString = Driver.FindElement(By.CssSelector(".price")).Text;
            var price = PriceHelper.ParsePrice(priceString);
            var details = new DetailedPetProposal
            {
                Title = Driver.FindElement(By.CssSelector(".main-header")).Text,
                Price = price
            };

            TrySetValue(value => details.Sex = value, ".item__name--sex + .item__value");
            TrySetValue(value =>
            {
                var (age, unit) = AgeUnitHelper.ParseAge(value);
                details.Age = age;
                details.AgeUnits = unit;
            }, ".item__name--age + .item__value");
            TrySetValue(value => details.Location = value, ".item__name--location + .item__value");
            TrySetValue(value => details.ContactName = value, ".item__name--contact + .item__value");
            TrySetValue(value => details.PhoneNumber = value, ".item__name--phone + .item__value");
            TrySetValue(value => details.PetType = value, ".item__name--type + .item__value");
            TrySetValue(value => details.Breed = value, ".item__name--breed + .item__value");
            TrySetValue(value => details.Color = value, ".item__name--color + .item__value");
            TrySetValue(value => details.OwnerType = value, ".item__name--origin + .item__value");

            details.HealthInfo = Driver.FindElements(By.CssSelector(".item__name--health + .item__chips .chips"))
                                        .Select(el => el.Text).ToList();
            details.Documents = Driver.FindElements(By.CssSelector(".item__name--documents + .item__chips .chips"))
                                        .Select(el => el.Text).ToList();

            var imageUploaderElements = Driver.FindElements(By.CssSelector("pet-world-photo-uploader .upload-button__photo"));
            details.ImageUrl = imageUploaderElements.Count != 0
                ? imageUploaderElements[0].GetAttribute("src")
                : string.Empty;

            return details;
        }

        private void TrySetValue(Action<string> setter, string cssSelector)
        {
            var elements = Driver.FindElements(By.CssSelector(cssSelector));
            if (elements.Count > 0)
            {
                setter(elements[0].Text);
            }
        }
    }
}
