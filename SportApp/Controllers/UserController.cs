namespace SportApp.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ServiceLayer.Interfaces;
    using ViewModels.User;

    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<IdentityUser> _userManager;


        public UserController(IUserService userService, UserManager<IdentityUser> _userManager, IUserStore<IdentityUser> _userStore)
        {
            this.userService = userService;
            this._userManager = _userManager;
        }


        public async Task<IActionResult> All()
        {
            IEnumerable<UserAllViewModel> allUsers =
                await userService.AllUsersAsync();

            return View(allUsers);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            UserFormModel model = new UserFormModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            await userService.UpdateUserAsync(id, model);

            return RedirectToAction("All", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            UserAllViewModel model = new UserAllViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserAllViewModel model)
        {

            await this.userService.DeleteUserAsync(model);

            return RedirectToAction("All", "User");
        }
    }
}
