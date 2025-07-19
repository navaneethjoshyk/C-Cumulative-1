var builder = WebApplication.CreateBuilder(args);

//  Enable MVC (not just API)
builder.Services.AddControllersWithViews(); // <- REQUIRED for Views

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

//  Setup view routing
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TeachersPage}/{action=Index}/{id?}");

// Still support APIs
app.MapControllers();

app.Run();