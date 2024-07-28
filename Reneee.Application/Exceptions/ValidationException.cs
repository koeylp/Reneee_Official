using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Reneee.Application.Exceptions
{
    public class ValidationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            //foreach (var error in validationResult.Errors)
            //{
            //    Errors.Add(error.ErrorMessage);
            //}
        }
    }
}
