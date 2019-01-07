using cfp.online.Persistance;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;

namespace cfp.online.Controllers
{
    public class DataController : Controller
    {
        private readonly DatabaseWorker m_worker;
        private readonly TelemetryClient m_telemetry;

        public DataController(DatabaseWorker ctx, TelemetryClient client)
        {
            m_worker = ctx ?? throw new ArgumentNullException(nameof(ctx));
            m_telemetry = client ?? throw new ArgumentNullException(nameof(client));
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
