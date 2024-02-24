using A1CRCS_SOF_202231_.Data;
using A1CRCS_SOF_202231_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace A1CRCS_SOF_202231_.Controllers
{
    public class OwnController : Controller
    {
        IItemRepository repository;
        //private readonly ILogger<HomeController> _logger;
        private readonly UserManager<SiteUser> _userManager;
        private readonly ApplicationDbContext _db;

        public OwnController(ILogger<HomeController> logger, UserManager<SiteUser> userManager, ApplicationDbContext db, IItemRepository repo)
        {
            //_logger = logger;
            _userManager = userManager;
            _db = db;
            repository = repo;
        }
        [Authorize]
        public IActionResult Index(string id)
        {
            var user = _userManager.GetUserId(this.User);
            return View(this.repository.OwnItems(user));
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Item item)
        {
            item.OwnerId = _userManager.GetUserId(this.User);
            item.Uid = Guid.NewGuid().ToString();

            //no need for this, instead use bodel binder
            /*
            item.Date = DateTime.Now;
            item.Uid= Guid.NewGuid().ToString();
            var selectedOption = Request.Form["category-select"];
            item.Category = (ItemCategory)int.Parse(selectedOption);
            */
            if (!ModelState.IsValid)
            {
                return View("Add", item);
            }
            
            repository.Create(item);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(string uid)
        {
            Item item = repository.ReadFromId(uid);
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Item itemToUpdate)
        {
            //TODO: remove these ifs
            /*
            itemToUpdate.Reserved = false;
            itemToUpdate.OwnerId = _userManager.GetUserId(this.User);

            var selectedOption = Request.Form["category-select"];
            itemToUpdate.Category = (ItemCategory)int.Parse(selectedOption);
            */
            repository.Update(itemToUpdate);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string uid)
        {
            repository.Delete(uid);
            return RedirectToAction(nameof(Index));
        }

    }
}
