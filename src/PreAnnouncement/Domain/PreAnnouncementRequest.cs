using PreAnnouncement.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Domain
{
    public class PreAnnouncementRequest : IValidatable
    {
        public PostalAddress SenderAddress { get; set; }
        public string SenderName { get; set; }
        public PostalAddress RecipientAddress { get; set; }
        public string RecipientName { get; set; }
        public string DispatchingService { get; set; }

        public ValidationResult Validate()
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(SenderName))
                result.AddError($"{nameof(SenderName)} can not be empty");

            if (string.IsNullOrEmpty(RecipientName))
                result.AddError($"{nameof(RecipientName)} can not be empty");

            if (!SenderAddress.Validate().IsValid)
                result.AddError($"{nameof(SenderAddress)} is invalid");

            if (!RecipientAddress.Validate().IsValid)
                result.AddError($"{nameof(RecipientAddress)} is invalid");

            if (!SenderAddress.Validate().IsValid)
                result.AddError($"{nameof(RecipientAddress)} is invalid");

            return result;
        }
    }
}
