using Cache.Data;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<RepositoryEcommerce>();
var app = builder.Build();
app.UseHttpsRedirection();


app.MapGet("/cachetest", (IMemoryCache _memoryCache, RepositoryEcommerce repositoryEcommerce) =>
{
    return repositoryEcommerce.GetProducts();
});
app.Run();