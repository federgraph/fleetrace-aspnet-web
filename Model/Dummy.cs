using System;
using static RiggVar.FR.ApiItem;

namespace RiggVar.FR
{
    public class Dummy
    {
        public static ApiCollection ApiCol = new ApiCollection();

        public static readonly ApiRetValue rvOK = new ApiRetValue();

        public static bool InputConnected = false;
        public static bool OutputConnected = false;

        public static string EventData = "";
        public static EventDataJson EventDataJson = new EventDataJson();
        public static RaceDataJson RaceDataJson = new RaceDataJson();

        public static EventParamJson EventParams = new EventParamJson();

        public static string InputNetto = "";
        public static string OutputNetto = "";

        public static string slot2 = "";
        public static string slot3 = "";

        public static string Time
        {
            get
            {
                DateTime d = new DateTime();
                int hh = d.Hour;
                int mm = d.Minute;
                int ss = d.Second;
                int t = d.Millisecond;

                string shh = "" + hh;
                string smm = (mm < 10) ? "0" + mm.ToString() : mm.ToString();
                string sss = ss < 10 ? "0" + ss.ToString() : ss.ToString();
                string sms = t.ToString();
                if (t < 10) { sms = "00" + t; }
                else if (t < 100) sms = "0" + t;

                var tm = shh + ':' + smm + ':' + sss + '.' + sms;
                return tm;
            }
        }
    }

}
