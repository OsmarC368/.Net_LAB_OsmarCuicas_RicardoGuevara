using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Core.Interfaces.Services;
using Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.OpenApi.Models;

using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi;
using Core.Interfaces.Entities;
using Core.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "",
        Description = "",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Osmar Cuicas",
            Url = new Uri("https://example.com")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com")
        }
    });
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});



//builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//
builder.Services.AddScoped(typeof(IUserTypeService), typeof(UserTypeService));
builder.Services.AddScoped(typeof(IMeasureService), typeof(MeasureService));
//
builder.Services.AddScoped(typeof(IUserTypeRepository), typeof(UserTypeRepository));
builder.Services.AddScoped(typeof(IMeasureRepository), typeof(MeasureRepository));
//
//
builder.Services.AddDbContext<AppDbContext>(dataBase => 
        dataBase.UseNpgsql("Host=ep-proud-fog-a8gjdnjq-pooler.eastus2.azure.neon.tech; Database=neondb; Username=neondb_owner; Password=npg_lH6pvc3KSVCD; SSL Mode=VerifyFull; Channel Binding=Require;",
        b => b.MigrationsAssembly("Infrastructure")));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Introduce el token usando: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "MiApiBackend",
        ValidAudience = "MiApiClientes",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("superclaveultrasegura_12345678900000!")
        )
    };
});
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
