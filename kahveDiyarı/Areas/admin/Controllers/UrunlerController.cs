using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View("UrunForm",model);
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
    }
}