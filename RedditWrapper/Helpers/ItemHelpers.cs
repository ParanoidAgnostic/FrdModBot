using RedditWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Helpers
{
    public static class ItemHelpers
    {
        private static Dictionary<string, ItemKind> KindsByValue = StringValueAttributeHelpers.StringValues<ItemKind>();

        public static ItemKind KindFromStringValue(string stringValue)
        {
            if (!KindsByValue.ContainsKey(stringValue)) throw new Exception(String.Format("StringValue '{0}' does not resolve to a RedditItemKind", stringValue));
            return KindsByValue[stringValue];
        }

        public static ItemKind KindFromItemId(string id)
        {
            string[] idParts = id.Split('_');
            if(idParts.Length!=2) throw new Exception(String.Format("Id '{0}' is not a full ID", id));
            return KindFromStringValue(idParts[0]);
        }

        public static string GetShortId(string id)
        {
            string[] idParts = id.Split('_');
            if (idParts.Length == 2) return idParts[1];
            return id;
        }
    }
}
