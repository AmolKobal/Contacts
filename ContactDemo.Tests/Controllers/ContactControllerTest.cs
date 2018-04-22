using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactDemo.Controllers;
using ContactDemo.Models;
using System.Web.Mvc;
using System.Linq;

namespace ContactDemo.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ContactController controller = new ContactController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            ContactController controller = new ContactController();

            ContactRepository _contact = new ContactRepository();
            Contact contact = _contact.GetAllContacts().First();

            // Act
            ViewResult result = controller.Details(contact.ID) as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            ContactController controller = new ContactController();

            ContactRepository _contact = new ContactRepository();
            Contact contact = _contact.GetAllContacts().First();
            contact.PhoneNumber = "123456789";
            int id = contact.ID;

            // Act
            ViewResult result = controller.Edit(contact) as ViewResult;

            Contact editcontact = _contact.GetAllContacts().First(model => model.ID == id);

            // Assert
            Assert.AreEqual(contact.PhoneNumber, editcontact.PhoneNumber);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ContactController controller = new ContactController();

            ContactRepository _contact = new ContactRepository();
            Contact contact = _contact.GetAllContacts().First();

            // Act
            ViewResult result = controller.Edit(contact) as ViewResult;

            Contact deleteContact = _contact.GetAllContacts().First(model => model.ID == contact.ID);

            // Assert
            Assert.IsNull(deleteContact);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ContactController controller = new ContactController();

            ContactRepository _contact = new ContactRepository();
            Contact contact = new Contact
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                EMail = "first.lsat@test.com",
                PhoneNumber = "123456789",
            };

            // Act
            ViewResult result = controller.Create(contact) as ViewResult;

            Contact createcontact = _contact.GetAllContacts().First(model => model.FirstName == contact.FirstName);

            // Assert
            Assert.IsNotNull(createcontact, "Failed to add new Contact");

            // Assert
            Assert.AreEqual(contact.FirstName, createcontact.FirstName);
        }
    }
}
