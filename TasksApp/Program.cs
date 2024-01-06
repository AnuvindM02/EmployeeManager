using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//adding services to IoC container
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
