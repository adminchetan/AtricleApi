using System.ComponentModel.DataAnnotations;

namespace NewsService.DTO
{
    public class AuthDTO
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number. The mobile number must be 10 digits.")]

        public string MobileNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string Password { get; set; }

        //public bool isActive {  get; set; }

        // public string loggerMessage { get; set; }


        public string CurrentUser { get; set; } = null;


        // below can be used while user try to login
        //public string LoginValidatorMesage { get; set; }

       // public bool IsValidated { get; set; }

        



    }
}
