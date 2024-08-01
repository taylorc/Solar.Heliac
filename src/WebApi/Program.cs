using Solar.Heliac.Application;
using Solar.Heliac.Infrastructure;
using Solar.Heliac.Infrastructure.Persistence;
using Solar.Heliac.WebApi;
using Solar.Heliac.WebApi.Features;
using Solar.Heliac.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:3000");
                      });
});

builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<KnownExceptionsHandler>();

var app = builder.Build();



app.UseCors(myAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHealthChecks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseRouting();

app.UseDefaultExceptionHandler();
app.MapHeroEndpoints();
app.MapTeamEndpoints();

app.Run();

public partial class Program { }