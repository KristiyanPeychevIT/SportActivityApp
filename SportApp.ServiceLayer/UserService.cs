namespace SportApp.ServiceLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using DataLayer;
    using Interfaces;
    using ViewModels.User;


    public class UserService : IUserService
    {
        private readonly SportAppDbContext dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(SportAppDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this._userManager = userManager;
        }

        public async Task<IEnumerable<UserAllViewModel>> AllUsersAsync()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Administrator");
            List<UserAllViewModel> allUsers = dbContext
                .Users
                .Where(u => !admins.Contains(u))
                .Select(u => new UserAllViewModel()
                {
                    Id = u.Id.ToString(),
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .ToList();


            return allUsers;
        }

        public async Task UpdateUserAsync(string id, UserFormModel viewModel)
        {
            var userToEdit = await this.dbContext
                .Users
                .FirstAsync(u => u.Id == id);

            userToEdit.UserName = viewModel.UserName;
            userToEdit.NormalizedUserName = viewModel.UserName.ToUpper();

            userToEdit.Email = viewModel.Email;
            userToEdit.NormalizedEmail = viewModel.Email.ToUpper();

            userToEdit.PhoneNumber = viewModel.PhoneNumber;


            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(UserAllViewModel viewModel)
        {
            var userToDelete = await this.dbContext
                .Users
                .FirstAsync(u => u.Id == viewModel.Id);

            this.dbContext.Users.Remove(userToDelete);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
