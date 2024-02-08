using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace DummyJsonMiddleware;

public static class DummyJsonExtensions
{
    public static void MapGets(this WebApplication app)
    {

        app.MapGet("/products/{id}", async Task<Results<Ok<Product>, NotFound>> (int id, DummyJsonService service) =>
          {
              var product = await service.GetProductAsync(id);

              return product != null ? TypedResults.Ok(product) : TypedResults.NotFound();

          });

        app.MapGet("/products", async Task<Ok<AllProducts>> (DummyJsonService service) =>
        {
            var products = await service.GetProductsAsync();

            return TypedResults.Ok(products);

        });

    }

    public static void MapPosts(this WebApplication app)
    {
        app.MapPost("/products", async (Product product, DummyJsonService service) =>
        {

            var responseMessage = await service.AddProductAsync(new Product(0, product.Title, product.Description, product.Price, product.DiscountPercentage, product.Rating, product.Stock, product.Brand, product.Category, product.Thumbnail, product.Images.ToList()));


            return responseMessage;
        });
    }

    public static void MapPatches(this WebApplication app)
    {
        app.MapPatch("/products/{id}", async (int id, Product product, DummyJsonService service) =>
        {

            var responseMessage = await service.UpdateProductAsync(id, new Product(0, product.Title, product.Description, product.Price, product.DiscountPercentage, product.Rating, product.Stock, product.Brand, product.Category, product.Thumbnail, product.Images.ToList()));


            return responseMessage;
        });
    }

    public static void MapDeletes(this WebApplication app)
    {

        app.MapDelete("/products/{id}", async Task<Results<Ok<Product>, NotFound>> (int id, DummyJsonService service) =>
          {
              var product = await service.DeleteProductAsync(id);

              return product != null ? TypedResults.Ok(product) : TypedResults.NotFound();

          });

    }
}