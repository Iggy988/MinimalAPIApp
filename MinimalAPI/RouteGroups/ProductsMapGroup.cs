using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;
using System.Text.Json;

namespace MinimalAPI.RouteGroups;

public static class ProductsMapGroup
{

    private static List<Product> products = new List<Product>()
        {
            new Product(){ Id = 1, ProductName ="SmarthPhone"},
            new Product(){ Id = 2, ProductName ="LCD TV"},
            new Product(){ Id = 3, ProductName ="Laptop"}
        };
    public static RouteGroupBuilder ProductsAPI(this RouteGroupBuilder group)
    {

        // GET / products
        //"/products" ne treba posto koristimo u mapGroup
        group.MapGet("/", async (HttpContext context) =>
        {
            //var content = string.Join('\n', products.Select(temp => temp.ToString()));
            //1, xxxxx
            //2, xxxxx
            //await context.Response.WriteAsync(JsonSerializer.Serialize(products));
            return Results.Ok(products);
        });


        //GET /products/{id}
        group.MapGet("/{id:int}", async (HttpContext context, int id) =>
        {
            Product? product = products.FirstOrDefault(temp => temp.Id == id);
            if (product == null)
            {
                //context.Response.StatusCode = 400; // BadRequest
                //await context.Response.WriteAsync("Id is inncorect");
                //return;
                return Results.BadRequest(new { error = "Incorrect Product ID" });
            }
            //await context.Response.WriteAsync(JsonSerializer.Serialize(product));

            return Results.Ok(product);

        });

        //POST /products
        group.MapPost("/", async (HttpContext context, Product product) =>
        {
            products.Add(product);
            //await context.Response.WriteAsync("Product Added");
            return Results.Ok(new { message = "Product Added" });
        });

        //PUT /products/{id}
        group.MapPut("/{id}", async (HttpContext context, int id, [FromBody] Product product) =>
        {
            Product? productFromCollection = products.FirstOrDefault(temp => temp.Id == id);
            if (productFromCollection == null)
            {
                //context.Response.StatusCode = 400; // BadRequest
                //await context.Response.WriteAsync("Id is inncorect");
                //return;
                return Results.BadRequest(new { error = "Incorrect Product ID" });
            }

            productFromCollection.ProductName = product.ProductName;

            //await context.Response.WriteAsync("Product Updated");

            return Results.Ok(new { message = "Product Updated" });
        });

        //DELETE /products/{id}
        group.MapDelete("/{id}", async (HttpContext context, int id) =>
        {
            Product? productFromCollection = products.FirstOrDefault(temp => temp.Id == id);
            if (productFromCollection == null)
            {
                //context.Response.StatusCode = 400; // BadRequest
                //await context.Response.WriteAsync("Id is inncorect");
                //return;

                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "id", new string[] { "Id is incorect" }}
                });
            }

            products.Remove(productFromCollection);

            //await context.Response.WriteAsync("Product Deleted");

            return Results.Ok(new { message = "Product Deleted"});
        });
        return group;
    }
}
