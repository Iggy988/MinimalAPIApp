using MinimalAPI.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Product> products = new List<Product>() 
{
    new Product(){ Id = 1, ProductName ="SmarthPhone"},
    new Product(){ Id = 2, ProductName ="LCD TV"},
    new Product(){ Id = 3, ProductName ="Laptop"}
};

//GET /products
app.MapGet("/products", async (HttpContext context) =>
{
    //var content = string.Join('\n', products.Select(temp => temp.ToString()));
    //1, xxxxx
    //2, xxxxx
    await context.Response.WriteAsync(JsonSerializer.Serialize(products)); 
});

//GET /products/{id}
app.MapGet("/products/{id:int}", async (HttpContext context, int id) =>
{
    Product? product = products.FirstOrDefault(temp => temp.Id == id);
    if (product == null)
    {
        context.Response.StatusCode = 400; // BadRequest
        await context.Response.WriteAsync("Id is inncorect");
        return;
    }
    await context.Response.WriteAsync(JsonSerializer.Serialize(product));
    
});

//POST /products
app.MapPost("/products", async (HttpContext context, Product product) =>
{
    products.Add(product);
    await context.Response.WriteAsync("Product Added");
});

app.Run();
