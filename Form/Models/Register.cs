using Form.Data;
using System.ComponentModel.DataAnnotations;

namespace Form.Models
{
    public class Register
    {
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name must contain only letters.")]
        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

       
        [UniqueEmail(ErrorMessage ="The email already exists. So use another email.  ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The phone is required.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Age is required.")]
        [Range(18, int.MaxValue, ErrorMessage = "Age must be 18 years or older.")]
        public int Age { get; set; }
    }

    //this validation is for email.

    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null || !(value is string))
            {
                return new ValidationResult("The email is invalid.");
            }
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var email = value.ToString();

            var existingUser = context.Students.FirstOrDefault(x => x.Email == email);

            if (existingUser != null)
            {
                return new ValidationResult("This email is already in use. Please use another email.");
            }
            return ValidationResult.Success;
        }
    }
}
