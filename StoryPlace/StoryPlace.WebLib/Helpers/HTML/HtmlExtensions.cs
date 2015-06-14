using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using StoryPlace.WebLib.Helpers.Functions;

namespace StoryPlace.WebLib.Helpers.HTML
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString TogglabeTextValue(this HtmlHelper htmlHelper, int ? value,
            string nonEmptyValue,string emptyValue)
        {
            return new MvcHtmlString(StringHelpers.TogglabeTextValue(value, nonEmptyValue, emptyValue));
        }

    }
}
