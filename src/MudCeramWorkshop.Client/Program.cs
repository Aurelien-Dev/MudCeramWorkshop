using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudCeramWorkshop.Client.Components;
using MudCeramWorkshop.Client.Components.Account;
using MudCeramWorkshop.Client.Routes;
using MudCeramWorkshop.Client.Utils;
using MudCeramWorkshop.Data.Domain.Models.Identity;
using MudCeramWorkshop.Data.Domain.Models.WorkshopDomaine;
using MudCeramWorkshop.Data.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddLocalization();
builder.Services.AddMudLocalization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddRepository();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddTransient<IdentityCookieHandler>();
builder.Services.AddHttpClient("LocalApi", (client) =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["BaseUrl:ApiUrl"]}/api/");
}).AddHttpMessageHandler<IdentityCookieHandler>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(op => op.Events.OnRedirectToLogin = (contxext) =>
{
    if (contxext.HttpContext.Request.Path.HasValue && contxext.HttpContext.Request.Path.Value.Contains("api"))
    {
        contxext.Response.StatusCode = 401;
    }
    else
    {
        var redirectpath = contxext.RedirectUri;
        contxext.HttpContext.Response.Redirect(redirectpath);
    }
    return Task.CompletedTask;
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<UserService>();
builder.Services.AddCascadingValue(p => p.GetService<UserService>().GetUserState());

var app = builder.Build();

// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapPageRoute();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();// Add additional endpoints required by the Identity /Account Razor components.

app.MapAdditionalIdentityEndpoints();

await app.RunAsync();