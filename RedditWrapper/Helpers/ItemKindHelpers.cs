using RedditWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Helpers
{
    public static class ItemKindHelpers
    {
        private static Dictionary<string, ItemKind> byValue = StringValueAttributeHelpers.StringValues<ItemKind>();

        public static ItemKind FromStringValue(string stringValue)
        {
            if (!byValue.ContainsKey(stringValue)) throw new Exception(String.Format("StringValue '{0}' does not resolve to a RedditItemKind", stringValue));
            return byValue[stringValue];
        }
    }
}
