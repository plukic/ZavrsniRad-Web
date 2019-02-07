using System.Collections.Generic;

namespace COAssistance.COMMONS.Models.Paging
{
    public class PagingResultVM<T>
    {
        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public List<T> Result { get; set; }
    }
}