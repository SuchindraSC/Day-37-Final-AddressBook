using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookSQL
{
    public class AddressBookDetail
    {
        public int personid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string booktype { get; set; }
        public int bookid { get; set; }
        public string bookname { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string state { get; set; }
        public DateTime dateadded { get; set; }
        public AddressBookDetail(string firstname, string lastname, string phone, string email, string city, int bookid, int personid, int zip, DateTime dateadded)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.phone = phone;
            this.email = email;
            this.city = city;
            this.bookid = bookid;
            this.personid = personid;
            this.zip = zip;
            this.dateadded = dateadded;
        }
    }
}

