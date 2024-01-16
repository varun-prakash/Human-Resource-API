using System.ComponentModel.DataAnnotations;

namespace Human_Resource_API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Contact Number is a required field.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime? TerminationDate { get; set; }
        public string TerminationReason { get; set; }
        public bool IsActive { get; set; }
    }
}
