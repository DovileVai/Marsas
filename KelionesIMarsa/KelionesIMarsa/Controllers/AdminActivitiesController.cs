using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using System.Data;

namespace KelionesIMarsa.Controllers
{
    public class AdminActivitiesController : Controller
    {
        // GET: AdminActivities
        public ActionResult ActivitiesList()
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
            {
                return View(model.Veiklas.ToList());
            }
        }

        // GET: AdminActivities/Create
        public ActionResult CreateActivity()
        {
            return View();
        }

        // POST: AdminActivities/Create
        [HttpPost]
        public ActionResult CreateActivity(Veikla newActivity)
        {
            try
            {
                // TODO: Add insert logic here
                using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
                {
                    model.Veiklas.Add(newActivity);
                    model.SaveChanges();
                }
                return RedirectToAction("ActivitiesList");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminActivities/Edit/5
        public ActionResult EditActivity(int id)
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
            {
                return View(model.Veiklas.Where(x => x.id_Veikla == id).FirstOrDefault());
            }
        }

        // POST: AdminActivities/Edit/5
        [HttpPost]
        public ActionResult EditActivity(int id, Veikla newData)
        {
            try
            {
                // TODO: Add update logic here
                using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
                {
                    model.Entry(newData).State = EntityState.Modified;
                    model.SaveChanges();
                }
                return RedirectToAction("ActivitiesList");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminActivities/Delete/5
        public ActionResult DeleteActivity(int id)
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
            {
                return View(model.Veiklas.Where(x => x.id_Veikla == id).FirstOrDefault());
            }
        }

        // POST: AdminActivities/Delete/5
        [HttpPost]
        public ActionResult DeleteActivity(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
                {
                    Veikla activityDeleted = model.Veiklas.Where(x => x.id_Veikla == id).FirstOrDefault();
                    model.Veiklas.Remove(activityDeleted);
                    model.SaveChanges();
                }
                return RedirectToAction("ActivitiesList");
            }
            catch
            {
                return View();
            }
        }
    }
}
