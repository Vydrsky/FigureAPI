using Figure.Core;
using Figure.Core.Handlers.Order;
using Figure.Core.MappingProfiles;
using Figure.Core.Models.Order;
using Figure.Core.Queries;
using Figure.DataAccess;
using Figure.DataAccess.Interfaces;
using Figure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//CONTROLLERS
builder.Services.AddControllers();

//SEEDERS
builder.Services.AddSingleton<IDbSeeder, DbSeeder>();

//DB CONTEXT
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

//HANDLERS
QueryHandlerConfig.AddQueryHandler<GetAllOrdersQuery, IEnumerable<ReadOrderModel>, GetAllOrdersQueryHandler>(builder.Services);

var app = builder.Build();

//----------------------------------------------------------------------------------------------
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
