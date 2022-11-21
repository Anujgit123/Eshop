using System;

namespace Ecommerce.Application.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age
        {
            get
            {
                int age;
                if (DateOfBirth != null)
                {
                    age = DateTime.Today.Year - Convert.ToDateTime(DateOfBirth).Year;
                    return age;
                }

                return null;

            }
        }
        public string Gender { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsActive { get; set; }
    }
}
