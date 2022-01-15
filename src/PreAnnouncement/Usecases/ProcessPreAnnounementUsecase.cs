using PreAnnouncement.Domain;
using PreAnnouncement.Exceptions;
using PreAnnouncement.Misc;
using PreAnnouncement.Ports;
using PreAnnouncement.Services;
using Serilog;
using System;
using System.Threading.Tasks;

namespace PreAnnouncement.Usecases
{
    public class ProcessPreAnnounementUsecase
    {
        private readonly IPreAnnouncementDispatcherQueue _preAnnouncementDispatcherQueue;
        private readonly IBarcodeService _barcodeService;
        private readonly ILogger _logger;

        public ProcessPreAnnounementUsecase(
            IPreAnnouncementDispatcherQueue preAnnouncementDispatcherQueue,
            IBarcodeService barcodeService,
            ILogger logger)
        {
            _preAnnouncementDispatcherQueue = preAnnouncementDispatcherQueue;
            _barcodeService = barcodeService;
            _logger = logger;
        }

        private PreAnnouncementReceipt CreateReceipt(PreAnnouncementRequest preAnnouncementRequest)
        {
            var result = PreAnnouncementReceipt.FromPreAnnouncementRequest(preAnnouncementRequest);
            result.Barcode = _barcodeService.CreateNew();

            return result;
        }

        public async Task<PreAnnouncementProcessingResult> ExecuteAsync(PreAnnouncementRequest preAnnouncementRequest)
        {
            var result = PreAnnouncementProcessingResult.FromPreAnnouncementRequest(preAnnouncementRequest);
            var validationResult = preAnnouncementRequest.Validate();

            result.IsValid = validationResult.IsValid;
            if (result.IsValid)
            {
                result.Barcode = _barcodeService.CreateNew();
                var dispatchResult = await _preAnnouncementDispatcherQueue.EnqueueAsync(result);

                if (dispatchResult.IsSuccess)
                    return result.Succeed();
                else
                {
                    _logger.Error(dispatchResult.Error, "Error while proccessing PreAnnouncement");
                    return result.Failed();
                }
            }

            _logger.Warning($@"Invalid PreAnnouncement request received, validation errors: 
                {string.Join(Environment.NewLine, validationResult.Errors)}");

            return result.Failed();
        }
    }
}
