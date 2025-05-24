using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models
{
    public class BookingType: GeneralData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingTypeId {  get; set; }
        public string type {  get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
