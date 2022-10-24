using Figure.Application;
using Figure.Core.MappingProfiles;
using Figure.DataAccess;
using Figure.DataAccess.Models;
using Figure.DataAccess.Repositories;
using Figure.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//CONTROLLERS
builder.Services.AddControllers().AddNewtonsoftJson();

//SEEDERS
builder.Services.AddSingleton<IDbSeeder, DbSeeder>();

//DB CONTEXT
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    if(builder.Environment.IsProduction()){
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));   // CHANGE THIS FOR ACTUAL DB IN CLOUD
    }
    else {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
    }
});

//AUTOMAPPER
builder.Services.AddAutoMapper(typeof(DefaultMappingProfile));

//LOGGING
var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log",rollingInterval: RollingInterval.Day).CreateLogger();
builder.Services.AddSingleton(typeof(Serilog.ILogger),log);

//AUTH
if (!builder.Environment.IsEnvironment("Testing")) {
    builder.Services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("ApiSecretKey"))),
            ValidateAudience = false,
            ValidateIssuer = false,
        };
    });
    builder.Services.AddAuthorization(options => {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
    });
}

//SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "JWT Authorization header using the bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{Reference = new OpenApiReference {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//REPOSITORIES
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IFiguresRepository, FiguresRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//IDENTITY
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//HANDLERS
HandlerConfig.RegisterAllHandlers(builder.Services);


//----------------------------------------------------------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

