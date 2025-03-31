using Microsoft.EntityFrameworkCore;
using ShopNow.Infrastructure.Data;
using ShopNow.Infrastructure;
using ShopNow.Application;
using ShopNow.Application.DTOs.Mail;
using Microsoft.AspNetCore.Identity;
using ShopNow.Application.Services.Implements;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using ShowNow.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);
//"DefaultConnection": "Data Source=localhost;Initial Catalog=ShopNowDB;User ID=sa;Password=123;Encrypt=True;Trust Server Certificate=True"
// Add services to the container.
builder.Services.AddControllersWithViews();

#region configure db connection
builder.Services.AddDbContext<ShopNowDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region configure layer
builder.Services.AddInfrastructureService();
builder.Services.AddApplicationService(builder.Configuration);
#endregion

builder.Services.AddSession();

#region Identity
//Dang ki Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ShopNowDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
	// Thiết lập về Password
	options.Password.RequireDigit = false; // Không bắt phải có số
	options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
	options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
	options.Password.RequireUppercase = false; // Không bắt buộc chữ in
	options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
	options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

	// Cấu hình Lockout - khóa user
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
	options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
	options.Lockout.AllowedForNewUsers = true;

	// Cấu hình về User.
	options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = true;  // Email là duy nhất

	// Cấu hình đăng nhập.
	options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
	options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
	options.SignIn.RequireConfirmedAccount = true;
});

// Cấu hình Cookie
builder.Services.ConfigureApplicationCookie(options => {
	// options.Cookie.HttpOnly = true;  
	options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
	options.LoginPath = $"/login/"; // Url đến trang đăng nhập
	options.LogoutPath = $"/logout/";
	options.AccessDeniedPath = $"/Identity/Account/AccessDenied"; // Trang khi User bị cấm truy cập
});

builder.Services.AddAuthentication()
	.AddGoogle(options =>
	{
		var gconfig = builder.Configuration.GetSection("Authentication:Google");
		options.ClientId = gconfig["ClientId"];
		options.ClientSecret = gconfig["ClientSecret"];
		options.CallbackPath = "/login-google";
	})
	.AddFacebook(facebookOptions => {
		// Đọc cấu hình
		IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
		facebookOptions.AppId = facebookAuthNSection["AppId"];
		facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
		// Thiết lập đường dẫn Facebook chuyển hướng đến
		facebookOptions.CallbackPath = "/login-facebook";
	});
#endregion

var app = builder.Build();
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}");

//app.MapControllerRoute(
//	name: "default",
//	pattern: "{controller=Product}/{action=Manage}");

app.Run();