using System.Collections.Generic;

namespace cfp.online.Shared.Models
{
    public class CallForPaperResponse
    {
        public string Region { get; set; }

        public IEnumerable<ProposalModel> Proposals { get; set; }
    }
}
