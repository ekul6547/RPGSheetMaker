using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPGSheet2.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AlphaNumericAttribute : RegularExpressionAttribute
    {
        public AlphaNumericAttribute() : base("^[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._\\ ]+$")
        {
            ErrorMessage = "Characters must be a-z, A-Z, 0-9, _, . or spaces.";
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} is invalid. {ErrorMessage}";
        }
    }
}
