using System.Reflection.Metadata.Ecma335;
using DummyJsonMiddleware;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DummyJsonOptions>(builder.Configuration.GetSection("DummyJsonSettings"));

builder.Services.AddHttpClient<DummyJsonService>((serviceProvider, httpClient) =>
{
    var baseUrl = serviceProvider.GetRequiredService<IOptions<DummyJsonOptions>>().Value.BaseUrl;

    httpClient.BaseAddress = new Uri(baseUrl);

});


var app = builder.Build();

app.MapGets();

app.MapPosts();

app.MapPatches();

app.MapDeletes();

app.Run();
