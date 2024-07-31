namespace Reneee.Application.Utils
{
    public static class StringUtils
    {
        public static string GetSortDirection(string sortBy)
        {
            var parts = sortBy.Split('-');
            if (parts.Length == 2)
            {
                return parts[1];
            }
            return string.Empty;
        }
    }
}
