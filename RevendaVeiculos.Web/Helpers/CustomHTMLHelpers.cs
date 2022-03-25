using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RevendaVeiculos.Web.Helpers
{
    public static class CustomHTMLHelpers
    {
        public static IEnumerable<SelectListItem> HelloWorldHTMLString(this IHtmlHelper htmlHelper)
        {
            IEnumerable<SelectListItem> list = new List<SelectListItem>();

            return list;
        }
    }
}
