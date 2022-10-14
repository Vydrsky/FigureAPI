using Figure.Application._Commands.Order;
using Figure.Application._Queries.Order;
using Figure.Application.Handlers.Order;
using Figure.Application.Models.Order;
using Figure.Core;
using Figure.Core.MappingProfiles;
using Figure.DataAccess;
using Figure.DataAccess.Interfaces;
using Figure.DataAccess.Repositories;
using Figure.Infrastructure;
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

//HANDLERS
QueryHandlerConfig.AddQueryHandler<GetAllOrdersQuery, IEnumerable<ReadOrderModel>, GetAllOrdersQueryHandler>(builder.Services);
QueryHandlerConfig.AddQueryHandler<GetArchivedOrdersQuery, IEnumerable<ReadOrderModel>, GetArchivedOrdersQueryHandler>(builder.Services);
QueryHandlerConfig.AddQueryHandler<GetNotArchivedOrdersQuery, IEnumerable<ReadOrderModel>, GetNotArchivedOrdersQueryHandler>(builder.Services);
QueryHandlerConfig.AddQueryHandler<GetOrderQuery, ReadOrderModel, GetOrderQueryHandler>(builder.Services);
CommandHandlerConfig.AddCommandHandler<PostOrderCommand, PostOrderCommandHandler>(builder.Services);
CommandHandlerConfig.AddCommandHandler<UpdateOrderCommand, UpdateOrderCommandHandler>(builder.Services);
CommandHandlerConfig.AddCommandHandler<DeleteOrderCommand, DeleteOrderCommandHandler>(builder.Services);
CommandHandlerConfig.AddCommandHandler<PatchOrderCommand, PatchOrderCommandHandler>(builder.Services);
CommandHandlerConfig.AddCommandHandler<ArchiveOrderCommand, ArchiveOrderCommandHandler>(builder.Services);
CommandHandlerConfig.AddCommandHandler<DeArchiveOrderCommand, DeArchiveOrderCommandHandler>(builder.Services);

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
