using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Veritabaný adýný V3 yaptýk ki tertemiz Türkçe kurulsun
builder.Services.AddDbContext<KutuphaneYonetimContext>(options =>
    options.UseSqlite("Data Source=KutuphaneV3.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
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

    // Veritabaný boþsa TÜRKÇE verileri ekle
    if (!context.Kitaplar.Any())
    {
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
                SayfaSayisi = 216,
                Tur = "Roman",
                BasimYili = 1990,
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
        context.SaveChanges();
    }
}

app.Run();