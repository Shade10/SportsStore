using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;

namespace SportsStore.WebUI.Controllers {
    public class AccontController : Controller {
        private IAuthProvider authProvider;

        public AccontController(IAuthProvider auth) {
            authProvider = auth;
        }
        // GET: Accont
        public ActionResult Index() {
            return View();
        }
    }
}