namespace SportApp.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BusinessLayer;
    using ServiceLayer.Interfaces;
    using ViewModels.Sport;

    [Authorize]
    public class SportController : Controller
    {
        private readonly ISportService sportService;

        public SportController(ISportService sportService)
        {
            this.sportService = sportService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<SportAllViewModel> allBoards =
                await sportService.AllAsync();
            return View(allBoards);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SportFormModel model = new SportFormModel();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SportFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await sportService.AddAsync(currentUserId, model);

            return RedirectToAction("Index", "Sport");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)

        {
            try
            {
                SportAllViewModel viewModel = await this.sportService.GetForDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Sport");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Sport sport = await this.sportService.GetSportByIdAsync(id);

            if (sport == null)
            {
                return BadRequest();
            }

            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != sport.UserId)
            {
                return Unauthorized();
            }

            SportFormModel model = new SportFormModel()
            {
                Name = sport.Name,
                Duration = sport.Duration,
                TimesPerWeek = sport.TimesPerWeek,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, SportFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await sportService.EditAsync(id, model);

            return RedirectToAction("Index", "Sport");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            Sport sport = await this.sportService.GetSportByIdAsync(id);

            if (sport == null)
            {
                return BadRequest();
            }

            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != sport.UserId)
            {
                return Unauthorized();
            }

            SportAllViewModel model = new SportAllViewModel()
            {
                Id = sport.Id.ToString(),
                Name = sport.Name,
                Duration = sport.Duration,
                TimesPerWeek = sport.TimesPerWeek
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SportAllViewModel model)
        {
            await sportService.DeleteAsync(model);

            return RedirectToAction("Index", "Sport");
        }
    }
}
