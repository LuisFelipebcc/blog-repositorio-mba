using BlogApp.Core.Entities;
using BlogApp.Core.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Web.Configurations
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public class DbMigrationHelpers
    {

        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var service = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(service);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.IsStaging())
            {
                await context.Database.MigrateAsync();

                await EnsureSeedProducts(context, serviceProvider);
            }

        }

        private static async Task EnsureSeedProducts(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Author>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            var adminEmail = "admin@blogapp.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new Author
                {
                    UserName = "admin",
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Substitua por uma senha segura

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Falha ao criar o usuário administrador.");
                }
            }

            var author = new Author
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Autor Exemplo",
                Email = "teste@teste.com",
                PasswordHash = "AQAAAAIAAYagAAAAEEdWhqiCwW/jZz0hEM7aNjok7IxniahnxKxxO5zsx2TvWs4ht1FUDnYofR8JKsA5UA=="

            };
            context.Author.Add(author);
            

            // Cria um post para o autor
            var post = new Post
            {
                Title = "Post Exemplo",
                Content = "Este é o conteúdo do post exemplo.",
                PublishedDate = DateTime.UtcNow,
                AuthorId = author.Id
            };
            context.Posts.Add(post);

            // Cria um comentário para o post
            var comment = new Comment
            {
                Content = "Este é um comentário exemplo.",
                CommentedDate = DateTime.UtcNow,
                PostId = post.Id,
                AuthorId = author.Id
            };
            context.Comments.Add(comment);

        }
    }
}
