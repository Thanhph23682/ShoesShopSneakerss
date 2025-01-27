﻿using APPDATA.DB;
using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using APPMVC.Data;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<APPMVCContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("APPMVCContext") ?? throw new InvalidOperationException("Connection string 'APPMVCContext' not found.")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShopDbContext>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 3;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight; //thông số cài tùy ý
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.UseStaticFiles();  // Cấu hình để phục vụ các tệp tĩnh từ wwwroot
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
