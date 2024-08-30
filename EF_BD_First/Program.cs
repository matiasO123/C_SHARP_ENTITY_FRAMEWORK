using Microsoft.EntityFrameworkCore;
using EF_BD_First.Models; // Asegúrate de que este namespace sea el correcto para tu proyecto

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Agrega servicios para MVC con vistas
builder.Services.AddDbContext<EfDbFirstContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("cs") ?? throw new NotImplementedException("Falta configurar la conexión a la base de datos")));

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Define endpoint routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Persona}/{action=Index}/{id?}");



app.Use(async (context, next) =>
{
    if (context.Request.Path == "/routes")
    {
        var endpointDataSource = context.RequestServices.GetRequiredService<EndpointDataSource>();
        var endpoints = endpointDataSource.Endpoints;

        var response = context.Response;
        response.ContentType = "text/plain";

        foreach (var endpoint in endpoints)
        {
            await response.WriteAsync(endpoint.DisplayName + "\n");
        }
    }
    else
    {
        await next();
    }
});



app.Run();
