using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Z_Store.Models;

namespace Z_Store.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<DefaultUser> _userManager;
        private readonly SignInManager<DefaultUser> _signInManager;

        public IndexModel(
            UserManager<DefaultUser> userManager,
            SignInManager<DefaultUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
 //-----------------------------------------------add my Var---------------------//

            [Required]
            [Display(Name = "First Name")]
            [DataType(DataType.Text)]
            public string FirstName { get; set; }


            [Required]
            [Display(Name = "Last Name")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Address")]
            [DataType(DataType.Text)]
            public string Address { get; set; }


            [Required]
            [Display(Name = "City")]
            [DataType(DataType.Text)]
            public string City { get; set; }
//-----------------------------------------------add my Var---------------------//

        }

        private async Task LoadAsync(DefaultUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
 //-----------------------------------------------add my input---------------------//
                FirstName = user.FirstName,
                LastName=user.LastName,
                Address=user.Address,
                City=user.City,
 //-----------------------------------------------add my input---------------------//

                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
 //-----------------------------------------------add my if state---------------------//
            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }
            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }
            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }
            if (Input.City != user.City)
            {
                user.City = Input.City;
            }

            await _userManager.UpdateAsync(user);
 //-----------------------------------------------add my if state---------------------//

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
