using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Selection.ProductService.Data;

var builder = WebApplication.CreateBuilder(args);

// Prepara la app para exponer y visualizar autom�ticamente la documentaci�n de las APIs REST.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Restringe las culturas admitidas tanto para los datos como para la UI solo a "en-US".
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = new[] { new CultureInfo("en-US") };
    options.SupportedUICultures = new[] { new CultureInfo("en-US") };
});

// Conexi�n a SQL server
builder.Services.AddDbContext<ProductContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilita el uso de controladores que derivan de ControllerBase o Controller.
builder.Services.AddControllers();

var app = builder.Build();
app.UseRequestLocalization();

// Garantiza que la DB est� siempre actualizada con las �ltimas migraciones pendientes antes de procesar cualquier solicitud
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
    dbContext.Database.Migrate();
}

// Activa la documentaci�n interactiva de Swagger solo cuando la aplicaci�n est� en modo desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();