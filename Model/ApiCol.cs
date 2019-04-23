using System.Collections.Generic;
using System.Linq;

namespace RiggVar.FR
{

    /// <summary>
    /// As defined in Delphi App (FR62)
    /// </summary>    
    enum TApiEnum
    {
        api_none,

        api_rd_json,
        api_ed_json,
        api_event_data,

        api_get_input_connection_status,
        api_get_output_connection_status,
        api_input_wire_connect,
        api_input_wire_disconnect,
        api_output_wire_connect,
        api_output_wire_disconnect,

        api_send_msg,
        api_manage_clear,
        api_manage_clear_timepoint,
        api_manage_clear_race,
        api_manage_go_back_to_race,
        api_query_params,

        api_event_data_html,
        api_event_data_json,
        api_race_data_json,
        api_event_menu_json,

        api_get_simple_text,
        api_get_simple_json,

        api_widget_test,
        api_widget_time,
        api_widget_netto,

        api_widget_do_time,
        api_widget_do_finish,
        api_widget_do_time_for_table,
        api_widget_do_finish_for_table,

        api_widget_do_timing_event_quick,
        api_widget_do_timing_event,
        api_widget_do_timing_event_for_table,

        api_widget_get_race_table_html,
        api_widget_get_race_table_json,
        api_widget_get_narrow_race_table_json,
        api_widget_get_wide_race_table_json,

        api_widget_get_event_table_json,
        api_widget_get_finish_table_json,
        api_widget_get_points_table_json
    }

    public enum ApiMethodEnum
    {
        Get,
        Post,
        //GetAndPost
    }

    public enum ApiControllerEnum
    {
        Bridge,
        Data,
        Delphi,
        Node,
        Slot,
        //Local,
        //Remote,
        //Widget,
    }

    public enum ApiReturnValueType
    {
        //Nothing,
        JsonStatus,
        StringStatus,
        String,
        Text,
        //Xml,
        Json,
        JsonText,
    }

    public class ApiGroupItem
    {
        public string ControllerName;
        public IEnumerable<ApiItem> ControllerItems;
    }

    public class ApiItem
    {
        public int AngularID;
        public string AngularName;
        public string AngularCall;
        public string Description;
        public ApiMethodEnum HttpMethod;
        public ApiControllerEnum Controller;
        public ApiReturnValueType ReturnValueType;
        public string UrlString;
        public string UrlStringNet;
        //public string UrlStringDelphi;
        public string Params;
        public string Body;
        public string ImplementationHint;
        public bool DelphiOnly = false;
        public bool NodeOnly = false;
        public bool Available = false;

        public string MethodCaption
        {
            get
            {
                switch (HttpMethod)
                {
                    case ApiMethodEnum.Get: return "Get";
                    case ApiMethodEnum.Post: return "Post";
                    default: return "";
                }
            }
        }

        public string ControllerCaption
        {
            get
            {
                switch (Controller)
                {
                    case ApiControllerEnum.Bridge: return "Bridge";
                    case ApiControllerEnum.Data: return "Data";
                    case ApiControllerEnum.Delphi: return "Delphi";
                    case ApiControllerEnum.Node: return "Node";
                    case ApiControllerEnum.Slot: return "Slot";
                    default: return "";
                }
            }
        }

        public class ApiCollection : List<ApiItem>
        {
            public ApiCollection()
            {
                Init();
            }

            public List<ApiItem> GetItems(ApiControllerEnum c)
            {
                List<ApiItem> cl = new List<ApiItem>();
                foreach (ApiItem cr in this)
                {
                    if (cr.Controller == c)
                    {
                        cl.Add(cr);
                    }
                }
                return cl;
            }

            public IEnumerable<ApiGroupItem> GetLinqList()
            {
                var q = from cr in this
                        orderby cr.Controller, cr.HttpMethod, cr.UrlString
                        group cr by new { cr.ControllerCaption, cr.Controller } into g
                        select new ApiGroupItem
                        {
                            ControllerName = g.Key.ControllerCaption,
                            ControllerItems = from cr2 in g
                                              where cr2.Controller == g.Key.Controller
                                              select cr2
                        };

                return q;
            }

            public List<string> GetDashUrls()
            {
                return GetAllApis(false);
            }

            public List<string> GetAspNetUrls()
            {
                return GetAllApis(true);
            }

            public List<string> GetAllApis(bool wantAspNet = false)
            {
                List<string> sl = new List<string>();
                foreach (ApiControllerEnum c in (ApiControllerEnum[])System.Enum.GetValues(typeof(ApiControllerEnum)))
                {
                    sl.Add(@"{c.ToString()}:");
                    GetApiList(sl, c, wantAspNet);
                    sl.Add("");
                }
                return sl;

            }

            public void GetApiList(List<string> sl, ApiControllerEnum c, bool wantAspNet = false)
            {
                foreach (var cr in this)
                {
                    if (cr.Controller == c)
                    {
                        if (wantAspNet)
                        {
                            sl.Add(cr.UrlStringNet);
                        }
                        else
                        {
                            sl.Add(cr.UrlString);
                        }

                    }

                }
            }

            public void Init()
            {
                InitBridge();
                InitData();
                InitDelphi();
                InitNode();
                InitSlot();
            }

            public void InitNode()
            {

                /**
                * app.get('/api/input-wire-connect', (req, res) => {
                *     //iconn.connect();
                *     dummy.inputConnected = true;
                *     res.send('input connected');
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 1,
                    AngularName = "inputWireConnect(): Observable<string>",
                    AngularCall = "http.get('/api/input-wire-connect', { responseType: 'text' })",
                    Description = "Ask Node-Server to establish tcp-connection to input-socket of desktop-program",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/InputWireConnect",
                    UrlString = "/api/input-wire-connect",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "dummy.inputConnected = true",
                    ReturnValueType = ApiReturnValueType.StringStatus,
                    DelphiOnly = false,
                    Available = true
                });

                /**
                * app.get('/api/input-wire-disconnect', (req, res) => {
                *     //iconn.disconnect();
                *     dummy.inputConnected = false;
                *     res.send('input disconnected');
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 2,
                    AngularName = "inputWireDisconnect(): Observable<string>",
                    AngularCall = "http.get('/api/input-wire-disconnect', { responseType: 'text' })",
                    Description = "Ask Node-Server to close tcp-connection to input-socket of desktop-program",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/InputWireDisconnect",
                    UrlString = "/api/input-wire-disconnect",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "dummy.inputConnected = false",
                    ReturnValueType = ApiReturnValueType.StringStatus,
                    DelphiOnly = false,
                    Available = true
                });

                /**
                * app.get('/api/output-wire-connect', (req, res) => {
                *     //oconn.connect();
                *     dummy.outputConnected = true;
                *     res.send('output connected');
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 3,
                    AngularName = "outputWireConnect(): Observable<string>",
                    AngularCall = "http.get('/api/output-wire-connect', { responseType: 'text' })",
                    Description = "Ask Node-Server to establish tcp-connection to output-socket of desktop-program",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/OutputWireConnect",
                    UrlString = "/api/output-wire-connect",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "dummy.outputConnected = true",
                    ReturnValueType = ApiReturnValueType.StringStatus,
                    DelphiOnly = false,
                    Available = true
                });

                /**
                * app.get('/api/output-wire-disconnect', (req, res) => {
                *     //oconn.disconnect();
                *     dummy.outputConnected = false;
                *     res.send('output disconnected');
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 4,
                    AngularName = "outputWireDisconnect(): Observable<string>",
                    AngularCall = "http.get('/api/output-wire-disconnect', { responseType: 'text' })",
                    Description = "Ask Node-Server to close tcp-connection to output-socket of desktop-program",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/OutputWireDisconnect",
                    UrlString = "/api/output-wire-disconnect",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "dummy.outputConnected = false",
                    ReturnValueType = ApiReturnValueType.StringStatus,
                    DelphiOnly = false,
                    Available = true
                });

                /**
                * app.get('/api/query-params', (req, res) => {
                *     res.json(dummy.getEventParams());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 5,
                    AngularName = "queryParams(): Observable<EventParams>",
                    AngularCall = "http.get<EventParams>('/api/query-params', {})",
                    Description = "Ask for Event-Params returned as Json",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/QueryParams",
                    UrlString = "/api/query-params",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.json(dummy.getEventParams()",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

                Add(new ApiItem()
                {
                    AngularID = 12,
                    AngularName = "requestNetto(): Observable<string>",
                    AngularCall = "http.get('/api/widget/netto', {responseType: 'text'})",
                    Description = "request netto",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/Netto",
                    UrlString = "/api/widget/netto",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                Add(new ApiItem()
                {
                    AngularID = 13,
                    AngularName = "requestOutputNetto(): Observable<string>",
                    AngularCall = "http.get('/api/widget/get-output-netto', {responseType: 'text'})",
                    Description = "Requeset Output Netto",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/GetOutputNetto",
                    UrlString = "/api/widget/get-output-netto",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                Add(new ApiItem()
                {
                    AngularID = 14,
                    AngularName = "requestInputNetto(): Observable<string>",
                    AngularCall = "http.get('/api/widget/get-input-netto', {responseType: 'text'})",
                    Description = "Requeset Netto",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/GetInputNetto",
                    UrlString = "/api/widget/get-input-netto",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                Add(new ApiItem()
                {
                    AngularID = 15,
                    AngularName = "requestNetto1(): Observable<string>",
                    AngularCall = "http.get('/api/widget/netto', {responseType: 'text'})",
                    Description = "Requeset Netto with Access-Control-Origin of localhost:3000",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/Netto",
                    UrlString = "/api/widget/netto",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/get-input-connection-status', (req, res) => {
                *     res.send(iconn.getInputConnectionStatus());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 51,
                    AngularName = "getConnectionStatus() : Observable<ConnectionStatus>",
                    AngularCall = "?",
                    Description = "Ask Node-Server to return Input connection status as Json",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/GetInputConnectionStatus",
                    UrlString = "/api/get-input-connection-status",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "iconn.getInputConnectionStatus()",
                    ReturnValueType = ApiReturnValueType.Json,
                    NodeOnly = true,
                    Available = true
                });

                /**
                * app.get('/api/get-output-connection-status', (req, res) => {
                *     res.send(iconn.getOutputConnectionStatus());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 52,
                    AngularName = "getOutputConnectionStatus() : Observable<ConnectionStatus>",
                    AngularCall = "http.get<ConnectionStatus>('/api/get-output-connection-status', {})",
                    Description = "Ask Node-Server to return Output connection status as Json",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Node,
                    UrlStringNet = "api/Node/GetOutputConnectionStatus",
                    UrlString = "/api/get-output-connection-status",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "oconn.getOutConnectionStatus()",
                    ReturnValueType = ApiReturnValueType.Json,
                    NodeOnly = true,
                    Available = true
                });

            }

            public void InitDelphi()
            {
                /**
                * app.get('/api/manage-clear', (req, res) => {
                *     dummy.clear();
                *     dummy.broadcastToConnectedApps('Manage.Clear');
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 6,
                    AngularName = "manageClear(): Observable<string>",
                    AngularCall = "http.get('/api/manage-clear', { responseType: 'text' })",
                    Description = "Send Request to Clear Event to desktop app via Get",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetWideRaceTableJson",
                    UrlString = "/api/manage-clear",
                    Params = "",
                    Body = "none",
                    ImplementationHint = "dummy.broadcastToConnectedApps('Manage.Clear')",
                    ReturnValueType = ApiReturnValueType.Json,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/manage-clear-race', (req, res) => {
                *     //dummy.clear();
                *     //dummy.broadcastToConnectedApps('Manage.Clear');
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 7,
                    AngularName = "manageClearRace(race: number): Observable<string>",
                    AngularCall = "http.get('/api/manage-clear-race?race=${race}', { responseType: 'text' })",
                    Description = "Send Request to Clear Event to desktop app via Get",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/ManageClearRace",
                    UrlString = "/api/manage-clear-race",
                    Params = "race",
                    Body = "none",
                    ImplementationHint = "not implemented",
                    ReturnValueType = ApiReturnValueType.Json,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/manage-go-back-to-race', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 8,
                    AngularName = "manageGoBackToRace(race: number): Observable<string>",
                    AngularCall = "http.get('/api/manage-go-back-to-race?race=${race}', { responseType: 'text' })",
                    Description = "Send Request to Clear Event to desktop app via Get",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/ManageGoBackToRace",
                    UrlString = "/api/manage-go-back-to-race",
                    Params = "race",
                    Body = "none",
                    ImplementationHint = "not implemented",
                    ReturnValueType = ApiReturnValueType.Json,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/manage-clear-time-point', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 9,
                    AngularName = "manageClearTimepoint(race: number, it: number): Observable<string>",
                    AngularCall = "http.get('/api/manage-clear-timepoint?race=${race}&it=${it}', { responseType: 'text' })",
                    Description = "Send Request to Clear Timepoint to desktop app via Get",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/ManageClearTimepoint",
                    UrlString = "/api/manage-clear-timepoint",
                    Params = "int race, int it",
                    Body = "none",
                    ImplementationHint = "not implemented')",
                    ReturnValueType = ApiReturnValueType.Json,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/widget/time', (req, res) => {
                *     var race = req.query.race;
                *     var it = req.query.it;
                *     var bib = req.query.bib;
                *     var tme = getTime();
                *     var t = "FR.*.W" + race + ".Bib" + bib + ".RV=500";  //example for finish msg
                *     iconn.writeToSocket(t);
                *     res.send(t);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 10,
                    AngularName = "sendTime(race: number, it: number, bib: number): Observable<string>",
                    AngularCall = "http.get('/api/widget/time?race=${race}&it=${it}&bib=${bib}', { responseType: 'text' })",
                    Description = "Ask to generate time for bib in race at timepoint and return msg as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/SendTime",
                    UrlString = "/api/widget/time",
                    Params = "int race, int it, int bib",
                    Body = "none",
                    ImplementationHint = "iconn.writeToSocket(msg)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                Add(new ApiItem()
                {
                    AngularID = 11,
                    AngularName = "sendMsg(value: string): Observable<string>",
                    AngularCall = "http.get(`/api/send-msg?value=${value}`, { responseType: 'text' })",
                    Description = "pass msg to server",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/SendTime",
                    UrlString = "/api/send-msg",
                    Params = "string value",
                    Body = "none",
                    ImplementationHint = "iconn.writeToSocket(msg)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/widget/get-event-table-json', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 37,
                    AngularName = "getEventTableJson(mode: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/get-event-table-json? mode =${mode}`, {responseType: 'text'})",
                    Description = "Ask Server to return event or points table json as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetEventTableJson",
                    UrlString = "/api/widget/get-event-table-json",
                    Params = "int mode (layout 1 = finish, layout 2 = points)",
                    Body = "none",
                    ImplementationHint = "res.send(okNI);",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/widget/get-finish-table-json', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 38,
                    AngularName = "getFinishTableJson() : Observable<string>",
                    AngularCall = "http.get('/api/widget/get-finish-table-json', {responseType: 'text'})",
                    Description = "Ask Server to return finish table json as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetFinishTableJson",
                    UrlString = "/api/widget/get-finish-table-json",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(okNI);",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                /**
                * app.get('/api/widget/get-points-table-json', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 39,
                    AngularName = "getPointsTableJson() : Observable<string>",
                    AngularCall = "http.get('/api/widget/get-points-table-json', {responseType: 'text'})",
                    Description = "Ask Server to return points table json as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetPointsTableJson",
                    UrlString = "/api/widget/get-points-table-json",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(okNI);",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                /**
                * app.get('/api/get-narrow-race-table-json', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 40,
                    AngularName = "getNarrowRaceTableJson(race: number, it: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/get-narrow-race-table-json? race =${race}&it=${it}`, {responseType: 'text'})",
                    Description = "Ask delphi desktop app for Narrow Race-Table Json",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetNarrowRaceTableJson",
                    UrlString = "/api/get-narrow-race-table-json",
                    Params = "int race, int it",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                /**
                * app.get('/api/get-wide-race-table-json', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 41,
                    AngularName = "getWideRaceTableJson(race: number, it: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/get-wide-race-table-json? race =${race}&it=${it}`, {responseType: 'text'})",
                    Description = "Ask delphi desktop app for Wide Race-Table Json as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetWideRaceTableJson",
                    UrlString = "/api/get-wide-race-table-json",
                    Params = "int race, int it",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/get-race-table-json', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 42,
                    AngularName = "getRaceTableJson() : Observable<string>",
                    AngularCall = "http.get(`/api/widget/get-race-table-json`, {responseType: 'text'})",
                    Description = "Ask delphi desktop app for current Race-Table Json as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetRaceTableJson",
                    UrlString = "/api/get-race-table-json",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/get-race-table-html', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 43,
                    AngularName = "getRaceTableHtml() : Observable<string>",
                    AngularCall = "http.get(`/api/widget/get-race-table-html`, {responseType: 'text'})",
                    Description = "Ask delphi desktop app for current Race-Table Html",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/GetRaceTableHtml",
                    UrlString = "/api/get-race-table-html",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                Add(new ApiItem()
                {
                    AngularID = 44,
                    AngularName = "getTime(race: number, it: number, bib: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-time?race=${race}&it=${it}&bib=${bib}`, {responseType: 'text'})",
                    Description = "generate time for bib in race",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/DoTime",
                    UrlString = "/api/widget/do-time",
                    Params = "int race, int it, int bib",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                Add(new ApiItem()
                {
                    AngularID = 45,
                    AngularName = "getFinish(race: number, bib: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-finish?race=${race}&bib=${bib}`, {responseType: 'text'})",
                    Description = "generate finish for bib in race",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/DoFinish",
                    UrlString = "/api/widget/do-finish",
                    Params = "int race, int bib",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                Add(new ApiItem()
                {
                    AngularID = 46,
                    AngularName = "getTimeAndTable(race: number, it: number, bib: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-time-for-table?race=${race}&it=${it}&bib=${bib}`, {responseType: 'text'})",
                    Description = "generate time for bib in race at timepoint and return race table",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/DoTimeForTable",
                    UrlString = "/api/widget/do-time-for-table",
                    Params = "int race, int it, int bib",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                Add(new ApiItem()
                {
                    AngularID = 47,
                    AngularName = "getFinishAndTable(race: number, bib: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-finish-for-table?race=${race}&bib=${bib}`, {responseType: 'text'})",
                    Description = "generate finish for bib in race and return event table",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "Api/Delhi/DoFinishForTable",
                    UrlString = "/api/widget/do-finish-for-table",
                    Params = "int race, int bib",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                Add(new ApiItem()
                {
                    AngularID = 48,
                    AngularName = "getTimingEventForTable(race: number, it: number, bib: number, option: number, mode: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-timing-event-for-table? race =${race}&it=${it}&bib=${bib}&option=${option}&mode=${mode}`, { responseType: 'text' })",
                    Description = "generate time for bib in race at timepoint and return requested table (mode)",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/DoTimingEventForTable",
                    UrlString = "/api/widget/do-timing-event-for-table",
                    Params = "int race, int it, int bib, int option, int mode",
                    Body = "none",
                    ImplementationHint = "res.send(okNI)",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                /**
                * app.get('/api/widget/do-timing-event', (req, res) => {
                *     res.send(okNI);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 49,
                    AngularName = "getTimingEvent(race: number, it: number, bib: number, option: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-timing-event?race=${race}&it=${it}&bib=${bib}&option=${option}`, {responseType: 'text'})",
                    Description = "Ask Server to generate message and return msg string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delphi/DoTimingEvent",
                    UrlString = "/api/widget/do-timing-event",
                    Params = "int race, int it, int bib, int option",
                    Body = "none",
                    ImplementationHint = "res.send(okNI);",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

                Add(new ApiItem()
                {
                    // Trigger generation of time and/or finish position on server.
                    // But this version does not do status updates and it cannot do erasures.
                    AngularID = 50,
                    AngularName = "getTimingEventQuick(race: number, it: number, bib: number) : Observable<string>",
                    AngularCall = "http.get(`/api/widget/do-timing-event-quick?race=${race}&it=${it}&bib=${bib}`, {responseType: 'text'})",
                    Description = "Ask Server to generate message (quick - no option) and return msg string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Delphi,
                    UrlStringNet = "api/Delhi/DoTimingEventQuick",
                    UrlString = "/api/widget/do-timing-event-quick",
                    Params = "int race, int it, int bib",
                    Body = "none",
                    ImplementationHint = "res.send(okNI);",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = true
                });

            }

            public void InitData()
            {

                Add(new ApiItem()
                {
                    AngularID = 53,
                    AngularName = "getSimleText() : Observable<SimpleText>",
                    AngularCall = "http.get<SimpleText>('/api/get-simple-json",
                    Description = "Retrieve json object, which contains a 'EventDataSimpleText' property of type string[]",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/GetSimpleJson",
                    UrlString = "/api/get-simple-json",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "",
                    ReturnValueType = ApiReturnValueType.Json,
                    NodeOnly = true,
                    Available = true
                });

                /**
                * app.get('/api/event-data', (req, res) => {
                *     res.send(dummy.EventData);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 16,
                    AngularName = "pullEventData(): Observable<string>",
                    AngularCall = "http.get('/api/event-data', { responseType: 'text' }",
                    Description = "Ask for EventData to be returned as plain Text",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/EventData",
                    UrlString = "/api/event-data",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.EventData)",
                    ReturnValueType = ApiReturnValueType.Text,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/event-data-json', (req, res) => {
                *     res.send(dummy.EventDataJson);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 17,
                    AngularName = "pullEventDataJson(): Observable<EventDataJson>",
                    AngularCall = "http.get<EventDataJson>('/api/event-data-json', JsonOptions)",
                    Description = "Ask for EventData to be returned as Json",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/EventDataJson",
                    UrlString = "/api/event-data-json",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.EventDataJson)",
                    ReturnValueType = ApiReturnValueType.Json,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/race-data-json', (req, res) => {
                *     const race = req.query.race;
                *     res.send(dummy.getRaceDataJson(race));
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 18,
                    AngularName = "pullRaceDataJson(race: number): Observable<RaceDataJson>",
                    AngularCall = "http.get<RaceDataJson>('/api/race-data-json', o)",
                    Description = "Ask for RaceData to be returned as Json",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/RaceDataJson",
                    UrlString = "/api/race-data-json",
                    Params = "int race",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getRaceDataJson(race)",
                    ReturnValueType = ApiReturnValueType.Json,
                    DelphiOnly = false
                });

                /**
                 * app.post('/api/event-data', (req, res) =>  {
                 *     dummy.EventData = req.body;
                 *     res.json(rvOK);
                 * })
                 */
                Add(new ApiItem()
                {
                    AngularID = 19,
                    AngularName = "pushEventData(value: string): Observable<ApiRetValue>",
                    AngularCall = "http.post<ApiRetValue>('/api/event-data', value, JsonOptions)",
                    Description = "Post EventData compact plain text body to Server",
                    HttpMethod = ApiMethodEnum.Post,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/EventData",
                    UrlString = "/api/event-data",
                    Params = "none",
                    Body = "event data text",
                    ImplementationHint = "dummy.EventData = req.body",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

                /**
                 * app.post('/api/event-data-json', (req, res) =>  {
                 *     dummy.EventDataJson = req.body;
                 *     res.json(rvOK);
                 * })
                 */
                Add(new ApiItem()
                {
                    AngularID = 20,
                    AngularName = "pushEventDataJson(value: EventDataJson): Observable<ApiRetValue>",
                    AngularCall = "http.post<ApiRetValue>('/api/event-data-json', value, JsonOptions)",
                    Description = "Post EventDataJson to Server",
                    HttpMethod = ApiMethodEnum.Post,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/EventDataJson",
                    UrlString = "/api/event-data-json",
                    Params = "none",
                    Body = "EventDataJson",
                    ImplementationHint = "dummy.EventDataJson = req.body",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

                /*
                    app.post('/api/race-data-json', (req, res) =>  {
                        const race = req.query.race;
                        dummy.putRaceDataJson(race, req.body);
                        res.json(rvOK);
                    })
                 */
                Add(new ApiItem()
                {
                    AngularID = 21,
                    AngularName = "pushRaceDataJsonForRace(race: number, value: RaceDataJson): Observable<ApiRetValue>",
                    AngularCall = "http.post<ApiRetValue>('/api/race-data-json', value, o)",
                    Description = "Post RaceDataJson to Server",
                    HttpMethod = ApiMethodEnum.Post,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/RaceDataJson",
                    UrlString = "/api/race-data-json",
                    Params = "int race",
                    Body = "RaceDataJson",
                    ImplementationHint = "putRaceDataJson(race, req.body)",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

                /*
                    app.post('/api/rd.json', (req, res) =>  {
                        dummy.raceDataJson = req.body;
                        res.send(rvOK);
                    })
                */
                Add(new ApiItem()
                {
                    AngularID = 22,
                    AngularName = "pushRaceDataJson(value: RaceDataJson): Observable<ApiRetValue>",
                    AngularCall = "http.post<ApiRetValue>('/api/rd.json', value, JsonOptions)",
                    Description = "Post RaceDataJson to Server",
                    HttpMethod = ApiMethodEnum.Post,
                    Controller = ApiControllerEnum.Data,
                    UrlStringNet = "api/Data/PushRaceDataJson",
                    UrlString = "/api/rd.json",
                    Params = "none",
                    Body = "RaceDataJson",
                    ImplementationHint = "raceDataJson = req.body",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

            }

            public void InitSlot()
            {

                Add(new ApiItem()
                {
                    AngularID = 25,
                    AngularName = "readFromSlot(id: number): Observable<string>",
                    AngularCall = "http.get(`ud${id}`, { responseType: 'text' })",
                    Description = "retrieve Slot data as string",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Slot,
                    UrlStringNet = "api/Slot/UD/{id}",
                    UrlString = "/ud/{id}",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "return Dummy.slot{id}",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                 * app.post('/ud/2', (req, res) =>  {
                 *   dummy.slot2 = req.body;
                 *   res.json(rvOK);
                 * }) 
                 */
                Add(new ApiItem()
                {
                    AngularID = 26,
                    AngularName = "push2(value: EventDataJson): Observable<ApiRetValue>",
                    AngularCall = "http.post<ApiRetValue>('/api/ed.json', value, JsonOptions)",
                    Description = "Post EventDatJson to Slot controller",
                    HttpMethod = ApiMethodEnum.Post,
                    Controller = ApiControllerEnum.Slot,
                    UrlStringNet = "api/Slot/2",
                    UrlString = "/ud/2",
                    Params = "none",
                    Body = "EventDataJson",
                    ImplementationHint = "dummy.slot2 = req.body",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

                /**
                 * app.post('/ud/3', (req, res) =>  {
                 *   dummy.slot3 = req.body;
                 *   res.json(rvOK);
                 * }) 
                 */
                Add(new ApiItem()
                {
                    AngularID = 27,
                    AngularName = "push3(value: RaceDataJson): Observable<ApiRetValue>",
                    AngularCall = "http.get<RaceDataJson>('/api/rd.json', JsonOptions)",
                    Description = "Post RaceDataJson to Slot controller",
                    HttpMethod = ApiMethodEnum.Post,
                    Controller = ApiControllerEnum.Slot,
                    UrlStringNet = "api/Slot/3",
                    UrlString = "/ud/3",
                    Params = "none",
                    Body = "RaceDataJson",
                    ImplementationHint = "dummy.slot3 = req.body",
                    ReturnValueType = ApiReturnValueType.JsonStatus,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/slot/2', (req, res) => {
                *     res.send(dummy.slot2);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 28,
                    AngularName = "pull2(): Observable<EventDataJson>",
                    AngularCall = "http.get<EventDataJson>('/slot/2', JsonOptions)",
                    Description = "Ask for EventDataJson Text, returned from Variable as is",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Slot,
                    UrlStringNet = "api/Slot/2",
                    UrlString = "/ud/2",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.slot2)",
                    ReturnValueType = ApiReturnValueType.JsonText,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/slot/3', (req, res) => {
                *     res.send(dummy.slot3);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 29,
                    AngularName = "pull3(): Observable<RaceDataJson>",
                    AngularCall = "http.get<RaceDataJson>('/ud/3', JsonOptions)",
                    Description = "Ask for all RaceDataJson Text, returned from Variable as is",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Slot,
                    UrlStringNet = "api/Slot/3",
                    UrlString = "/ud/3",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.slot3)",
                    ReturnValueType = ApiReturnValueType.JsonText,
                    DelphiOnly = false
                });

            }

            public void InitBridge()
            {

                /**
                * app.get('/api/backup', (req, res) => {
                *     res.send(dummy.getBackup());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 30,
                    AngularName = "getBackup(): Observable<string[]>",
                    AngularCall = "http.get<string[]>('/api/backup', JsonOptions)",
                    Description = "Ask for Backup as Json-Text",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/Backup",
                    UrlString = "/api/backup",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBackup())",
                    ReturnValueType = ApiReturnValueType.JsonText,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/backlog', (req, res) => {
                *     const sl: string[] = dummy.getBacklog();
                *     res.send(sl);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 31,
                    AngularName = "getBacklog(): Observable<string[]>",
                    AngularCall = "http.get<string[]>('/api/backlog', JsonOptions)",
                    Description = "Ask for Backlog as Text",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/Backlog",
                    UrlString = "/api/backlog",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBacklog())",
                    ReturnValueType = ApiReturnValueType.Text,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/backlog-and-log', (req, res) => {
                *     const sl: string[] = dummy.getBacklogAndLog();
                *     res.send(sl);
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 32,
                    AngularName = "getBackupAndLog(): Observable<string[]>",
                    AngularCall = "http.get<string[]>('/api/backup-and-log', JsonOptions)",
                    Description = "Ask for Backup plus Log as Text",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/BackupAndLog",
                    UrlString = "/api/backup-and-log",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBackupAndLog())",
                    ReturnValueType = ApiReturnValueType.Text,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/backup-string', (req, res) => {
                *     res.send(dummy.getBackupString());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 33,
                    AngularName = "getBackupString(): Observable<string>",
                    AngularCall = "http.get('/api/backup-string', { responseType: 'text' })",
                    Description = "Ask for Backup String as Text",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/BackupString",
                    UrlString = "/api/backup-string",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBackupString())",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/backlog-string', (req, res) => {
                *     res.send(dummy.getBacklogString());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 34,
                    AngularName = "getBacklogString(): Observable<string>",
                    AngularCall = "http.get('/api/backlog-string', { responseType: 'text' })",
                    Description = "Ask for Backlog String",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/BacklogString",
                    UrlString = "/api/backlog-string",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBacklogString())",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/backlog-and-log-string', (req, res) => {
                *     res.send(dummy.getBackupAndLogString());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 35,
                    AngularName = "getBackupAndLogString(): Observable<string>",
                    AngularCall = "http.get('/api/backup-and-log-string', { responseType: 'text' })",
                    Description = "Ask for (Backlup plus Log) String",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/BackupAndLogString",
                    UrlString = "/api/backup-and-log-string",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBackupAndLogString())",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

                /**
                * app.get('/api/backlog-and-log-json-string', (req, res) => {
                *     res.send(dummy.getBackupAndLogJsonString());
                *  })   
                */
                Add(new ApiItem()
                {
                    AngularID = 36,
                    AngularName = "getBackupAndLogJsonString(): Observable<string>",
                    AngularCall = "http.get('/api/backup-and-log-json-string', { responseType: 'text' })",
                    Description = "Ask for Backup plus Log String as Json-Text",
                    HttpMethod = ApiMethodEnum.Get,
                    Controller = ApiControllerEnum.Bridge,
                    UrlStringNet = "api/Bridge/BackupAndLogJsonString",
                    UrlString = "/api/backup-and-log-json-string",
                    Params = "none",
                    Body = "none",
                    ImplementationHint = "res.send(dummy.getBackupAndLogJsonString())",
                    ReturnValueType = ApiReturnValueType.String,
                    DelphiOnly = false
                });

            }
        }
    }
}