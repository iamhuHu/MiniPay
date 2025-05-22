using MiniPay_Contracts;
using MiniPay_PaymentClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var paymentApiUrl = builder.Configuration.GetSection("AppSettings")["PaymentApiUrl"];
if (string.IsNullOrEmpty(paymentApiUrl))
{
    throw new ApplicationException("AppSettings/PaymentApiUrl not given");
}
builder.Services.AddSingleton<IPaymentApi>(new PaymentClient(paymentApiUrl));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Provider}/{action=Index}/{id?}");

app.Run();
