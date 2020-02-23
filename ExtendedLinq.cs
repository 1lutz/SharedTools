using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharedTools
{
    public static class ExtendedLinq
    {
        public static Dictionary<T, int> CountDuplicates<T>(this IEnumerable<T> source)
        {
            Dictionary<T, int> countDict = new Dictionary<T, int>();

            foreach (T wert in source)
            {
                int anz;

                if (countDict.TryGetValue(wert, out anz))
                    countDict[wert] = ++anz;
                else
                    countDict[wert] = 1;
            }
            return countDict;
        }
    }
}
