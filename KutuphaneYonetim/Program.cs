using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;
using KutuphaneYonetim.Models;

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
app.UseStaticFiles(); 

// Yüklenen resimlerin dýþarýdan görünmesi için bu kýsmý ekledim
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "uploads")),
    RequestPath = "/uploads"
});
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<KutuphaneYonetim.Data.KutuphaneYonetimContext>();
    context.Database.Migrate();
    if (!context.Kitaplar.Any())
    {
        context.Kitaplar.AddRange(
            new Kitap
            {
                Ad = "Uçurtma Avcýsý", // KitapAdý deðil, sende "Ad" yazýyor
                Yazar = "Khaled Hosseini",
                SayfaSayisi = 375, // SayfaSayýsý deðil, sende "SayfaSayisi" (ý harfi yok)
                Tur = "Roman",
                BasimYili = 2003, // Bu alan sende zorunlu, eklemezsek hata verir
                ResimYolu = "https://img.kitapyurdu.com/v1/getImage/fn:11333160/wh:true/wi:220" // KapakResmi deðil, "ResimYolu"
            },
            new Kitap
            {
                Ad = "OD",
                Yazar = "Ýskender Pala",
                SayfaSayisi = 358,
                Tur = "Roman",
                BasimYili = 2011,
                ResimYolu = "https://img.kitapyurdu.com/v1/getImage/fn:11267812/wh:true/wi:220"
            },
            new Kitap
            {
                Ad = "Kafkas Kartalý – Þeyh Þamil",
                Yazar = "Yavuz Bahadýroðlu",
                SayfaSayisi = 216,
                Tur = "Biyografik Roman",
                BasimYili = 1990,
                ResimYolu = "https://img.kitapyurdu.com/v1/getImage/fn:11252192/wh:true/wi:220"
            },
            new Kitap
            {
                Ad = "Çile",
                Yazar = "Necip Fazýl Kýsakürek",
                SayfaSayisi = 502,
                Tur = "Þiir",
                BasimYili = 1962,
                ResimYolu = "https://img.kitapyurdu.com/v1/getImage/fn:11303273/wh:true/wi:220"
            },
            new Kitap
            {
                Ad = "Kürk Mantolu Madonna",
                Yazar = "Sabahattin Ali",
                SayfaSayisi = 160,
                Tur = "Roman",
                BasimYili = 1943,
                ResimYolu = "https://img.kitapyurdu.com/v1/getImage/fn:11340150/wh:true/wi:220"
            }
        );
        context.SaveChanges();
    }
}

app.Run();


