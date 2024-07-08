using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudCeramWorkshop.Client.Components.Account;
using MudCeramWorkshop.Client.IdentityData;
using MudCeramWorkshop.Client.IdentityData.Model;
using MudCeramWorkshop.Client.MinimalApi;
using MudCeramWorkshop.Data.Repository;
using MudCeramWorkshop.Client.Components;

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


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<WorkshopUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<WorkshopUser>, IdentityNoOpEmailSender>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(op => op.Events.OnRedirectToLogin = async (contxext) =>
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
});

var app = builder.Build();

// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var identitydbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    identitydbContext.Database.Migrate();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
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

app.Run();

