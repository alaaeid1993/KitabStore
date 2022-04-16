using System;
using System.ComponentModel.DataAnnotations;

namespace Z_Store.ViewModels
{
    public class AddRoleViewModel
    {

        [Required(ErrorMessage ="Please Add Role")]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
