using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simargl.Web.Site.Core;

namespace Simargl.Web.Site.Pages.Services
{
    [CLSCompliant(false)]
    public class PositionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int GroupKey { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PositionKey { get; set; }

        public ServiceGroup ServiceGroup { get; set; } = null!;
        public ServicePosition ServicePosition { get; set; } = null!;

        public void OnGet()
        {
            ServiceGroup = ServiceGroup.ServiceGroups[GroupKey];
            ServicePosition = ServiceGroup.ServicePositions[PositionKey];
        }
    }
}
