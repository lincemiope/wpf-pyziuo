using PyziWrap.DataTypes;
using PyziWrap.Lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace PyziWrap
{
    [ComVisible(true)]
    public partial class Wrapper
	{
        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern short GetAsyncKeyState(int vKey);
        }
        /*[DllImport("user32.dll")]
		extern short VkKeyScan(char ch);*/
        public Wrapper()
        {
            if (Global.GetHandle() == IntPtr.Zero)
            {
                Open();
            }
        }
        public IntPtr Handle => Global.GetHandle();
        public int TopObject => DLL.NativeMethods.GetTop(Handle);
        private bool GetBoolean(string command)
        {
            try
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Get");
                DLL.NativeMethods.PushStrVal(Handle, command);
                var result = DLL.NativeMethods.Execute(Handle);
                if (result == 0)
                    return DLL.NativeMethods.GetBoolean(Handle, 1);
                else
                    return false;
            } catch
            {
                return false;
            }  
        }

        private void SetBoolean(string command, bool value)
        {
            try
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Set");
                DLL.NativeMethods.PushStrVal(Handle, command);
                DLL.NativeMethods.PushBoolean(Handle, value);
                DLL.NativeMethods.Execute(Handle);
            } catch (Exception)
            {

            }
        }

        private int GetInt(string command)
        {
            try
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Get");
                DLL.NativeMethods.PushStrVal(Handle, command);
                var result = DLL.NativeMethods.Execute(Handle);
                if (result == 0)
                    return DLL.NativeMethods.GetInteger(Handle, 1);
                else
                    return 0;
            } catch
            {
                return 0;
            }
        }

        private void SetInt(string command, int value)
        {
            try
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Set");
                DLL.NativeMethods.PushStrVal(Handle, command);
                DLL.NativeMethods.PushInteger(Handle, value);
                DLL.NativeMethods.Execute(Handle);
            }
            catch
            {

            }
        }

        private string GetString(string command)
        {
            try
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Get");
                DLL.NativeMethods.PushStrVal(Handle, command);
                var _r = DLL.NativeMethods.Execute(Handle);

                IntPtr sc = DLL.NativeMethods.GetString(Handle, 1);
                return Marshal.PtrToStringAnsi(sc);
            }
            catch
            {
                return string.Empty;
            }   
        }
        private void SetString(string command, string value)
        {
            try
            {
                DLL.NativeMethods.SetTop(Handle, 0);
                DLL.NativeMethods.PushStrVal(Handle, "Set");
                DLL.NativeMethods.PushStrVal(Handle, command);
                DLL.NativeMethods.PushStrVal(Handle, value);
                DLL.NativeMethods.Execute(Handle);
            }
            catch
            {

            }
        }
        public List<string> Journal => journal;

        //public int TileCount { get { return _TileCount; } }

        private void SetFoundVars(FoundItem item)
        {
            //_Found = item;
            Global._FoundID = item.ID;
            Global._FoundType = item.Type;
            Global._FoundX = item.X;
            Global._FoundY = item.Y;
            Global._FoundZ = item.Z;
            Global._FoundKind = item.Kind;
            Global._FoundStack = item.Stack;
            Global._FoundCont = item.ContID;
            Global._FoundRep = item.Rep;
            Global._FoundCol = item.Col;
            Global._FoundDist = Math.Max(Math.Abs(Global._FoundX - CharPosX), Math.Abs(Global._FoundY - CharPosY));
        }
        private void ClearFoundVars()
        {
            //_Found = new FoundItem();
            Global._FoundID = 0;
            Global._FoundType = 0;
            Global._FoundX = 0;
            Global._FoundY = 0;
            Global._FoundZ = 0;
            Global._FoundKind = 0;
            Global._FoundStack = 0;
            Global._FoundCont = 0;
            Global._FoundRep = 0;
            Global._FoundCol = 0;
            Global._FoundDist = 0;
        }
        private void SetTileVars(Tile tile)
        {
            Global._TileType = tile.Type;
            Global._TileZ = tile.Z;
            Global._TileName = tile.Name;
            Global._TileFlags = tile.Flags;
        }
        private int GetVK(string keystr)
		{
            return HOTKEY_VALUES.Where(k => k.Key == keystr.ToLower()).Select(k => k.Value).FirstOrDefault();
		}
        /*public void TestHotkey()
        {
            int keystroke = 0x79;

            byte[] result = BitConverter.GetBytes(NativeMethods.GetAsyncKeyState(keystroke));

            if (result[0] == 1)
                Sysmessage("the key was pressed after the previous call to GetAsyncKeyState.", 40);

            if (result[1] == 0x80)
                HeadMsg("The key is down", 60);
        }
        public enum KeyStates
        {
            None,
            Down,
            Toggled
        }

        public bool IsKeyDown(Key key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        private KeyStates GetKeyState(Key key)
        {
            KeyStates state = KeyStates.None;
            short k = Convert.ToInt16(GetKeyState(key));
            if (((int)k & 32768) == 32768)
            {
                state |= KeyStates.Down;
            }
            if ((k & 1) == 1)
            {
                state |= KeyStates.Toggled;
            }
            return state;
        }*/
        public bool OnHotkey(string keystr, string mod = "")
		{
			int VK = GetVK(keystr);
            Dictionary<string, int> mods = new Dictionary<string, int> {
                { "ctrl", 0xA2 }, { "alt", 0x12 }, { "shift", 0xA0 }
            };
            if (VK == 0) return false;
            if (mod != "" && !mods.ContainsKey(mod.ToLower()) || mod == "")
                return (BitConverter.GetBytes(NativeMethods.GetAsyncKeyState(VK))[0] == 1);
            else
                return (BitConverter.GetBytes(NativeMethods.GetAsyncKeyState(VK))[0] == 1 && BitConverter.GetBytes(NativeMethods.GetAsyncKeyState(mods[mod.ToLower()]))[0] == 1);
        }
        public void Close()
		{
			DLL.NativeMethods.Close(Handle);
		}
		#region CharList
		public ArrayList OnlineCharlist()
		{
			for (int i = 1; i <= CliCnt; i++)
			{
				CliNr = i;
				EventMacro(8, 2);
			}
			Thread.Sleep(1000);

			ArrayList _ret = new ArrayList();
			int OriginalNr = CliNr;
			for (int i = 1; i <= CliCnt; i++)
			{
                CliNr = i;
				EventMacro(8, 2);
				int _cID = CharID;
				string _char = CharName;
				if (string.IsNullOrEmpty(_char))
				{
					_char = CharName;
				}
				if (!string.IsNullOrEmpty(_char))
				{
					_ret.Add(CharName);
				}
			}
            CliNr = OriginalNr;
			return _ret;

		}
		#endregion

		#region []FceList Character Selection
		public int ClientIbyCharName(string cName)
		{
			int _ret = 0;
			int OriginalNr = CliNr;
			for (int i = 1; i <= CliCnt; i++)
			{
                CliNr = i;
				int _cID = CharID;
				string _char = CharName;
				if (string.IsNullOrEmpty(_char))
				{
					EventMacro(8, 2);
					_char = CharName;
				}
				if (!string.IsNullOrEmpty(_char))
				{
					_ret = i;
				}
			}
            CliNr = OriginalNr;
			return _ret;
		}
		#endregion
		#region Open/Close [Open provadi konstruktor]
		public bool Open(int clinr = 1)
		{
            if (CliNr < 1)
            {
                Global.GetHandle(true);
            }
            //Global.GetHandle(true); // _UOHandle = DLL.NativeMethods.Open();
			if (DLL.NativeMethods.Version() != 3) { return false; }
			DLL.NativeMethods.SetTop(Handle, 0);
			DLL.NativeMethods.PushStrVal(Handle, "Set");
			DLL.NativeMethods.PushStrVal(Handle, "CliNr");
			DLL.NativeMethods.PushInteger(Handle, clinr);
			if (DLL.NativeMethods.Execute(Handle) != 0)
			{
				return false;
			};
			return true;
		}
        #endregion
        #region Journal
        public JournalScan ScanJournal(int OldRef)
        {
            var results = new List<object>();
            if (_executeCommand(out results, "ScanJournal", OldRef))
            {
                JournalScan jScan = new JournalScan(Convert.ToInt32(results[0]), Convert.ToInt32(results[1]));
                return jScan;
            }
            return null;
        }
        public void ClearJournal()
        {
            journal.Clear();
        }
        /// <summary>
		/// Finds the designated string in new journal entries since last call.
		/// </summary>
		/// <param name="StringToFind"></param>
		/// <returns>True if found, False if not found</returns>
        public bool InJournal(string StringToFind)
        {
            journal.Clear();

            var jf = ScanJournal(journalRef);
            if (jf == null) { return false; }
            if (jf.NewRef > journalRef)
            { // new journal entries
                for (int i = 0; i <= jf.Cnt; ++i)
                {
                    var _getjornal = GetJournal(i);
                    if (_getjornal != null)
                    {
                        journal.Add(_getjornal.Line);
                    }
                        
                }
                journalRef = jf.NewRef;
            }

            if (journal.Where(j => j.ToLower().Contains(StringToFind.ToLower())).Count() > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Finds the designated string in new journal entries since last call.
        /// </summary>
        /// <param name="StringsToFind"></param>
        /// <returns>Returns found string or string.Empty if not found</returns>
        public string GetJournalEntry(string[] StringsToFind)
        {
            journal.Clear(); // Maybe dont clear?
            var jf = ScanJournal(journalRef);
            if (jf.NewRef > journalRef)
            { // new journal entries
                for (int i = journalRef; i < jf.NewRef; i++)
                {
                    journal.Add(GetJournal(i).Line);
                }
                journalRef = jf.NewRef;
            }

            foreach (string s in StringsToFind)
            {
                if (journal.Where(j => j.ToLower().Contains(s.ToLower())).Count() > 0)
                    return s;
            }
            return string.Empty;
        }
        public JournalEntry GetJournal(int index)
        {
            var results = new List<object>();
            if (_executeCommand(out results, "GetJournal", new object[] { index }) && !string.IsNullOrEmpty((string)results[0]))
            {
                return new JournalEntry((string)results[0], 0);
            }    
            return null;
        }
        #endregion
        #region Custom Helper Commands
        /// <summary>
        /// Sleeps for designated time in EasyUO style 10 = 500ms 20 = 1s
        /// </summary>
        /// <param name="Time"></param>
        public void Wait(int Time)
		{
			Thread.Sleep((Time * 100) / 2);
		}
		
		/// <summary>
		/// Waits the designated timeout for a Target cursor, or 2000ms
		/// </summary>
		/// <param name="TimeOutMS"></param>
		/// <returns>True if target cursor is active</returns>
		public bool Target(int TimeOutMS = 3000)
		{
            if (TimeOutMS < 250)
                TimeOutMS = 250;

            long end = Millisec + TimeOutMS;

			while (Millisec < end)
			{
				if (TargCurs)
                    return true;
                			
				Thread.Sleep(250);
			}
            return TargCurs;
		}
        public void ClearIgnoreList()
        {
            IgnoreList.Clear();
        }
        public void IgnoreItem(object item)
        {
            if (item.GetType() == typeof(FoundItem))
                IgnoreList.Add(((FoundItem)item).ID);
            else if (item.GetType() == typeof(int))
                IgnoreList.Add((int)item);
        }
        public FoundItem FindItem(int ID, int containerID = 0)
        {
            ClearFoundVars();

            int itemcnt = ScanItems(true);
            
           // if (ID.GetType() == typeof(int))
            for (int i = 0; i < itemcnt; i++)
            {
                FoundItem item = GetItem(i);
                if (item != null)
                {
                    if (IgnoreList.Contains(item.ID)) continue;
                    if ((item.ID == ID || ID == 0) && (containerID == 0 || item.ContID == containerID))
                    {
                        SetFoundVars(item);
                        return item;
                    }
                }

            }
            return null;
        }
        public FoundItem FindType(object TYPE, int Color = -1, int containerID = 0)
        {
            ClearFoundVars();

            int itemcnt = ScanItems(true);

            if (TYPE.GetType() == typeof(int))
                for (int i = 0; i < itemcnt; i++)
                {
                    FoundItem item = GetItem(i);
                    if (item != null)
                    {
                        if (IgnoreList.Contains(item.ID)) continue;
                        if ((item.Type == (int)TYPE || (int)TYPE == 0) && 
                            (containerID == 0 || item.ContID == containerID) &&
                            (item.Col == Color || Color == -1))
                        {
                            SetFoundVars(item);
                            return item;
                        }                           
                    }
                }
            else if (TYPE.GetType() == typeof(int[]) || TYPE.GetType() == typeof(List<int>))
            {
                for (int i = 0; i < itemcnt; i++)
                {
                    FoundItem item = GetItem(i);
                    if (item == null || IgnoreList.Contains(item.ID)) { continue; }
                    if ((((int[])TYPE).Contains(item.Type)) &&
                        (containerID == 0 || item.ContID == containerID) &&
                        (item.Col == Color || Color == -1))
                    {
                        SetFoundVars(item);
                        return item;
                    }
                }
            }   
            return null;
        }
        public List<FoundItem> FindOnGround(object TYPE, int Color = -1)
        {
            return FindInCont(TYPE, Color, 0);
        }
        public List<FoundItem> FindInCont(object TYPE, int Color = -1, int containerID = -1)
        {
            ClearFoundVars();

            int itemcnt = ScanItems(true);
            if (containerID == -1) { containerID = BackpackID; }
            List<FoundItem> items = new List<FoundItem>();
            if (TYPE.GetType() == typeof(int))
                for (int i = 0; i < itemcnt; i++)
                {
                    FoundItem item = GetItem(i);
                    if (item != null)
                    {
                        if (IgnoreList.Contains(item.ID)) continue;
                        if ((item.Type == (int)TYPE || (int)TYPE == 0) &&
                            (item.ContID == containerID)
                            && (item.Col == Color || Color == -1))
                            items.Add(item);
                    }

                }
            else
                for (int i = 0; i < itemcnt; i++)
                {
                    FoundItem item = GetItem(i);
                    if (item != null)
                    {
                        if (IgnoreList.Contains(item.ID)) continue;
                        if ((((int[])TYPE).Contains(item.Type)) && 
                            (item.ContID == containerID)
                            && (item.Col == Color || Color == -1))
                        {
                            items.Add(item);
                        }
                            
                    }

                }
            if (items.Count > 0)
            {
                SetFoundVars(items[0]);
                return items;
            }
            return null;
                
        }
        public Stats GetStats(int Serial)
        {
            Stats _result = new Stats(-1, "None", 0, -1);
            Container container = new Container();
            int _hp = 0;
            //int index = -1;
            for (int index = 0; index < 100; ++index)
            {
                container = GetCont(index);
                if (container.ID == Serial && (container.Name == "status gump" || container.Name == "reticle gump"))
                {
                    int x, y, c, b, g, r;
                    x = 38 + container.X;
                    y = 43 + container.Y;
                    c = GetPix(x, y);
                    b = (int)Math.Floor((double)(c / 65535));
                    g = (int)Math.Floor((double)((c % 65535)) / 256);
                    r = (int)(Math.Floor((double)(c % 65535)) % 256);
                    string color = "Blue";
                    if (b > r && b > g) { color = "Blue"; }
                    if (g > b && g > r) { color = "Green"; }
                    if (r > b && r > g) { color = "Red"; }

                    //_result.HP = container.HP;
                    _result.Color = color;
                    _result.ContID = container.ID;
                    _result.Index = index;
                    return _result;
                }
            }
            if (_result.Color != "Red")
            {         
                for(int _x = 38; _x < 138; ++_x)
                {
                    int x, y, c, b, g, r;
                    x = _x + container.X;
                    y = 43 + container.Y;
                    c = GetPix(x, y);
                    b = (int)Math.Floor((double)(c / 65535));
                    g = (int)Math.Floor((double)((c % 65535)) / 256);
                    r = (int)(Math.Floor((double)(c % 65535)) % 256);
                    string color = "Blue";
                    if (b > r && b > g) { color = "Blue"; }
                    if (g > b && g > r) { color = "Green"; }
                    if (r > b && r > g) { color = "Red"; }
                    if (color == "Green" || color == "Blue")
                    {
                        _hp++;
                    }
                }
            }
            _result.HP = _hp;
            return _result;
        }
        #endregion
        #region Supported GameDLL Events
        /// <summary>
        /// This method of dragging should be used with a click to drop.
        /// </summary>
        /// <param name="ItemID"></param>
        public void CliDrag(int ItemID)
		{
			// This should be completed with a click to drop, use Drag.
			_executeCommand("CliDrag", ItemID);
		}

		public void Drag(int ItemID, int Amount)
		{
            _executeCommand("Drag", ItemID, Amount);
		}
		public void DropC(int ContID, int X, int Y)
		{
			_executeCommand("DropC", ContID, X, Y);
		}
		public void DropC(int ContID)
		{
			_executeCommand("DropC", ContID);
		}

		public void DropG(int X, int Y, int Z)
		{
			_executeCommand("DropG", X, Y, Z);
		}
		public void ExMsg(int ItemID, int FontID, int Color, string message)
		{
			_executeCommand("ExMsg", ItemID, FontID, Color, message);
		}


		public void Pathfind(int X, int Y, int Z)
		{
			_executeCommand("Pathfind", X, Y, Z);
		}
		public void UseLObject()
		{
			EventMacro(17, 0);
		}
		public PropertyInfo Property(int ItemID, int millisec = 3000)
		{
			PropertyInfo p = new PropertyInfo();
            long timeout = Millisec + millisec;
            int _sleep = (millisec > 250) ? 250 : millisec;
			var o = _executeCommand(true, "Property", new object[] { ItemID });
            while (o == null)
            {
                if (Millisec > timeout)
                    return null;
                Thread.Sleep(_sleep);
            }
			p.Name = Convert.ToString(o[0]);
			p.Info = Convert.ToString(o[1]);
            Global._PropertyName = p.Name;
            Global._PropertyString = p.Info;
			return p;
		}
		public ItemProp GetProperties(int ItemID)
		{
			PropertyInfo p = Property(ItemID);
			ItemProp pdict = new ItemProp();
			string[] plist = p.Info.Split('\n');
			foreach (string s in plist)
			{
				string temp = s.Replace("%", "").Replace(":", "");
				pdict.Name = temp.Substring(0, temp.LastIndexOf(' '));
				pdict.Value = temp.Substring(temp.LastIndexOf(' '));
			}
			return pdict;
		}
        private bool ContainsAny(string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }
        public Dictionary<string, object> GetPropList(int ItemID)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            string[] noValue = new string[] {
                "Insured", "Exceptional", "Crafted", "Blessed", "Imbued",
                "Enchanted", "Heartwood", "Ash", "Oak", "Yew", "Bloodwood",
                "Frostwood", "Weight", "Handed Weapon", "Owned"
            };
            PropertyInfo p = Property(ItemID);
            if (p.Info == null || p.Info == "")
                return null;
            string[] plist = p.Info.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in plist)
            {
                string temp = s.Replace("%", "").Replace(":", "").Replace("+", "").Trim();
                int dpos = temp.IndexOfAny("0123456789".ToCharArray());
                int spc = temp.LastIndexOf(' ');
                if (temp == p.Name || ContainsAny(temp, noValue))
                {
                    res[temp] = true;
                }
                else if (spc > -1)
                {
                    res[temp.Substring(0, spc).Trim()] = temp.Substring(spc).Trim();
                }
                else if (dpos > -1)
                {
                    res[temp.Substring(0, dpos).Trim()] = temp.Substring(dpos).Trim();
                }
                else
                {
                    Sysmessage("Prop Error: " + temp, 33);
                    continue;
                }
            }
            return res;
        }
        public SmallBod GetSmallBod(int ItemID)
        {
            PropertyInfo p = Property(ItemID);
            if (p == null) return null;
            string[] plist = p.Info.ToLower().Split('\n');
            SmallBod b = new SmallBod();
            b.Exceptional = p.Info.ToLower().Contains("exceptional");
            foreach(string prop in plist)
            {
                if (prop.Contains("amount to make"))
                {
                    b.Amount = Convert.ToInt32(prop.Replace("amount to make: ", "").Trim());
                }
                else if (prop.Contains("all items must be made with"))
                {
                    b.Material = prop.Replace("all items must be made with ", "").Replace("ingots.", "").Trim();
                }
            }
            if (b.Material == null)
                b.Material = "iron";
            b.Equip = plist.Last().Split(':')[0];
            return b;
        }
        public SingleSop GetSop(int ItemID)
		{
            SingleSop result = null;
            try
            {
                PropertyInfo p = Property(ItemID);
                if (p != null && p.Name.Contains("Scroll Of"))
                {
                    string[] plist = p.Info.Split('\n');
                    result = new SingleSop
                    {
                        Serial = ItemID,
                        Skill = SOP_SKILLS.Where(sk => p.Name.ToLower().Contains(sk.ToLower())).First(),
                        Value = SOP_VALUES.Where(sv => p.Name.Contains(sv.ToString())).First()
                    };
                    if (plist[plist.Length - 1].Contains("Days Until Deletion"))
                    {
                        result.Days = Convert.ToInt32(plist[plist.Length - 1].Replace("Days Until Deletion", ""));
                    }                     
                    else
                    {
                        result.Days = -1;
                    }
                        
                }
            } catch (Exception)
            {

            }
            return result;
		}
        public SingleSot GetSot(int itemID)
        {
            SingleSot result = null;
            
            try
            {
                PropertyInfo info = Property(itemID);

                if (info == null || !info.Info.ToUpper().Contains("SCROLL OF TRANSCENDENCE") && !info.Name.ToUpper().Contains("SCROLL OF TRANSCENDENCE"))
                {
                    return null;
                }
                string skillText = info.Info.Split('\n').Where(i => i.ToUpper().Contains("SKILL:")).FirstOrDefault();

                if (string.IsNullOrEmpty(skillText))
                {
                    return null;
                }

                var re = new Regex(skillText);
                var match = re.Match(@"\d+(\.\d{1,2})?");

                if (!match.Success)
                {
                    return null;
                }
                if (double.TryParse(match.Value, out double value) && value > 0)
                {
                    result = new SingleSot
                    {
                        Serial = itemID,
                        Value = value
                    };
                    result.Skill = skillText.Replace("Skill: ", "").Replace(value.ToString("F1"), "").Replace("Skill Points", "").Trim();
                }
            } catch (Exception)
            {

            }

            return result;
        }
		public void RenamePet(int ID, string Name)
		{
			_executeCommand("RenamePet", ID, Name);
		}
		public bool WaitForGump(string gump, int TimeOutMS = 3000)
		{
            EGump _gump = (EGump)Enum.Parse(typeof(EGump), gump);
			System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch(); _stopwatch.Start();
			while (_stopwatch.ElapsedMilliseconds < TimeOutMS)
			{
				if (ContKind == (int)_gump)
					return true;
				Thread.Sleep(10);
			}
			return false;
		}
		public void ContextMenu(int ItemID, int x = -1, int y = -1)
		{
			if (x > -1 && y > -1)
				_executeCommand("Popup", ItemID, x, y);
			else
				_executeCommand("Popup",  ItemID);
		}
		public void WaitForContext(int ItemID, int Choice, int TimeOutMS = 3000, string gump = "Context")
		{
			ContextMenu(ItemID);
			WaitForGump(gump, TimeOutMS);
			Click(80, 20 * Choice);
		}
		public int CountType(int ItemID, int Color = -1, int Cont = 0)
		{
            int itemcnt = ScanItems(true);
            if (Cont == 0) { Cont = BackpackID; }
            int Stack = 0;
            for (int i = 0; i < itemcnt; i++)
            {
                FoundItem item = GetItem(i);
                if (item != null)
                {
                    if (IgnoreList.Contains(item.ID))
                        continue;

                    if (item.Type == ItemID && item.ContID == Cont
                        && (item.Col == Color || Color == -1))
                        Stack += item.Stack;
                }

            }
            return Stack;
		}
        #endregion
        #region PropertyRes
        public string PropName { get { return Global._PropertyName; } }
        public string PropStr { get { return Global._PropertyString; } }
        #endregion
        #region Supported GameDLL Commands
        public void Click(int X, int Y, bool Left = true, bool Down = true, bool Up = true, bool Middle = false)
		{
			_executeCommand("Click", X, Y, Left, Down, Up, Middle);
		}


		public FoundItem GetItem(int index)
		{
			DLL.NativeMethods.SetTop(Handle, 0);
			DLL.NativeMethods.PushStrVal(Handle, "Call");
			DLL.NativeMethods.PushStrVal(Handle, "GetItem");
			DLL.NativeMethods.PushInteger(Handle, index);
			if (DLL.NativeMethods.Execute(Handle) != 0)
				return new FoundItem();
            FoundItem item = new FoundItem
            {
                ID = DLL.NativeMethods.GetInteger(Handle, 1),
                Type = DLL.NativeMethods.GetInteger(Handle, 2),
                Kind = DLL.NativeMethods.GetInteger(Handle, 3),
                ContID = DLL.NativeMethods.GetInteger(Handle, 4),
                X = DLL.NativeMethods.GetInteger(Handle, 5),
                Y = DLL.NativeMethods.GetInteger(Handle, 6),
                Z = DLL.NativeMethods.GetInteger(Handle, 7),
                Stack = DLL.NativeMethods.GetInteger(Handle, 8),
                Rep = DLL.NativeMethods.GetInteger(Handle, 9),
                Col = DLL.NativeMethods.GetInteger(Handle, 10)
            };
			return (item.ID > 0 ? item : null);

		}
		public int GetPix(int X, int Y)
		{
            var results = new List<object>();
			if (_executeCommand(out results, "GetPix", X, Y) && results.Count > 0)
            {
                try { return Convert.ToInt32(results[0]); } catch (Exception) { }
            }
            return 0;
		}
		public void HideItem(int ID)
		{
			_executeCommand("HideItem", ID);
		}
		public void Key(string Key, params bool[] mods) // Ctrl, Alt, Shift
        {
			_executeCommand("Key", Key, mods);
		}
		public bool CharMove(int X, int Y, int Accuracy, int TimeoutMS)
		{
            var results = new List<object>();
            if (_executeCommand(out results, "Move", new object[] { X, Y, Accuracy, TimeoutMS }))
            {
                try
                {
                    return bool.Parse((string)results[0]);
                } catch (Exception)
                {
                    return false;
                }
            }
			return false;
		}
		public void Msg(string message, bool send = true)
		{
			_executeCommand("Msg", message);
			if (send) { Key("enter", false, false, false); }
		}
		public void HeadMsg(string message, int Color = 0, int Font = 3)
		{
			ExMsg(CharID, Font, Color, message);
		}
		public void PublicMsg(string message)
		{
			Msg($"[c {message}");
		}
		public void GuildMsg(string message)
		{
			Msg($"[g {message}");
		}
		public void VendorMsg(string message)
		{
			Msg($"[v {message}");
		}
		public void MapMsg(string message)
		{
			Msg($"--{message}");
		}
		public void StatLock(string stat, string status)
		{
			string s = Char.ToUpper(stat[0]) + stat.Substring(1,2);
			string[] stati = { "up", "down", "locked" };
			int st = Array.IndexOf(stati, status);
			_executeCommand("StatLock", s, st);
		}
		public int Distance(int Serial)
		{
			FindItem(Serial);
			if (FoundKind == -1) { return 999; }
			List<int> dist = new List<int> {
				Math.Abs(CharPosX - FoundX),
				Math.Abs(CharPosY - FoundY)
			};
			return dist.Max();
		}
		public bool InRange(int Serial, int Range)
		{
			return Distance(Serial) <= Range;
		}
		public void UseType(int TYPE, int Color = -1, int Cont = 0)
		{
            int itemcnt = ScanItems(true);
            if (Cont == 0) { Cont = BackpackID; }
            int Serial = 0;
            for (int i = 0; i < itemcnt; i++)
            {
                FoundItem item = GetItem(i);
                if (item != null)
                {
                    if (IgnoreList.Contains(item.ID))
                        continue;

                    if (item.Type == TYPE && item.ContID == Cont
                        && (item.Col == Color || Color == -1))
                    {
                        Serial = item.ID;
                        break;
                    }
                }

            }
            LObjectID = Serial;
			EventMacro(17, 0);
		}
		public void UseObject(int ItemID)
		{
			LObjectID = ItemID;
			EventMacro(17, 0);
		}
		public bool IsGhost(int ID)
		{
			FindItem(ID, 0);
			return (FoundType == 402);
		}
		public int ScanItems(bool visibleOnly)
		{
			DLL.NativeMethods.SetTop(Handle, 0);
			DLL.NativeMethods.PushStrVal(Handle, "Call");
			DLL.NativeMethods.PushStrVal(Handle, "ScanItems");
			DLL.NativeMethods.PushBoolean(Handle, visibleOnly);
			if (DLL.NativeMethods.Execute(Handle) != 0)
				return 0;
			return DLL.NativeMethods.GetInteger(Handle, 1);
		}

		public Container GetCont(int index)
		{
            var results = new List<object>();
            _executeCommand(out results, "GetCont", index);
			return new Container(results);

		}
		public void ContTop(int index)
		{
			_executeCommand("ContTop", index);
		}

		//Probably should replace tile commands with openuo rather than uo.dll
		public bool TileInit(bool NoOverRides)
		{
            var results = new List<object>();
            if (_executeCommand(out results, "TileInit", new object[] { NoOverRides }))
            {
                try { return bool.Parse((string)results[0]); } catch (Exception) { return false; }
            }
            return false;

        }
        public int TileCnt(int X, int Y, int facet = -1)
		{
            int _facet = (facet > -1) ? facet : CursKind;
            var results = new List<object>();
            if (TopObject == _facet && _executeCommand(out results, "TileCnt", new object[] { X, Y, _facet }) || _executeCommand(out results, "TileCnt", new object[] { X, Y }))
            {
                return Convert.ToInt16(results[0]);
            }
            return 0;
		}
		public Tile TileGet(int x, int y, int index, int facet = -1)
		{
			int _facet = (facet > -1) ? facet : CursKind;
            var results = new List<object>();
            if (_executeCommand(out results, "TileGet", x, y, index, _facet))
            {
                Tile _resTile = new Tile(results);
                SetTileVars(_resTile);
                return _resTile;
            }
			return null;
		}
        #endregion    
        #region Sysmessage
        public void Sysmessage(string msg, int hue = 0)
		{
			_executeCommand("Sysmessage", msg, hue);
		}

		#endregion
		#region Helpers
		//Executes a GameDLL command, Idea taken from jultima http://code.google.com/p/jultima/
		//A ja to ukrad z UOnet AHAHAHAHA
        public JournalEntry GetJournalTest(int index)
        {
            List<object> Results = new List<object>();
            //IntPtr jindex = (IntPtr)index;
            DLL.NativeMethods.SetTop(Handle, 0);
            DLL.NativeMethods.PushStrVal(Handle, "Call");
            DLL.NativeMethods.PushStrVal(Handle, "GetJournal");
            DLL.NativeMethods.PushInteger(Handle, index);
            if (DLL.NativeMethods.Execute(Handle) != 0) { return null; }
            
            string line = Marshal.PtrToStringAnsi((DLL.NativeMethods.GetString(Handle, 1)));
            int col = DLL.NativeMethods.GetInteger(Handle, 2);
            return new JournalEntry(line, col);

        }
        private bool _executeCommand(string command, params object[] args)
        {
            DLL.NativeMethods.SetTop(Handle, 0);
            DLL.NativeMethods.PushStrVal(Handle, "Call");
            DLL.NativeMethods.PushStrVal(Handle, command);
            if (args != null && args.Length > 0)
            {
                foreach (var o in args)
                {
                    if (o is int)
                    {
                        DLL.NativeMethods.PushInteger(Handle, Convert.ToInt32(o));
                    } else if (o is string)
                    {
                        DLL.NativeMethods.PushStrVal(Handle, Convert.ToString(o));
                    } else if (o is bool)
                    {
                        DLL.NativeMethods.PushBoolean(Handle, Convert.ToBoolean(o));
                    } else if (o is IntPtr)
                    {
                        DLL.NativeMethods.PushPointer(Handle, (IntPtr)o);
                    }
                }
            } 
            return DLL.NativeMethods.Execute(Handle) == 0;
        }
        private bool _executeCommand(out List<object> results, string command, params object[] args)
        {
            DLL.NativeMethods.SetTop(Handle, 0);
            DLL.NativeMethods.PushStrVal(Handle, "Call");
            DLL.NativeMethods.PushStrVal(Handle, command);
            results = null;
            foreach (var o in args)
            {
                if (o is int)
                {
                    DLL.NativeMethods.PushInteger(Handle, Convert.ToInt32(o));
                } else if (o is string)
                {
                    DLL.NativeMethods.PushStrVal(Handle, Convert.ToString(o));
                } else if (o is bool)
                {
                    DLL.NativeMethods.PushBoolean(Handle, Convert.ToBoolean(o));
                } else if (o is IntPtr)
                {
                    DLL.NativeMethods.PushPointer(Handle, (IntPtr)o);
                }
            }
            var hResult = 0;
            try
            {
                hResult = DLL.NativeMethods.Execute(Handle);
            } catch (Exception) { hResult = 1; }
            if (hResult != 0) { return false; }
            try
            {
                int objectcnt = DLL.NativeMethods.GetTop(Handle); //TopObject
                if (objectcnt <= 0) { return false; }
                results = new List<object>();
                for (int i = 1; i <= objectcnt; i++)
                {
                    switch (DLL.NativeMethods.GetType(Handle, i))
                    {
                        case T_BOOLEAN:
                            results.Add(DLL.NativeMethods.GetBoolean(Handle, i).ToString());
                            break;
                        case T_POINTER:
                            results.Add(DLL.NativeMethods.GetPointer(Handle, i));
                            break;
                        case T_NUMBER:
                            results.Add(DLL.NativeMethods.GetInteger(Handle, i).ToString());
                            break;
                        case T_STRING:
                            results.Add(Marshal.PtrToStringAnsi(DLL.NativeMethods.GetString(Handle, i))); // Marshal.PtrToStringAnsi()
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                return results.Count > 0;
            } catch (Exception ex)
            {
                var e = ex;
                return false;
            }
        }
        private List<object> _executeCommand(bool ReturnResults, string CommandName, object[] args)
		{
			// Maybe return bool and results as an Out?
			var results = new List<object>();
			DLL.NativeMethods.SetTop(Handle, 0);
			DLL.NativeMethods.PushStrVal(Handle, "Call");
			DLL.NativeMethods.PushStrVal(Handle, CommandName);
			foreach (var o in args)
			{
				if (o is int)
				{
					DLL.NativeMethods.PushInteger(Handle, Convert.ToInt32(o));
				}
				else if (o is string)
				{
					DLL.NativeMethods.PushStrVal(Handle, Convert.ToString(o));
				}
				else if (o is bool)
				{
					DLL.NativeMethods.PushBoolean(Handle, Convert.ToBoolean(o));
				}
                else if (o is IntPtr)
                {
                    DLL.NativeMethods.PushPointer(Handle, (IntPtr)o);
                }
			}
			if (DLL.NativeMethods.Execute(Handle) != 0) { return null; }
			if (!ReturnResults) { return null; }
            int objectcnt = DLL.NativeMethods.GetTop(Handle); //TopObject
            for (int i = 1; i <= objectcnt; i++)
			{
				int gettype = DLL.NativeMethods.GetType(Handle, i);
                //HeadMsg(String.Format("Res Type: {0}, Res index: {1}", gettype, i), 59);
                switch (gettype)
				{
					case T_BOOLEAN:
						results.Add(DLL.NativeMethods.GetBoolean(Handle, i).ToString());
						break;
                    case T_POINTER:
                        results.Add(DLL.NativeMethods.GetPointer(Handle, i));
                        break;
					case T_NUMBER:
						results.Add(DLL.NativeMethods.GetInteger(Handle, i).ToString());
						break;
					case T_STRING:
						results.Add(Marshal.PtrToStringAnsi(DLL.NativeMethods.GetString(Handle, i))); // Marshal.PtrToStringAnsi()
                        break;
					default:
						throw new NotImplementedException();
						//break;
				}
			}
			return results;
		}
        #endregion
        #region Items
        public void Move(int ItemID, int Amount, int Cont)
        {
            Drag(ItemID, Amount);
            Wait(5);
            DropC(Cont);
        }
        public void MoveOnGround(int ItemID, int Amount, int X, int Y, int Z)
        {
            Drag(ItemID, Amount);
            Wait(5);
            DropG(X, Y, Z);
        }
        #endregion
    }
}
