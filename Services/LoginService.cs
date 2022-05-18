using LHScheduler.Models;
using System.DirectoryServices.AccountManagement;
using System.Web;

namespace LHScheduler.Services
{
    public class LoginService
    {
        public bool login(string username, string password)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "classroom.lcl"))
            {
                // validate the credentials
                isValid = pc.ValidateCredentials(username, password);
            }

            return isValid;
        }

        public UserModel CreateUser(string username)
        {
            UserModel newUser = new UserModel();

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "classroom.lcl"))
            {
                UserPrincipal up = UserPrincipal.Current;
                newUser.UserName = up.UserPrincipalName;
                newUser.Name = up.Name;
            }

                return newUser;
        }
    }
}
