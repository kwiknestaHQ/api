using KwikNestaGateway.API.Extensions;
using KwikNestaGateway.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();
app.UseErrorHandler();
app.UseMiddlewares(builder.Configuration);
app.Run();