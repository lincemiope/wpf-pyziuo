using System;
using System.Net;

namespace PyziUO.Tools
{
    static class CheckVer
	{
		public static bool IsOutDated(string oldver)
		{
            bool result = false;
			//using (var wc = new WebClient())
   //         {
   //             try
   //             {
   //                 string newver = wc.DownloadString("http://www.uodguildutils.com/pyziuo/version.php");
   //                 result = newver != oldver;
   //             } catch (Exception ex)
   //             {
   //                 Logger.Log("CheckVersion", ex.Message, Logger.LogSector.Internal, Logger.LogType.Exception);
   //             }
   //         }
            return result;
		}
	}
}
