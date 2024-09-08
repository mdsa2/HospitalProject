using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Util
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage,int itemsPerPage,int totalItem,int totalPages ) {
            currentPage = currentPage;
            itemsPerPage = ItemPerPage;
            totalItem = totalItem;
            totalPages = TotalPages;
        
        
        }
        public int currentPage {  get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages { get; set; }
    }
}
