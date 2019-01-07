using System;

namespace cfp.online.Shared.Models
{
    public class ProposalModel
    {
        public Guid Id { get; set; }

        public string ConferenceName { get; set; }

        public Uri Website { get; set; }

        public DateTime EndDate { get; set; }

        public string Region { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Approved { get; set; }
    }
}
