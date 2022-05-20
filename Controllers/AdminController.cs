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

        public IActionResult NewActivity()
        {
            return View();
        }

        public IActionResult EditEvent(int id)
        {
            ActivityModel activity = new ActivityModel();
            activity = binarySearch(id);
            Debug.WriteLine(id);
            return View("EditEvent", activity);
        }

        public IActionResult ProcessEdit(ActivityModel updatedActivity)
        {
            Debug.WriteLine(updatedActivity.ActivityId);
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
            if (username.ToLower() == "admin" && password == "admin")
            {
                activities = securityDAO.ReturnEvents();

                return View("AdminPanel", activities);
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteEvent(int id)
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
            while(left <= right)
            {
                int mid = (left + right) / 2;
                if (activities[mid].ActivityId == id)
                {
                    return activities[mid];
                }
                if(activities[mid].ActivityId > id)
                {
                    left = mid + 1;
                }
                right = mid - 1;
            }
            activity.ActivityName = "Not found";
            return activity;
        }
    }
}
