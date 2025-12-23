using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneYonetim.Data;
using KutuphaneYonetim.Models;
using Microsoft.AspNetCore.Hosting; // Dosya işlemleri için gerekli

namespace KutuphaneYonetim.Controllers
{
    public class KitapsController : Controller
    {
        private readonly KutuphaneYonetimContext _context;
        private readonly IWebHostEnvironment _hostEnvironment; // Resim klasörünü bulmak için

        public KitapsController(KutuphaneYonetimContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Kitaps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kitaplar.ToListAsync());
        }

        // GET: Kitaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var kitap = await _context.Kitaplar.FirstOrDefaultAsync(m => m.Id == id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        // GET: Kitaps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kitaps/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                // Resim yukleme islemi
                if (kitap.ResimDosyasi != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(kitap.ResimDosyasi.FileName);
                    string extension = Path.GetExtension(kitap.ResimDosyasi.FileName);
                    kitap.ResimYolu = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    // Klasor yoksa olustur
                    if (!Directory.Exists(Path.Combine(wwwRootPath + "/images/")))
                    {
                        Directory.CreateDirectory(Path.Combine(wwwRootPath + "/images/"));
                    }

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await kitap.ResimDosyasi.CopyToAsync(fileStream);
                    }
                }

                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kitap);
        }

        // GET: Kitaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        // POST: Kitaps/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kitap kitap)
        {
            if (id != kitap.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Resim guncelleme islemi
                    if (kitap.ResimDosyasi != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(kitap.ResimDosyasi.FileName);
                        string extension = Path.GetExtension(kitap.ResimDosyasi.FileName);
                        string yeniDosyaAdi = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/", yeniDosyaAdi);

                        if (!Directory.Exists(Path.Combine(wwwRootPath + "/images/")))
                        {
                            Directory.CreateDirectory(Path.Combine(wwwRootPath + "/images/"));
                        }

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await kitap.ResimDosyasi.CopyToAsync(fileStream);
                        }
                        kitap.ResimYolu = yeniDosyaAdi;
                    }

                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kitap);
        }

        // GET: Kitaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var kitap = await _context.Kitaplar.FirstOrDefaultAsync(m => m.Id == id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        // POST: Kitaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap != null) _context.Kitaplar.Remove(kitap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
            return _context.Kitaplar.Any(e => e.Id == id);
        }
    }
}