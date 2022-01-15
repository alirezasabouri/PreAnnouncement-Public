using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Misc
{
    public class ValidationResult
    {
        public IList<string> Errors { get; } = new List<string>();
        public bool IsValid { get; private set; } = true;

        public void AddError(string errorMesssage)
        {
            this.Errors.Add(errorMesssage);
            this.IsValid = false;
        }
    }
}
