using Figure.Application;
using Figure.Core.MappingProfiles;
using Figure.DataAccess;
using Figure.DataAccess.Repositories;
using Figure.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//CONTROLLERS
builder.Services.AddControllers().AddNewtonsoftJson();

//SEEDERS
builder.Services.AddSingleton<IDbSeeder, DbSeeder>();

//DB CONTEXT
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    if(builder.Environment.IsDevelopment()){
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // CHANGE THIS FOR ACTUAL DB IN CLOUD
    }
});

//AUTOMAPPER
builder.Services.AddAutoMapper(typeof(DefaultMappingProfile));

//LOGGING
var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log",rollingInterval: RollingInterval.Day).CreateLogger();
builder.Services.AddSingleton(typeof(Serilog.ILogger),log);

//SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//REPOSITORIES
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IFiguresRepository, FiguresRepository>();

//HANDLERS
HandlerConfig.RegisterAllHandlers(builder.Services);

//----------------------------------------------------------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

