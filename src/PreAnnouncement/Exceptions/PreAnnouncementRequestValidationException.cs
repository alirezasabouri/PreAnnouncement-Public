using System;
using System.Collections.Generic;

namespace PreAnnouncement.Exceptions
{
    public class PreAnnouncementRequestValidationException : Exception
    {
        private static string createValidationError(IEnumerable<string> validationErrors)
        {
            return $@"Validation failed for PreAnnouncement Request: 
                {string.Join(Environment.NewLine, validationErrors)}";
        }

        public PreAnnouncementRequestValidationException(IEnumerable<string> validationErrors)
            : base(createValidationError(validationErrors))
        { }
    }
}
