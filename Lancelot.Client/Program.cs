using Lancelot.Client;
using Lancelot.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<PlayerManager>();
builder.Services.AddScoped<EnemiesManager>();
builder.Services.AddScoped<BulletManager>();
builder.Services.AddScoped<GameStateManager>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Lancelot.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Lancelot.ServerAPI"));

await builder.Build().RunAsync();
