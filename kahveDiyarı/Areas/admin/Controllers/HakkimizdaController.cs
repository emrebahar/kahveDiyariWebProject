using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kahveDiyarı.Models;
using kahveDiyarı.App_Code;
using System.IO;

namespace kahveDiyarı.Areas.admin.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        public ActionResult Index()
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.hakkimizda.First();

                return View(model);
            }

        }
        public ActionResult HakkimizdaGuncelle()
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Kaydet (hakkimizda GelenVeri)
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var GuncellenecekVeri = db.hakkimizda.First();
                if (!ModelState.IsValid )
                {
                    return View("HakkimizdaGuncelle",GelenVeri);
                }
                if (GelenVeri.fotoFile!=null)
                {
                    GelenVeri.foto = Seo.DosyaAdiDuzenle(GelenVeri.fotoFile.FileName);
                    GelenVeri.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(GelenVeri.foto)));
                }

                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();

                TempData["hakkimdaGuncelle"] = " ";
                return RedirectToAction("index", "hakkimizda");
            }
        }
    }
}