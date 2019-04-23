using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace RiggVar.FR
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {

        // POST: api/Slot/2 (EventDataJson)
        // POST: api/Slot/3 (RaceDataJson)
        [HttpPost("{id}", Name = "PostToSlot")]
        public IActionResult Post(int id)
        {
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                string json = stream.ReadToEnd();
                switch (id)
                {
                    case 2: Dummy.slot2 = json; break;
                    case 3: Dummy.slot3 = json; break;
                }
            }
            return new JsonResult(Dummy.rvOK);
        }

        // POST: api/ud/3
        // POST: api/Slot/3
        [HttpGet("{id}", Name = "GetFromSlot")]
        public IActionResult Get(int id)
        {
            string json = "";
            switch (id)
            {
                case 2: json = Dummy.slot2; break;
                case 3: json = Dummy.slot3; break;
            }
            try
            {
                object o = JsonConvert.DeserializeObject(json);
                return new JsonResult(o);
            }
            catch (Exception)
            {
                return new JsonResult(null);
            }
        }

        // GET: api/Slot/UD/2
        // GET: api/Slot/UD/3
        [HttpGet("[action]/{id}")]
        public string UD(int id)
        {
            string json = "";
            switch (id)
            {
                case 2: json = Dummy.slot2; break;
                case 3: json = Dummy.slot3; break;
                default: json = ""; break;
            }
            return json;
        }

    }
}