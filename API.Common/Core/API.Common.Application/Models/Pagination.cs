using System.Linq;

namespace API.Common.Application.Models
{
    public class PaginationResult<T>
    {
        public List<T> Data { get; set; } // Sayfaya ait veriler
        public int TotalRecords { get; set; } // Toplam kayıt sayısı
    }

    public static class Pagination
    {
        public static IQueryable<T> ApplyPagination<T>(IQueryable<T> query, int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                if (pageNumber <= 0) throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));
                if (pageSize <= 0) throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

                return query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            return query;
        }
        public static PaginationResult<T> ApplyPaginationWithCount<T>(IQueryable<T> query, int? pageNumber, int? pageSize)
        {
            var totalRecords = query.Count();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                if (pageNumber <= 0) throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));
                if (pageSize <= 0) throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

                var data = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .ToList();

                return new PaginationResult<T>
                {
                    Data = data,
                    TotalRecords = totalRecords
                };
            }

            return new PaginationResult<T>
            {
                Data = query.ToList(),
                TotalRecords = totalRecords
            };
        }

    }
}
