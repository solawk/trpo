using TRPO_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:7001;http://localhost:7000");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ElementService>(client => client.BaseAddress = new Uri("https://localhost:5001"));
builder.Services.AddHttpClient<CategoryService>(client => client.BaseAddress = new Uri("https://localhost:5001"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Element}/{action=List}/{id?}");

app.Run();