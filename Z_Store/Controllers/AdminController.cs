using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z_Store.Models;
using Z_Store.ViewModels;

namespace Z_Store.Controllers
{
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> RoleManager { get; }
        UserManager<DefaultUser> UserManager { get; }

        public AdminController(RoleManager<IdentityRole> _roleManager,UserManager<DefaultUser> _userManager)
        {
            RoleManager = _roleManager;
            UserManager = _userManager;
        }


        public IActionResult index()
        {
            var Role = RoleManager.Roles;
            return View(Role);
        }

        public async Task<IActionResult> AddRole(AddRoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identity = new()
                {
                    Name = role.RoleName
                };
                var result = await RoleManager.CreateAsync(identity);
                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    foreach (var i in result.Errors)
                    {
                        ModelState.AddModelError("", i.Description);
                    }
                }
            }
            return View(role);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var identyrole = await RoleManager.FindByIdAsync(id);
            if (identyrole == null) 
            {
                ViewData["Message"] = $"No Role With This Id: {id}";
               return  View("Error");
            }
            EditAdminViewModel editrole = new()
            {
                id = identyrole.Id,
                Name = identyrole.Name
            };

            return View(editrole);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditAdminViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.id);
            if(role == null)
            {
                ViewData["Message"] = $"No Role With This Id: {model.id}";
                return View("Error");
            }
            else
            {
                role.Name = model.Name;
                var result = await RoleManager.UpdateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("index");
                }
          
                foreach (var i in result.Errors)
                {
                    ModelState.AddModelError("", i.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id,EditAdminViewModel model)
        {
            var role = await RoleManager.FindByIdAsync(model.id);
            if (role == null)
            {
                ViewData["Message"] = $"No Role With This Id: {model.id}";
                return View("Error");
            }
            else
            {
                role.Name = model.Name;
                var result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }
                foreach(var i in result.Errors)
                {
                    ModelState.AddModelError("", i.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> AssignRole(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["Message"] = $"No Role With This Id: {id}";
                return View("Error");
            }
            var roleview = new List<AssignRoleViewModel>();
            foreach(var i in UserManager.Users)
            {
                AssignRoleViewModel assign = new()
                {
                    Id = i.Id,
                    Name = i.UserName,
                };
                if (await UserManager.IsInRoleAsync(i,role.Name))
                {
                    assign.IsSelected = true;
                }
                else
                {
                    assign.IsSelected = false;
                }
                roleview.Add(assign);
            }
            return View(roleview);
        }
        //public IActionResult listrole()
        //{
        //    //List<RoleManager> ro = RoleManager.Roles.ToList();
        //    var Role = RoleManager.Roles;
        //    return View(Role);
        //}
    }
}
