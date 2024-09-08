using Play.Inventory.Service.Clients;
using Polly;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient<CatalogClient>(client => {
    client.BaseAddress = new Uri("https://localhost:5001");
})
.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(
    5,
    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
    onRetry : (OutcomeType, TimeSpan, retryAttempt) => {
    }
))
.AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
    3,
    TimeSpan.FromSeconds(15),
    onBreak: (outcome, timeSpan, retryAttempt) => {},
    onReset: () => {}
))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
