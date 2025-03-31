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

namespace ShopNow.Presentation.Controllers
{
	[Authorize(Roles = "Administrator")]
	//[Area("Identity")]
	[Route("/Role/[action]")]
	public class RoleController : Controller
	{
		private readonly ILogger<RoleController> _logger;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;
		private readonly ShopNowDbContext _context;
		private readonly UserManager<User> _userManager;

		public RoleController(ILogger<RoleController> logger, RoleManager<IdentityRole<Guid>> roleManager, ShopNowDbContext context, UserManager<User> userManager)
		{
			_logger = logger;
			_roleManager = roleManager;
			_context = context;
			_userManager = userManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		//
		// GET: /Role/Index
		[HttpGet]
		public async Task<IActionResult> Index()
		{

			var r = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
			var roles = new List<RoleModel>();
			foreach (var _r in r)
			{
				var claims = await _roleManager.GetClaimsAsync(_r);
				var claimsString = claims.Select(c => c.Type + "=" + c.Value);

				var rm = new RoleModel()
				{
					Name = _r.Name,
					Id = _r.Id,
					Claims = claimsString.ToArray()
				};
				roles.Add(rm);
			}

			return View(roles);
		}

		// GET: /Role/Create
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		// POST: /Role/Create
		[HttpPost, ActionName(nameof(Create))]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAsync(CreateRoleModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var newRole = new IdentityRole<Guid>(model.Name);
			var result = await _roleManager.CreateAsync(newRole);
			if (result.Succeeded)
			{
				StatusMessage = $"You just create new role: {model.Name}";
				return RedirectToAction(nameof(Index));
			}
			else
			{
				ModelState.AddModelError(result);
			}
			return View();
		}

		// GET: /Role/Delete/roleid
		[HttpGet("{roleid}")]
		public async Task<IActionResult> DeleteAsync(string roleid)
		{
			if (roleid == null) return NotFound("Can't find role");
			var role = await _roleManager.FindByIdAsync(roleid);
			if (role == null)
			{
				return NotFound("Can't find role");
			}
			return View(role);
		}

		// POST: /Role/Edit/1
		[HttpPost("{roleid}"), ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmAsync(string roleid)
		{
			if (roleid == null) return NotFound("Can't find role");
			var role = await _roleManager.FindByIdAsync(roleid);
			if (role == null) return NotFound("Can't find role");

			var result = await _roleManager.DeleteAsync(role);

			if (result.Succeeded)
			{
				StatusMessage = $"You just delete: {role.Name}";
				return RedirectToAction(nameof(Index));
			}
			else
			{
				ModelState.AddModelError(result);
			}
			return View(role);
		}

		// GET: /Role/Edit/roleid
		[HttpGet("{roleid}")]
		public async Task<IActionResult> EditAsync(string roleid, [Bind("Name")] EditRoleModel model)
		{
			if (roleid == null) return NotFound("Can't find role");
			var role = await _roleManager.FindByIdAsync(roleid);
			if (role == null)
			{
				return NotFound("Can't find role");
			}
			model.Name = role.Name;
			model.Claims = await _context.RoleClaims.Where(rc => rc.RoleId == role.Id).ToListAsync();
			model.role = role;
			ModelState.Clear();
			return View(model);

		}

		// POST: /Role/Edit/1
		[HttpPost("{roleid}"), ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditConfirmAsync(string roleid, [Bind("Name")] EditRoleModel model)
		{
			if (roleid == null) return NotFound("Can't find role");
			var role = await _roleManager.FindByIdAsync(roleid);
			if (role == null)
			{
				return NotFound("Can't find role");
			}
			model.Claims = await _context.RoleClaims.Where(rc => rc.RoleId == role.Id).ToListAsync();
			model.role = role;
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			role.Name = model.Name;
			var result = await _roleManager.UpdateAsync(role);

			if (result.Succeeded)
			{
				StatusMessage = $"You just rename: {model.Name}";
				return RedirectToAction(nameof(Index));
			}
			else
			{
				ModelState.AddModelError(result);
			}

			return View(model);
		}

		// GET: /Role/AddRoleClaim/roleid
		[HttpGet("{roleid}")]
		public async Task<IActionResult> AddRoleClaimAsync(string roleid)
		{
			if (roleid == null) return NotFound("Can't find role");
			var role = await _roleManager.FindByIdAsync(roleid);
			if (role == null)
			{
				return NotFound("Can't find role");
			}

			var model = new EditClaimModel()
			{
				role = role
			};
			return View(model);
		}

		// POST: /Role/AddRoleClaim/roleid
		[HttpPost("{roleid}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRoleClaimAsync(string roleid, [Bind("ClaimType", "ClaimValue")] EditClaimModel model)
		{
			if (roleid == null) return NotFound("Can't find role");
			var role = await _roleManager.FindByIdAsync(roleid);
			if (role == null)
			{
				return NotFound("Can't find role");
			}
			model.role = role;
			if (!ModelState.IsValid) return View(model);


			if ((await _roleManager.GetClaimsAsync(role)).Any(c => c.Type == model.ClaimType && c.Value == model.ClaimValue))
			{
				ModelState.AddModelError(string.Empty, "Claim is existed in role");
				return View(model);
			}

			var newClaim = new Claim(model.ClaimType, model.ClaimValue);
			var result = await _roleManager.AddClaimAsync(role, newClaim);

			if (!result.Succeeded)
			{
				ModelState.AddModelError(result);
				return View(model);
			}

			StatusMessage = "You just add new claims";

			return RedirectToAction("Edit", new { roleid = role.Id });

		}

		// GET: /Role/EditRoleClaim/claimid
		[HttpGet("{claimid:int}")]
		public async Task<IActionResult> EditRoleClaim(int claimid)
		{
			var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
			if (claim == null) return NotFound("Can't find role");

			var role = await _roleManager.FindByIdAsync(claim.RoleId.ToString());
			if (role == null) return NotFound("Can't find role");
			ViewBag.claimid = claimid;

			var Input = new EditClaimModel()
			{
				ClaimType = claim.ClaimType,
				ClaimValue = claim.ClaimValue,
				role = role
			};


			return View(Input);
		}

		// GET: /Role/EditRoleClaim/claimid
		[HttpPost("{claimid:int}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditRoleClaim(int claimid, [Bind("ClaimType", "ClaimValue")] EditClaimModel Input)
		{
			var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
			if (claim == null) return NotFound("Can't find role");

			ViewBag.claimid = claimid;

			var role = await _roleManager.FindByIdAsync(claim.RoleId.ToString());
			if (role == null) return NotFound("Can't find role");
			Input.role = role;
			if (!ModelState.IsValid)
			{
				return View(Input);
			}
			if (_context.RoleClaims.Any(c => c.RoleId == role.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue && c.Id != claim.Id))
			{
				ModelState.AddModelError(string.Empty, "Claim is existed in role");
				return View(Input);
			}


			claim.ClaimType = Input.ClaimType;
			claim.ClaimValue = Input.ClaimValue;

			await _context.SaveChangesAsync();

			StatusMessage = "You just update claim";

			return RedirectToAction("Edit", new { roleid = role.Id });
		}
		// POST: /Role/EditRoleClaim/claimid
		[HttpPost("{claimid:int}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteClaim(int claimid, [Bind("ClaimType", "ClaimValue")] EditClaimModel Input)
		{
			var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
			if (claim == null) return NotFound("Can't find role");

			var role = await _roleManager.FindByIdAsync(claim.RoleId.ToString());
			if (role == null) return NotFound("Can't find role");
			Input.role = role;
			if (!ModelState.IsValid)
			{
				return View(Input);
			}
			if (_context.RoleClaims.Any(c => c.RoleId == role.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue && c.Id != claim.Id))
			{
				ModelState.AddModelError(string.Empty, "Claim is existed in role");
				return View(Input);
			}


			await _roleManager.RemoveClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));

			StatusMessage = "You just delete claim";


			return RedirectToAction("Edit", new { roleid = role.Id });
		}
	}
}
