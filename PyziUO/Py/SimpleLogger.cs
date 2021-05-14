using System;
using System.Collections.Generic;
using System.Threading;

namespace PyziUO.Py
{
    public class SimpleLogger : IDisposable
	{
		private Mutex _mutex = new Mutex(false);
		private UInt32 _entryCount = 0;
		public class Entry
		{
			public enum EntryType
			{
				Info,
				Warning,
				Error,
				Fault
			}

			public Entry(EntryType _entryType, string _msg, UInt32 _index)
			{
                msg = _msg;
				timestamp = DateTime.Now;
                entryType = _entryType;
                index = _index;
			}

            public string msg { get; }
            public DateTime timestamp { get; }
            public EntryType entryType { get; }
            public UInt32 index { get; }

            public string Tostring()
			{
				return string.Format("[{0}][{1}][{2}][{3}]", timestamp, index, entryType, msg);
			}
		}

		private List<Entry> _entries = new List<Entry>();

		public void Reset()
		{
			try
			{
				_mutex.WaitOne();
				_entries = new List<Entry>();
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		public Int32 Count
		{
			get
			{
				_mutex.WaitOne();
				Int32 result = _entries.Count; _mutex.ReleaseMutex(); return result;
			}
		}

		/// <summary>
		/// Gets the first entry in log and removes it from the log.
		/// Returns null if the log is empty.
		/// </summary>
		/// <returns></returns>
		public Entry GetFirst()
		{
			Entry result = null;
			try
			{
				_mutex.WaitOne();
				if (_entries.Count > 0)
				{
					result = _entries[0];
					_entries.RemoveAt(0);
				}

			}
			finally
			{
				_mutex.ReleaseMutex();
			}
			return result;
		}

		/// <summary>
		/// Retrives all the entries from the log.  The log will be 
		/// empty after the operation has been executed.
		/// </summary>
		/// <returns></returns>
		public List<Entry> GetAll()
		{
			List<Entry> result = null;
			try
			{
				_mutex.WaitOne();
				result = _entries;
				_entries = new List<Entry>();
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
			return result;
		}

		public void AddInfo(string msg)
		{
			try
			{
				_mutex.WaitOne();
				_entries.Add(new Entry(Entry.EntryType.Info, msg, _entryCount++));
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		public void AddWarning(string msg)
		{
			try
			{
				_mutex.WaitOne();
				_entries.Add(new Entry(Entry.EntryType.Warning, msg, _entryCount++));
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		public void AddError(string msg)
		{
			try
			{
				_mutex.WaitOne();
				_entries.Add(new Entry(Entry.EntryType.Error, msg, _entryCount++));
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		public void AddFault(string msg)
		{
			try
			{
				_mutex.WaitOne();
				_entries.Add(new Entry(Entry.EntryType.Fault, msg, _entryCount++));
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		public void AddFault(Exception ex)
		{

			try
			{
				_mutex.WaitOne();
				string msg = ex.Message;
				if (ex.InnerException != null)
					msg += " (+INNER): " + ex.InnerException.Message;
				_entries.Add(new Entry(Entry.EntryType.Fault, msg, _entryCount++));
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (_mutex != null)
                    {
                        _mutex.Dispose();
                        _mutex = null;
                    }
                    if (_entries != null)
                    {
                        _entries = null;
                    }
                } catch (Exception) { }
            }
        }
    }
}
