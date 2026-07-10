namespace EmbarcaPro.API.Common.Pagination
{
    public record PagedList<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int TotalCount { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PagedList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

    }
}
