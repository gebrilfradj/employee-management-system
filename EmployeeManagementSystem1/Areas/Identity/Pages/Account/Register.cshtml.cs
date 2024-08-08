using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem1.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;  // Add RoleManager dependency

        public RegisterModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)  // Inject RoleManager
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }




        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identity = new IdentityUser { UserName = Input.Email, Email=Input.Email };
                var result = await _userManager.CreateAsync(identity, Input.Password);
                if (result.Succeeded)
                {
                    if (_userManager.Users.Count() == 1)
                    {
                        // Check if the Role exists, if not, create it
                        if (!await _roleManager.RoleExistsAsync("Manager"))
                            await _roleManager.CreateAsync(new IdentityRole("Manager"));
                        await _userManager.AddToRoleAsync(identity, "Manager");
                    }
                    else
                    {
                        // Check if the Role exists, if not, create it
                        if (!await _roleManager.RoleExistsAsync("Employee"))
                            await _roleManager.CreateAsync(new IdentityRole("Employee"));
                        await _userManager.AddToRoleAsync(identity, "Employee");
                    }

                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect("/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
