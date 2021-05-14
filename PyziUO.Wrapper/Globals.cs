using System.Collections.Generic;

namespace PyziWrap
{
    public partial class Wrapper
    {
        public string UODllInfo { get { return Global._UODllInfo; } }
        private int journalRef { get { return Global._journalRef; } set { Global._journalRef = value; } }
        private List<string> journal { get { return Global._journal; } set { Global._journal = value; } }
        public string StackVer { get { return Global._stackver; } }
        public string PyziVer { get { return Global._pyziver; } }
        private List<int> IgnoreList { get { return Global._ignorelist; } set { Global._ignorelist = value; } }
    }
}
