var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration
    .AddJsonFile("./pizzas.json");

var pizzaSection = builder.Configuration.GetSection("Pizzas");
var pizzaList = pizzaSection.Get<List<laboratorna6_schedrov.Models.Product>>();

builder.Services.AddSingleton<IEnumerable<laboratorna6_schedrov.Models.Product>>(pizzaList);

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Register}/{id?}");

app.Run();
