using PreAnnouncement.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Domain
{
    public class PostalAddress : IValidatable
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public int Number { get; set; }
        public string NumberAddition { get; set; }

        public ValidationResult Validate()
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(City))
                result.AddError($"{nameof(City)} can not be empty");
            if (string.IsNullOrEmpty(AddressLine))
                result.AddError($"{nameof(AddressLine)} can not be empty");
            if (string.IsNullOrEmpty(PostalCode))
                result.AddError($"{nameof(PostalCode)} can not be empty");

            return result;
        }
    }
}
