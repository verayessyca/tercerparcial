using Microsoft.EntityFrameworkCore;
using PrimerParcial.Data;
using PrimerParcial.Interfaces;
using PrimerParcial.Models;
using PrimerParcial.Repositories;
using PrimerParcial.Services;
using PrimerParcial;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUserRepository, UsersInMemoryRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddEndpointsApiExplorer();

var info = new OpenApiInfo
{
    Title = "Curso JWT"
};
var security = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT para curso ua"
};

var requirement = new OpenApiSecurityRequirement()
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
        new List<string>() 
    }
};

builder.Services.AddSwaggerGen(options =>
{
    //options.SwaggerDoc("api", info);
    options.AddSecurityDefinition("Bearer", security);
    options.AddSecurityRequirement(requirement);
});


builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(nameof(TokenSettings)));
builder.Services.AddAuthentication().AddJwtBearer("CURSO-UA", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["TokenSettings:Issuer"],
        ValidAudience = builder.Configuration["TokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenSettings:Secret"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes("CURSO-UA")
    .Build();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("PrimerParcialContext");
builder.Services.AddDbContext<PrimerParcialContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.MapPost("/login", async (IUserService userService, IAuthenticationService authenticationService, AuthenticationRequest request) =>
{
    var isValidAuthentication = await authenticationService.Authenticate(request.Username, request.Password);
    if (isValidAuthentication)
    {
        var user = await userService.GetByCredentials(request.Username, request.Password);
        var token = await authenticationService.GenerateJwt(user);
        return Results.Ok(new { AccessToken = token });
    }

    return Results.Forbid();
});

app.Run();
