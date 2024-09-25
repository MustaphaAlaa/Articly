using Entities;
using Microsoft.EntityFrameworkCore;

using ServicesInterfaces;
using Repository_Interfaces;
using Repositories;
using Articly_Services;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventLog();
});



//Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ITag, TagService>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IArticle, ArticleServices>();
builder.Services.AddScoped<IArticleTagRepository, ArticleTagRepository>();
builder.Services.AddScoped<IArticleTag, ArticleTagServices>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddDbContext<ArticleDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});



builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders
     | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    //The default HSTS value is 30 days. You may want to change this for production scenarios,
    //see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();