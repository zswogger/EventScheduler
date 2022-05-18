using LHScheduler.Models;
using LHScheduler.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LHScheduler.Controllers
{
    public class HomeController : Controller
    {
        List<ActivityModel> activities = new List<ActivityModel>();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View("AdminLogin");
        }

        public IActionResult SignUp(int id, int maxusers, int currentusers)
        {
            ActivityModel activity = new ActivityModel();
            activity.ActivityId = id;
            activity.MaxParticipants = maxusers;
            activity.CurrentParticipants = currentusers;
            return PartialView("SignUpModal", activity);
        }

        public IActionResult ProcessLogin(string username, string password)
        {
            LoginService ls = new LoginService();

            if(ls.login(username, password))
            {
                SecurityDAO securityDAO = new SecurityDAO();
                activities = securityDAO.ReturnEvents();

                return View("RetrieveEvents", activities);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult processSignUp(int id, string name, string email, string phone, int remainingParticipants)
        {
            SecurityDAO securityDAO = new SecurityDAO();
            string status = "";

            Debug.WriteLine("Remaining " + remainingParticipants);

            if (remainingParticipants > 0)
            {
                status = "On Trip";
            }
            else
            {
                status = "Waitlist";
            }

            Debug.WriteLine(status);
            securityDAO.SignUp(id, name, email, phone, status);

            return View("RetrieveEvents", activities);
        }
    }
}