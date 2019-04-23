using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.IO;

namespace RiggVar.FR
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        // AngularID = 16
        // AngularName = pullEventData() : Observable<string>
        // GET: api/event-data
        // GET: api/Data/EventData
        [HttpGet]
        public string EventData()
        {
            return Dummy.EventData;
        }

        // AngularID = 17
        // AngularName = pullEventDataJson(): Observable<EventDataJson>
        // GET: api/event-data-json
        // GET: api/Data/EventDataJson
        [HttpGet]
        public IActionResult EventDataJson()
        {
            return new JsonResult(Dummy.EventDataJson);
        }

        // AngularID = 18
        // AngularName = pullRaceDataJson(): Observable<RaceDataJson>
        // GET: api/race-data-json
        // GET: api/Data/RaceDataJson
        [HttpGet]
        public IActionResult RaceDataJson()
        {
            return new JsonResult(Dummy.RaceDataJson);
        }

        // AngularID = ?
        // AngularName = pullRaceDataJsonForRace(int race): Observable<RaceDataJson>
        // GET: api/race-data-json-for-race
        // GET: api/Data/RaceDataJson/2
        [HttpGet("{id}", Name = "GetRaceDataJsonForRace")]
        public IActionResult GetRaceDataJsonForRace()
        {
            var queryString = Request.Query;
            queryString.TryGetValue("race", out StringValues someInt);
            int race = int.Parse(someInt);

            return new JsonResult(Dummy.RaceDataJson);
        }

        // AngularID = 19
        // AngularName = pushEventData(value: string): Observable<ApiRetValue>
        // POST: api/event-data
        // POST: api/Data/PushEventData
        [HttpPost]
        public IActionResult PushEventData()
        {
            Dummy.EventData = Request.Body.ToString();
            return new JsonResult(Dummy.rvOK);
        }

        // AngularID = 20
        // AngularName = pushEventDataJson(value: EventDataJson): Observable<ApiRetValue>
        // POST: api/event-data-json
        // POST: api/Data/PushEventDataJson
        [HttpPost]
        public IActionResult PushEventDataJson()
        {
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                string json = stream.ReadToEnd();
                Dummy.EventDataJson = JsonConvert.DeserializeObject<EventDataJson>(json);
            }
            return new JsonResult(Dummy.rvOK);
        }

        // AngularID = 21
        // AngularName = pushRaceDataJsonForRace(race: number, value: RaceDataJson): Observable<ApiRetValue>
        // POST: api/race-data-json
        // POST: api/Data/PushRaceDataJsonForRace
        [HttpPost]
        public IActionResult PushRaceDataJsonForRace()
        {
            var queryString = Request.Query;
            queryString.TryGetValue("race", out StringValues someInt);
            int race = int.Parse(someInt);

            using (StreamReader stream = new StreamReader(Request.Body))
            {
                string json = stream.ReadToEnd();
                Dummy.RaceDataJson = JsonConvert.DeserializeObject<RaceDataJson>(json);
            }
            return new JsonResult(Dummy.rvOK);
        }

        // AngularID = 22
        // AngularName = pushRaceDataJson(value: RaceDataJson): Observable<ApiRetValue>
        // POST: api/rd.json
        // POST: api/Data/PushRaceDataJson
        [HttpPost]
        public IActionResult PushRaceDataJson()
        {
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                string json = stream.ReadToEnd();
                Dummy.RaceDataJson = JsonConvert.DeserializeObject<RaceDataJson>(json);
            }
            return new JsonResult(Dummy.rvOK);
        }

    }
}
