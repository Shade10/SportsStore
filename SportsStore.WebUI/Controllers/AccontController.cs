using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers {
    public class AccontController : Controller {
        IAuthProvider authProvider;

        public AccontController(IAuthProvider auth) {
            authProvider = auth;
        }
        // GET: Accont
        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (authProvider.Authenticate(model.UseName, model.Password)) {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                } else {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub haslo");
                }

                return View();
            } else {
                return View();
            }
        }
    }
}