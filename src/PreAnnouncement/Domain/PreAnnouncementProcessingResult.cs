namespace PreAnnouncement.Domain
{
    public class PreAnnouncementProcessingResult
    {
        public bool IsValid { get; set; }
        public string Barcode { get; set; }
        public PostalAddress SenderAddress { get; set; }
        public string SenderName { get; set; }
        public PostalAddress RecipientAddress { get; set; }
        public string RecipientName { get; set; }
        public string DispatchingService { get; set; }

        public bool ProceedSuccessfully { get; private set; }
        public PreAnnouncementProcessingResult Succeed()
        {
            this.ProceedSuccessfully = true;
            return this;
        }
        public PreAnnouncementProcessingResult Failed()
        {
            this.ProceedSuccessfully = false;
            return this;
        }

        public static PreAnnouncementProcessingResult FromPreAnnouncementRequest(PreAnnouncementRequest preAnnouncementRequest)
        {
            return new PreAnnouncementProcessingResult
            {
                SenderName = preAnnouncementRequest.SenderName,
                SenderAddress = preAnnouncementRequest.SenderAddress,
                RecipientName = preAnnouncementRequest.RecipientName,
                RecipientAddress = preAnnouncementRequest.RecipientAddress,
                DispatchingService = preAnnouncementRequest.DispatchingService
            };
        }
    }
}
