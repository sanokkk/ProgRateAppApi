using System.Collections.Generic;
using System.Linq;

namespace Antoher.Helpers
{
    public static class ChunkHelper
    {
        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(
        this IEnumerable<TValue> values,
        int chunkSize)
        {
            return values
                   .Select((v, i) => new { v, groupIndex = i / chunkSize })
                   .GroupBy(x => x.groupIndex)
                   .Select(g => g.Select(x => x.v));
        }
    }
}
