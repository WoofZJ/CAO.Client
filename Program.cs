using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CAO.Client;
using CAO.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    string apiBaseAddress = config["ApiSettings:BaseAddress"]
        ?? builder.HostEnvironment.BaseAddress;
    Console.WriteLine($"Using API base address: {apiBaseAddress}");
    return new HttpClient
    {
        BaseAddress = new Uri(apiBaseAddress)
    };
});
builder.Services.AddScoped<BlogService>();

await builder.Build().RunAsync();
