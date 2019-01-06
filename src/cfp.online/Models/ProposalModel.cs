using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cfp.online.Models
{
    public class ProposalModel
    {
        public Guid Id { get; set; }

        public string ConferenceName { get; set; }

        public Uri Website { get; set; }

        public DateTime EndDate { get; set; }

        public string Region { get; set; }

        public DateTime CreatedOn
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
