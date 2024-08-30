using Ef_code_first.Models;
using Ef_code_first.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Curso_EF_Context>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("cs") ?? throw new NotImplementedException("Falta configurar la conexión a la base de datos")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddScoped<IUserRepository, UserRepository>();

//if a lot of interface repositories must be added, we should use reflection for adding all of them at once
/*
 var assembly = typeof(Startup).Assembly;

var repositoryTypes = assembly.GetTypes()
    .Where(t => t.Name.EndsWith("Repository") && !t.IsInterface);

foreach (var implementationType in repositoryTypes)
{
    var interfaceType = implementationType.GetInterfaces().FirstOrDefault(i => i.Name == $"I{implementationType.Name}");
    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, implementationType);
    }
}
 */

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    Curso_EF_Context context = scope.ServiceProvider.GetRequiredService<Curso_EF_Context>();
    //For creating the database from the code
    //context.Database.EnsureCreated();

    //------------------------------------------------------

    //For creatin the databade from the code, but with migrations...
    context.Database.Migrate();
    //Run this in the console -> dotnet ef migrations add MigrationInitial
    //If the database already exists, delete it.
    //add "email" property  to "user" and then run that in the console
    //dotnet ef migrations add addEmailToUser
    //Restart the program. Migrations will automatically update the "user" table
    //If you wanna add initial data, do it with seeds -> dotnet ef migrations add addEmailToUser

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});
app.Run();
