using System;
using System.Runtime.InteropServices;
//[assembly: System.Security.AllowPartiallyTrustedCallers()]
namespace PyziWrap
{
    public sealed class DLL
	{
		internal static class NativeMethods
		{
			[DllImport("uo.dll")]
			public static extern IntPtr Open();

			[DllImport("uo.dll")]
			public static extern int Version();

			[DllImport("uo.dll")]
			public static extern void Close(IntPtr handle);

			[DllImport("uo.dll")]
			public static extern void Clean(IntPtr handle);

			[DllImport("uo.dll")]
			public static extern int Query(IntPtr handle);

			[DllImport("uo.dll")]
			public static extern int Execute(IntPtr handle);

			[DllImport("uo.dll")]
			public static extern int GetTop(IntPtr handle);

			[DllImport("uo.dll")]
			public static extern int GetType(IntPtr handle, int index);

			[DllImport("uo.dll")]
			public static extern void SetTop(IntPtr handle, int index);
            [DllImport("uo.dll")]
			public static extern void PushStrVal(IntPtr handle, string value);

			[DllImport("uo.dll")]
			public static extern void PushInteger(IntPtr handle, int value);

			[DllImport("uo.dll")]
			public static extern void PushBoolean(IntPtr handle, Boolean value);

            [DllImport("uo.dll")]
            public static extern void PushPointer(IntPtr handle, IntPtr value);

            [DllImport("uo.dll")]
            public static extern void PushPtrOrNil(IntPtr handle, IntPtr value);

            [DllImport("uo.dll")]
            public static extern void PushValue(IntPtr handle, int index);

            [DllImport("uo.dll")]
			public static extern IntPtr GetString(IntPtr handle, int index);

			[DllImport("uo.dll")]
			public static extern int GetInteger(IntPtr handle, int index);

			[DllImport("uo.dll")]
			public static extern bool GetBoolean(IntPtr handle, int index);

            [DllImport("uo.dll")]
            public static extern IntPtr GetPointer(IntPtr handle, int index);

            [DllImport("uo.dll")]
            public static extern double GetDouble(IntPtr handle, int index);
        }
	}
}
