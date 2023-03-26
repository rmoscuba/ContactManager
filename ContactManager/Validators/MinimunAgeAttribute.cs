using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Validators
{
    public class MinimunAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        public MinimunAgeAttribute(int minAge, string ErrorMessage)
        {
            _minAge = minAge;
            base.ErrorMessage = ErrorMessage;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value is null) {
                return false;
            }

            DateTime DateofBirth = (DateTime)value;

            var age = DateTime.Today.Year - DateofBirth.Year;

            return (age > _minAge);
        }
    }

}
