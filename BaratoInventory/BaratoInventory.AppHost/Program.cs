var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache", 6379);

var apiService = builder.AddProject<Projects.BaratoInventory_ApiService>("apiservice").WithReference(cache);
builder.AddProject<Projects.BaratoInventory_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
