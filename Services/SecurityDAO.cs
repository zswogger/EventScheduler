using LHScheduler.Models;
using System.Data.SqlClient;
using System.Diagnostics;

// SecurityDAO will be used to access the database and return information.

namespace LHScheduler.Services
{
    public class SecurityDAO
    {
        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=LHEvents;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        // Insert new activity
        public void InsertNewActivity(ActivityModel activity)
        {
            DateTime dateTime = activity.DateTime;
            string format = "MM-dd-yyy HH:mm:ss tt";

            string sqlStatement = "INSERT INTO dbo.events (date, name, description, max_users, current_users) VALUES ('" + dateTime.ToString(format) + "', '" + activity.ActivityName + "', '" + activity.ActivityDescription + "', " + activity.MaxParticipants + ", " + 0 +")";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, conn);

                try
                {
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Query Database for existing events and return list of events
        public List<ActivityModel> ReturnEvents()
        {
            List<ActivityModel> activities = new List<ActivityModel>();

            string sqlStatement = "SELECT * FROM dbo.events";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatement, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            ActivityModel activity = new ActivityModel(reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5));
                            
                            activity.ActivityId = reader.GetInt32(0);

                            activities.Add(activity);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                conn.Close();
            }
            return activities;
        }

        public void SignUp(int eventId, string name, string email, string phone, string status)
        {

            string insertStatement = "INSERT INTO dbo.signups (eventid, name, email, phone, status) VALUES (" + eventId + ", '" + name + "', '" + email + "', '" + phone + "', '" + status + "');";

            string incrementUsers = "UPDATE dbo.events SET current_users = current_users + 1 WHERE id = " + eventId;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd1 = new SqlCommand(insertStatement, conn);
                SqlCommand cmd2 = new SqlCommand(incrementUsers, conn);

                try
                {
                    conn.Open();
                    int result = cmd1.ExecuteNonQuery();
                    int result2 = cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
