using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace cfp.online.Models
{
    public class ProposalViewModel
    {
        public string ConferenceName { get; set; }

        public Uri Website { get; set; }

        public DateTime EndDate { get; set; }

        public string Region { get; set; }

        public bool Success { get; set; }

        public bool Empty { get; set; }

        public List<SelectListItem> Regions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value="NA", Text ="North America"},
            new SelectListItem { Value="SA", Text ="South America"},
            new SelectListItem { Value="EU", Text ="Europe"},
            new SelectListItem { Value="IT", Text ="Italy"},
            new SelectListItem { Value="AF", Text ="Africa"},
            new SelectListItem { Value="AUS", Text ="Asia"}
        };
    }
}
