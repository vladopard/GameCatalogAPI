using System.Linq.Dynamic.Core;

namespace GameCatalogAPI.Helpers
{
    public static class IQueryableExtensions
    {
        //orderBy=genre,-releaseDate
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> source, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                throw new Exception($" EMPTY ordering parameter in: {orderBy}");

            var finalSort = string.Empty;
            var sorters = orderBy.Split(",");
            foreach (var item in sorters)
            {
                if (finalSort != string.Empty)
                    finalSort += ",";
                finalSort += SortOneField(item);
            }

            return source.OrderBy(finalSort);
        }

        public static bool IsGameSortField(string orderBy)
        {
            string[] allowedFields = { "Name", "Genre", "Platform", "Rating", "ReleaseDate" };
            return allowedFields.Contains(orderBy, StringComparer.OrdinalIgnoreCase);
        }

        public static string SortOneField(string item)
        {
            var isDescending = item.StartsWith("-");
            var field = isDescending ? item.Substring(1) : item;

            if(!IsGameSortField(field))
                throw new Exception($"Invalid ordering parameter: {field}");

            return $"{field} {(isDescending ? "descending" : "ascending")}";
        }
    }
}


