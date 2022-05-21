using SolvaBot.Data;
using SolvaBot.Services;
using SolvaBot.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ParameterFinder>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
//builder.Services.AddHttpClient<HttpService>();

builder.Services.AddScoped(x =>
{
    var apiUrl = new Uri(builder.Configuration["apiUrl"]);

    if (builder.Configuration["fakeBackend"] == "true")
    {
        var fakeBackendHandler = new FakeBackendHandler(x.GetService<ILocalStorageService>());
        return new HttpClient(fakeBackendHandler) { BaseAddress = apiUrl };
    }
    return new HttpClient() { BaseAddress = apiUrl };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
//var scope = app.Services.CreateScope();
//var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
//await accountService.Initialize();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

