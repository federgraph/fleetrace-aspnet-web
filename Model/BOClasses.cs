using System.Collections.Generic;

namespace RiggVar.FR
{

    public class TStringList : List<string>
    {
        public string Strings(int index)
        {
            return "";
        }

        public string Text { get; set; }
    }

    public class TStringListArray : List<TStringList>
    {

    }

    //public class TExcelExporter
    //{

    //}

    public class TBO
    {
        public ApiEventParams BOParams = new ApiEventParams();
        public ApiEventProps EventProps = new ApiEventProps();

        public bool UseFleets = false;

        public void BackupPenalties(TStringList Memo, int r)
        {

        }
    }
}
