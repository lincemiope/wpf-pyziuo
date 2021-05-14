using System;
using System.Collections.Generic;

namespace PyziWrap
{
    public static class Global
    {
        private static IntPtr _UOHandle = DLL.NativeMethods.Open();
        public static string _UODllInfo = "uo.dll v" + DLL.NativeMethods.Version().ToString();
        public static int _journalRef = 1;
        public static List<string> _journal = new List<string>();

        public static string _stackver = "2019-30-04";
        public static string _pyziver = "2017-16-09";
        public static List<int> _ignorelist = new List<int>();

        public static int _FoundID = 0x0;
        public static int _FoundType = 0x0;
        public static int _FoundX = 0;
        public static int _FoundY = 0;
        public static int _FoundZ = 0;
        public static int _FoundKind = 0;
        public static int _FoundStack = 0;
        public static int _FoundCont = 0;
        public static int _FoundRep = 0;
        public static int _FoundCol = 0;
        public static int _FoundDist = 0;

        public static int _TileType = 0;
        public static int _TileZ = 0;
        public static string _TileName = "";
        public static int _TileFlags = 0;

        public static string _PropertyName = "";
        public static string _PropertyString = "";
        public static IntPtr GetHandle(bool newHandle = false)
        {
            if (newHandle)
            {
                _UOHandle = DLL.NativeMethods.Open();
            }
            return _UOHandle;
        }
    }
}
