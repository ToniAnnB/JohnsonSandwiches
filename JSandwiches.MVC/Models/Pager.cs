namespace JSandwiches.MVC.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        //total number of items in a list
        public int CurrentPage { get; private set; }
        // current page being viewed
        public int PageSize { get; private set; }
        // the number of items that are displayed for each page
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public Pager() { }
        public Pager(int totalItems, int page, int pageSize = 5)
        {
            int totalPages = (int)Math
                .Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;
            int startPage = currentPage + 3;
            int endPage = currentPage + 3;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 7)
                {
                    startPage = endPage - 6;
                }
            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            StartPage = startPage;
            EndPage = endPage;
            TotalPages = totalPages;
            PageSize = pageSize;

        }
    }

}
