using CRUD_Practice.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProductoDAL>();
builder.Services.AddScoped<MarcaDAL>();
builder.Services.AddScoped<CategoriaDAL>();
builder.Services.AddScoped<ClienteDAL>();
builder.Services.AddScoped<ProveedorDAL>();
builder.Services.AddScoped<StockDAL>();
builder.Services.AddScoped<CompraDAL>();
builder.Services.AddScoped<VentaDal>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
