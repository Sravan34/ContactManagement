using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ContactManagementApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        List<Contacts> lstcontacts = new List<Contacts>();
        [HttpGet("GetContacts")]
        public List<Contacts> GetContacts()
        {
            using StreamReader reader = new(@"Contacts.json");
            var json = reader.ReadToEnd();
            var jarray = JObject.Parse(json);
            List<Contacts> contacts = new();
            foreach (var item in jarray["contacts"])
            {
                Contacts contact = new Contacts { id = Convert.ToInt32(((JValue)item["id"]).Value), firstName = (string)((JValue)item["firstName"]).Value, lastName = (string)((JValue)item["lastName"]).Value, email = (string)((JValue)item["email"]) };
                contacts.Add(contact);
            }
            return contacts;
        }
        [HttpGet("GetcontactById")]
        public Contacts GetcontactById(int id)
        {
            using StreamReader reader = new(@"Contacts.json");
            var json = reader.ReadToEnd();
            var jarray = JObject.Parse(json);
            Contacts contact = new Contacts();
            foreach (var item in jarray["contacts"])
            {
                if (item["id"].ToString() == id.ToString())
                {
                    contact.id = Convert.ToInt32(((JValue)item["id"]).Value);
                    contact.firstName = (string)((JValue)item["firstName"]).Value;
                    contact.lastName = (string)((JValue)item["lastName"]).Value;
                    contact.email = (string)((JValue)item["email"]).Value;
                }

            }
            return contact;
        }
        [HttpPost("saveContact")]
        public Contacts saveContact(Contacts contact)
        {
            StringBuilder strJson = new StringBuilder();
            //using StreamReader reader = new(@"Contacts.json");
            //var json = reader.ReadToEnd();
            var json = System.IO.File.ReadAllText(@"Contacts.json");
            var jarray = JObject.Parse(json);
            List<Contacts> contacts = new();
            var nextId = jarray["contacts"].AsEnumerable().Max(x => x["id"]);
            int Id = Convert.ToInt32(((JValue)nextId).Value) + 1;
            Contacts contact1 = new Contacts { id = Id, firstName = contact.firstName, lastName = contact.lastName, email = contact.email };
            contacts.Add(contact1);
            var contactNew = JsonConvert.SerializeObject(contact1);
            
            string newJson = json.Replace("]",(','+contactNew + ']'));
            System.IO.File.WriteAllText(@"Contacts.json",newJson);
            return contact1;
        }

    }

}
