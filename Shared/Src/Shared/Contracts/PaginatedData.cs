namespace Shared.Contracts;

public record PaginatedData<TItem>(
    IEnumerable<TItem> List,
    int TotalCount);