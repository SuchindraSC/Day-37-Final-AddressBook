using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;

namespace FinalAddressBook_System
{
    class ReadandWriteFileIO
    {
        static string file = @"C:\Users\suchi\Documents\BridgeLbaz Assignments\Day 37 Classwork & Assignments\FinalAddressBook System\FinalAddressBook System\Address.txt";
      
        public void WriteToFile(Dictionary<string, AddressBookBuilder> addressBookDictionary)
        {
            StreamWriter writer = new StreamWriter(file, true);
            foreach (AddressBookBuilder item in addressBookDictionary.Values)
            {
                foreach (Person contact in item.addressBook.Values)
                {
                    writer.WriteLine(contact.ToString());
                }
            }
            Console.WriteLine("\nSuccessfully added to Text file.");
            writer.Close();
        }
        public void ReadFromFile()
        {
            Console.WriteLine(File.ReadAllText(file));
        }
    }
}
