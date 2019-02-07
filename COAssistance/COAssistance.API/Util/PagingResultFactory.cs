using COAssistance.COMMONS.Models.Paging;
using System;
using System.Linq;

namespace COAssistance.API.Util
{
    public class PagingResultFactory
    {
        #region Methods

        public PagingResultVM<R> CreatePagingResult<R, I, TKey>(IQueryable<I> data, int pageSize, int pageIndex, Func<I, TKey> orderBy, Func<I, R> selector)
        {
            PagingResultVM<R> pagingResult = CreatePageResult<I, R>(data, pageSize, pageIndex);
            pagingResult.Result =
                 data
                .OrderBy(orderBy)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToList();

            return pagingResult;
        }

        public PagingResultVM<R> CreatePagingResultDesc<R, I, TKey>(IQueryable<I> data, int pageSize, int pageIndex, Func<I, TKey> orderBy, Func<I, R> selector)
        {
            PagingResultVM<R> pagingResult = CreatePageResult<I, R>(data, pageSize, pageIndex);
            pagingResult.Result =
                 data
                .OrderByDescending(orderBy)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToList();

            return pagingResult;
        }

        private PagingResultVM<R> CreatePageResult<I, R>(IQueryable<I> data, int pageSize, int pageIndex)
        {
            var count = data.Count();
            var totalPages = (int)Math.Ceiling((decimal)count / (decimal)pageSize);
            var startPage = pageIndex - 5;
            var endPage = pageIndex + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            return new PagingResultVM<R>
            {
                TotalItems = count,
                CurrentPage = pageIndex,
                EndPage = endPage,
                StartPage = startPage,
                PageSize = pageSize,
                TotalPages = totalPages,
            };
        }

        #endregion Methods
    }
}