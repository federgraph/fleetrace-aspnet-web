using Newtonsoft.Json;
using System.Collections.Generic;

namespace RiggVar.FR
{

    public class JsonInfo
    {
        private readonly string[] EmptyStrings;
        private readonly List<string> SL;
        //private TExcelExporter ee;
        private readonly TStringList Memo;
        private readonly TBO BO;

        public JsonInfo(TBO abo)
        {
            BO = abo;
            //ee = new TExcelExporter();
            Memo = new TStringList();
            SL = new List<string>();
            SL.Clear();
            EmptyStrings = SL.ToArray();

        }

        public void WriteJson(TStringList ML)
        {
            EventDataJson obj = GetEventDataJson();
            string s = JsonConvert.SerializeObject(obj, Formatting.Indented);
            ML.Text = s;
        }

        public string[] MemoStrings(TStringList Memo)
        {
            SL.Clear();
            for (int i = 0; i < Memo.Count; i++)
            {
                if (Memo.Strings(i) != string.Empty)
                {
                    SL.Add(Memo.Strings(i));
                }
            }
            return SL.ToArray();
        }

        public void UpdateParamJson(EventParamJson o)
        {
            o.RaceCount = BO.BOParams.raceCount;
            o.ITCount = BO.BOParams.itCount;
            o.StartlistCount = BO.BOParams.startlistCount;
        }

        public void UpdatePropJson(EventPropJson o)
        {
            o.Name = BO.EventProps.eventName;
            //o.Throwouts = BO.EventProps.Throwouts;
            //o.DivisionName = BO.EventProps.DivisionName;
            //o.RaceLayout = BO.EventProps.RaceLayout;
            //o.NameSchema = BO.EventProps.NameSchema;
            //o.FieldMap = BO.EventProps.FieldMap;
            //o.FieldCaptions = BO.EventProps.FieldCaptions;
            //o.FieldCount = BO.StammdatenNode.Collection.FieldCount;
            //o.NameFieldOrder = BO.EventProps.NameFieldOrder;
            //o.UseFleets = BO.EventProps.UseFleets;
            //o.TargetFleetSize = BO.EventProps.TargetFleetSize;
            //o.FirstFinalRace = BO.EventProps.FirstFinalRace;
            o.IsTimed = BO.EventProps.isTimed;
            //o.UseCompactFormat = BO.EventProps.UseCompactFormat;
        }

        public string[] GetEventParams()
        {
            EventParamJson o = new EventParamJson();
            UpdateParamJson(o);
            return o.ToArray();
        }

        public string[] GetEventProps()
        {
            EventPropJson o = new EventPropJson();
            UpdatePropJson(o);
            return o.ToArray();
        }

        public string[] GetNames()
        {
            Memo.Clear();
            //ee.AddSection(TExcelImporter.TableID_NameList, BO, Memo);
            return MemoStrings(Memo);
        }

        public string[] GetStartList()
        {
            Memo.Clear();
            //ee.AddSection(TExcelImporter.TableID_StartList, BO, Memo);
            return MemoStrings(Memo);
        }

        public string[] GetFleetList()
        {
            if (BO.UseFleets) //(BO.EventNode.UseFleets)
            {
                Memo.Clear();
                //ee.AddSection(TExcelImporter.TableID_FleetList, BO, Memo);
                return MemoStrings(Memo);
            }
            return EmptyStrings;
        }

        public string[] GetRaceFinishList(int r)
        {
            Memo.Clear();
            //ee.AddRaceFinishSection(TMain.BO, Memo, r);
            return MemoStrings(Memo);
        }

        public string[] GetFinishList()
        {
            Memo.Clear();
            //ee.AddSection(TExcelImporter.TableID_FinishList, BO, Memo);
            return MemoStrings(Memo);
        }

        public string[] GetTL(int r)
        {
            Memo.Clear();
            //ee.AddTimingSection(TMain.BO, Memo, r);
            return MemoStrings(Memo);
        }

        public string[] GetPL(int r)
        {
            if (r > 0 && r <= BO.BOParams.raceCount)
            {
                Memo.Clear();
                //Memo.Add($"PenaltyList.Begin.R{r}");
                BO.BackupPenalties(Memo, r);
                //Memo.Add($"PenaltyList.End.R{r}");
                return MemoStrings(Memo);
            }
            else
            {
                return EmptyStrings;
            }
        }

        public List<string[]> GetTimeLists()
        {
            if (BO.BOParams.itCount > 0 || BO.EventProps.isTimed)
            {
                List<string[]> a = new List<string[]>();
                for (int r = 1; r <= BO.BOParams.raceCount; r++)
                {
                    a.Add(GetTL(r));
                }
                return a;
            }
            return null;
        }

        public List<string[]> GetPenaltyLists()
        {
            List<string[]> a = new List<string[]>();
            for (int r = 1; r <= BO.BOParams.raceCount; r++)
            {
                a.Add(GetPL(r));
            }
            return a;
        }

        public object GetPenaltyInfo()
        {
            Dictionary<string, string[]> d = new Dictionary<string, string[]>();
            for (int r = 1; r <= BO.BOParams.raceCount; r++)
            {
                d.Add("R" + r, GetPL(r));
            }
            return d;
        }

        public EventDataJson GetEventDataJson()
        {
            EventDataJson o = new EventDataJson();
            List<string[]> temp;

            o.EventParams = GetEventParams();
            o.EventProps = GetEventProps();
            o.NameTable = GetNames();
            o.StartList = GetStartList();
            o.FleetList = GetFleetList();
            o.FinishInfo = GetFinishList();

            if (BO.BOParams.itCount > 0 || BO.EventProps.isTimed)
            {
                temp = GetTimeLists();
                if (temp != null)
                {
                    o.TimingInfo = temp.ToArray();
                }
            }
            else
            {
                o.TimingInfo = new List<string[]>().ToArray();
            }

            o.PenaltyInfo = GetPenaltyLists().ToArray();
            return o;
        }

        RaceDataJson GetRaceDataJson(int r)
        {
            RaceDataJson o = new RaceDataJson
            {
                FinishInfo = GetRaceFinishList(r),
                TimingInfo = GetTL(r),
                PenaltyInfo = GetPL(r)
            };
            return o;
        }

        public string[] GetRaceData(int r)
        {
            RaceDataJson o = GetRaceDataJson(r);
            return ConvertRaceDataJson(o);
        }

        public string[] ConvertRaceDataJson(RaceDataJson o)
        {
            List<string> a = new List<string>();

            foreach (string s1 in o.FinishInfo)
            {
                a.Add(s1);
            }

            foreach (string s2 in o.TimingInfo)
            {
                a.Add(s2);
            }

            foreach (string s3 in o.PenaltyInfo)
            {
                a.Add(s3);
            }

            return a.ToArray();
        }

        public string[] GetEventData()
        {
            EventDataJson o = GetEventDataJson();
            return ConvertEventDataJson(o);
        }

        public string[] ConvertEventDataJson(EventDataJson o)
        {
            List<string> a = new List<string>();

            foreach (string s in o.EventParams)
            {
                a.Add(s);
            }

            foreach (string s in o.EventProps)
            {
                a.Add(s);
            }

            if (o.NameTable.Length > 2)
            {
                foreach (string s in o.NameTable)
                {
                    a.Add(s);
                }
            }

            foreach (string s in o.StartList)
            {
                a.Add(s);
            }

            if (o.FleetList.Length > 2)
            {
                foreach (string s in o.FleetList)
                {
                    a.Add(s);
                }
            }

            foreach (string s in o.FinishInfo)
            {
                a.Add(s);
            }

            if (o.TimingInfo != null && o.TimingInfo.Length > 0)
            {
                foreach (var ti in o.TimingInfo)
                {
                    foreach (string s in ti)
                    {
                        a.Add(s);
                    }
                }
            }

            if (o.PenaltyInfo != null && o.PenaltyInfo.Length > 0)
            {
                foreach (var pi in o.PenaltyInfo)
                {
                    foreach (string s in pi)
                    {
                        a.Add(s);
                    }
                }
            }

            return a.ToArray();
        }

    }
}
