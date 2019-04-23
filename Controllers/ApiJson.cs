using System.Collections.Generic;

namespace RiggVar.FR
{
    public class EventParamJson
    {
        private List<string> SL = new List<string>(3);
        public static readonly string prefix = "DP";

        public int RaceCount = 2;
        public int ITCount = 0;
        public int StartlistCount = 2;

        public string[] ToArray()
        {
            SL.Clear();
            WriteLn("RaceCount", RaceCount);
            WriteLn("ITCount", ITCount);
            WriteLn("StartlistCount", StartlistCount);
            return SL.ToArray();
        }

        private void WriteLn(string key, int value)
        {
            string s = $"{prefix}.{key}={value}";
            SL.Add(s);
        }
    }

    public class EventPropJson
    {
        public static readonly string prefix = "EP";

        public string Name = "Test Event Name";
        public string ScoringSystem = "Low Point System";
        public int Throwouts = 0;
        public string DivisionName = "*";
        public string InputMode = "Strict";
        public string RaceLayout = "Finish";
        public string NameSchema = "";
        public string FieldMap = "SN";
        public string FieldCaptions = "";
        public int FieldCount = 6;
        public int NameFieldCount = 2;
        public string NameFieldOrder = "041256";
        public bool UseFleets = false;
        public int TargetFleetSize = 8;
        public int FirstFinalRace = 20;
        public bool IsTimed = false;
        public bool UseCompactFormat = true;

        private List<string> SL = new List<string>();

        public string[] ToArray()
        {
            SL.Clear();
            WriteLn("Name", Name);
            WriteLn("Throwouts", Throwouts);
            WriteLn("DivisionName", DivisionName);
            WriteLn("InputMode", InputMode);
            WriteLn("RaceLayout", RaceLayout);
            WriteLn("NameSchema", NameSchema);
            WriteLn("FieldMap", FieldMap);
            WriteLn("FieldCaptions", FieldCaptions);
            WriteLn("FieldCount", FieldCount);
            WriteLn("NameFieldCount", NameFieldCount);
            WriteLn("NameFieldOrder", NameFieldOrder);
            WriteLn("UseFleets", UseFleets);
            WriteLn("TargetFleetSize", TargetFleetSize);
            WriteLn("FirstFinalRace", FirstFinalRace);
            WriteLn("IsTimed", IsTimed);
            WriteLn("UseCompactFormat", UseCompactFormat);
            return SL.ToArray();
        }

        private void WriteLn(string key, object value)
        {
            string s = $"{prefix}.{key}={value}";
            SL.Add(s);
        }
        private void WriteLn(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                SL.Add("{EventPropJson.prefix}.{key}={value}");
        }
        private void WriteLn(string key, int value)
        {
            SL.Add("{EventPropJson.prefix}.{key}={value}");
        }
        private void WriteLn(string key, bool value)
        {
            SL.Add("{EventPropJson.prefix}.{key}={value}");
        }

    }

    public class EventParamsJson
    {
        public string[] EventParams;
    }

    public class EventPropsJson
    {
        public string[] EventProps;
    }

    public class NameTableJson
    {
        public string[] NameTable;
    }

    public class StartListJson
    {
        public string[] StartList;
    }

    public class FleetListJson
    {
        public string[] FleetList;
    }

    public class FinishInfoJson
    {
        public string[] FinishInfo;
    }

    public class TimingInfoJson
    {
        public string[] TimingInfo;
    }

    public class PenaltyInfoJson
    {
        public string[] PenaltyInfo;
    }

    public class EventDataJson
    {
        public string[] EventParams;
        public string[] EventProps;
        public string[] NameTable;
        public string[] StartList;
        public string[] FleetList;
        public string[] FinishInfo;
        public string[][] TimingInfo;
        public string[][] PenaltyInfo;
    }

    public class RaceDataJson
    {
        public string[] FinishInfo;
        public string[] TimingInfo;
        public string[] PenaltyInfo;
    }

}
