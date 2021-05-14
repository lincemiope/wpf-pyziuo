using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PyziUO
{
    public partial class PyziIDE : Window
	{
		private string _startingscript;
		private string _filename;
		public PyziIDE()
		{
			_startingscript = "";
			_filename = "NewScript";
			InitializeComponent();	
			Loaded += Window_Initialized;
		}
		public PyziIDE(string path)
		{
			InitializeComponent();
			_startingscript = File.ReadAllText(path);
			_filename = GetName(path);
			Loaded += Window_Initialized;
		}
		private string GetName(string path)
		{
			string fn = path.Substring(path.LastIndexOf('\\') + 1);
			if (fn.EndsWith(".py"))
				fn = fn.Substring(0, fn.Length - 3);
			return fn;
		}
		private void Window_Initialized(object sender, EventArgs e)
		{
			scriptEditor.Text = _startingscript;
		}
		public static void UseDefaultExtAsFilterIndex(FileDialog dialog)
		{
			var ext = "*." + dialog.DefaultExt;
			var filter = dialog.Filter;
			var filters = filter.Split('|');
			for (int i = 1; i < filters.Length; i += 2)
			{
				if (filters[i] == ext)
				{
					dialog.FilterIndex = 1 + (i - 1) / 2;
					return;
				}
			}
		}
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string name = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + _filename + ".py";
			StringBuilder res_script = new StringBuilder();
			string[] script = scriptEditor.Text.Split('\n');
			foreach (string s in script)
			{
				res_script.Append(s.Replace("\t", "    ")).Append('\n');
			}
			File.WriteAllText(name, res_script.ToString());
		}
		private void btnSaveAs_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sdlg = new SaveFileDialog();
			sdlg.FileName = _filename;
			sdlg.DefaultExt = "*.py";
			sdlg.Filter = "Python|*.py";
			string path = AppDomain.CurrentDomain.BaseDirectory + "Scripts";
			var dir = new DirectoryInfo(path);
			if (dir.Exists)
				sdlg.InitialDirectory = path;
			else
				sdlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
			//scriptEditor.Text = path;
			UseDefaultExtAsFilterIndex(sdlg);

			if (sdlg.ShowDialog() == true)
			{
				string res_script = "";
				string[] script = scriptEditor.Text.Split('\n');
				foreach (string s in script)
				{
					res_script += s.Replace("\t", "    ") + '\n';
				}
				File.WriteAllText(sdlg.FileName, res_script);
				_filename = GetName(sdlg.FileName);
			}

		}

		private void btnLoad_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog odlg = new OpenFileDialog();
			odlg.FileName = "newscript";
			odlg.DefaultExt = ".py";
			odlg.Filter = "Python|*.py";
			string path = AppDomain.CurrentDomain.BaseDirectory + "Scripts";
			var dir = new DirectoryInfo(path);
			if (dir.Exists)
				odlg.InitialDirectory = path;
			else
				odlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

			UseDefaultExtAsFilterIndex(odlg);

			if (odlg.ShowDialog() == true)
				scriptEditor.Text = File.ReadAllText(odlg.FileName);

		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			scriptEditor.Text = "";
		}

		private void scriptEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyboardDevice.IsKeyDown(Key.LeftCtrl)) && e.KeyboardDevice.IsKeyDown(Key.S))
			{
				e.Handled = true;
				string name = AppDomain.CurrentDomain.BaseDirectory + "Scripts\\" + _filename;
				string res_script = "";
				string[] script = scriptEditor.Text.Split('\n');
				foreach (string s in script)
				{
					res_script += s.Replace("\t", "    ") + '\n';
				}
				File.WriteAllText(name, res_script);
			}
		}
	}
}
