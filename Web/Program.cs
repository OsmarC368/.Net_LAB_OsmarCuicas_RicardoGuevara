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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

