// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using DomainModel.Models;

namespace GolpaMotor.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IUserEmailStore<ApplicationUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly GolpaMotorDbContext _dbContext;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        SignInManager<ApplicationUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        GolpaMotorDbContext dbContext)
    {
        userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
        signInManager = signInManager;
        _logger = logger;
        _emailSender = emailSender;
        _dbContext = dbContext;
    }

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public string? ReturnUrl { get; set; }

    public IList<AuthenticationScheme>? ExternalLogins { get; set; }

    public Microsoft.AspNetCore.Mvc.Rendering.SelectList ProvinceList { get; set; } = default!;

    public Microsoft.AspNetCore.Mvc.Rendering.SelectList CityList { get; set; } = default!;

    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = default!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Province")]
        public int ProvinceID { get; set; }

        [Display(Name = "City")]
        public int CityID { get; set; }
    }


    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        var provinces = _dbContext.Provinces.OrderBy(p => p.Name).ToList();
        ProvinceList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(provinces, "ProvinceID", "Name");
        var cities = _dbContext.Cities.OrderBy(c => c.Name).ToList();
        CityList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(cities, "CityID", "Name");
        ViewData["BodyClass"] = "hold-transition register-page";
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (ModelState.IsValid)
        {
            var user = CreateUser();

            // set custom properties (ensure province/city exist)
            var province = _dbContext.Provinces.Find(Input.ProvinceID);
            var city = _dbContext.Cities.Find(Input.CityID);
            if (province == null || city == null)
            {
                ModelState.AddModelError(string.Empty, "Selected province or city is invalid.");
                // repopulate select lists
                var provinces = _dbContext.Provinces.OrderBy(p => p.Name).ToList();
                ProvinceList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(provinces, "ProvinceID", "Name");
                var cities = _dbContext.Cities.OrderBy(c => c.Name).ToList();
                CityList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(cities, "CityID", "Name");
                return Page();
            }
            user.PhoneNumber = Input.PhoneNumber;
            user.ProvinceID = Input.ProvinceID;
            user.CityID = Input.CityID;

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                // remove email confirmation: mark confirmed and sign in
                var userId = await userManager.GetUserIdAsync(user);
                var createdUser = await userManager.FindByIdAsync(userId);
                if (createdUser != null)
                {
                    createdUser.EmailConfirmed = true;
                    await userManager.UpdateAsync(createdUser);
                }

                await signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<ApplicationUser>)_userStore;
    }
}
