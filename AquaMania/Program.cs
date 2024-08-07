using AquaMania.DataContext;
using AquaMania.Repository;
using AquaMania.Repository.Interface;
using AquaMania.Service;
using AquaMania.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var environment = "";

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is null)
{
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
    environment = "Development";
    Console.WriteLine("ASPNETCORE_ENVIRONMENT does not exists! Setting up Development Value: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
}
else
{
    Console.WriteLine("ASPNETCORE_ENVIRONMENT Value: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
}

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = environment,
    WebRootPath = "wwwroot"
});


// Add services to the container.

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISerVivoRepository, SerVivoRepository>();
builder.Services.AddScoped<ISerVivoService, SerVivoService>();


builder.Services.AddControllers();

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddCors();

var privateKey = builder.Configuration.GetSection("PrivateKey").Value;

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Aquamania API",
        Description = "Documentação da API do Microserviço de Integração com Aquariomania",
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
    config.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
            });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
             new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.DocumentTitle = "Aquamania";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aquamania V1");
    });
}

app.UseCors(builder => builder
       .AllowAnyMethod()
       .AllowAnyHeader()
       .SetIsOriginAllowed(origin => true)
       .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
