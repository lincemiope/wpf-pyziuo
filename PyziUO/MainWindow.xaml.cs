using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using PyziUO.Tools;
using PyziUO.Py;
using PyziWrap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PyziUO
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDataReceiver, IDisposable
	{
        private int _CliNr = 1;
        private Wrapper UO;
        private int _PreviousCliCnt = 1;
		private Dictionary<string, Dictionary<string, object>> _Nodes;
		private List<ScriptCtrl> Current;
        //private MenuItem m_mAttach;
        
        public MainWindow()
		{
			InitializeComponent();
            CreateEnv();
            UO = new Wrapper();
			while (!UO.Open()) { Thread.Sleep(1); };
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			Current = new List<ScriptCtrl>();
			TitleAndVersion();
			popNodes();
			popTree();
			popScripts();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
			dispatcherTimer.Start();
		}
        protected override void OnClosing(CancelEventArgs e)
        {
            if (UO != null && UO.Handle != IntPtr.Zero)
            {
                UO.Close();
            }
            base.OnClosing(e);
        }
        private void CreateEnv()
        {
            if (Directory.Exists(Path.Combine(App.AssemblyDirectory, "Scripts")) && Directory.Exists(Path.Combine(App.AssemblyDirectory, "Logs")))
            {
                return;
            }
            try
            {
                var scripts = Path.Combine(App.AssemblyDirectory, "Scripts");
                if (!Directory.Exists(scripts))
                {
                    Directory.CreateDirectory(scripts);
                }
                var logs = Path.Combine(App.AssemblyDirectory, "Logs");
                if (!Directory.Exists(logs))
                {
                    Directory.CreateDirectory(logs);
                }
            } catch (Exception ex)
            {
                Logger.Log("CreateEnv", ex.Message, Logger.LogSector.Internal, Logger.LogType.Exception);
                Application.Current.Shutdown();
            }
        }
		public void SetData(string[] data, int index)
		{
			// use the data that is received
			ScriptItem si = listScripts.Items[index] as ScriptItem;
			si.State = data[0];
			lblStateRecv.Content = data[0];
			if (data[0] == "Idle")
				this.Current[index] = null;            
        }
		private void TitleAndVersion()
		{
			Version ver = Assembly.GetEntryAssembly().GetName().Version;
			this.Title = this.Title + " " + ver.ToString();
			mNewVer.Visibility = (CheckVer.IsOutDated(ver.ToString())) ? Visibility.Visible : Visibility.Hidden;
		}
		private void popNodes()
		{
            UO.CliNr = _CliNr;
            this._Nodes = new Dictionary<String, Dictionary<String, Object>>
			{
				{
					"Character Info", new Dictionary<String, Object>
					{
						{"UO.CharPosX", UO.CharPosX}, {"UO.CharPosY", UO.CharPosY},
						{"UO.CharPosZ", UO.CharPosZ}, {"UO.CharDir", UO.CharDir},
						{"UO.CharGhost", UO.CharGhost },
						{"UO.CharID", UO.CharID}, {"UO.CharType", UO.CharType},
						{"UO.BackpackID", UO.BackpackID}, {"UO.CharStatus", UO.CharStatus}
					}
				},

				{
					"Status Bar", new Dictionary<String, Object>
					{
						{"UO.CharName", UO.CharName}, {"UO.Sex", UO.Sex}, {"UO.Str", UO.Str},
						{"UO.Dex", UO.Dex}, {"UO.Int", UO.Int}, {"UO.Hits", UO.Hits},
						{"UO.MaxHits", UO.MaxHits}, {"UO.Stamina", UO.Stamina}, {"UO.MaxStam", UO.MaxStam},
						{"UO.Mana", UO.Mana}, {"UO.MaxMana", UO.MaxMana}, {"UO.MaxStats", UO.MaxStats},
						{"UO.Luck", UO.Luck}, {"UO.Weight", UO.Weight}, {"UO.MaxWeight", UO.MaxWeight},
						{"UO.DiffWeight", UO.DiffWeight}, {"UO.MinDmg", UO.MinDmg},
						{"UO.MaxDmg", UO.MaxDmg}, {"UO.Followers", UO.Followers}, {"UO.MaxFol", UO.MaxFol},
						{"UO.AR", UO.AR}, {"UO.FR", UO.FR}, {"UO.CR", UO.CR},
						{"UO.PR", UO.PR}, {"UO.ER", UO.ER}, {"UO.TP", UO.TP}, {"UO.Gold", UO.Gold}
					}
				},

				{
					"Container Info", new Dictionary<String, Object>
					{
						{"UO.NextCPosX", UO.NextCPosX}, {"UO.NextCPosY", UO.NextCPosY},
						{"UO.ContPosX", UO.ContPosX}, {"UO.ContPosY", UO.ContPosY},
						{"UO.ContSizeX", UO.ContSizeX}, {"UO.ContSizeY", UO.ContSizeY},
						{"UO.ContKind", UO.ContKind}, {"UO.ContName", UO.ContName},
                        { "UO.ContID", UO.ContID }, {"UO.ContType", UO.ContType}
                        /*, {"UO.ContHP", UO.ContHP}*/
					}
				},

				{
					"Last Action", new Dictionary<String, Object>
					{
						{"UO.LObjectID", UO.LObjectID}, {"UO.LObjectType", UO.LObjectType}, {"UO.LTargetID", UO.LTargetID},
						{"UO.LTargetX", UO.LTargetX}, {"UO.LTargetY", UO.LTargetY}, {"UO.LTargetZ", UO.LTargetZ},
						{"UO.LTargetKind", UO.LTargetKind}, {"UO.LTargetTile", UO.LTargetTile}, {"UO.LLiftedID", UO.LLiftedID},
						{"UO.LLiftedType", UO.LLiftedType}, {"UO.LLiftedKind", UO.LLiftedKind} /*, {"UO.LSkill", UO.LSkill},
						{"UO.LSpell", UO.LSpell}*/
					}
				},
				{
					"Entity Info", new Dictionary<String, Object>
                    {
                        {"UO.FoundID", UO.FoundID }, {"UO.FoundType", UO.FoundType }, {"UO.FoundKind", UO.FoundKind },
                        {"UO.FoundCont", UO.FoundCont }, {"UO.FoundX", UO.FoundX }, {"UO.FoundY", UO.FoundY },
                        {"UO.FoundZ", UO.FoundZ }, {"UO.FoundStack", UO.FoundStack }, {"UO.FoundCol", UO.FoundCol },
                        {"UO.FoundRep", UO.FoundRep }, {"UO.FoundDist", UO.FoundDist }
					}
				},
                {
                    "Property Info", new Dictionary<String, Object>
                    {
                        {"UO.PropName", UO.PropName }, {"UO.PropStr", UO.PropStr }
                    }
                },
                {
					"Extended Info", new Dictionary<String, Object>
					{
						{"UO.SysMsg", UO.SysMsg}, {"UO.CursKind", UO.CursKind}, {"UO.TargCurs", UO.TargCurs}                     
					}
				},  
				{
					"Client Info", new Dictionary<String, Object>
					{
						{"UO.CliCnt", UO.CliCnt}, {"UO.CliNr", UO.CliNr}, {"UO.CliLogged", UO.CliLogged},
						{"UO.CliXRes", UO.CliXRes}, {"UO.CliYRes", UO.CliYRes}, {"UO.CliLeft", UO.CliLeft},
						{"UO.CliTop", UO.CliTop}, {"UO.CliVer", UO.CliVer}, {"UO.CliLang", UO.CliLang},
						{"UO.CliTitle", UO.CliTitle}
					}
				},

				{
					"Combat Info", new Dictionary<String, Object>
					{
						{ "UO.LHandID", UO.LHandID }, { "UO.RHandID", UO.RHandID },
                        { "UO.EnemyHits", UO.EnemyHits }, { "UO.EnemyID", UO.EnemyID }
					}
				},
				{
					"Tile Info", new Dictionary<String, Object>
					{
                        { "UO.TileType", UO.TileType }, { "UO.TileZ", UO.TileZ },
                        { "UO.TileName", UO.TileName }, { "UO.TileFlags", UO.TileFlags },
                        //{ "UO.TileCount", UO.TileCount }
					}
				},
				{
                    "Time Info", new Dictionary<String, Object>
					{
						{ "UO.Time", UO.Time }, { "UO.Date", UO.Date },
						{ "UO.Seconds", UO.Seconds }, { "UO.Millisec", UO.Millisec }
					}
				},
				{
					"Miscellaneous", new Dictionary<String, Object>
					{
						{"UO.Shard", UO.Shard}, /*{"UO.LShard", UO.LShard},*/ {"UO.CursorX", UO.CursorX},
						{"UO.CursorY", UO.CursorY}, {"UO.RNG", UO.RNG} // {"UO.PixCol", UO.PixCol}
					}
				}
			};
		}
		private void listScripts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listScripts.SelectedIndex != -1)
			{
				ScriptItem si = listScripts.SelectedItem as ScriptItem;
				lblStateRecv.Content = si.State;
			}

		}
		private void popTree()
		{
			sideTree.Items.Clear();
			SolidColorBrush hBrush = new SolidColorBrush(); // header foreground
			SolidColorBrush iBrush = new SolidColorBrush(); // item foreground
			SolidColorBrush bBrush = new SolidColorBrush(); //header and item background
			hBrush.Color = Color.FromRgb(245, 245, 245);
			iBrush.Color = Color.FromRgb(237, 237, 237);
			bBrush.Color = Color.FromArgb(0, 0, 0, 0);

			foreach (KeyValuePair<String, Dictionary<String, Object>> cat in _Nodes)
			{
				TreeViewItem CInfo = new TreeViewItem();
                Label hitem = new Label();
                //TextBlock item = new TextBlock();
				hitem.Content = cat.Key;
				CInfo.Tag = cat.Key;
				hitem.Foreground = hBrush;
				CInfo.Header = hitem;
				foreach (KeyValuePair<String, Object> e in cat.Value)
				{
					TextBlock sitem = new TextBlock();
                    sitem.MaxWidth = 170;
                    sitem.TextWrapping = TextWrapping.Wrap;
					sitem.Foreground = iBrush;
					sitem.MouseDown += new MouseButtonEventHandler(TreeItemDClick);
					sitem.Tag = e.Key;
                    sitem.Padding = new Thickness(0, 5, 0, 5);
					sitem.Text = string.Format("{0}: {1}", e.Key, e.Value);
					CInfo.Items.Add(sitem);
				}
				sideTree.Items.Add(CInfo);
			}
		}
		private void popScripts()
		{
			string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\Scripts";
			var dir = new DirectoryInfo(path);
			if (!dir.Exists || !Directory.EnumerateFileSystemEntries(path).Any()) { return; }
			FileInfo[] files = dir.GetFiles("*.py");
			List<ScriptItem> list = new List<ScriptItem>();
			Current = new List<ScriptCtrl>();
			foreach (FileInfo file in files)
			{
				Current.Add(null);
				string _size = (file.Length >= 1024) ? string.Format("{0} KB", Math.Floor(file.Length / 1024.0)) : string.Format("{0} B", file.Length);
				list.Add(new ScriptItem
				{
					Name = file.Name.Replace(".py", ""),
					Size = _size,
					Modify = file.LastWriteTime.ToString("MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
					State = "Idle"
				}
				);
			}
			listScripts.ItemsSource = list;
		}
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			TimerCallBack();
			// Forcing the CommandManager to raise the RequerySuggested event
			CommandManager.InvalidateRequerySuggested();
		}
        private void CliCnt_Click(object sender, RoutedEventArgs e)
        {
            MenuItem _Attach = sender as MenuItem;
            _CliNr = Convert.ToInt32(_Attach.Tag);
            //UO.CliNr = _CliNr;
            popNodes();
        }
        private void AreThereMoreClients()
        {
            if (UO.CliCnt != _PreviousCliCnt)
            {
                foreach (MenuItem mi in mTools.Items)
                {
                    if (mi.Header.ToString() == "Attach")
                    {
                        mTools.Items.Remove(mi);
                        break;
                    }
                }
                if (UO.CliCnt > 1)
                { 
                    MenuItem mAttach = new MenuItem();
                    mAttach.Header = "Attach";
                    //<MenuItem x:Name="mAttach" Header="_Attach" Click="btnAttach_Click"/>
                    for (int i = 1; i <= UO.CliCnt; ++i)
                    {
                        MenuItem attach = new MenuItem();
                        attach.Name = string.Format("sAttach{0}", i);
                        attach.Tag = i;
                        attach.Header = string.Format("Client {0}", i);
                        attach.Click += CliCnt_Click;
                        mAttach.Items.Add(attach);
                    }
                    mTools.Items.Add(mAttach);
                    _PreviousCliCnt = UO.CliCnt;
                }
                else if (UO.CliCnt == 1)
                {
                    UO.CliNr = 1;
                }
            }
        }
		private void TimerCallBack()
		{
			popNodes();
            //RecursiveThreadCheck();
            AreThereMoreClients();

            foreach (TreeViewItem itm in sideTree.Items)
			{
                string cat = itm.Tag.ToString();
				foreach (TextBlock lbl in itm.Items)
				{
					try
					{
						String tag = lbl.Tag.ToString();
                        if (tag.Contains("Type") || tag.Contains("ID") || tag.Contains("FoundCont"))
                            lbl.Text = string.Format("{0}: 0x{1:x}", tag, this._Nodes[cat][tag]);
                        else
                            lbl.Text = string.Format("{0}: {1}", tag, this._Nodes[cat][tag]);
					}
					catch (Exception ex)
					{
						ReportEx(ex);
                        Logger.Log("CreateEnv", ex.Message, Logger.LogSector.Internal, Logger.LogType.Exception);
                        break;
					}
				}

			}
		}
        private void ReportEx(Exception ex)
        {
            string _trace = ex.StackTrace;
            if (_trace.Contains("\\"))
            {
                string[] stacks = _trace.Split('\\');
                List<string> lines = new List<string>();
                foreach (string stack in stacks)
                {
                    if (stack.Length > 0 && stack.Contains("line") && stack.Contains("\n"))
                    {
                        string tmp = stack.Substring(0, stack.IndexOf('\n'));
                        lines.Add(tmp);
                    }
                }
                if (lines.Count > 0)
                    _trace = string.Join("; ", lines.ToArray());
            }
            string errormsg = string.Format("Source={0}\nMessage={1}\nCode Line(s)={2}",
                ex.Source, ex.Message, _trace);
            MessageBoxResult result = MessageBox.Show(errormsg,
                "Exception",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
		#region Buttons
		private void btnNew_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Window w = new PyziIDE();
			w.Show();

		}
        private void btnEdit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string spath = "";
            if (listScripts.SelectedIndex != -1)
            {
                int index = listScripts.SelectedIndex;
                ScriptItem si = listScripts.SelectedItem as ScriptItem;
                if (si.State == "Idle")
                {
                    spath = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + si.Name + ".py";
                    Window w = new PyziIDE(spath);
                    w.Show();
                }
            }
        }
        private void btnPlay_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (listScripts.SelectedIndex != -1)
			{
				int index = listScripts.SelectedIndex;
				ScriptItem si = listScripts.SelectedItem as ScriptItem;
				if (si.State == "Idle")
				{
					si.State = "Playing";
					lblStateRecv.Content = si.State;
					string spath = AppDomain.CurrentDomain.BaseDirectory + "\\Scripts\\" + si.Name + ".py";
					string sbody = File.ReadAllText(spath);
					this.Current[index] = new ScriptCtrl(sbody.Split(new Char[] { '\r', '\n' }), this, _CliNr, index);
					this.Current[index].DoSomeWork();
				}
			}
		}

		private void btnStop_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (listScripts.SelectedIndex != -1)
			{
				int index = listScripts.SelectedIndex;
				ScriptItem si = listScripts.SelectedItem as ScriptItem;
				if (si.State == "Playing")
				{
					si.State = "Idle";
					lblStateRecv.Content = si.State;
					this.Current[index].Terminate();
					Thread.Sleep(1000);
				}
			}
		}
		private void TreeItemDClick(object sender, MouseButtonEventArgs e)
		{
            if (e.ClickCount < 2)
                return;
			TextBlock i = (TextBlock)sender;
            int diacritic = i.Text.IndexOf(':');
            string _resultstr = (i.Text.Length > diacritic + 1) ? i.Text.Substring(diacritic + 1) : "";

            string value = _resultstr.Trim();
			if (!(value is null) && value is string)
				Clipboard.SetText(value);
		}
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			while (!UO.Open()) { Thread.Sleep(1); };
		}
        #region Game Target Info
        public static int EntityInfoTimer_Timeout;
        public static int TileInfoTimer_Timeout;
        public static int HideItemTimer_Timeout;

        private void btnHideItem_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer t = new DispatcherTimer();
            HideItemTimer_Timeout = 0;
            UO.LTargetID = 0;
            UO.TargCurs = true;
            t.Tick += new EventHandler(HideItemTimer_Tick);
            t.Interval = new TimeSpan(0, 0, 1);
            t.Start();
        }
        private void HideItemTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer t = (DispatcherTimer)sender;
            if (HideItemTimer_Timeout >= 5)
            {
                UO.TargCurs = false;
                t.Stop();
                return;
            }
            if (UO.TargCurs)
            {
                HideItemTimer_Timeout++;
            }
            else if (UO.LTargetID != 0)
            {
                HideItemTimer_Timeout = 0;
                UO.HideItem(UO.LTargetID);
                t.Stop();
            }
        }
        private void btnEntityInfo_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer t = new DispatcherTimer();
            EntityInfoTimer_Timeout = 0;
            UO.LTargetID = 0;
            UO.TargCurs = true;
            t.Tick += new EventHandler(EntityInfoTimer_Tick);
            t.Interval = new TimeSpan(0, 0, 1);
            t.Start();
        }
        private void EntityInfoTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer t = (DispatcherTimer)sender;
            if (EntityInfoTimer_Timeout >= 5)
            {
                UO.TargCurs = false;
                t.Stop();
                return;
            }
            if (UO.TargCurs)
            {
                EntityInfoTimer_Timeout++;
            }
            else if (UO.LTargetID != 0)
            {
                EntityInfoTimer_Timeout = 0;
                UO.FindItem(UO.LTargetID);
                UO.Property(UO.LTargetID);
                t.Stop();
            }            
        }
        private void btnTileInfo_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
		{
			foreach (ScriptCtrl w in this.Current)
				if (w != null)
					w.Terminate();
			Thread.Sleep(250);
			popScripts();
		}

		private void mExit_Click(object sender, RoutedEventArgs e)
		{
			foreach (ScriptCtrl w in this.Current)
				if (w != null)
					w.Terminate();
			Thread.Sleep(250);
			Application.Current.Shutdown();
		}

		private void mWiki_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://pyziuo.000webhostapp.com/wiki/doku.php/start");
		}
		private void mNewVer_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.uodguildutils.com/pyziuo/PyziUO.7z");
		}
		#endregion
		#region Internal Classes
        public class ABGWorker : BackgroundWorker
        {
            private Thread workerThread;

            protected override void OnDoWork(DoWorkEventArgs e)
            {
                workerThread = Thread.CurrentThread;
                try
                {
                    base.OnDoWork(e);
                }
                catch (ThreadAbortException ex)
                {
                    e.Cancel = true; //We must set Cancel property to true!
                    Thread.ResetAbort(); //Prevents ThreadAbortException propagation
                    Logger.Log("Script Thread", ex.Message, Logger.LogSector.Internal, Logger.LogType.Exception);
                }
            }
            public bool WorkerIsNull
            {
                get { return (workerThread == null); }
            }
            public void Abort()
            {
                if (workerThread != null)
                {
                    workerThread.Abort();
                    workerThread = null;
                }
            }
        }
		public class ScriptCtrl : IDisposable
		{
			private bool _stop;
			private string[] mScript;
			private int recv_index, mCliNr;
            private BackgroundWorker mWorker;
			private IDataReceiver mReceiver;
			private ScriptEngine pyEngine = null;
			private ScriptScope pyScope = null;
			private SimpleLogger mLogger = new SimpleLogger();

			public ScriptCtrl(string[] script, IDataReceiver receiver, int clinr, int index)
			{
				_stop = false;
				mScript = script;
				//_uo.Open();
				recv_index = index;
                mCliNr = clinr;
				pyInit();
                mWorker = new BackgroundWorker();
                mWorker.DoWork += new DoWorkEventHandler(Worker_DoWork);
				mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
				mWorker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
				mWorker.WorkerReportsProgress = true;
				mReceiver = receiver;
			}
			void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
			{
				mReceiver.SetData(new string[] 
                { e.UserState.ToString() }, this.recv_index);
			}

			public void DoSomeWork()
			{
				// start the worker
				mWorker.RunWorkerAsync();
			}

			private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
			{
				// call method to pass data to receiver
				mReceiver.SetData(new string[] { e.Result.ToString() }, this.recv_index);
			}

			private void Worker_DoWork(object sender, DoWorkEventArgs e)
			{
				// do some work here
				// assign the resulting data to e.Result
				mWorker.ReportProgress(0, "Playing");
				CompileSourceAndExecute();
				e.Result = "Idle";
			}

			public void Terminate()
			{
                if (mWorker.IsBusy)
                {
                    this._stop = true;
                    pyScope.SetVariable("Ctrl_Stopped", this._stop);
                    //_worker.Abort();
                    mWorker.Dispose();
                    mWorker = null;
                }		
			}
			private void pyInit() //initialize python, pythonic way
			{
				if (pyEngine == null)
				{
					pyEngine = Python.CreateEngine();
					pyEngine.SetSearchPaths(new[] { AppDomain.CurrentDomain.BaseDirectory });
					pyScope = pyEngine.CreateScope();
					pyScope.SetVariable("log", mLogger);
					mLogger.AddInfo("Python Initialized");
				}
			}
			private void CompileSourceAndExecute()
			{
				string code = Scriptify(mScript);
				// Executes in the scope of Python
				try
				{
					ScriptSource source = pyEngine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
					CompiledCode compiled = source.Compile();
					pyScope.SetVariable("Ctrl_Stopped", _stop);
					compiled.Execute(pyScope);
				}
				catch (Exception ex)
				{
                    string _trace = ex.StackTrace;
                    if (_trace.Contains("\\"))
                    {
                        string[] stacks = _trace.Split('\\');
                        List<string> lines = new List<string>();
                        foreach (string stack in stacks)
                        {
                            if (stack.Length > 0 && stack.Contains("line") && stack.Contains("\n"))
                            {
                                string tmp = stack.Substring(0, stack.IndexOf('\n'));
                                lines.Add(tmp);
                            }
                        }
                        if (lines.Count > 0)
                            _trace = string.Join("; ", lines.ToArray());
                    }
                    string errormsg = string.Format("Source={0}\nMessage={1}\nCode Line(s)={2}",
                     ex.Source, ex.Message, _trace);
                    MessageBoxResult result = MessageBox.Show(errormsg,
                        "Exception",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
			}
			private string Scriptify(string[] script)
			{
                List<string> res_script = new List<string>
                {
                    "import clr",
                    "clr.AddReference('PyziWrap')",
                    "from PyziWrap import Wrapper",
                    "UO = Wrapper()",
                    $"UO.CliNr = {mCliNr}"
                };
                //res.AddRange(script);
                //List<string> res_script = new List<string>();
                for (int r = 0; r < script.Length; ++r)
				{
                    string res = script[r];
                    if (string.IsNullOrEmpty(res))
                    {
                        continue;
                    }
					res_script.Add(res);
					if ((res.StartsWith("for ") || res.StartsWith("while ")) && res.EndsWith(":"))
					{
						string spc = "    ";
						for (int i = 0; i < res.Length - res.TrimStart().Length; ++i) { spc += " "; }
						res_script.Add($"{spc}if Ctrl_Stopped:");
                        res_script.Add($"{spc}    UO = None");
                        res_script.Add($"{spc}    break");
                    }
				}
				return string.Join("\n", res_script);
			}

			#region IDisposable Support
			private bool disposedValue = false;

			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue)
				{
					if (disposing)
					{
                        mWorker.Dispose();
                        mLogger.Dispose();
					}
					disposedValue = true;
				}
			}
			public void Dispose()
			{
				Dispose(true);
			}
			#endregion
		}

		#region IDisposable Support
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
                    if (UO != null && UO.Handle != IntPtr.Zero)
                    {
                        UO.Close();
                    } 
				}
				disposedValue = true;
			}
		}
		public void Dispose()
		{
			Dispose(true);
		}
        #endregion
        #endregion     
    }

    #region Other Classes
    public interface IDataReceiver
	{
		void SetData(string[] data, int index);
	}
	public class ScriptItem
	{
		public string Name { get; set; }
		public string Size { get; set; }
		public string Modify { get; set; }
		public string State { get; set; }
		//public Button Action { get; set; }
	}
	#endregion
}
