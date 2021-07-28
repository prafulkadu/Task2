using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data;
using ContactWebApi.Models;
using System.Data.Entity.Validation;

namespace ContactWebApi.Controllers
{
    public class ContactController : ApiController
    {
        // GET: api/Contact
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Contact/name
        public IHttpActionResult Get(string name)
        {
            try
            {
                IList<ContactViewModel> contact = null;
                using (var x = new ContactsEntities())
                {
                    contact = x.contacts.Where(a => a.firstName == name).Select(c => new ContactViewModel()
                    {
                        FirstName = c.firstName,
                        LastName = c.lastName,
                        Dob = c.dob,
                        Email = c.email,
                        Phone = c.contact1
                    }
                    ).ToList<ContactViewModel>();
                }
                if (contact.Count == 0)
                    return NotFound();
                else
                    return Ok(contact);
            }
            catch (DbEntityValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Contact
        public IHttpActionResult Post(ContactViewModel contact)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid input");
                using (var x = new ContactsEntities())
                {
                    x.contacts.Add(new contact()
                    {
                        firstName = contact.FirstName,
                        lastName = contact.LastName,
                        dob = contact.Dob,
                        email = contact.Email,
                        contact1 = contact.Phone
                    });
                    x.SaveChanges();
                }
                return Ok();
            }
            catch (DbEntityValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Contact/5
        public IHttpActionResult Put(ContactViewModel contact)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid input");
                using (var x = new ContactsEntities())
                {
                    var ifExists = x.contacts.Where(w => w.firstName == contact.FirstName).FirstOrDefault<contact>();
                    if (ifExists != null)
                    {
                        ifExists.lastName = contact.LastName;
                        ifExists.dob = contact.Dob;
                        ifExists.email = contact.Email;
                        ifExists.contact1 = contact.Phone;
                        x.SaveChanges();
                    }
                    else
                        return NotFound();
                }
                return Ok();
            }
            catch (DbEntityValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE: api/Contact/5
        public void Delete(int id)
        {
        }
    }
}
