using Auth0.AspNetCore.Authentication;
using Blazored.SessionStorage;
using UltralinkAdmin;
using UltralinkAdmin.Components;
using MudBlazor.Services;
using UltralinkAdmin.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddMudServices();

builder.Services.AddHttpClient("ServerAPI",
      client => client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]))
      .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("ServerAPI"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.Scope = "openid profile email";
}).WithAccessToken(options => 
{
    options.Audience = builder.Configuration["Auth0:Audience"];
}
);

builder.Services.AddSingleton<ILoaderService, LoaderService>();
builder.Services.AddScoped<IDealerService, DealerService>();

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<TokenProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<AccessTokenMiddleware>();
app.UseRouting();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseStatusCodePagesWithRedirects("/404");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
