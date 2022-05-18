using LHScheduler.Models;
using LHScheduler.Services;
using Microsoft.AspNetCore.Mvc;

namespace LHScheduler.Controllers
{
    public class AdminController : Controller
    {
        SecurityDAO securityDAO = new SecurityDAO();

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

        public IActionResult ProcessNewActivity(ActivityModel newActivity)
        {
            securityDAO.InsertNewActivity(newActivity);
            List<ActivityModel> activites = securityDAO.ReturnEvents();

            return View("AdminPanel", activites);
        }

        public IActionResult ProcessAdminLogin(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                List<ActivityModel> activites = securityDAO.ReturnEvents();

                return View("AdminPanel", activites);
            }

            return View();
        }
    }
}
