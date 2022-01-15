using PreAnnouncement.Domain;
using PreAnnouncement.Misc;
using System.Threading.Tasks;

namespace PreAnnouncement.Ports
{
    public interface IPreAnnouncementDispatcherQueue
    {
        Task<Result> EnqueueAsync(PreAnnouncementProcessingResult preAnnouncementResult);
    }
}
