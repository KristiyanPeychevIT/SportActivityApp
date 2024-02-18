namespace SportApp.Infrastructure.Extensions
{
    using System.Security.Claims;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            var userManager = 
                services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = 
                services.GetRequiredService<RoleManager<IdentityRole>>();


            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync("Administrator"))
                {
                    return;
                }

                IdentityRole role = new IdentityRole("Administrator");

                await roleManager.CreateAsync(role);

                IdentityUser adminUser = await userManager.FindByEmailAsync(email);

                await userManager.AddToRoleAsync(adminUser, "Administrator");
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Administrator");
        }
    }
}
