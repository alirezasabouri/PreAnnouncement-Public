using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Misc
{
    public interface IValidatable
    {
        ValidationResult Validate();
    }
}
