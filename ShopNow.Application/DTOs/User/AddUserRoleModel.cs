using System.Collections.Generic;
using System.ComponentModel;
//using App.Models;
//using ASP.NET_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using ShowNow.Domain.Entities;

namespace App.Areas.Identity.Models.UserViewModels
{
  public class AddUserRoleModel
  {
    public User user { get; set; }

    [DisplayName("Các role gán cho user")]
    public string[] RoleNames { get; set; }

    public List<IdentityRoleClaim<Guid>> claimsInRole { get; set; }
    public List<IdentityUserClaim<Guid>> claimsInUserClaim { get; set; }

  }
}