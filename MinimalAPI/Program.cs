using MinimalAPI.Models;

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
    var content = string.Join('\n', products.Select(temp => temp.ToString()));
    //1, xxxxx
    //2, xxxxx
    await context.Response.WriteAsync(content); 
});

app.MapPost("/products", async (HttpContext context, Product product) =>
{
    products.Add(product);
    await context.Response.WriteAsync("Product Added");
});

app.Run();
