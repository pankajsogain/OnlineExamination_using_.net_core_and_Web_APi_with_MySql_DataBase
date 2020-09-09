
using System.ComponentModel.DataAnnotations;

namespace OnlineExaminationApps.Models
{
    public class Registration
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }
        public int Country { get; set; }
        [Required(ErrorMessage = "State is required")]
        [Range(1, 50000, ErrorMessage = "State is required")]
        public int State { get; set; }
        [Required(ErrorMessage = "City is required")]
        [Range(1,100000, ErrorMessage = "City is required")]
        public int City { get; set; }
        [Required]
        public string Role { get; set; }
        
        [Range(typeof(bool), "True", "True",ErrorMessage = "Required")]
        public bool TAndC { get; set; }       

    }
}
