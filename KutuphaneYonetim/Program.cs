using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// Konsolda ve projede Türkçe karakterlerin (ç, þ, ð gibi) bozuk görünmemesi için dil ayarlarýný yaptým.
Console.OutputEncoding = System.Text.Encoding.UTF8;
var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// MVC yapýsýný projeye ekledim.
builder.Services.AddControllersWithViews();

// Veritabaný olarak SQLite kullanacaðýmý belirttim.
builder.Services.AddDbContext<KutuphaneYonetimContext>(options =>
    options.UseSqlite("Data Source=Kutuphane.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// wwwroot klasöründeki resimlerin ve dosyalarýn çalýþmasý için bu kod gerekli.
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Veritabaný oluþturma ve baþlangýç verilerini ekleme iþlemleri
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<KutuphaneYonetimContext>();

    // Veritabanýný otomatik oluþtur (yoksa kurar).
    context.Database.Migrate();

    // Verilerin sürekli üst üste eklenmesini önlemek için önce eski listeyi temizliyorum.
    var eskiKayitlar = context.Kitaplar.ToList();
    context.Kitaplar.RemoveRange(eskiKayitlar);
    context.SaveChanges();

    // Kitaplarý ve resim yollarýný veritabanýna ekliyorum.
    // Resimler projemdeki wwwroot/images klasöründe duruyor.
    context.Kitaplar.AddRange(
        new Kitap
        {
            Ad = "Uçurtma Avcýsý",
            Yazar = "Khaled Hosseini",
            SayfaSayisi = 375,
            Tur = "Roman",
            BasimYili = 2003,
            ResimYolu = "/images/ucurtma.jpg"
        },
        new Kitap
        {
            Ad = "OD",
            Yazar = "Ýskender Pala",
            SayfaSayisi = 358,
            Tur = "Roman",
            BasimYili = 2011,
            ResimYolu = "/images/od.jpg"
        },
        new Kitap
        {
            Ad = "Kafkas Kartalý – Þeyh Þamil",
            Yazar = "Yavuz Bahadýroðlu",
            SayfaSayisi = 256,
            Tur = "Roman",
            BasimYili = 2007,
            ResimYolu = "/images/seyhsamil.jpg"
        },
        new Kitap
        {
            Ad = "Çile",
            Yazar = "Necip Fazýl Kýsakürek",
            SayfaSayisi = 502,
            Tur = "Þiir",
            BasimYili = 1962,
            ResimYolu = "/images/cile.png"
        },
        new Kitap
        {
            Ad = "Kürk Mantolu Madonna",
            Yazar = "Sabahattin Ali",
            SayfaSayisi = 160,
            Tur = "Roman",
            BasimYili = 1943,
            ResimYolu = "/images/madonna.jpg"
        }
    );
    // Deðiþiklikleri veritabanýna kaydettim.
    context.SaveChanges();
}

app.Run();