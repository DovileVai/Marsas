using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;

namespace KelionesIMarsa.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            Naudotoja naudotojas = new Naudotoja();
            return View(naudotojas);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Naudotoja naudotojas)
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
            {
                /*if(model.Naudotojas.Any(x => x.ElPastas == naudotojas.ElPastas))
                {

                }*/



                model.Naudotojas.Add(naudotojas);
                model.SaveChanges();
            }
            ModelState.Clear();
            return View("AddOrEdit", new Naudotoja());
        }
    }
}