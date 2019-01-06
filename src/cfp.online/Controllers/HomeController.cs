using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cfp.online.Models;

namespace cfp.online.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            var emptyModel = new ProposalViewModel();
            return View(emptyModel);
        }

        public IActionResult Privacy() => View();

        public IActionResult Submit(ProposalViewModel model)
        {
            model.Region = string.Concat(model.Region, " selected");
            model.Success = ModelState.IsValid;
            return View("Index", model);
        }
    }
}
