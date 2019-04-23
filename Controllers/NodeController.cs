using Microsoft.AspNetCore.Mvc;

namespace RiggVar.FR
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        // AngularID = 1
        // AngularName: inputWireConnect() : Observable<string>
        // GET: api/input-wire-connect
        // GET: api/Node/InputWireConnect
        [HttpGet]
        public string InputWireConnect()
        {
            Dummy.InputConnected = true;
            return "input-wire-connect (not implemented)";
        }

        // AngularID = 2
        // AngularName: inputWireDisconnect() : Observable<string>
        // GET: api/input-wire-disconnect
        // GET: api/Node/InputWireDisconnect
        [HttpGet]
        public string InputWireDisconnect()
        {
            Dummy.InputConnected = false;
            return "input-wire-disconnected (not implemented)";
        }

        // AngularID = 3
        // AngularName: outputWireConnect() : Observable<string>
        // GET: api/output-wire-connect
        // GET: api/Node/OutputWireConnect
        [HttpGet]
        public string OutputWireConnect()
        {
            Dummy.OutputConnected = true;
            return "output-wire-connect (not implemented)";
        }

        // AngularID = 4
        // AngularName: outputWireDisconnect() : Observable<string>
        // GET: api/output-wire-disconnect
        // GET: api/Node/OutputWireDisconnect
        [HttpGet]
        public string OutputWireDisconnect()
        {
            Dummy.OutputConnected = false;
            return "output-wire-disconnect (not implemented)";
        }

        // AngularID = 5
        // AngularName: queryParams() : Observable<EventParams>
        // GET: api/query-params
        // GET: api/Node/QueryParams
        [HttpGet]
        public IActionResult QueryParams()
        {
            EventParamJson o = Dummy.EventParams;

            ApiEventParams ep = new ApiEventParams
            {
                raceCount = o.RaceCount,
                itCount = o.ITCount,
                startlistCount = o.StartlistCount
            };
            return new JsonResult(ep);
        }

        // AngularID = 12
        // AngularName = requestNetto(): Observable<string>
        // GET: api/netto
        // GET: api/Delphi/Netto
        [HttpGet]
        public string Netto()
        {
            if (Dummy.InputNetto == string.Empty)
            {
                return "netto is empty";
            }
            return Dummy.InputNetto;
        }

        // AngularID = 13
        // AngularName = requestInputNetto(): Observable<string>
        // GET: api/input-netto
        // GET: api/Delphi/InputNetto
        [HttpGet]
        public string InputNetto()
        {
            if (Dummy.InputNetto == string.Empty)
            {
                return "input-netto is empty";
            }
            return Dummy.InputNetto;
        }

        // AngularID = 14
        // AngularName = requestOutputNetto(): Observable<string>
        // GET: api/output-netto
        // GET: api/Delphi/OutputNetto
        [HttpGet]
        public string OutputNetto()
        {
            if (Dummy.OutputNetto == string.Empty)
            {
                return "output-netto is empty";
            }
            return Dummy.OutputNetto;
        }

        // AngularID = 51
        // AngularName = getConnectionStatus() : Observable<ConnectionStatus>
        // GET: api/get-input-connection-status
        // GET: api/Node/InputConnectionStatus
        [HttpGet]
        public IActionResult InputConnectionStatus()
        {
            ApiConnectionStatus cs = new ApiConnectionStatus
            {
                connected = Dummy.InputConnected
            };
            return new JsonResult(cs);
        }

        // AngularID = 52
        // AngularName = getOuputConnectionStatus() : Observable<ConnectionStatus>
        // GET: api/get-output-connection-status
        // GET: api/Node/OutputConnectionStatus
        [HttpGet]
        public IActionResult OutputConnectionStatus()
        {
            ApiConnectionStatus cs = new ApiConnectionStatus
            {
                connected = Dummy.OutputConnected
            };
            return new JsonResult(cs);
        }

    }
}