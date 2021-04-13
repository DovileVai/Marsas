using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using System.Data;

namespace KelionesIMarsa.Controllers
{
    public class AdminMealsController : Controller
    {
        // GET: AdminMeals
        public ActionResult MealList()
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities()) {
                return View(model.Patiekalas.ToList());
            }
        }

        // GET: AdminMeals/Create
        public ActionResult CreateMeal()
        {
            return View();
        }

        // POST: AdminMeals/Create
        [HttpPost]
        public ActionResult CreateMeal(Patiekala newMeal)
        {
            try
            {
                // TODO: Add insert logic here
                using(KelionesIMarsaEntities model = new KelionesIMarsaEntities())
                {
                    model.Patiekalas.Add(newMeal);
                    model.SaveChanges();
                }
                return RedirectToAction("MealList");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminMeals/Edit/5
        public ActionResult EditMeal(int id)
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
            {
                return View(model.Patiekalas.Where(x => x.id_Patiekalas == id).FirstOrDefault());
            }
        }

        // POST: AdminMeals/Edit/5
        [HttpPost]
        public ActionResult EditMeal(int id, Patiekala newData)
        {
            try
            {
                // TODO: Add update logic here
                using(KelionesIMarsaEntities model = new KelionesIMarsaEntities())
                {
                    model.Entry(newData).State = EntityState.Modified;
                    model.SaveChanges();
                }
                return RedirectToAction("MealList");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminMeals/Delete/5
        public ActionResult DeleteMeal(int id)
        {
            using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
            {
                return View(model.Patiekalas.Where(x => x.id_Patiekalas == id).FirstOrDefault());
            }
        }

        // POST: AdminMeals/Delete/5
        [HttpPost]
        public ActionResult DeleteMeal(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (KelionesIMarsaEntities model = new KelionesIMarsaEntities())
                {
                    Patiekala mealDeleted = model.Patiekalas.Where(x => x.id_Patiekalas == id).FirstOrDefault();
                    model.Patiekalas.Remove(mealDeleted);
                    model.SaveChanges();
                }
                return RedirectToAction("MealList");
            }
            catch
            {
                return View();
            }
        }
    }
}
