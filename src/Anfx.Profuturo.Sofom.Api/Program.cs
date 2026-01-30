using Anfx.Profuturo.Sofom.Api;
using Anfx.Profuturo.Sofom.Api.Infrastructure;
using Anfx.Profuturo.Sofom.Infrastructure;
using Anfx.Profuturo.Sofom.Application;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.AddWebServices();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.WithTitle("Sofom API Documentation");
    options.WithTheme(ScalarTheme.DeepSpace);
    options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    options.HideSearch = true;// Habilita/Deshabilita el buscador (Ctrl+K)
    options.ShowSidebar = true; // Muestra u oculta la barra lateral
    options.DarkMode = false;
});
app.UseRouting();

app.Map("/", () => Results.Redirect("/scalar"));


app.MapEndpoints();

app.MapControllers();


app.Run();
