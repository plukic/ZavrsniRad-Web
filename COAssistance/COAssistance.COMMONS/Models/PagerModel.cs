using System.Collections.Generic;

namespace COAssistance.COMMONS.Models
{
    public class PagerModel<T>
    {
        public int Count { get; set; }
        public int NumberOfPages { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public IEnumerable<T> Result { get; set; }
    }
}