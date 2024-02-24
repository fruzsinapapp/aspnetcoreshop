using A1CRCS_SOF_202231_.Data;
using A1CRCS_SOF_202231_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A1CRCS_SOF_202231_.Controllers
{
    public class SearchController : Controller
    {
        IItemRepository repository;
        private readonly ApplicationDbContext _db;

        public SearchController(ApplicationDbContext db, IItemRepository repo)
        {
            _db = db;
            repository = repo;
        }

        public IActionResult Index(ItemCategory cat, int price, bool res)
        {
            return View(this.repository.Search(cat, price, res));
        }
        [Authorize]
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(string valami, int valami2, string megvalami)
        {
            var selectedOption = Request.Form["category-select"];

            ItemCategory cat;
            if (selectedOption == "0")
            {
                cat = ItemCategory.electronics;
            }
            else if (selectedOption == "1")
            {
                cat = ItemCategory.clothes;
            }
            else if (selectedOption == "2")
            {
                cat = ItemCategory.housing;
            }
            else if (selectedOption == "3")
            {
                cat = ItemCategory.sport;
            }
            else if (selectedOption == "4")
            {
                cat = ItemCategory.book;
            }
            else
            {
                cat = ItemCategory.game;
            }

            string isReserved = Request.Form["is-reserved"];
            bool res = false;
            if (isReserved == "on")
                res = true;

            string priceString = Request.Form["price-input"];
            int price;
            if (priceString != "")
                price = int.Parse(priceString);

            else
                price = int.MaxValue;

            return RedirectToAction(nameof(Index), new { cat, price, res });
        }
    }
}
