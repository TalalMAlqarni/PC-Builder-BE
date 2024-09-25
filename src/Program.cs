using Npgsql;
using Microsoft.EntityFrameworkCore;
using src.Database;
var builder = WebApplication.CreateBuilder(args);
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
builder.Services.AddDbContext<DatabaseContext>( options => {
    options.UseNpgsql(dataSourceBuilder.Build());
    }
);
//Add controllers
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.Run();
