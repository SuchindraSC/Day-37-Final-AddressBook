using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookSQL;
using System;
using System.Collections.Generic;

namespace AddressBookSQLMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenQuery_WhenRetrieve_ShouldReturnNumberOfRowsRetrieved()
        {
            int expectedResult = 6;
            addressbooksql database = new addressbooksql();
            int result = database.GetPersonDetailsfromDatabase();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GivenQuery_whenUpdate_ShouldUpdateContactInDB()
        {
            bool expectedResult = true;
            addressbooksql database = new addressbooksql();
            AddressBookModel model = new AddressBookModel()
            {
                firstname = "Suchindra",
                lastname = "Chitnis",
                phone = "9087456321",
                emailid = "qwerty.uio@gmail.com",
                zip = 400001,
            };
            bool result = database.UpdateContact(model);
            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void GivenDate_ShouldReturnRecordsInAParticularPeriod()
        {
            bool expectedResult = true;
            addressbooksql database = new addressbooksql();
            bool result = database.RetriveContactInParticularPeriod();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GivenQuery_WhenRetrieveByCityOrState_ShouldRetrievContactAndReturnNoOfCounts()
        {
            int expectedResult = 3;
            addressbooksql database = new addressbooksql();
            AddressBookModel model = new AddressBookModel()
            {
                state = "Assam"
            };
            int result = database.RetriveContactByCityOrState(model);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void GivenQuery_WhenInsert_ShouldAddNewContactToDB()
        {

            addressbooksql database = new addressbooksql();
            List<AddressBookDetail> model = new List<AddressBookDetail>();

            model.Add(new AddressBookDetail(firstname: "Suchindra", lastname: "Chitnis", phone: "9999999999", email: "qwerty.uio@gmail.com", city: "Mumbai", bookid: 1, personid: 6, zip: 400001, dateadded: new DateTime(2014, 01, 01)));
            model.Add(new AddressBookDetail(firstname: "Adarsh", lastname: "Pandith", phone: "8888888888", email: "asdfgh.jkl@gmail.com", city: "Bangalore", bookid: 2, personid: 7, zip: 560070, dateadded: new DateTime(2016, 08, 01)));
            model.Add(new AddressBookDetail(firstname: "Tejas", lastname: "Sharma", phone: "7777777777", email: "zxcvb.nm@gmail.com", city: "Chennai", bookid: 1, personid: 8, zip: 587090, dateadded: new DateTime(2017, 12, 01)));
            model.Add(new AddressBookDetail(firstname: "Mahesh", lastname: "Shetty", phone: "9085671432", email: "qazwsx.cde@gmail.com", city: "Hyderabad", bookid: 2, personid: 9, zip: 523400, dateadded: new DateTime(2018, 04, 15)));
            model.Add(new AddressBookDetail(firstname: "Yogesh", lastname: "Yadav", phone: "8079164253", email: "awdry.jil@gmail.com", city: "Pune", bookid: 1, personid: 10, zip: 478050, dateadded: new DateTime(2019, 11, 25)));


            DateTime startDateTime = DateTime.Now;
            database.AddNewContactWithoutThread(model);
            DateTime stopDateTime = DateTime.Now;
            Console.WriteLine("Duration without thread: " + (stopDateTime - startDateTime));


            DateTime startDateTimeThread = DateTime.Now;
            database.AddNewContactWithThread(model);
            DateTime stopDateTimeThread = DateTime.Now;
            Console.WriteLine("Duration with thread: " + (stopDateTimeThread - startDateTimeThread));
        }
    }
}
