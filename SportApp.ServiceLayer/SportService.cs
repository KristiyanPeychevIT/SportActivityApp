namespace SportApp.ServiceLayer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using BusinessLayer;
    using DataLayer;
    using Interfaces;
    using ViewModels.Sport;

    public class SportService : ISportService
    {
        private readonly SportAppDbContext dbContext;

        public SportService(SportAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<SportAllViewModel>> AllAsync()
        {
            IEnumerable<SportAllViewModel> sports = await dbContext
                .Sports.Select(s => new SportAllViewModel()
                {
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Duration = s.Duration,
                    TimesPerWeek = s.TimesPerWeek,
                    User = s.User.UserName
                }).ToArrayAsync();

            return sports;
        }
        public async Task AddAsync(string userId, SportFormModel viewModel)
        {
            Sport sport = new Sport()
            {
                Name = viewModel.Name,
                Duration = viewModel.Duration,
                TimesPerWeek = viewModel.TimesPerWeek,
                UserId = userId
            };

            await this.dbContext.AddAsync(sport);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<SportAllViewModel> GetForDetailsByIdAsync(string id)
        {
            SportAllViewModel viewModel = await this.dbContext
                .Sports
                .Select(s => new SportAllViewModel
                {
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Duration = s.Duration,
                    TimesPerWeek = s.TimesPerWeek,
                    User = s.User.UserName,
                })
                .FirstAsync(s => s.Id == id);

            return viewModel;
        }

        public async Task<Sport> GetSportByIdAsync(string id)
        {
            Sport sport = await this.dbContext
                .Sports.FirstAsync(s => s.Id.ToString() == id);

            return sport;
        }

        public async Task EditAsync(string id, SportFormModel editedViewModel)
        {
            Sport sportToEdit = await this.dbContext
                .Sports
                .FirstAsync(s => s.Id.ToString() == id);

            sportToEdit.Name = editedViewModel.Name;
            sportToEdit.Duration = editedViewModel.Duration;
            sportToEdit.TimesPerWeek = editedViewModel.TimesPerWeek;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SportAllViewModel viewModel)
        {
            Sport sportToDelete = await this.dbContext
                .Sports
                .FirstAsync(t => t.Id.ToString() == viewModel.Id);

            this.dbContext.Sports.Remove(sportToDelete);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
