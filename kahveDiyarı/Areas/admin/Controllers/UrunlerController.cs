using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kahveDiyarı.Controllers;
using kahveDiyarı.Models;

namespace kahveDiyarı.Areas.admin.Controllers
{

    [Authorize]                     // kullanıcı giriş yapmadan asla sayfaya erişemez..
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler

        public ActionResult Index()
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }
        }
        public ActionResult Yeni()
        {
            var model = new urunler();
            return View("UrunForm", model);
        }
        public ActionResult Guncelle(int id)
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.urunler.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunForm", model);
            }

        }
        public ActionResult Kaydet(urunler gelenUrun)
        {
            if (!ModelState.IsValid)
            {
                return View("UrunForm", gelenUrun);
            }
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                if (gelenUrun.id == 0)       //Yeni Ürün kayıt için!
                {
                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyin!";
                        return View("UrunForm", gelenUrun);
                    }
                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                    gelenUrun.foto = fotoAdi;
                    db.urunler.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    TempData["urun"] = "Ürün başarılı bir şekilde eklendi.";
                }

                else            //Güncellemek için!
                {
                    var GuncellenecekVeri = db.urunler.Find(gelenUrun.id);
                    if (gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                        gelenUrun.foto = fotoAdi;
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));

                    }
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urun"] = "Ürün başarılı bir şekilde Güncellendi.";

                }
                db.SaveChanges();
                return RedirectToAction("index");
            }
            
        }

        public ActionResult Sil(int id)
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var silinecekUrun = db.urunler.Find(id);
                if (silinecekUrun == null)
                {
                    return HttpNotFound();
                }
                db.urunler.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["urun"] = "Ürün başarılı bir şekilde silindi";
                return RedirectToAction("index");
            }
        }
    }
}