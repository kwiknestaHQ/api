using Microsoft.EntityFrameworkCore;

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

        public static async Task<PagedResponse<T>> PaginateAsync<T>(this IQueryable<T> query, int page, int size,
            CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);

            var data = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return new PagedResponse<T>(data, page, size, count);
        }
    }
}