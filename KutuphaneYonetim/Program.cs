
using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// Türkçe karakter desteðini konsol ve uygulama genelinde aktif etmesi için
Console.OutputEncoding = System.Text.Encoding.UTF8;
var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekler
builder.Services.AddControllersWithViews();

// SQLite veritabaný ayarý
builder.Services.AddDbContext<KutuphaneYonetimContext>(options =>
    options.UseSqlite("Data Source=Kutuphane.db"));

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// wwwroot klasöründeki her þeyi  okuyabilmesi için
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<KutuphaneYonetimContext>();
    context.Database.Migrate();

    if (!context.Kitaplar.Any())
    {
        context.Kitaplar.AddRange(
            new Kitap { Ad = "Uçurtma Avcýsý", Yazar = "Khaled Hosseini", SayfaSayisi = 375, Tur = "Roman", BasimYili = 2003, ResimYolu = "/images/ucurtma.jpg" },
            new Kitap { Ad = "OD", Yazar = "Ýskender Pala", SayfaSayisi = 358, Tur = "Roman", BasimYili = 2011, ResimYolu = "/images/od.jpg" },
            new Kitap { Ad = "Kafkas Kartalý", Yazar = "Yavuz Bahadýroðlu", SayfaSayisi = 256, Tur = "Roman", BasimYili = 2007, ResimYolu = "/images/seyhsamil.jpg" },
            new Kitap { Ad = "Çile", Yazar = "Necip Fazýl Kýsakürek", SayfaSayisi = 502, Tur = "Þiir", BasimYili = 1962, ResimYolu = "/images/cile.png" },
            new Kitap { Ad = "Kürk Mantolu Madonna", Yazar = "Sabahattin Ali", SayfaSayisi = 160, Tur = "Roman", BasimYili = 1943, ResimYolu = "/images/madonna.jpg" }
        );
        context.SaveChanges();
    }
}

app.Run();