using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryPlace.WebLib.Helpers.Functions
{
    public static class StringHelpers
    {
        public static string TogglabeTextValue(int? value, string nonEmptyValue, string emptyValue)
        {
            return value.HasValue ? nonEmptyValue : emptyValue;
        }
    }
}
