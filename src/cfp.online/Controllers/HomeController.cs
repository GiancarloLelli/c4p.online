using cfp.online.Models;
using cfp.online.Persistance;
using cfp.online.Shared.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System;

namespace cfp.online.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseWorker m_worker;
        private readonly TelemetryClient m_telemetry;

        public HomeController(DatabaseWorker ctx, TelemetryClient client)
        {
            m_worker = ctx ?? throw new ArgumentNullException(nameof(ctx));
            m_telemetry = client ?? throw new ArgumentNullException(nameof(client));
        }

        public IActionResult Privacy() => View();

        public IActionResult Index()
        {
            var emptyModel = new ProposalViewModel { Empty = true };
            return View(emptyModel);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Submit(ProposalViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Success = ModelState.IsValid;
                    var item = new ProposalModel
                    {
                        ConferenceName = model.ConferenceName,
                        EndDate = model.EndDate,
                        Region = model.Region,
                        Website = model.Website,
                        CreatedOn = DateTime.UtcNow
                    };

                    m_worker.AddProposal(item);
                }
                catch (Exception ex)
                {
                    model.Success = false;
                    m_telemetry.TrackException(ex);
                }
            }

            model.Empty = false;
            model.Region = string.Concat(model.Region, " selected");

            return View("Index", model);
        }
    }
}