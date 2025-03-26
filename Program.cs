using Digital_Menu.Data;
using Digital_Menu.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// OVA LINIJA omogućava da aplikacija sluša na svim interfejsima (uključujući Docker)
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Registracija DbContext-a
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Locals.Any())
    {
        var local = new Local
        {
            Id = "sun-burgers",
            Name = "Sun Burgers",
            Package = "premium",
            Categories = new List<Category>
            {
                new Category
                {
                    Id = "burgers",
                    Name = "Burgeri",
                    Images = true,
                    Products = new List<Product>
                    {
                        new Product { Id = "p1", Name = "Cheeseburger", Price = 560 },
                        new Product { Id = "p2", Name = "Double Burger", Price = 760 }
                    },
                    Tags = new List<Tag> { new Tag { Name = "Food" } }
                }
            },
            Notifications = new List<Notification>
            {
                new Notification { Title = "Akcija", Message = "Popust na sve burgere danas!" }
            },
            Tables = Enumerable.Range(1, 24).Select(i => new Table { Id = i }).ToList()
        };

        db.Locals.Add(local);
        db.SaveChanges();


    }

    if (!db.Orders.Any())
    {
        var order = new Order
        {
            Status = "PORUCENO",
            CreatedAt = DateTime.UtcNow,
            TableId = 1, // Prvi sto (koji je već ubačen)
            Items = new List<OrderItem>
        {
            new OrderItem
            {
                Name = "Cheeseburger",
                Quantity = 2,
                Price = 560,
                Note = "Bez luka"
            },
            new OrderItem
            {
                Name = "Double Burger",
                Quantity = 1,
                Price = 760,
                Note = "Sa dodatnim sirom"
            }
        }
        };

        db.Orders.Add(order);
        db.SaveChanges();
    }

}





// Swagger samo u development modu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
