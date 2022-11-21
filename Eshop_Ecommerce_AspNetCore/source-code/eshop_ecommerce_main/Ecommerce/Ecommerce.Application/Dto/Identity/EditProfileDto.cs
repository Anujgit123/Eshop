using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dto
{
    public class EditProfileDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicturePreview { get; set; }
        public string ProfilePicture { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //[DataType(DataType.Password)]
        //public string OldPassword { get; set; }

        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //public string NewPassword { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmNewPassword { get; set; }
    }
}
