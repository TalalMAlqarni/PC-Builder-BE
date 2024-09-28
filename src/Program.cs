using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using src.Database;
using src.Entity;
using src.Repository;
using src.Services.Category;
using src.Services.Payment;
using src.Services.product;
using src.Services.SubCategory;
using src.Services.user;
using src.Utils;

var builder = WebApplication.CreateBuilder(args);
//connect to database
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
}
);
// add utomapper
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
// add DI service
builder
    .Services.AddScoped<IUserService , UserService>().AddScoped<UserRepository , UserRepository>();

builder
    .Services.AddScoped<IProductService, ProductService>()
    .AddScoped<ProductRepository, ProductRepository>();

builder
    .Services.AddScoped<ICategoryService, CategoryService>()
    .AddScoped<CategoryRepository, CategoryRepository>();

builder
    .Services.AddScoped<ISubCategoryService, SubCategoryService>()
    .AddScoped<SubCategoryRepository, SubCategoryRepository>();
    
builder
    .Services.AddScoped<IPaymentService, PaymentService>()
    .AddScoped<PaymentRepository, PaymentRepository>();


//Add controllers
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// test database connection
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    try
    {
        if (context.Database.CanConnect())
        {
            Console.WriteLine("Database connection successful");
        }
        else
        {
            Console.WriteLine("Database connection failed");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.Run();
