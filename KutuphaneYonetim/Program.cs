using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<KutuphaneYonetimContext>(options =>
    options.UseSqlite("Data Source=KutuphaneTR.db"));

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
    
    // Veritabanını oluştursun diye
    context.Database.Migrate();

   
    if (!context.Kitaplar.Any())
    {
        context.Kitaplar.AddRange(
            new Kitap { 
                Ad = "Uçurtma Avcısı", 
                Yazar = "Khaled Hosseini", 
                SayfaSayisi = 375, Tur = "Roman", BasimYili = 2003, 
                ResimYolu = "/images/ucurtma.jpg" 
            },
            new Kitap { 
                Ad = "OD", 
                Yazar = "İskender Pala", 
                SayfaSayisi = 358, Tur = "Roman", BasimYili = 2011, 
                ResimYolu = "/images/od.jpg" 
            },
            new Kitap { 
                Ad = "Kafkas Kartalı – Şeyh Şamil", 
                Yazar = "Yavuz Bahadıroğlu", 
                SayfaSayisi = 256, Tur = "Roman", BasimYili = 2007, 
                ResimYolu = "/images/seyhsamil.jpg" 
            },
            new Kitap { 
                Ad = "Çile", 
                Yazar = "Necip Fazıl Kısakürek", 
                SayfaSayisi = 502, Tur = "Şiir", BasimYili = 1962, 
                ResimYolu = "/images/cile.png" 
            },
            new Kitap { 
                Ad = "Kürk Mantolu Madonna", 
                Yazar = "Sabahattin Ali", 
                SayfaSayisi = 160, Tur = "Roman", BasimYili = 1943, 
                ResimYolu = "/images/madonna.jpg" 
            }
        );
        context.SaveChanges();
    }
}

app.Run();