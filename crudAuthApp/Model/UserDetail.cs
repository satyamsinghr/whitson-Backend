using System.ComponentModel.DataAnnotations;

namespace crudAuthApp.Model
{
    public class UserDetail
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Opt { get; set; } 
        public bool? Status { get; set; } = false;
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EmailDetail
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
    public class VerifyOtpModel
    {
        public Guid Id { get; set; }
        public int Otp { get; set; }
    }
}
