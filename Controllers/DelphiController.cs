using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace RiggVar.FR
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DelphiController : ControllerBase
    {

        [HttpGet]
        public string[] DashUrlInfo()
        {
            TStringList sl = new TStringList();
            Dummy.ApiCol.GetApiList(sl, ApiControllerEnum.Delphi, false);
            return sl.ToArray();
        }

        [HttpGet]
        public string[] AspNetUrlInfo()
        {
            TStringList sl = new TStringList();
            Dummy.ApiCol.GetApiList(sl, ApiControllerEnum.Delphi, true);
            return sl.ToArray();
        }

        // AngularID = 6
        // AngularName = manageClear(): Observable<string>
        // GET: api/manage-clear
        // GET: api/Delphi/ManageClear
        [HttpGet]
        public string ManageClear()
        {
            return "manage-clear";
        }

        // AngularID = 7
        // AngularName = manageClearRace(race: number): Observable<string>
        // GET: api/manage-clear-race
        // GET: api/Delphi/ManageClearRace
        [HttpGet]
        public string ManageClearRace()
        {
            Request.Query.TryGetValue("race", out StringValues someInt);
            int race = int.Parse(someInt);
            return @"manage-clear-race for race = {race}";
        }

        // AngularID = 8
        // AngularName = manageClearGoBackToRace(race: number): Observable<string>
        // GET: api/manage-clear-go-back-to-race
        // GET: api/Delphi/ManageClearRace
        [HttpGet]
        public string ManageClearGoBackToRace()
        {
            Request.Query.TryGetValue("race", out StringValues someInt);
            int race = int.Parse(someInt);
            return @"manage-clear-go-back-to-race for race = {race}";
        }

        // AngularID = 9
        // AngularName = manageClearTimepoint(race: number, it: number): Observable<string>
        // GET: api/manage-clear-timepoint
        // GET: api/Delphi/ManageClearTimepoint
        [HttpGet]
        public string ManageClearTimepoint()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it= int.Parse(t);

            return @"manage-clear-timepoint for race = {race}, it = {it}";
        }

        // AngularID = 10
        // AngularName = sendTime(race: number, it: number, bib: number): Observable<string>
        // GET: api/send-time
        // GET: api/Delphi/SendTime
        [HttpGet]
        public string SendTime()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            return @"send-time for race = {race}, it = {it}, bib = {bib}";
        }

        // AngularID = 11
        // AngularName = sendTime(value: string): Observable<string>
        // GET: api/send-msg
        // GET: api/Delphi/SendMsg
        [HttpGet]
        public string SendMsg()
        {
            string msg = Request.Query["value"];

            return @"send-msg : {msg}";
        }

        // AngularID = 37
        // AngularName = getEventTableJson(mode: number): Observable<string>
        // GET: api/get-event-table-json
        // GET: api/Delphi/GetEventTableJson
        [HttpGet]
        public string GetEventTableJson()
        {
            Request.Query.TryGetValue("mode", out StringValues m);
            int mode = int.Parse(m);

            return @"get-event-table-json, mode = {mode}";
        }

        // AngularID = 38
        // AngularName = getFinishTableJson(): Observable<string>
        // GET: api/get-finish-table-json
        // GET: api/Delphi/GetFinishTableJson
        [HttpGet]
        public string GetFinishTableJson()
        {
            return @"get-finish-table-json";
        }

        // AngularID = 39
        // AngularName = getPointsTableJson(): Observable<string>
        // GET: api/get-points-table-json
        // GET: api/Delphi/GetPointsTableJson
        [HttpGet]
        public string GetPointsTableJson()
        {
            return @"get-points-table-json";
        }

        // AngularID = 40
        // AngularName = getNarrowRaceTableJson(race: number, it: number): Observable<string>
        // GET: api/get-narrow-race-table-json
        // GET: api/Delphi/GetNarrowRaceTableJson
        [HttpGet]
        public string GetNarrowRaceTableJson()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            return @"get-narrow-race-table-json, race = {race}, it = {it}";
        }

        // AngularID = 41
        // AngularName = getWideRaceTableJson(race: number, it: number): Observable<string>
        // GET: api/get-wide-race-table-json
        // GET: api/Delphi/GetWideRaceTableJson
        [HttpGet]
        public string GetWideRaceTableJson()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            return @"get-wide-race-table-json, race = {race}, it = {it}";
        }

        // AngularID = 42
        // AngularName = getRaceTableJson(): Observable<string>
        // GET: api/get-race-table-json
        // GET: api/Delphi/GetRaceTableJson
        [HttpGet]
        public string GetRaceTableJson()
        {
            return @"get-race-table-json";
        }

        // AngularID = 43
        // AngularName = getRaceTableHtml(): Observable<string>
        // GET: api/get-race-table-html
        // GET: api/Delphi/GetRaceTableHtml
        [HttpGet]
        public string GetRaceTableHtml()
        {
            return @"get-race-table-html";
        }

        // AngularID = 44
        // AngularName = getTime(race: number, it: number, bib: number): Observable<string>
        // GET: api/do-time
        // GET: api/Delphi/DoTime
        [HttpGet]
        public string DoTime()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            return @"R{race}.Bib{bib}.IT{it} = {Dummy.Time}";
        }

        // AngularID = 45
        // AngularName = getFinish(race: number, bib: number): Observable<string>
        // GET: api/do-finish
        // GET: api/Delphi/DoFinish
        [HttpGet]
        public string DoFinish()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            return @"R{race}.Bib{bib}.FT = {Dummy.Time}";
        }

        // AngularID = 46
        // AngularName = getTimeAndTable(race: number, it: number, bib: number): Observable<string>
        // GET: api/do-time-for-table
        // GET: api/Delphi/DoTimeForTable
        [HttpGet]
        public string DoTimeForTable()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            return @"do-time-for-table : R{race}.Bib{bib}.IT{it}";
        }

        // AngularID = 47
        // AngularName = getFinishAndTable(race: number, bib: number): Observable<string>
        // GET: api/do-finish-for-table
        // GET: api/Delphi/DoFinishForTable
        [HttpGet]
        public string DoFinishForTable()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            return @"do-finish-for-table : R{race}.Bib{bib}.FT";
        }

        // AngularID = 48
        // AngularName = getTimingEventForTable(race: number, it: number, bib: number, option: number, mode: number): Observable<string>
        // GET: api/do-timing-event-for-table
        // GET: api/Delphi/DoTimingEventForTable
        [HttpGet]
        public string DoTimingEventForTable()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            Request.Query.TryGetValue("option", out StringValues o);
            int option = int.Parse(o);

            Request.Query.TryGetValue("mode", out StringValues m);
            int mode = int.Parse(m);

            return @"do-timing-event-for-table : R{race}.Bib{bib}.IT{it} option={option}, mode={mode}";
        }

        // AngularID = 49
        // AngularName = getTimingEvent(race: number, it: number, bib: number, option: number): Observable<string>
        // GET: api/do-timing-event
        // GET: api/Delphi/DoTimingEvent
        [HttpGet]
        public string DoTimingEvent()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            Request.Query.TryGetValue("option", out StringValues o);
            int option = int.Parse(o);

            return @"do-timing-event : R{race}.Bib{bib}.IT{it} option={option}";
        }

        // AngularID = 50
        // AngularName = getTimingEvent(race: number, it: number, bib: number): Observable<string>
        // GET: api/do-timing-event-quick
        // GET: api/Delphi/DoTimingEventQuick
        [HttpGet]
        public string DoTimingEventQuick()
        {
            Request.Query.TryGetValue("race", out StringValues r);
            int race = int.Parse(r);

            Request.Query.TryGetValue("it", out StringValues t);
            int it = int.Parse(t);

            Request.Query.TryGetValue("bib", out StringValues b);
            int bib = int.Parse(b);

            return @"do-timing-event-quick : R{race}.Bib{bib}.IT{it}";
        }

    }
}