using MiniPay_Backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet($"/", () => Results.Redirect($"/swagger/index.html")).ExcludeFromDescription();

app.UseProviderEndpoints();
app.UseTransactionEndpoints();

app.Run();
