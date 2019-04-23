namespace RiggVar.FR
{

    public class ApiRetValue
    {
        public string retvalue = "ok";
    }

    public class ApiConnectionStatus
    {
        public bool connected = false;
        public bool websockets = false;
    }

    public class ApiEventParams
    {
        public int raceCount = 2;
        public int itCount = 2;
        public int startlistCount = 8;
    }

    public class ApiEventProps
    {
        public string eventName = "";
        public int scoringSystem = 0;
        public int schemaCode = 0;
        public bool isTimed = true;
    }

    public class StringContainer {
        private const string okNI = "ok, but not implemented";
        private const string ok = "ok";

        public readonly string[] ScoringSystemStrings = new string[] { "Low Point", "Bonus Point", "Bonus Point DSV" };
        public readonly string[] NameFieldSchemaStrings = new string[] { "Default", "Long Names", "NX" };
    }


}
