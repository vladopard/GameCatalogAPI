namespace GameCatalogAPI.Helpers
{
    public static class DapperOrderByExtensions
    {
        private static readonly string[] ValidSortColumns =
        {
            "Name", "Genre", "Platform", "Rating", "ReleaseDate"
        };

        public static string ApplyDapperSort(this string? orderBy, string tableAlias = "g")
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return "";

            var clauses = new List<String>();
            var instructions = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var instruction in instructions)
            {
                var parts = instruction.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                var column = parts[0];
                var direction = (parts.Length > 1 && parts[1].Equals("desc",
                    StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC");

                if (!ValidSortColumns.Contains(column, StringComparer.OrdinalIgnoreCase))
                    continue;

                clauses.Add($"{tableAlias}.{column} {direction}");
            }

            return clauses.Count > 0 ? $" ORDER BY {string.Join(", ", clauses)}" : "";
        }
    }
}
