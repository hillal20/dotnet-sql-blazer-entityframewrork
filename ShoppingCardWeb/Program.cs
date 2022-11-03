using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShoppingCardWeb;
using ShoppingCardWeb.Services;
using ShoppingCardWeb.Services.Contracts;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7031/") });


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService , ShoppingCartService>();

await builder.Build().RunAsync();

