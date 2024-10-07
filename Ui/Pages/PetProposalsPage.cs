using Core.Helpers;
using OpenQA.Selenium;
using Ui.Elements;
using Ui.Models;

namespace Ui.Pages
{
    public class PetProposalsPage : BasePage
    {
        public List<PetProposal> GetPetProposals()
        {
            var proposals = new List<PetProposal>();

            var cards = Driver.FindElements(By.CssSelector("pet-world-pet-card"));
            foreach (var card in cards)
            {
                string imageUrl = string.Empty;
                var imageElement = card.FindElements(By.CssSelector("img")).FirstOrDefault();
                if (imageElement != null)
                {
                    imageUrl = imageElement.GetAttribute("src");
                }

                var ageText = card.FindElement(By.CssSelector(".details__item--age")).Text;
                var (age, ageUnit) = AgeUnitHelper.ParseAge(ageText);
                var priceString = card.FindElement(By.CssSelector(".price")).Text;
                var price = PriceHelper.ParsePrice(priceString);

                var proposal = new PetProposal
                {
                    Title = card.FindElement(By.CssSelector(".title")).Text,
                    Location = card.FindElement(By.CssSelector(".details__item--location")).Text,
                    Sex = card.FindElement(By.CssSelector(".details__item--sex")).Text,
                    Age = age,
                    AgeUnits = ageUnit,
                    Price = price,
                    ImageUrl = imageUrl,  // Handle missing image case
                    Element = new UiElement(card, "Pet card")
                };

                proposals.Add(proposal);
            }

            return proposals;
        }

        public List<PetProposal> WaitAndGetPetProposals()
        {
            WaitForLoaderToDisappear();
            GetFiltersHeader().WaitForVisible();
            return GetPetProposals();
        }

        public Label GetFiltersHeader() => new(By.XPath(".//h2[@class='filter-title']"));
        public Button GetListViewButton() => new(By.XPath("//button[./svg[contains(@xlink:href, '#grid-rows')]]"));
        public Button GetGridViewButton() => new(By.XPath("//button[./svg[contains(@xlink:href, '#grid-columns')]]"));
    }
}
