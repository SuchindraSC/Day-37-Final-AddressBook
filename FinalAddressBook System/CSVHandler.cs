using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FinalAddressBook_System
{
    class CSVHandler
    {
        string importFilePath = @"C:\Users\suchi\Documents\BridgeLbaz Assignments\Day 37 Classwork & Assignments\FinalAddressBook System\FinalAddressBook System\Address.txt";
        string exportFilePath = @"C:\Users\suchi\Documents\BridgeLbaz Assignments\Day 37 Classwork & Assignments\FinalAddressBook System\FinalAddressBook System\Address.csv";
        public void WriteToCsv(Dictionary<string, AddressBookBuilder> addressbookDictionary)
        {
            using (StreamReader reader = new StreamReader(importFilePath))
            {
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var Record = csv.GetRecords<Person>().ToList();
                    using (StreamWriter writer = new StreamWriter(exportFilePath))
                    {
                        using (CsvWriter csvExport = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            foreach (AddressBookBuilder item in addressbookDictionary.Values)
                            {
                                csvExport.WriteRecords(Record);
                            }
                        }
                    }
                }
            }
        }
        public void ReadFromCSV()
        {
            using (StreamReader reader = new StreamReader(exportFilePath))
            {
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Console.WriteLine("Below are Contents of CSV File");
                    var contactRecord = csv.GetRecords<Person>().ToList();
                    foreach (Person contact in contactRecord)
                    {
                        Console.WriteLine(contact.ToString());
                    }
                }
            }
        }
    }
}
