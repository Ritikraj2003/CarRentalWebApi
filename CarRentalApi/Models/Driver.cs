using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRentalApi.Models;

public class Driver: GeneralData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DriverID { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string gmail {  get; set; }

    public string Address { get; set; }

    public bool IsDeleted { get; set; } = false;
}
