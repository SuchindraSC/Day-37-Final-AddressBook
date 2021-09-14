using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace FinalAddressBook_System
{
    class ReadandWriteFileJSon
    {
        string filePath = @"C:\Users\suchi\Documents\BridgeLbaz Assignments\Day 37 Classwork & Assignments\FinalAddressBook System\FinalAddressBook System\Address.json";
        public void WriteToFile(Dictionary<string, AddressBookBuilder> addressBookDictionary)
        {
            foreach (AddressBookBuilder obj in addressBookDictionary.Values)
            {
                foreach (Person contact in obj.addressBook.Values)
                {
                    string json = JsonConvert.SerializeObject(contact);
                    File.WriteAllText(filePath, json);
                }
            }
            Console.WriteLine("\nSuccessfully added to JSON file.");
        }
        public void ReadFromFile()
        {
            Person contact = JsonConvert.DeserializeObject<Person>(File.ReadAllText(filePath));
            Console.WriteLine(contact.ToString());
        }
    }
}
