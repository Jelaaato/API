using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendapi.Models
{
    public class Paging<T>
    {     
            public class PagingInfo
            {
                public int PageNo { get; set; }

                public int PageSize { get; set; }

                public int PageCount { get; set; }

                public long TotalRecordCount { get; set; }

            }
            public List<T> Data { get; private set; }

            public PagingInfo Page { get; private set; }

            public Paging(IEnumerable<T> items, int pageno, int pagesize, long totalrecordcount)
            {
                Data = new List<T>(items);
                Page = new PagingInfo
                {
                    PageNo = pageno,
                    PageSize = pagesize,
                    TotalRecordCount = totalrecordcount,
                    PageCount = totalrecordcount > 0
                        ? (int)Math.Ceiling(totalrecordcount / (double)pagesize)
                        : 0
                };
            }
        }
    }