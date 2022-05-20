using LHScheduler.Models;
using LHScheduler.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Diagnostics;

namespace LHScheduler.Controllers
{
    public class AdminController : Controller
    {
        SecurityDAO securityDAO = new SecurityDAO();
        public static List<ActivityModel> activities = new List<ActivityModel>();


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ActivityModel activity = new ActivityModel();
            return PartialView("SignUpModal", activity);
        }

        public IActionResult NewActivity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditActivity(int id)
        {
            ActivityModel activity = new ActivityModel();
            Debug.WriteLine(id);
            activity = binarySearch(id);
            Debug.WriteLine("Name " + activity.ActivityName);
            return View("EditActivity", activity);
        }

        public IActionResult ProcessEdit(ActivityModel updatedActivity)
        {
            securityDAO.UpdateActivity(updatedActivity);
            activities = securityDAO.ReturnEvents();
            return View("AdminPanel", activities);
        }

        public IActionResult ProcessNewActivity(ActivityModel newActivity)
        {
            securityDAO.InsertNewActivity(newActivity);
            activities = securityDAO.ReturnEvents();

            return View("AdminPanel", activities);
        }

        public IActionResult ProcessAdminLogin(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                activities = securityDAO.ReturnEvents();

                return View("AdminPanel", activities);
            }

            return View();
        }

        public IActionResult DeleteActivity(int id)
        {
            Debug.WriteLine(id);
            securityDAO.DeleteEvent(id);
            activities = securityDAO.ReturnEvents();

            return View("AdminPanel", activities);
        }

        public ActivityModel binarySearch(int id)
        {
            ActivityModel activity = new ActivityModel();

            int left = 0;
            int right = activities.Count;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (id == activities[mid].ActivityId)
                {
                    return activities[mid];
                }
                if (id < activities[mid].ActivityId)
                {
                    right = mid - 1;
                }
                if (id > activities[mid].ActivityId)
                {
                    left = mid + 1;
                }
            }
            activity.ActivityName = "Not found";
            return activity;
        }
    }
}
