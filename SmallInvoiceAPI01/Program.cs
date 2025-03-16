using Microsoft.EntityFrameworkCore;
using SmallInvoice.Infrastructure.Entity.Data;
using SmallInvoice.Infrastructure.Adapters.Repository;
using SmallInvoice.Domain.Ports.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Logging.AddLog4Net();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string conectionName = "Devdb";
string strConection = builder.Configuration.GetConnectionString(conectionName) ?? throw new InvalidOperationException($"Connection string '{conectionName}' not found");
builder.Services.AddDbContext<SmallInvoice025DbContext>(option => option.UseSqlServer(strConection), ServiceLifetime.Scoped);

builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerKitchenRepository, CustomerKitchenRepository>();
builder.Services.AddScoped<IPriceListRepository, PriceListRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); G.R

app.UseAuthorization();

app.MapControllers();

app.Run();
