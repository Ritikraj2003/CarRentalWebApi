namespace CarRentalApi.Models
{
    public class User
    {
        public Guid? id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }



        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
    }


    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

   
    }


