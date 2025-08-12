using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Restaurant.DTOS;

namespace Restaurant.Application.Common
{
    public class PageResult<T>
    {
        public PageResult(IEnumerable<T> item , int totalCount, int pagesize ,int pageNumber)
        {
           Items = item;
           TotalItemCount = totalCount;
           Totalpages = (int)Math.Ceiling(pagesize / (double)pageNumber);
            ItemsFrom = pagesize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pagesize - 1;
        }

        public IEnumerable<T> Items { get; set; }
        public int Totalpages { get; set; }
        public int TotalItemCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
