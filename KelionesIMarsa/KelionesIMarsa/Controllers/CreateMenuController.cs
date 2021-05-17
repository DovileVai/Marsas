using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KelionesIMarsa.Models;
using MySql.Data.MySqlClient;

namespace KelionesIMarsa.Controllers
{
    public class CreateMenuController : Controller
    {

        public void Fill(Menu menu)
        {
            List<Meal> meals = menu.getMealList();
            List<SelectListItem> selectListMeals = new List<SelectListItem>();
            foreach (var item in meals)
            {
                selectListMeals.Add(new SelectListItem() { Value = Convert.ToString(item.id_Meal), Text = item.name });
            }
            menu.MealList = selectListMeals;
        }
        public ActionResult CreateMenu()
        {
            Menu menu = new Menu();
            //Užpildomi pasirinkimų sąrašai duomenimis iš duomenų saugyklų
            Fill(menu);
            return View(menu);
        }
        [HttpPost]
        public ActionResult CreateMenu(Menu menu)
        {
            try
            {
                menu.SaveMenu(menu);
                Fill(menu);
                return View(menu);
            }
            catch
            {
                Fill(menu);
                return View(menu);
            }
        }
    }
}