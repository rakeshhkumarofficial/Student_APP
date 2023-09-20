using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using STUDENT_WEB;
using STUDENT_WEB.Services;
using STUDENT_WEB.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredToast();
builder.Services.AddScoped<IStudentContract, StudentContract>();
builder.Services.AddScoped<IAPIGatewayContract, APIGatewayContract>();

await builder.Build().RunAsync();
