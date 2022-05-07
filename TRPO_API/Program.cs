using TRPO_API;
using TRPO_BL.BusinessLogic;
using TRPO_DA;
using TRPO_DA.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutomapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Add(new ServiceDescriptor(typeof(ElementBL), typeof(ElementBL), ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(typeof(ElementDataAccess), typeof(ElementDataAccess), ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(typeof(CategoryBL), typeof(CategoryBL), ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(typeof(CategoryDataAccess), typeof(CategoryDataAccess), ServiceLifetime.Transient));
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
