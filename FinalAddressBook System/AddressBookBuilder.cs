using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FinalAddressBook_System
{
    public class AddressBookBuilder : IPerson
    {
        internal Dictionary<string, Person> addressBook = new Dictionary<string, Person>();
        internal Dictionary<string, AddressBookBuilder> addressBookDictionary = new Dictionary<string, AddressBookBuilder>();
        private Dictionary<Person, string> cityDictionary = new Dictionary<Person, string>();
        private Dictionary<Person, string> stateDictionary = new Dictionary<Person, string>();

        public void AddContact(string firstName, string lastName, string address, string city, string state, string email, int zip, long phoneNumber, string addressbookName)
        {
            Person contact = new Person(firstName, lastName, address, city, state, email, zip, phoneNumber);
            addressBookDictionary[addressbookName].addressBook.Add(contact.FirstName + " " + contact.LastName, contact);
            Console.WriteLine("\nAdded Succesfully. \n");
        }
        public void DisplayContact(string addressbookName)
        {
            Console.WriteLine($"You are working on {addressbookName} addressbook", addressbookName);
            foreach (KeyValuePair<string, Person> item in addressBookDictionary[addressbookName].addressBook)
            {
                Console.WriteLine("First Name : " + item.Value.FirstName);
                Console.WriteLine("Last Name : " + item.Value.LastName);
                Console.WriteLine("Address : " + item.Value.Address);
                Console.WriteLine("City : " + item.Value.City);
                Console.WriteLine("State : " + item.Value.State);
                Console.WriteLine("Email : " + item.Value.Email);
                Console.WriteLine("Zip : " + item.Value.Zip);
                Console.WriteLine("Phone Number : " + item.Value.PhoneNumber + "\n");
            }
        }
        public void EditContact(string name, string addressbookName)
        {
            bool flag = false;
            foreach (KeyValuePair<string, Person> item in addressBookDictionary[addressbookName].addressBook)
            {
                if (item.Key.Equals(name))
                {
                    flag = true;
                    Console.WriteLine("Enter your choich \n1.First Name \n2.Last Name \n3.Address \n4.City \n5.State \n6.Email \n7.Zip \n8.Phone Number");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter New First Name :");
                            item.Value.FirstName = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Enter New Last Name :");
                            item.Value.LastName = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Enter New Address :");
                            item.Value.Address = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Enter New City :");
                            item.Value.City = Console.ReadLine();
                            break;
                        case 5:
                            Console.WriteLine("Enter New State :");
                            item.Value.State = Console.ReadLine();
                            break;
                        case 6:
                            Console.WriteLine("Enter New Email :");
                            item.Value.Email = Console.ReadLine();
                            break;
                        case 7:
                            Console.WriteLine("Enter New Zip :");
                            item.Value.Zip = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 8:
                            Console.WriteLine("Enter New Phone Number :");
                            item.Value.PhoneNumber = Convert.ToInt64(Console.ReadLine());
                            break;
                    }
                }

            }
            if (flag == false)
                Console.WriteLine("not exits");
        }
        public void DeleteContact(string name, string addressbookName)
        {
            if (addressBookDictionary[addressbookName].addressBook.ContainsKey(name))
            {
                addressBookDictionary[addressbookName].addressBook.Remove(name);
                Console.WriteLine("\nDeleted Succesfully.\n");
            }
            else
            {
                Console.WriteLine("\nNot Found, Try Again.\n");
            }
        }
        public void AddAddressBook(string addressbookName)
        {
            AddressBookBuilder addressBook = new AddressBookBuilder();
            addressBookDictionary.Add(addressbookName, addressBook);
            Console.WriteLine("AddressBook Created.");
        }
        public Dictionary<string, AddressBookBuilder> GetAddressBook()
        {
            return addressBookDictionary;
        }
        public bool CheckFor_Duplicate(Person c, string addressbookName)
        {

            List<Person> book = GetListOfDictionaryValues(addressbookName);
            if (book.Any(b => b.Equals(c)))
            {
                Console.WriteLine("Name already Exists.");
                return true;
            }
            return false;
        }
        public void SearchPersonByCity(string city)
        {
            foreach (AddressBookBuilder addressbookobj in addressBookDictionary.Values)
            {
                CreateCityDictionary();
                List<Person> contactList = GetListOfDictionaryKeys(addressbookobj.cityDictionary);
                foreach (Person contact in contactList.FindAll(c => c.City.Equals(city)).ToList())
                {
                    Console.WriteLine(contact.ToString());
                }
            }
        }
        public void SearchPersonByState(string state)
        {
            foreach (AddressBookBuilder addressbookobj in addressBookDictionary.Values)
            {
                CreateStateDictionary();
                List<Person> contactList = GetListOfDictionaryKeys(addressbookobj.stateDictionary);
                foreach (Person contact in contactList.FindAll(c => c.State.Equals(state)).ToList())
                {
                    Console.WriteLine(contact.ToString());
                }
            }
        }
        public void CreateCityDictionary()
        {
            foreach (AddressBookBuilder addressBookObj in addressBookDictionary.Values)
            {
                foreach (Person contact in addressBookObj.addressBook.Values)
                {
                    addressBookObj.cityDictionary.TryAdd(contact, contact.City);
                }
            }
        }
        public void CreateStateDictionary()
        {
            foreach (AddressBookBuilder addressBookObj in addressBookDictionary.Values)
            {
                foreach (Person contact in addressBookObj.addressBook.Values)
                {
                    addressBookObj.stateDictionary.TryAdd(contact, contact.State);
                }
            }
        }

        public List<Person> GetListOfDictionaryValues(string bookName)
        {
            List<Person> book = new List<Person>();
            foreach (var value in addressBookDictionary[bookName].addressBook.Values)
            {
                book.Add(value);
            }
            return book;
        }
        public List<Person> GetListOfDictionaryKeys(Dictionary<Person, string> d)
        {
            List<Person> book = new List<Person>();
            foreach (var value in d.Keys)
            {
                book.Add(value);
            }
            return book;
        }
        public void GetCountByCityOrState(string addressbookName)
        {
            int cityCount = 0;
            int stateCount = 0;
            Console.WriteLine("Enter city or state :");
            string cityNameOrStateName = Console.ReadLine();
            foreach (KeyValuePair<string, Person> item in addressBookDictionary[addressbookName].addressBook)
            {
                if (cityNameOrStateName.Equals(item.Value.City))
                    cityCount++;
                if (cityNameOrStateName.Equals(item.Value.State))
                    stateCount++;

            }
            Console.WriteLine("The count of pesron by city is : " + cityCount + "and by state is : " + stateCount);

        }
        public void SortEntryByName()
        {
            foreach (AddressBookBuilder item in addressBookDictionary.Values)
            {
                List<string> list = item.addressBook.Keys.ToList();
                list.Sort();
                foreach (var name in list)
                {
                    Console.WriteLine(item.addressBook[name].ToString());
                }
            }
        }
    }
}
