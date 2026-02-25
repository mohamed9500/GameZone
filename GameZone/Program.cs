using GameZone.Data;
using GameZone.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("No connection was string");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));



// ✅ Configure Antiforgery Cookies
//builder.Services.AddAntiforgery(options =>
//{
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // الكوكي يشتغل بس على HTTPS
//    options.Cookie.SameSite = SameSiteMode.Strict;           // يحمي من CSRF
//    options.Cookie.HttpOnly = true;                          // يمنع الجافاسكربت من الوصول
//});


builder.Services.AddScoped<ICategoriesService,CategoriesService>();
builder.Services.AddScoped<IDevicesService,DevicesService>();
builder.Services.AddScoped<IGamesServices, GamesServices>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Games/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();   // مهم علشان الصور و CSS و JS


app.UseAuthentication(); // لو عامل login
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
