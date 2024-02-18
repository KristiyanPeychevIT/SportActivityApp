namespace SportApp.ServiceLayer.Interfaces
{
    using BusinessLayer;
    using ViewModels.Sport;
    public interface ISportService
    {
        Task<IEnumerable<SportAllViewModel>> AllAsync();
        Task AddAsync(string ownerId, SportFormModel viewModel);

        Task<SportAllViewModel> GetForDetailsByIdAsync(string id);

        Task<Sport> GetSportByIdAsync(string id);

        Task EditAsync(string id, SportFormModel viewModel);
        Task DeleteAsync(SportAllViewModel viewModel);
    }
}
