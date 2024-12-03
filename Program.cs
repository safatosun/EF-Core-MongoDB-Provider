using Microsoft.EntityFrameworkCore;
using MongoDbProvider.Repositories;
using MongoDbProvider.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var mongoDBConfig = builder.Configuration.GetSection("MongoDbSettings");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseMongoDB(mongoDBConfig["AtlasURI"], mongoDBConfig["DatabaseName"]));

builder.Services.AddScoped<IPetService,PetService>();
builder.Services.AddScoped<IUserService,UserService>();

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
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
