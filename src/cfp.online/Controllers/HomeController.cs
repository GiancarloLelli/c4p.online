using cfp.online.Models;
using cfp.online.Persistance;
using cfp.online.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace cfp.online.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseWorker m_worker;

        public HomeController(DatabaseWorker ctx)
        {
            m_worker = ctx ?? throw new ArgumentNullException(nameof(ctx));
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
            model.Empty = false;
            model.Region = string.Concat(model.Region, " selected");
            model.Success = ModelState.IsValid;

            if (ModelState.IsValid)
            {
                var item = new ProposalModel
                {
                    ConferenceName = model.ConferenceName,
                    EndDate = model.EndDate,
                    Region = model.Region,
                    Website = model.Website
                };

                m_worker.AddProposal(item);
            }

            return View("Index", model);
        }
    }
}