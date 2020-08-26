namespace GameWebApi.Features.Ban.Models
{
    using System;
    using Core.Enums;

    public class BanPlayerRequestModel
    {
        public int PlayerId { get; set; }
        public BanReason BanReason { get; set; }
        public string BanMessage { get; set; }
        public DateTime EndBanDate { get; set; }
    }
}
