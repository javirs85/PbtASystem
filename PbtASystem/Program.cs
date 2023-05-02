using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PbtASystem;
using PbtASystem.Components;
using PbtASystem.Pages;
using PbtASystem.Services;
using PbtASystem.Services.Moves;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<FirebaseAuth>();
builder.Services.AddScoped<FirebaseData>();
builder.Services.AddScoped<FirebaseMessaging>();
builder.Services.AddScoped<CharacterSelectionService>();
builder.Services.AddScoped<ConfirmationModalService>();
builder.Services.AddScoped<USMovesService>();

await builder.Build().RunAsync();
