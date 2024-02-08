using System.Net;

public sealed class DummyJsonService
{
    private readonly HttpClient _client;

    public DummyJsonService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var content = await _client.GetFromJsonAsync<Product>($"/products/{id}");

        return content;
    }

    public async Task<AllProducts> GetProductsAsync()
    {
        var content = await _client.GetFromJsonAsync<AllProducts>($"/products/");

        return content ?? new AllProducts([]);
    }

    public async Task<HttpResponseMessage?> AddProductAsync(Product product)
    {
        var response = await _client.PostAsJsonAsync($"/products/", product);

        return response;
    }

    internal async Task<HttpResponseMessage?> UpdateProductAsync(int id, Product product)
    {
        var response = await _client.PatchAsJsonAsync($"/products/", product);

        return response;
    }

    public async Task<Product?> DeleteProductAsync(int id)
    {
        var content = await _client.DeleteFromJsonAsync<Product>($"/products/{id}");

        return content;
    }
}