using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RuomRaCoffe.Admin.API;
using RuomRaCoffe.Admin.Services;
using RuomRaCoffe.API.Data.Entities;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();

if (apiSettings == null || string.IsNullOrEmpty(apiSettings.BaseUrl))
{
    throw new InvalidOperationException("ApiSettings:BaseUrl is not configured");
}

// HTTP Client
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(apiSettings.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/forbidden";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<StaffService>();

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// MudBlazor
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/_Host");

app.Run();