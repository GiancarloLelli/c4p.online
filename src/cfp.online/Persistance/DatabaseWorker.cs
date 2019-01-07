using cfp.online.Shared.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace cfp.online.Persistance
{
    public class DatabaseWorker
    {
        private static volatile object m_sync = new object();

        private readonly DatabaseContext m_context;
        private readonly IMemoryCache m_cache;

        public DatabaseWorker(DatabaseContext ctx, IMemoryCache cache)
        {
            m_context = ctx ?? throw new ArgumentNullException(nameof(ctx));
            m_cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public CallForPaperResponse GetCallForPapers(int count, string region)
        {
            var proposals = m_cache.Get<CallForPaperResponse>("Proposals");

            if (proposals == null)
            {
                lock (m_sync)
                {
                    var freshData = m_context.Poposals
                                            .Where(p => p.Region == region && p.EndDate >= DateTime.UtcNow && p.Approved)
                                            .OrderByDescending(p => p.CreatedOn)
                                            .Take(count);

                    var cfpData = new CallForPaperResponse
                    {
                        Region = region,
                        Proposals = freshData
                    };

                    m_cache.Set("Proposals", cfpData);
                    proposals = cfpData;
                }
            }

            return proposals;
        }

        public void AddProposal(ProposalModel item)
        {
            lock (m_sync)
            {
                m_cache.Remove("Proposals");
                m_context.Poposals.Add(item);
                m_context.SaveChanges();
            }
        }
    }
}
