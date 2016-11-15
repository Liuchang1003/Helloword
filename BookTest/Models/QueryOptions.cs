namespace BookTest.Models
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            Page = 1;
            PageSize = 5;
            SortField = "Id";
            SortOrder = SortOrderMolds.ASC;
        } 

        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }

        public string SortField { get; set; }

        public SortOrderMolds SortOrder { get; set; }

        public string Sort => string.Format("{0} {1}", SortField, SortOrder.ToString());
    }
}