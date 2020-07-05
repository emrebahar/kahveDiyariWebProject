using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kahveDiyarı.Areas.admin.Controllers
{

    [Authorize]                     // kullanıcı giriş yapmadan asla sayfaya erişemez..
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler

        public ActionResult Index()
        {
            return View();
        }
    }
}