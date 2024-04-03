using Business.Services.Obs.Abstract;
using Business.Services.Obs.Concrete;
using Caching.Abstract;
using Caching.Concrete;
using DataAccess.Dal.Abstract;
using DataAccess.Dal.Concrete;
using Microsoft.IdentityModel.Tokens;

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
