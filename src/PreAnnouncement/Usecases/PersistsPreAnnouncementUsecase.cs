using PreAnnouncement.Domain;
using PreAnnouncement.Misc;
using PreAnnouncement.Ports;
using System.Threading.Tasks;

namespace PreAnnouncement.Usecases
{
    public class PersistsPreAnnouncementUsecase
    {
        private readonly IPreAnnouncementRepository _preAnnouncementRepository;

        public PersistsPreAnnouncementUsecase(
           IPreAnnouncementRepository preAnnouncementRepository)
        {
            _preAnnouncementRepository = preAnnouncementRepository;
        }

        public async Task<Result> ExecuteAsync(PreAnnouncementProcessingResult preAnnouncementResult)
        {
            return await _preAnnouncementRepository.StoreAsync(preAnnouncementResult);
        }
    }
}
