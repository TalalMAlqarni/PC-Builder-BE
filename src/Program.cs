using Microsoft.EntityFrameworkCore;
using Npgsql;
using src.Database;
using src.Repository;
using src.Services.Category;
using src.Services.Payment;
using src.Services.product;
using src.Services.SubCategory;
using src.Services.user;
using src.Services.specifications;
using src.Services;
using src.Utils;
using src.Services.cart;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using src.Middlewares;
using static src.Entity.User;
using src.Services.review;
using Microsoft.EntityFrameworkCore.Diagnostics;
using src.Services.Coupon;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//connect to database
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
dataSourceBuilder.MapEnum<UserRole>();

var dataSource = dataSourceBuilder.Build();
builder.Services.AddSingleton(dataSource);


builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSource);
    options.ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
}
);


// add utomapper
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
// add DI service
builder
    .Services.AddScoped<IUserService, UserService>().AddScoped<UserRepository, UserRepository>()
    .AddScoped<IOrderService, OrderService>().AddScoped<OrderRepository, OrderRepository>()
    .AddScoped<IProductService, ProductService>().AddScoped<ProductRepository, ProductRepository>()
    .AddScoped<ICategoryService, CategoryService>().AddScoped<CategoryRepository, CategoryRepository>()
    .AddScoped<ISubCategoryService, SubCategoryService>().AddScoped<SubCategoryRepository, SubCategoryRepository>()
    .AddScoped<IPaymentService, PaymentService>().AddScoped<PaymentRepository, PaymentRepository>()
    .AddScoped<ICartService, CartService>().AddScoped<CartRepository, CartRepository>()
    .AddScoped<IReviewService, ReviewService>().AddScoped<ReviewRepository, ReviewRepository>()
    .AddScoped<ISpecificationsService, SpecificationsService>().AddScoped<SpecificationsRepository, SpecificationsRepository>()
    .AddScoped<ICouponService, CouponService>().AddScoped<CouponRepository, CouponRepository>();

//CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://pc-builder-fe.onrender.com", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
        });
});

builder.Services
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//auth for admin
// role
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


//Add controllers
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// for deployment part:
app.UseRouting();
app.MapGet("/", () =>
"Hello! please use https://pc-builder-be.onrender.com/api/v1/ {End Point You want to use or test}");
//
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
// add middleware 
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
//CORS
app.UseCors(MyAllowSpecificOrigins);


app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.Run();
