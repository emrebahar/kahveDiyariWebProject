using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kahveDiyarı.Models;

namespace kahveDiyarı.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.hakkimizda.Find(1);
                return View(model);
            }
        }
        [Route("Urunler")]
        public ActionResult Urunler()
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.urunler.Where(x => x.aktif == true).OrderBy(x => x.sira).ToList();
                return View(model);
            }
        }
        [Route("urun/{id}/{baslik}")]
        public ActionResult UrunDetay(int id)
        {
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var model = db.urunler.Where(x => x.aktif == true & x.id == id).FirstOrDefault();
                if(model == null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }
        [Route("Magaza")]
        public ActionResult Magaza()
        {
            return View();
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}