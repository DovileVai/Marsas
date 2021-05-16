using KelionesIMarsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KelionesIMarsa.Controllers
{
    public class TimeTableController : Controller
    {

        public ActionResult openWorkCreation()
        {
            List<Activity> jobs = new Activity().getAll("darbas");
            return View("makeWorkTimeTable", jobs);
        }

        public ActionResult openRestCreation() 
        {
            List<Activity> funs = new Activity().getAll("pramoga");
            return View("makeRestTimeTable", funs);
        }
        public ActionResult openSleepCreation()
        {
            List<Activity> sleep = new Activity().getAll("poilsis");
            return View("makeSleepTimeTable", sleep);
        }

        public ActionResult goBackToTimeTables() 
        {
            TempData.Add("back", "Back to Timetables!");
            return RedirectToAction("TimeTableCreate", "RegisterForJourney");
        }

        private Dictionary<int, int> GetOccupiedTimes(Activitiesschedule OneDaySchedule) 
        {
            Dictionary<int, int> times = new Dictionary<int, int>();
            List<Activitymode> occupied = new Activitymode().getAllActivitiesOfDay(OneDaySchedule.id_ActivitiesSchedule);
            int[] startTimes = occupied.Select(x => x.startTime).ToArray();
            int[] endTimes = occupied.Select(x => x.endTime).ToArray();
            for (int j = 0; j < startTimes.Length; j++) 
            {
                times.Add(startTimes[j],endTimes[j]);
            }
            return times;
        }

        private bool Validate(List<Activity> form, int boundary = 4) 
        {
            if (form.Count >= boundary) {
                return true;
            }
            return false;
        }

        private string AddToSchedule(List<Activity> chosenAct, List<Activitiesschedule> schedule, int minHours) 
        {
            int currentHours = 0;
            int last = -1;
            for (int i = 0; i < schedule.Count; i++)
            {
                Dictionary<int, int> OneDay = GetOccupiedTimes(schedule[i]);
                for (int j = 0; j < minHours; j++) //cia maxWorkTime is tiesu turetu buti minimum
                {
                    int rand = new Random().Next(0, chosenAct.Count);
                    if (rand == last) rand = (rand - 1 >= 0) ? 0 : chosenAct.Count-1;
                    last = rand;
                    Activity activity = chosenAct[rand];
                    int startTime = 0;
                    bool found = false;
                    while (!found)
                    {
                        if (OneDay.ContainsKey(startTime))
                            // oneday[startTime] grazins idetos veiklos ilgi tuo laiku.
                            startTime = OneDay[startTime];//priskiriam end time veiklos laika kitos veiklos pradžiai
                        else
                            found = true;
                    }
                    int endTime = startTime + activity.duration;
                    currentHours += activity.duration;
                    string Status = new Activitymode().AddActivity(schedule[i].id_ActivitiesSchedule, activity.id_Activity, startTime, endTime);
                    OneDay[startTime] = endTime;
                    if (currentHours >= minHours) 
                    {
                        currentHours = 0;
                        break;
                    }
                }
            }
            return "tvarkaraštis sukurtas sėkmingai!";
        }
        [HttpPost]
        public ActionResult openSleepCreation(FormCollection form)
        {
            try
            {
                //Gaunam veiklų pasirinkimus
                List<Activity> chosenAct = new List<Activity>();
                for (int i = 1; i < form.Count; i++)
                {
                    if (form[i].Contains("true"))
                    {
                        chosenAct.Add(new Activity().getActivity(i));
                    }
                }
                if (Validate(chosenAct,1)) 
                {
                    // užsakymo numeris
                    int order_id = Convert.ToInt32(Request.Cookies["order_id"].Value);

                    //gaunam 7 dienų tvarkaraščius susijusius su tuo užsakymu
                    List<Activitiesschedule> schedule = new Activitiesschedule().getOrderSchedules(order_id);
                    string status = AddToSchedule(chosenAct, schedule, schedule[0].maxSleepTime);
                    HttpCookie http = new HttpCookie("back");
                    http.Value = "back";
                    http.Expires = DateTime.Now.AddSeconds(5.0);
                    Response.Cookies.Add(http);
                    TempData.Add("success", "Miego " + status);
                    return RedirectToAction("openSleepCreation");
                }
                TempData.Add("Error", "Nieko nepasirinkote!");
                return RedirectToAction("openSleepCreation");

            }
            catch(ArgumentOutOfRangeException e)
            {
                TempData.Add("Message",e.ToString());
                List<Activity> jobs = new Activity().getAll("poilsis");
                return View(jobs);
            }
        }
        [HttpPost]
        public ActionResult openRestCreation(FormCollection form)
        {
            try
            {
                
                //Gaunam veiklų pasirinkimus
                List<Activity> chosenAct = new List<Activity>();
                for (int i = 1; i < form.Count; i++)
                {
                    if (form[i].Contains("true"))
                    {
                        chosenAct.Add(new Activity().getActivity(i));
                    }
                }
                if (Validate(chosenAct))
                {
                    // užsakymo numeris
                    int order_id = Convert.ToInt32(Request.Cookies["order_id"].Value);

                    //gaunam 7 dienų tvarkaraščius susijusius su tuo užsakymu
                    List<Activitiesschedule> schedule = new Activitiesschedule().getOrderSchedules(order_id);
                    string status = AddToSchedule(chosenAct, schedule, schedule[0].maxRestTime);
                    TempData.Add("success", "Pramogų " + status);
                    HttpCookie http = new HttpCookie("back");
                    http.Value = "back";
                    Response.Cookies.Add(http);
                    return RedirectToAction("openRestCreation");
                }
                TempData.Add("Error", "Pasirinkote per mažai pramogų! Pasirinkite bent 4");
                return RedirectToAction("openRestCreation");

            }
            catch
            {
                TempData.Add("Message", "Kuriant tvarkaraštį įvyko klaida!");
                List<Activity> jobs = new Activity().getAll("pramoga");
                return View(jobs);
            }
        }

        [HttpPost]
        public ActionResult openWorkCreation(FormCollection form)
        {
            try
            {
                
                //Gaunam veiklų pasirinkimus
                List<Activity> chosenAct = new List<Activity>();
                for (int i = 1; i < form.Count; i++)
                {
                    if (form[i].Contains("true")) {
                        chosenAct.Add(new Activity().getActivity(i));
                    }
                }
                if (Validate(chosenAct))
                {
                    // užsakymo numeris
                    int order_id = Convert.ToInt32(Request.Cookies["order_id"].Value);

                    //gaunam 7 dienų tvarkaraščius susijusius su tuo užsakymu
                    List<Activitiesschedule> schedule = new Activitiesschedule().getOrderSchedules(order_id);
                    string status = AddToSchedule(chosenAct, schedule, schedule[0].maxWorkTime);
                    TempData.Add("success", "Darbo " + status);
                    HttpCookie http = new HttpCookie("back");
                    http.Value = "back";
                    Response.Cookies.Add(http);
                    return RedirectToAction("openWorkCreation");
                }
                TempData.Add("Error", "Pasirinkote per mažai darbų! Pasirinkite bent 4");
                return RedirectToAction("openWorkCreation");
            }
            catch (ArgumentOutOfRangeException e)
            {
                TempData.Add("Message", e.ToString());
                List<Activity> jobs = new Activity().getAll("darbas");
                return View("makeWorkTimeTable",jobs);
            }
        }
        // GET: TimeTable
        public ActionResult Index()
        {
            return View();
        }

        // GET: TimeTable/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TimeTable/Create
        public ActionResult Create()
        {
            return View();
        }

        
        // POST: TimeTable/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeTable/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TimeTable/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeTable/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TimeTable/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
