using bai3.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(options =>
{
	options.Cookie.Name = "PhucLongCookie";
	options.LoginPath = "/User/Login";
});

builder.Services.AddSession();

var connectionString =

builder.Configuration.GetConnectionString("WebsiteBanHangConnection");
builder.Services.AddDbContext<WebsiteBanHangContext>(options =>
options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
   _=endpoints.MapControllerRoute(
    name: "trang-chu",
    pattern: "trang-chu",
    defaults: new { controller = "Home", action = "Index" });
    _ = endpoints.MapControllerRoute(
     name: "bai-viet",
     pattern: "bai-viet",
     defaults: new { controller = "Blog", action = "Index" });
    _ = endpoints.MapControllerRoute(
    name: "chi-tiet-bai-viet",
     pattern: "bai-viet/{slug}-{id}",
     defaults: new { controller = "Blog", action = "BlogDetail" });
    _ = endpoints.MapControllerRoute(
     name: "gio-hang",
     pattern: "gio-hang",
     defaults: new { controller = "Cart", action = "Index" });
    _=endpoints.MapControllerRoute(
     name: "them-gio-hang",
     pattern: "them-gio-hang",
     defaults: new { controller = "Cart", action = "AddItem" });
	_=endpoints.MapControllerRoute(
     name: "thanh-toan",
     pattern: "thanh-toan",
     defaults: new { controller = "Cart", action = "Payment" });
	_=endpoints.MapControllerRoute(
     name: "hoan-thanh",
     pattern: "hoan-thanh",
     defaults: new { controller = "Cart", action = "Success" });
	_ = endpoints.MapControllerRoute(
     name: "lien-he",
     pattern: "lien-he",
     defaults: new { controller = "Contact", action = "Index" });
    _ = endpoints.MapControllerRoute(
     name: "san-pham",
     pattern: "san-pham",
     defaults: new { controller = "Product", action = "Index" });

    _ = endpoints.MapControllerRoute(
	 name: "dang-ky",
	 pattern: "dang-ky",
	 defaults: new { controller = "User", action = "Register" });

	_=endpoints.MapControllerRoute(
	 name: "dang-nhap",
	 pattern: "dang-nhap",
	 defaults: new { controller = "User", action = "Login" });

    _=endpoints.MapControllerRoute(
     name: "thong-tin",
     pattern: "thong-tin",
     defaults: new { controller = "User", action = "Info" });

    _ = endpoints.MapControllerRoute(
    name: "Edit",
    pattern: "Edit",
    defaults: new { controller = "User", action = "Edit" });

    _ =endpoints.MapControllerRoute(
     name: "dang-xuat",
     pattern: "dang-xuat",
     defaults: new { controller = "User", action = "Logout" });

	

    _=endpoints.MapControllerRoute(
     name: "them-gio-hang",
     pattern: "them-gio-hang",
     defaults: new { controller = "Cart", action = "AddItem" });

  

	
    _=endpoints.MapControllerRoute(
     name: "the-loai-san-pham",
     pattern: "{slug}-{id}",
     defaults: new { controller = "Product", action = "CateProd" });

    _=endpoints.MapControllerRoute(
    name: "chi-tiet-san-pham",
     pattern: "san-pham/{slug}-{id}",
     defaults: new { controller = "Product", action = "ProdDetail" });
    
    _=endpoints.MapControllerRoute(
     name: "chuong-trinh",
     pattern: "chuong-trinh/{slug}",
     defaults: new { controller = "Product", action = "Index" });

    // Các route cho phần quản trị
    _=endpoints.MapControllerRoute(
        name: "admin-default",
        pattern: "admin/{controller=Admin}/{action=Index}/{id?}");

    _=endpoints.MapControllerRoute(
        name: "admin-login",
        pattern: "admin/login",
        defaults: new { controller = "Admin", action = "Login" });

    _=endpoints.MapControllerRoute(
        name: "admin-logout",
        pattern: "admin/logout",
        defaults: new { controller = "Admin", action = "Logout" });

    _=endpoints.MapControllerRoute(
        name: "admin-product",
        pattern: "admin/product",
        defaults: new { controller = "Admin", action = "Product" });

    _=endpoints.MapControllerRoute(
        name: "admin-user",
        pattern: "admin/user",
        defaults: new { controller = "Admin", action = "User" });

    _ =app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
