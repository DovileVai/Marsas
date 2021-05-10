using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using MySql.Data.MySqlClient;

namespace KelionesIMarsa.Controllers
{
    public class AdminMealsController : Controller
    {
        // GET: AdminMeals
        public ActionResult MealList()
        {
            Meal meal = new Meal();
            List<Meal> meals = meal.MealList();
            return View(meals.ToList());
        }

        // GET: AdminMeals/Create
        public ActionResult CreateMeal()
        {
            Meal meal = new Meal();
            return View(meal);
        }

        // POST: AdminMeals/Create
        [HttpPost]
        public ActionResult CreateMeal(Meal newMeal)
        {
            try
            {
                newMeal.CreateMeal(newMeal);

                return RedirectToAction("MealList");
            }
            catch
            {
                return View(newMeal);
            }
        }
       
        public bool updateMeal(Meal meal)
        {
            try
            {
                meal.updateMeal(meal);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        // GET: AdminMeals/Edit/5
        public ActionResult EditMeal(int id)
        {
            Meal meal = new Meal();
            return View(meal.getMeal(id));
        }

        // POST: AdminMeals/Edit/5
        [HttpPost]
        public ActionResult EditMeal(int id, Meal meal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    updateMeal(meal);
                }

                return RedirectToAction("MealList");
            }
            catch
            {
                return View(meal);
            }
        }

        // GET: AdminMeals/Delete/5
        public ActionResult DeleteMeal(int id)
        {
            Meal meal = new Meal();
            return View(meal.getMeal(id));
        }

        // POST: AdminMeals/Delete/5
        [HttpPost]
        public ActionResult DeleteMeal(int id, FormCollection collection)
        {
            try
            {
                Meal meal = new Meal();
                meal.deleteM(id);
                return RedirectToAction("MealList");
            }
            catch
            {
                return View();
            }
        }
    }
}
