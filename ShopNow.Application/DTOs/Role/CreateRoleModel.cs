using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.RoleViewModels
{
  public class CreateRoleModel
    {
        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Must input {0}")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} must lenght form {2} to {1} characters.")]
        public string Name { get; set; }


    }
}
