using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models
{
    public class User: GeneralData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        public bool IsDeleted { get; set; } = false;
    }


     public class UserLoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class CreateUserRequest 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }



}


