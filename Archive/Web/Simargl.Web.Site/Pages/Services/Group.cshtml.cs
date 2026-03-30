using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simargl.Web.Site.Core;

namespace Simargl.Web.Site.Pages.Services
{
    [CLSCompliant(false)]
    public class GroupModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Key { get; set; }

        /// <summary>
        /// Возвращает или задаёт группу.
        /// </summary>
        public ServiceGroup ServiceGroup { get; set; } = null!;

        public void OnGet()
        {
            ServiceGroup = ServiceGroup.ServiceGroups[Key];
        }
    }
}
