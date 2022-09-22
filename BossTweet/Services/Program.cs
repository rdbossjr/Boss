using BossTweet.DataAccess;
using BossTweet.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var webServiceContextSettings = new TwitterWebServiceContextConfiguration();
builder.Configuration.GetSection(nameof(TwitterWebServiceContextConfiguration)).Bind(webServiceContextSettings);
builder.Services.AddSingleton<ITwitterWebServiceContextConfiguration, TwitterWebServiceContextConfiguration>(ss => webServiceContextSettings);

builder.Services.RegisterAsTransientByConvention("BossTweet.DataAccess", t => t.Name.EndsWith("Context", StringComparison.OrdinalIgnoreCase));
builder.Services.RegisterAsTransientByConvention("BossTweet.DataAccess", t => t.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase));
builder.Services.RegisterAsTransientByConvention("BossTweet.Business", t => t.Name.Contains("UoW", StringComparison.OrdinalIgnoreCase));

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