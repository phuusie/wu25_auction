using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wu25_auction.Web.Data;
using wu25_auction.Web.Data.Entities;
using wu25_auction.Web.Data.SeedData;
using wu25_auction.Web.Infrastructure.HttpClientRegistration;
using wu25_auction.Web.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRazorPages();
builder.Services.AddHttpClientRegistration();

builder.Services.AddRazorPages();

builder.Services.AddHttpClientRegistration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    await context.Database.MigrateAsync();
    await scope.ServiceProvider.SeedIdentityAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();