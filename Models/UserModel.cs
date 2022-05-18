using LHScheduler.Services;

namespace LHScheduler.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }

        public UserModel(string userName)
        {
            LoginService ls = new LoginService();
            UserModel user = ls.CreateUser(userName);
            UserName = user.UserName;
            Name = user.Name;
        }

        public UserModel()
        {

        }
    }
}
