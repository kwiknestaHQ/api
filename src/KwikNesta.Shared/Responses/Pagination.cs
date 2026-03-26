namespace KwikNesta.Shared.Responses
{
    public static class Pagination
    {
        public static PagedResponse<T> Paginate<T>(this IQueryable<T> source, int page, int size)
        {
            var count = source.Count();
            var data = source
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return new PagedResponse<T>(data, page, size, count);
        }

        public static PagedResponse<T> Paginate<T>(this IEnumerable<T> source, int page, int size)
        {
            var count = source.Count();
            var data = source
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return new PagedResponse<T>(data, page, size, count);
        }
    }
}