using System.Collections.Generic;
using System.Linq;

namespace Antoher.Helpers
{
    public static class ChunkHelper
    {
        /// <summary>
        /// Метод расширения (в данной версии у linq нет метода chunk)
        /// </summary>
        /// <typeparam name="TValue">Дженерик парметр</typeparam>
        /// <param name="values">Коллекция для чанка</param>
        /// <param name="chunkSize">Размер для разбиения на коллекции</param>
        /// <returns></returns>
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
