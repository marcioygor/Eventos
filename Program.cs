using SistemaDeEventos.Data;
using SistemaDeEventos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string mySqlConnection =  builder.Configuration.GetConnectionString("myConnection");
        builder.Services.AddDbContext<Context>(options => options.UseSqlServer(mySqlConnection));

// Add services to the container.
builder.Services.AddControllersWithViews();

var key = Encoding.ASCII.GetBytes(Settings.Secret); //Buscando a chave na classe Settings

//Nescessário os pacotes abaixo

//Microsoft.AspNetCore.Authentication
//Microsoft.AspNetCore.Authentication.JwtBearer

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
       {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy => policy.RequireClaim("Store", "user"));
    options.AddPolicy("admin", policy => policy.RequireClaim("Store", "admin"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
