using entity_framework_tutorial_web_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace entity_framework_tutorial_web_api.Controllers
{
    [Route("api/[controller]")] // Use this to find right controller when making a call
    [ApiController] // Indicates that this and derived types areused to serve HTTP API requests
    public class ContactController : ControllerBase
    {
        private List<Contact> _contacts = new List<Contact>
        {
            new Contact { ID = 1, FirstName = "Peter", LastName = "Parker", Nickname = "Spiderman", Place = "New York City" },
            new Contact { ID = 2, FirstName = "Tony", LastName = "Stark", Nickname = "Iron Man", Place = "Long Island" }
        };

        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get() // was orginally IEnumerable<string>
        {
            // Get from http://localhost:5134/api/contact
            return Ok(_contacts); // Ok() called by default.
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            Contact? result = _contacts.FirstOrDefault(c => c.ID == id);
            if (result == null)
            {
                Console.WriteLine("Null contact");
                return NotFound(new { Message = $"No contact with an ID of {id} was found." });
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult<IEnumerable<Contact>> Post(Contact newContact) //[FromBody] string value)
        {
            // Post to the URL with a JSON body of:
            //{
            //    "id": 3,
            //    "firstName": "Bruce",
            //    "lastName": "Wayne",
            //    "nickname": "Batman",
            //    "place": "Gotham City"
            //}
            // DateCreated will be set automatically
            _contacts.Add(newContact);

            // this typically does not return data,
            // but we return the list to show a new contact was added
            return Ok(_contacts);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Contact>> Put(int id, Contact updatedContact) // [FromBody] string value)
        {
            Contact? toUpdate = _contacts.FirstOrDefault(u => u.ID == id);
            if (toUpdate == null)
            {
                return NotFound();
            }
            else
            {
                toUpdate.FirstName = updatedContact.FirstName;
                toUpdate.LastName = updatedContact.LastName;
                toUpdate.Nickname = updatedContact.Nickname;
                toUpdate.Place = updatedContact.Place;

                // this typically does not return data,
                // but we return the list to show a new contact was added
                return Ok(_contacts);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Contact>> Delete(int id)
        {
            Contact? toDelete = _contacts.FirstOrDefault(u => u.ID == id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                _contacts.Remove(toDelete);

                // this typically does not return data,
                // but we return the list to show a new contact was added
                return Ok(_contacts);
            }
        }
    }
}
