using App.Areas.Identity.Models.AccountViewModels;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using ShowNow.Domain.Entities;
using ShopNow.Application.Attributes;
using ShopNow.Application.ExtendMethod;
using App.Areas.Identity.Models.ManageViewModels;
using ShopNow.Shared.Enums;
using ShopNow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using App.Areas.Identity.Models.RoleViewModels;
using App.Areas.Identity.Models.UserViewModels;

namespace ShopNow.Presentation.Controllers
{
    [Authorize(Roles = $"{RoleName.Administrator},{RoleName.Editor}")]
	//[Area("Identity")]
	[Route("/ManageUser/[action]")]
	public class UserController : Controller
	{
		private readonly ILogger<RoleController> _logger;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;
		private readonly ShopNowDbContext _context;

		private readonly UserManager<User> _userManager;

		public UserController(ILogger<RoleController> logger, RoleManager<IdentityRole<Guid>> roleManager, ShopNowDbContext context, UserManager<User> userManager)
		{
			_logger = logger;
			_roleManager = roleManager;
			_context = context;
			_userManager = userManager;
		}



		[TempData]
		public string StatusMessage { get; set; }

        //
        // GET: /ManageUser/Index
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, [FromQuery(Name = "q")] string searchTerm)
        {
            var model = new UserListModel();
            model.currentPage = currentPage;

            var qr = _userManager.Users.AsQueryable();

            // Filter by search term
            if (!string.IsNullOrEmpty(searchTerm))
            {
                qr = qr.Where(u => u.UserName.Contains(searchTerm));
            }

            qr = qr.OrderBy(u => u.UserName);

            model.totalUsers = await qr.CountAsync();
            model.countPages = (int)Math.Ceiling((double)model.totalUsers / model.ITEMS_PER_PAGE);

            if (model.currentPage < 1)
                model.currentPage = 1;
            if (model.currentPage > model.countPages)
                model.currentPage = model.countPages;

            var qr1 = qr.Skip((model.currentPage - 1) * model.ITEMS_PER_PAGE)
                        .Take(model.ITEMS_PER_PAGE)
                        .Select(u => new UserAndRole()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Avatar = null,
                            FirstName = null,
                            LastName = null,
                            Address = null,
                            City = null,
                            Country = null
                        });

            model.users = await qr1.ToListAsync();

            foreach (var user in model.users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }

            return View(model);
        }

        // GET: /ManageUser/AddRole/id
        [Authorize(Roles = RoleName.Administrator)]
        [HttpGet("{id}")]
		public async Task<IActionResult> AddRoleAsync(string id)
		{
			// public SelectList allRoles { get; set; }
			var model = new AddUserRoleModel();
			if (string.IsNullOrEmpty(id))
			{
				return NotFound($"User doesn't existed");
			}

			model.user = await _userManager.FindByIdAsync(id);

			if (model.user == null)
			{
				return NotFound($"User doesn't existed, id = {id}.");
			}

			model.RoleNames = (await _userManager.GetRolesAsync(model.user)).ToArray<string>();

			List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
			ViewBag.allRoles = new SelectList(roleNames);

			await GetClaims(model);

			return View(model);
		}

		// GET: /ManageUser/AddRole/id
		[HttpPost("{id}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRoleAsync(string id, [Bind("RoleNames")] AddUserRoleModel model)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound($"User doesn't existed");
			}

			model.user = await _userManager.FindByIdAsync(id);

			if (model.user == null)
			{
				return NotFound($"User doesn't existed, id = {id}.");
			}
			await GetClaims(model);

			var OldRoleNames = (await _userManager.GetRolesAsync(model.user)).ToArray();

			var deleteRoles = OldRoleNames.Where(r => !model.RoleNames.Contains(r));
			var addRoles = model.RoleNames.Where(r => !OldRoleNames.Contains(r));

			List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

			ViewBag.allRoles = new SelectList(roleNames);

			var resultDelete = await _userManager.RemoveFromRolesAsync(model.user, deleteRoles);
			if (!resultDelete.Succeeded)
			{
				ModelState.AddModelError(resultDelete);
				return View(model);
			}

			var resultAdd = await _userManager.AddToRolesAsync(model.user, addRoles);
			if (!resultAdd.Succeeded)
			{
				ModelState.AddModelError(resultAdd);
				return View(model);
			}


			StatusMessage = $"You just update role for user: {model.user.UserName}";

			return RedirectToAction("Index");
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> SetPasswordAsync(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound($"User doesn't existed");
			}

			var user = await _userManager.FindByIdAsync(id);
			ViewBag.user = ViewBag;

			if (user == null)
			{
				return NotFound($"User doesn't existed, id = {id}.");
			}

			return View();
		}

		[HttpPost("{id}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SetPasswordAsync(string id, SetUserPasswordModel model)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound($"User doesn't existed");
			}

			var user = await _userManager.FindByIdAsync(id);
			ViewBag.user = ViewBag;

			if (user == null)
			{
				return NotFound($"User doesn't existed, id = {id}.");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await _userManager.RemovePasswordAsync(user);

			var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
			if (!addPasswordResult.Succeeded)
			{
				foreach (var error in addPasswordResult.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return View(model);
			}

			StatusMessage = $"You just update password for user: {user.UserName}";

			return RedirectToAction("Index");
		}


		[HttpGet("{userid}")]
		public async Task<ActionResult> AddClaimAsync(string userid)
		{

			var user = await _userManager.FindByIdAsync(userid);
			if (user == null) return NotFound("User doesn't existed");
			ViewBag.user = user;
			return View();
		}

		[HttpPost("{userid}")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddClaimAsync(string userid, AddUserClaimModel model)
		{

			var user = await _userManager.FindByIdAsync(userid);
			if (user == null) return NotFound("User doesn't existed");
			ViewBag.user = user;
			if (!ModelState.IsValid) return View(model);
			var claims = _context.UserClaims.Where(c => c.UserId == user.Id);

			if (claims.Any(c => c.ClaimType == model.ClaimType && c.ClaimValue == model.ClaimValue))
			{
				ModelState.AddModelError(string.Empty, "Claims is existed");
				return View(model);
			}

			await _userManager.AddClaimAsync(user, new Claim(model.ClaimType, model.ClaimValue));
			StatusMessage = "Claim for user is added";

			return RedirectToAction("AddRole", new { id = user.Id });
		}

		[HttpGet("{claimid}")]
		public async Task<IActionResult> EditClaim(int claimid)
		{
			var userclaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();
			var user = await _userManager.FindByIdAsync(userclaim.UserId.ToString());

			if (user == null) return NotFound("User doesn't existed");

			var model = new AddUserClaimModel()
			{
				ClaimType = userclaim.ClaimType,
				ClaimValue = userclaim.ClaimValue

			};
			ViewBag.user = user;
			ViewBag.userclaim = userclaim;
			return View("AddClaim", model);
		}
		[HttpPost("{claimid}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditClaim(int claimid, AddUserClaimModel model)
		{
			var userclaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();
			var user = await _userManager.FindByIdAsync(userclaim.UserId.ToString());
			if (user == null) return NotFound("User doesn't existed");

			if (!ModelState.IsValid) return View("AddClaim", model);

			if (_context.UserClaims.Any(c => c.UserId == user.Id
				&& c.ClaimType == model.ClaimType
				&& c.ClaimValue == model.ClaimValue
				&& c.Id != userclaim.Id))
			{
				ModelState.AddModelError("Claim is existed");
				return View("AddClaim", model);
			}


			userclaim.ClaimType = model.ClaimType;
			userclaim.ClaimValue = model.ClaimValue;

			await _context.SaveChangesAsync();
			StatusMessage = "You just update claim";


			ViewBag.user = user;
			ViewBag.userclaim = userclaim;
			return RedirectToAction("AddRole", new { id = user.Id });
		}
		[HttpPost("{claimid}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteClaimAsync(int claimid)
		{
			var userclaim = _context.UserClaims.Where(c => c.Id == claimid).FirstOrDefault();
			var user = await _userManager.FindByIdAsync(userclaim.UserId.ToString());

			if (user == null) return NotFound("User doesn't existed");

			await _userManager.RemoveClaimAsync(user, new Claim(userclaim.ClaimType, userclaim.ClaimValue));

			StatusMessage = "You just delete claim";

			return RedirectToAction("AddRole", new { id = user.Id });
		}

		private async Task GetClaims(AddUserRoleModel model)
		{
			var listRoles = from r in _context.Roles
							join ur in _context.UserRoles on r.Id equals ur.RoleId
							where ur.UserId == model.user.Id
							select r;

			var _claimsInRole = from c in _context.RoleClaims
								join r in listRoles on c.RoleId equals r.Id
								select c;
			model.claimsInRole = await _claimsInRole.ToListAsync();


			model.claimsInUserClaim = await (from c in _context.UserClaims
											 where c.UserId == model.user.Id
											 select c).ToListAsync();

		}
	}
}
