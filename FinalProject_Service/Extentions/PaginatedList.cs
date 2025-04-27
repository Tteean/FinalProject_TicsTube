using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Extentions
{
    public class PaginatedList<T>:List<T>
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public PaginatedList(List<T> items, int pageCount, int page)
        {
            this.AddRange(items);
            PageCount = pageCount;
            CurrentPage = page;
            HasNext = CurrentPage < PageCount;
            HasPrev = CurrentPage > 1;
            int start = CurrentPage - 2;
            int end = CurrentPage + 2;
            if (PageCount > 5)
            {
                if (start <= 0)
                {
                    end = end - (start - 1);
                    start = 1;
                }
                if (end > PageCount)
                {
                    end = PageCount;
                    start = end - 4;
                }
                Start = start; End = end;
            }
        }

        public static PaginatedList<T> Create(IQueryable<T> query, int take, int page)
        {
            var count = query.Count();
            var data = query.Skip((page - 1) * take).Take(take).ToList();
            var pageCount = (int)Math.Ceiling((decimal)count / take);
            return new PaginatedList<T>(data, pageCount, page);
        }
    }
}
