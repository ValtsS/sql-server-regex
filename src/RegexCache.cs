using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace dev
{
    public static class RegexCache
    {

        const int CacheSize = 128;

        struct Data
        {
            public string Pattern;
            public Regex regex;
        }


        [ThreadStatic]
        private static Dictionary<string, LinkedListNode<Data>> cache = new Dictionary<string, LinkedListNode<Data>>(CacheSize);
        [ThreadStatic]
        private static LinkedList<Data> list = new LinkedList<Data>();

        public static Regex Get(string pattern)
        {
            if (cache.TryGetValue(pattern, out LinkedListNode<Data> dataNode))
            {
                if (list.Last != dataNode)
                {
                    list.Remove(dataNode);
                    list.AddLast(dataNode);
                }

                return dataNode.Value.regex;
            } else
            {
                while (list.Count > CacheSize)
                {
                    var first = list.First; list.RemoveFirst();
                    cache.Remove(first.Value.Pattern);
                }

                var ne = new Data()
                {
                    Pattern = pattern,
                    regex = new Regex(pattern, RegexOptions.Compiled)
                };

                var entry = list.AddLast(ne);
                cache[pattern] = entry;
                return entry.Value.regex;
            }
        }


    }
}
