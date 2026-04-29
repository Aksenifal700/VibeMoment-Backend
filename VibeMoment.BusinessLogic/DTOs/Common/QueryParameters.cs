namespace VibeMoment.BusinessLogic.DTOs.Common;

public class QueryParameters<TSortBy> where TSortBy : struct, Enum
{
    public TSortBy SortBy { get; set; }
    public string?  SearchTerm  { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}