using System;
using System.Collections.Generic;
using System.Text;

namespace PreAnnouncement.Domain
{
    public class PreAnnouncementReceipt
    {
        public string Barcode { get; set; }
        public PostalAddress SenderAddress { get; set; }
        public string SenderName { get; set; }
        public PostalAddress RecipientAddress { get; set; }
        public string RecipientName { get; set; }
        public string DispatchingService { get; set; }


        public static PreAnnouncementReceipt FromPreAnnouncementRequest(PreAnnouncementRequest preAnnouncementRequest)
        {
            return new PreAnnouncementReceipt
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
