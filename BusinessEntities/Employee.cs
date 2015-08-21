using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebTu.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Display(Name="firstname")]
        [Required(ErrorMessage="Enter First Name")]
        [FirstNameValidation]
        public string FirstName { get; set; }
        [Display(Name="lastname")]
        [StringLength(5,ErrorMessage="Last Name Length should greater than 5")]
        public string LastName { get; set; }
        [DataType(DataType.Currency)]
        public int Salary { get; set; }
    }

    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("please provide first name");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("first name should not contains @");
                }
            }
            return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }
    }
}