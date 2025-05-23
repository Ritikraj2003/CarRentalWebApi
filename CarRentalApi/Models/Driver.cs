using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Driver
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DriverID { get; set; }

    public string Name { get; set; }

    [Phone]
    public string Phone { get; set; }

    public string Address { get; set; }
}
