using System;
using System.Collections.Generic;

namespace FinalAddressBook_System
{
    class Program
    {
        static void Main(string[] args)
        {
            int option, option1;
            string addressbookName = "Person";
            AddressBookBuilder addressBook = new AddressBookBuilder();

            Console.WriteLine("Enter Your Option: \n1. Write To Default Address Book");
            Console.WriteLine("2. To Add  New Address Book");
            option1 = Convert.ToInt32(Console.ReadLine());
            switch (option1)
            {
                case 1:
                    Console.WriteLine("Address Book Name Is Defaulty Taken As 'Person'");
                    addressBook.AddAddressBook(addressbookName);
                    break;
                case 2:
                    Console.WriteLine("Enter The Name Of New Addressbook You want to create : ");
                    addressbookName = Console.ReadLine();
                    addressBook.AddAddressBook(addressbookName);
                    break;
            }
            do
            {
                Console.WriteLine("You Are Working On {0} Addressbook ", addressbookName);
                Console.WriteLine("Enter your option :");
                Console.WriteLine("1. Add new contact ");
                Console.WriteLine("2. Display the contacts");
                Console.WriteLine("3. Edit  contacts");
                Console.WriteLine("4. Delete  contacts");
                Console.WriteLine("5. Add new addressbook");
                Console.WriteLine("6. Switch Addressbook");
                Console.WriteLine("7. Search person in a city or State");
                Console.WriteLine("8. Get count of  persons by city or State");
                Console.WriteLine("9. Sort Entries by Person name");
                Console.WriteLine("10.Read or write addressbook contacts using File IO");
                Console.WriteLine("11.Read or write addressbook contacts using CSV file");
                Console.WriteLine("12.Read or write addressbook contacts using Json file");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter First Name :");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter Last Name :");
                        string lastName = Console.ReadLine();
                        Person duplicateCheck = new Person(firstName, lastName, null, null, null, null, 0, 0);
                        if (addressBook.CheckFor_Duplicate(duplicateCheck, addressbookName))
                        {
                            break;
                        }
                        Console.WriteLine("Enter Address :");
                        string address = Console.ReadLine();
                        Console.WriteLine("Enter City :");
                        string city = Console.ReadLine();
                        Console.WriteLine("Enter State :");
                        string state = Console.ReadLine();
                        Console.WriteLine("Enter Email :");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter Zip :");
                        int zip = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Phone Number :");
                        long phoneNumber = Convert.ToInt64(Console.ReadLine());
                        addressBook.AddContact(firstName, lastName, address, city, state, email, zip, phoneNumber, addressbookName);
                        break;
                    case 2:
                        addressBook.DisplayContact(addressbookName);
                        break;
                    case 3:
                        Console.WriteLine("Enter Full Name of contact which to be edited");
                        string NameToEdit = Console.ReadLine();
                        addressBook.EditContact(NameToEdit, addressbookName);
                        break;

                    case 4:
                        Console.WriteLine("Enter Full Name of contact which to be deleted");
                        string NameToDelete = Console.ReadLine();
                        addressBook.DeleteContact(NameToDelete, addressbookName);
                        break;

                    case 5:
                        Console.WriteLine("Enter Name For New AddressBook");
                        string newAddressBook = Console.ReadLine();
                        addressBook.AddAddressBook(newAddressBook);
                        Console.WriteLine("Would you like to Switch to " + newAddressBook);
                        Console.WriteLine("1.Yes \n2.No");
                        int c = Convert.ToInt32(Console.ReadLine());
                        if (c == 1)
                        {
                            addressbookName = newAddressBook;
                        }
                        break;
                    case 6:
                        Console.WriteLine("Enter Name Of AddressBook From Below List");
                        foreach (KeyValuePair<string, AddressBookBuilder> item in addressBook.GetAddressBook())
                        {
                            Console.WriteLine(item.Key);
                        }
                        while (true)
                        {
                            addressbookName = Console.ReadLine();
                            if (addressBook.GetAddressBook().ContainsKey(addressbookName))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("No such AddressBook found. Try Again.");
                            }
                        }
                        break;
                    case 7:
                        Console.WriteLine("Would You Like To \n1.Search by city \n2.Search by state");
                        int opt = Convert.ToInt32(Console.ReadLine());
                        switch (opt)
                        {
                            case 1:
                                Console.WriteLine("Enter name of city :");
                                addressBook.SearchPersonByCity(Console.ReadLine());
                                break;
                            case 2:
                                Console.WriteLine("Enter name of state :");
                                addressBook.SearchPersonByState(Console.ReadLine());
                                break;
                            default:
                                Console.WriteLine("Invalid Input.Enter 1 or 2");
                                break;
                        }
                        break;
                    case 8:
                        addressBook.GetCountByCityOrState(addressbookName);
                        break;
                    case 9:
                        addressBook.SortEntryByName();
                        break;
                    case 10:
                        ReadandWriteFileIO fileIO = new ReadandWriteFileIO();
                        fileIO.WriteToFile(addressBook.addressBookDictionary);
                        fileIO.ReadFromFile();
                        break;
                    case 11:
                        CSVHandler handler = new CSVHandler();
                        handler.WriteToCsv(addressBook.addressBookDictionary);
                        handler.ReadFromCSV();
                        break;
                    case 12:
                        ReadandWriteFileJSon json = new ReadandWriteFileJSon();
                        json.WriteToFile(addressBook.addressBookDictionary);
                        json.ReadFromFile();
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        break;
                }
                Console.WriteLine("Do You Wish To Continue?");
                Console.WriteLine("Press 1 If Yes");
                Console.WriteLine("Press 0 To Exit");
                option = Convert.ToInt32(Console.ReadLine());
            }
            while (option != 0);

        }
    }
}
