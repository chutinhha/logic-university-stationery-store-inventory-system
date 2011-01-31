using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {

        // the user name must be alphanumeric (Alpha Numeric) values.
        [Required(ErrorMessage = "User name is required.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Please enter valid user name.")]
        [StringLength(255, ErrorMessage="User name cannot be longer than 255 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(255, ErrorMessage="First name cannot be longer than 255 characters")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(255, ErrorMessage="Last name cannot be longer than 255 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("\b[A-Z0-9._%-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}\b", ErrorMessage = "Please enter valid Email address.")]
        public string Email { get; set; }

        //the password must be at least 8 characters long and start and end with a letter
         [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^[A-Za-z]\\w{6,}[A-Za-z]$", ErrorMessage = "Please enter valid password.")]
        public string Password { get; set; }
    }
}
