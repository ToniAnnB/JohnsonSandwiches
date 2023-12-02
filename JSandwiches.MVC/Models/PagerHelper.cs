namespace JSandwiches.MVC.Models
{
    public static class PagerHelper<T> where T : class
    {
        public static (List<T>, Pager) Paging(List<T> lstEntity, int pg, int pageSize)
        {
            int nPageSize = pageSize;
            if (pg < 1)
                pg = 1;
            int resCount = lstEntity.Count();
            var pager = new Pager(resCount, pg, nPageSize);
            int recSkip = (pg - 1) * nPageSize;
            var list = lstEntity
                .Skip(recSkip)
                .Take(pager.PageSize)
                .ToList();
            return (list, pager);
        }

    }
}
