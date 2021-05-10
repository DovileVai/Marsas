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
    public class AdminActivitiesController : Controller
    {
        // GET: AdminActivities
        public ActionResult ActivitiesList()
        {
            Activity act = new Activity();
            List<Activity> activities = act.ActivitiesList();
            
            return View(activities.ToList());
        }

        // GET: AdminActivities/Create
        public ActionResult CreateActivity()
        {
            Activity activity = new Activity();
            return View(activity);
        }

        // POST: AdminActivities/Create
        [HttpPost]
        public ActionResult CreateActivity(Activity newActivity)
        {
            try
            {
                newActivity.CreateActivity(newActivity);

                return RedirectToAction("ActivitiesList");
            }
            catch
            {
                return View(newActivity);
            }
        }
        
        public bool updateActivity(Activity activity)
        {
            try
            {
                activity.updateActivity(activity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public ActionResult EditActivity(int id)
        {
            Activity act = new Activity();
            return View(act.getActivity(id));
        }
        [HttpPost]
        public ActionResult EditActivity(int id, Activity activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    updateActivity(activity);
                }

                return RedirectToAction("ActivitiesList");
            }
            catch
            {
                return View(activity);
            }
        }

        // GET: AdminActivities/Delete/5
        public ActionResult DeleteActivity(int id)
        {
            Activity act = new Activity();
            return View(act.getActivity(id));
        }

        // POST: AdminActivities/Delete/5
        [HttpPost]
        public ActionResult DeleteActivity(int id, FormCollection collection)
        {
            try
            {
                Activity act = new Activity();
                act.deleteA(id);
                return RedirectToAction("ActivitiesList");
            }
            catch
            {
                return View();
            }
        }
    }
}
