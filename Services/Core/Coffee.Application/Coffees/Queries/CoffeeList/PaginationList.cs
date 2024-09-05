using System.Text.Json.Serialization;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public sealed class PaginationList<T>
{
    private PaginationList()
    {
    }

    [JsonConstructor]
    public PaginationList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Items { get; } = null!;
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PaginationList<T>> Create(List<T> items, int page, int pageSize)
    {
        var totalCount = items.Count;
        var pagedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return await Task.FromResult(new PaginationList<T>(pagedItems, page, pageSize, totalCount));
    }
}