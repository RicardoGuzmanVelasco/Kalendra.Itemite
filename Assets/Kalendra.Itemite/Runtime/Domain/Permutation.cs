using System.Collections.Generic;
using System.Linq;

namespace Kalendra.Itemite.Runtime.Domain
{
    public static class Permutation
    {
        public static IEnumerable<IEnumerable<T>> Of<T>(IEnumerable<T> source)
        {
            return Of(source.ToList());
        }

        public static IEnumerable<IEnumerable<T>> Of<T>(IList<T> source)
        {
            return Permute(source.Count).Select(p => p.Select(i => source[i]));
        }

        #region Indexing
        static IEnumerable<IList<int>> Permute(int size)
        {
            return PermutedIndex
            (
                Enumerable.Range(0, size).ToList(),
                0,
                size - 1,
                new List<IList<int>>()
            );
        }

        static IList<IList<int>> PermutedIndex(IList<int> nums, int start, int end, IList<IList<int>> list)
        {
            if(start == end)
            {
                list.Add(new List<int>(nums));
                return list;
            }

            for(var i = start; i <= end; i++)
            {
                Swap(nums, start, i);
                PermutedIndex(nums, start + 1, end, list);
                Swap(nums, start, i);
            }

            return list;
        }

        static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }
        #endregion
    }
}