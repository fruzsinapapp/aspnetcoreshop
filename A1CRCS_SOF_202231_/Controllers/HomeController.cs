using A1CRCS_SOF_202231_.Data;
using A1CRCS_SOF_202231_.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace A1CRCS_SOF_202231_.Controllers
{
    public class HomeController : Controller
    {
        static Random rnd = new Random();
        IItemRepository repository;
        ICommentRepository commentsRepo;
        private readonly UserManager<SiteUser> _userManager;
        private readonly ApplicationDbContext _db;

        //BlobServiceClient serviceClient;
        //BlobContainerClient containerClient;

        public HomeController(UserManager<SiteUser> userManager, ApplicationDbContext db, IItemRepository repo, ICommentRepository commentsRepository)
        {
            _userManager = userManager;
            _db = db;
            repository = repo;
            commentsRepo = commentsRepository;
            //serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=pfruzsistorage;AccountKey=Placeholder;EndpointSuffix=core.windows.net");
            //containerClient = serviceClient.GetBlobContainerClient("testcontainer");
        }

        public IActionResult Index()
        {
            return View();

        }
        [Authorize]
        public IActionResult ItemView(string uid)
        {
            var item = repository.ReadFromId(uid);
            return View(item);
        }
        [HttpPost]
        public IActionResult SaveComment(string uid, string commentString)
        {
            Comment commentToAdd = new Comment();
            commentToAdd.Content = commentString;

            commentToAdd.OwnerId = _userManager.GetUserId(User);
            commentToAdd.ItemId = uid;
            commentsRepo.Create(commentToAdd);

            return RedirectToAction(nameof(ItemView), new { uid });
        }
        [Authorize]
        public IActionResult All()
        {
            return View(this.repository.Read().OrderBy(x => x.SequenceNum));
        }

        public IActionResult ChangeFirst()
        {
            var currentFirst = repository.Read().FirstOrDefault(x => x.SequenceNum == 1);
            if(currentFirst != null)
            {
                currentFirst.SequenceNum = rnd.Next(10, 10000);
                
            }
            var itemArray = repository.Read().ToArray();
            var newFirst = itemArray[rnd.Next(0, itemArray.Length)];
            newFirst.SequenceNum = 1;
            repository.Update(newFirst);
            return RedirectToAction(nameof(All));
        }
        public IActionResult Messages(string id)
        {
            return View(this.repository.GetComments(id));
        }

        public IActionResult UnReserve(string uid)
        {
            repository.UnReserve(uid);
            return RedirectToAction(nameof(ItemView), new { uid });
        }
        public IActionResult Reserve(string uid)
        {
            string userId = _userManager.GetUserId(User);
            repository.Reserve(uid, userId);
            return RedirectToAction(nameof(ItemView), new { uid });
        }
        [Authorize]
        public async Task<IActionResult> Rules()
        {
            var plain = this.User; //ez csak egy claims principal, nem a teljes objektum
            var user = await _userManager.GetUserAsync(plain); //GetUser simán nincs, csak async lehet, emiatt kell az egésznek annak lennie
            return View();
        }

        public async Task<IActionResult> GetImage(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == userId);
            return new FileContentResult(user.PictureData, user.PictureContentType);
        }
        public async Task<IActionResult> GetItemImage(string itemId)
        {
            var item = _db.Items.FirstOrDefault(t => t.Uid == itemId);
            return new FileContentResult(item.PictureData, item.PictureContentType);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}