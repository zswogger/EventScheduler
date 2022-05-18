using System.ComponentModel.DataAnnotations;
namespace LHScheduler.Models
{
    public class ActivityModel
    {
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "Date and time are required!")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Event name is required!")]
        public string ActivityName { get; set; }

        [Required(ErrorMessage = "Event description is required!")]
        public string ActivityDescription { get; set; }

        [Required(ErrorMessage = "Maximum participants is required!")]
        public int MaxParticipants { get; set; }
        public int CurrentParticipants { get; set; }

        public ActivityModel(DateTime dateTime, string activityName, string activityDescription, int maxParticipants,  int currentParticipants = 0)
        {
            DateTime = dateTime;
            ActivityName = activityName;
            ActivityDescription = activityDescription;
            MaxParticipants = maxParticipants;
            CurrentParticipants = currentParticipants;
        }

        public ActivityModel()
        {
        }
    }
}
