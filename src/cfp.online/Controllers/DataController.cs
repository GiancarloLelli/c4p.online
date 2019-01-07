using cfp.online.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;

namespace cfp.online.Controllers
{
    public class DataController : Controller
    {
        private readonly DatabaseWorker m_worker;

        public DataController(DatabaseWorker ctx)
        {
            m_worker = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public IActionResult GetAvailableCallForPapers(int count, string region)
        {
            var top = count > 0 ? count : 10;
            var area = !string.IsNullOrEmpty(region) ? region : "EU";
            var data = m_worker.GetCallForPapers(top, area);
            return Json(data);
        }
    }
}
