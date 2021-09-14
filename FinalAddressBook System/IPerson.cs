using System;
using System.Collections.Generic;
using System.Text;

namespace FinalAddressBook_System
{
    interface IPerson
    {
        public void AddContact(string firstName, string lastName, string address, string city, string state, string email, int zip, long phoneNumber, string addressbookName);
        public void EditContact(string name, string addressbookName);
        public void DeleteContact(string deletename, string addressbookName);
        public void AddAddressBook(string addressbookName);
        public void DisplayContact(string addressbookName);
    }
}
