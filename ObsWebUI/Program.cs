using System.Configuration;
using System.Diagnostics;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.AuthorizationServices;
using Business.AuthorizationServices.Abstract;
using Business.AuthorizationServices.Concrete;
using Business.CommonServices.Abstract;
using Business.CommonServices.Concrete;
using Business.Services.Obs.Abstract;
using Business.Services.Obs.Concrete;
using Business.Services.Obs.DependencyResolver;
using Caching.Abstract;
using Caching.Concrete;
using DataAccess.Dal.Abstract;
using DataAccess.Dal.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using ObsWebUI.MyMiddlewares;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new ObsDependencyModule());
} );


var cookieOptions = builder.Configuration.GetSection("CookieOptions").Get<CookieAuthOptions>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.Cookie.Name = cookieOptions!.Name;
        options.LoginPath = cookieOptions.LoginPath;
        options.LogoutPath = cookieOptions.LogoutPath;
        options.AccessDeniedPath = cookieOptions.AccessDeniedPath;
        options.SlidingExpiration = cookieOptions.SlidingExpiration;
        options.ExpireTimeSpan = TimeSpan.FromSeconds(cookieOptions.TimeOut);
    }
);


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

app.UseCookiePolicy();

app.UseRouting();




//basic simple middleware
//app.Run(async (context) =>
//{
//    //context.Response.Redirect($"/Auth/Login/");

//    Debug.WriteLine(context.Request.HttpContext.User.Identity.IsAuthenticated); //security
//    Debug.WriteLine(context.Request.Host.Value);//ip address 
//    Debug.WriteLine(context.Request.Path);//ip address 


//});

//app.Run(async (context) => Mid1.MyMiddleware1(context));


//app.Use(async (context, next) =>
//{
//    Debug.WriteLine("M1-Request:"+context.Request.Path); 
//    next();
//    Debug.WriteLine("M1-Response:" + context.Response.StatusCode);
//});
//app.Use(async (context, next) =>
//{
//    Debug.WriteLine("M2-Request:" + context.Request.Path);
//    next();
//    Debug.WriteLine("M2-Response:" + context.Response.StatusCode);
//});
//app.Use(async (context, next) =>
//{
//    Debug.WriteLine("M3-Request:" + context.Request.Path);
//    next();
//    Debug.WriteLine("M3-Response:" + context.Response.StatusCode);
//});
//app.Use(async (context, next) =>
//{
//    Debug.WriteLine("M4-Request:" + context.Request.Path);
//    next();
//    Debug.WriteLine("M4-Response:" + context.Response.StatusCode);
//});

app.UseMiddleware<IpLoggerMiddleware>();

app.UseMiddleware<AccessLoggerMiddleware>();

app.UseMiddleware<ErrorLoggerMiddleware>();

app.UseMiddleware<PerformanceLoggerMiddleware>();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
