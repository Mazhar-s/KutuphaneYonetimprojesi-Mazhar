using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Veritabanı tablolarını ayarlamak için lazım
using Microsoft.AspNetCore.Http; // Kapak fotoğrafı seçip yükleyebilmek için bu şart

namespace KutuphaneYonetim.Models
{
    public class Kitap
    {
        [Key] // Bu her kitabın kendine özel kimlik numarası  karışmasınlar diye
        public int Id { get; set; }

        // Kitap adı boş geçilirse sistem uyarı versin diye yazdım
        [Required(ErrorMessage = "Lütfen kitap adını giriniz.")]
        [Display(Name = "Kitap Adı")]
        public string Ad { get; set; } = "";

        // Yazar adı için
        [Required(ErrorMessage = "Lütfen yazar adını giriniz.")]
        [Display(Name = "Yazar")]
        public string Yazar { get; set; } = "";

        [Display(Name = "Sayfa Sayısı")]
        public int SayfaSayisi { get; set; }

        [Display(Name = "Tür")]
        public string Tur { get; set; } = "";

        [Display(Name = "Basım Yılı")]
        public int BasimYili { get; set; }

        // Sisteme o an hangi tarihte eklediysek onu otomatik yazıyor
        [Display(Name = "Eklenme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;

        

        // Burası sadece resmin ismini (örneğin: kitap.jpg) veritabanına kaydediyor
        [Display(Name = "Kapak Resmi")]
        public string? ResimYolu { get; set; }

        // NotMapped dedik çünkü resmi veritabanına değil klasöre atıyoruz
        [NotMapped]
        [Display(Name = "Resim Yükle")]
        public IFormFile? ResimDosyasi { get; set; }
    }
}