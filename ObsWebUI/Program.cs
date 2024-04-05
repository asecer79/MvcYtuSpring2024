using System.Diagnostics;
using Business.Services.Obs.Abstract;
using Business.Services.Obs.Concrete;
using Caching.Abstract;
using Caching.Concrete;
using DataAccess.Dal.Abstract;
using DataAccess.Dal.Concrete;
using Microsoft.IdentityModel.Tokens;
using ObsWebUI.MyMiddlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Dependency injection
builder.Services.AddSingleton<IFacultyDal, FacultyDal>();
builder.Services.AddSingleton<IDepartmentDal, DepartmentDal>();
builder.Services.AddSingleton<IFacultyService, FacultyService>();
builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
builder.Services.AddMemoryCache();
//builder.Services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
builder.Services.AddSingleton<ICacheProvider, RedisCacheProvider>();


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

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();


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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
