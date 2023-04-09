using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeCare.Domain.Entities;
using WeCare.Infrastructure.Identity;

namespace WeCare.Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles

        //var administratorRole = new IdentityRole("Admin");
        //var acadmicRole = new IdentityRole("AcademicStaff");
        //var volunteerRole = new IdentityRole("VolunteerStudent");
        //var disabilityRole = new IdentityRole("DisabilityStudent");
        //var deanRole = new IdentityRole("DeanOffice");
        //await _roleManager.CreateAsync(administratorRole);
        //await _roleManager.CreateAsync(acadmicRole);
        //await _roleManager.CreateAsync(volunteerRole);
        //await _roleManager.CreateAsync(disabilityRole);
        //await _roleManager.CreateAsync(deanRole);
        //var adminUser = await _userManager.FindByIdAsync("02bb28b6-2838-48d2-8019-2d7bcd3f1673");
        //var acadmicUser = await _userManager.FindByIdAsync("9280a0d4-950c-4bac-96ea-1fdda8ef850a");
        //var volunteerUser = await _userManager.FindByIdAsync("47d06e98-77fd-48df-8205-04429d0d0fe6");
        //var disabilityUser = await _userManager.FindByIdAsync("d5cd54f0-3a5b-45e4-af38-ff57097cc9d6");
        //var deanUser = await _userManager.FindByIdAsync("faee1e04-e0eb-4ad8-ba3c-008b4f4976de");



        //await _userManager.AddToRolesAsync(adminUser, new[] { administratorRole.Name });
        //await _userManager.AddToRolesAsync(acadmicUser, new[] { acadmicRole.Name });
        //await _userManager.AddToRolesAsync(volunteerUser, new[] { volunteerRole.Name });
        //await _userManager.AddToRolesAsync(disabilityUser, new[] { disabilityRole.Name });
        //await _userManager.AddToRolesAsync(deanUser, new[] { deanRole.Name });

        // Default data
        // Seed, if necessary
        /* if (!_context.TodoLists.Any())
         {
             _context.TodoLists.Add(new TodoList
             {
                 Title = "Todo List",
                 Items =
                 {
                     new TodoItem { Title = "Make a todo list 📃" },
                     new TodoItem { Title = "Check off the first item ✅" },
                     new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                     new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                 }
             });

             await _context.SaveChangesAsync();
         }
     }*/
    }
}
