using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<KutuphaneYonetimContext>(options =>
    options.UseSqlite("Data Source=Kutuphane.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Resimlerin çalýþmasý için þart

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// VERÝTABANI TEMÝZLÝK VE KURULUM
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<KutuphaneYonetimContext>();

    // Veritabanýný güncelle
    context.Database.Migrate();

    // 1. ESKÝ BOZUK KAYITLARI SÝL (Soru iþaretli olanlar gitsin)
    var eskiKayitlar = context.Kitaplar.ToList();
    context.Kitaplar.RemoveRange(eskiKayitlar);
    context.SaveChanges();

    // 2. TEMÝZ VERÝLERÝ EKLE
    // Not: Ýsimleri Ýngilizce karakterle (c, s, u) yazýyoruz. 
    // Böylece sunucu "Bu ne?" demez, yazýlar düzgün çýkar.
    // Site açýlýnca "Düzenle" diyip Türkçe yapabilirsin.
    context.Kitaplar.AddRange(
        new Kitap
        {
            Ad = "Ucurtma Avcisi",
            Yazar = "Khaled Hosseini",
            SayfaSayisi = 375,
            Tur = "Roman",
            BasimYili = 2003,
            ResimYolu = "/images/ucurtma.jpg"
        },
        new Kitap
        {
            Ad = "OD",
            Yazar = "Iskender Pala",
            SayfaSayisi = 358,
            Tur = "Roman",
            BasimYili = 2011,
            ResimYolu = "/images/od.jpg"
        },
        new Kitap
        {
            Ad = "Kafkas Kartali",
            Yazar = "Yavuz Bahadiroglu",
            SayfaSayisi = 216,
            Tur = "Roman",
            BasimYili = 1990,
            ResimYolu = "/images/seyhsamil.jpg"
        },
        new Kitap
        {
            Ad = "Cile",
            Yazar = "Necip Fazil Kisakurek",
            SayfaSayisi = 502,
            Tur = "Siir",
            BasimYili = 1962,
            ResimYolu = "/images/cile.png"
        },
        new Kitap
        {
            Ad = "Kurk Mantolu Madonna",
            Yazar = "Sabahattin Ali",
            SayfaSayisi = 160,
            Tur = "Roman",
            BasimYili = 1943,
            ResimYolu = "/images/madonna.jpg"
        }
    );
    context.SaveChanges();
}

app.Run();