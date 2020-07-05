using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kahveDiyarı.Models;
using System.Web.Security;

namespace kahveDiyarı.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(kullanicilar kullaniciFormu, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("index", kullaniciFormu);
            }

            string sifre1 = Sifrele.MD5Olustur(kullaniciFormu.sifre); //Kullanıcının girdiği şifreyi md 5 dönüştürür..
            using (kahvediyariEntities db = new kahvediyariEntities())
            {
                var kullaniciVarmi = db.kullanicilar.FirstOrDefault(
                    x => x.kadi == kullaniciFormu.kadi & x.sifre == sifre1);
                if (kullaniciVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.kadi, kullaniciFormu.BeniHatirla);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("/index", "urunler");
                    }
                }
                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!!";
                return View("index");

            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
    }
}