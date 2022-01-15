using PreAnnouncement.Domain;
using PreAnnouncement.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreAnnouncement.Ports
{
    public interface IPreAnnouncementRepository
    {
        Task<Result> StoreAsync(PreAnnouncementProcessingResult preAnnouncementRequest);
    }
}
