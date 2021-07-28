using System;
using ContactWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Web.Http.Results;
using ContactWebApi.Models;
using System.Collections.Generic;

namespace ContactWebApi.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        [TestMethod]
        public void GetByName()
        {
            ContactController controller = new ContactController();
            var result = controller.Get("praful");
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IList<ContactViewModel>>));
        }

        [TestMethod]
        public void Post()
        {
            ContactController controller = new ContactController();
            ContactViewModel contact = new ContactViewModel();
            contact.FirstName = "ftest1";     //change name to post
            contact.LastName = "ltest1";
            contact.Dob = DateTime.Now;
            contact.Email = "test1@gmail.com";
            contact.Phone = "2134509876";
            var result = controller.Post(contact);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Put()
        {
            ContactController controller = new ContactController();
            ContactViewModel contact = new ContactViewModel();
            contact.FirstName = "ftest"; 
            //change anything below
            contact.LastName = "ltest";
            contact.Dob = DateTime.Now;
            contact.Email = "test@gmail.com";
            contact.Phone = "2134509876";
            var result = controller.Put(contact);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
