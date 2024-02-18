namespace SportApp.ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ViewModels.User;

    public interface IUserService
    {
        Task<IEnumerable<UserAllViewModel>> AllUsersAsync();
        Task UpdateUserAsync(string id, UserFormModel viewModel);

        Task DeleteUserAsync(UserAllViewModel viewModel);
    }
}
