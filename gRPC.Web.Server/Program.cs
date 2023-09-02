var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
var app = builder.Build();
app.UseGrpcWeb();

app.MapGrpcService<StreamImplService>().EnableGrpcWeb();

app.Run();
