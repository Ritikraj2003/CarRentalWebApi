using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models
{
    public class Notification:GeneralData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public int NotificationFromId { get; set; }
        public bool NotificationStatus { get; set; }
        public string Message { get; set; }
        public bool? IsDeleted { get; set; } = false;

    }
}
