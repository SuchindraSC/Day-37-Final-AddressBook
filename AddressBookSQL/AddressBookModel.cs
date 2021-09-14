using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookSQL
{
    public class AddressBookModel
    {
        public int personid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string emailid { get; set; }
        public string booktype { get; set; }
        public int bookid { get; set; }
        public string bookname { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string state { get; set; }
        public DateTime dateadded { get; set; }
    }
}
