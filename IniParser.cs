using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileChecksum
{
	public class IniParser
	{
		private readonly Dictionary<string, Dictionary<string, string>> sections;

		private readonly string path;

		/// <summary>
		/// Opens the INI file at the given path and enumerates the values in the IniParser.
		/// </summary>
		/// <param name="path">Full path to INI file.</param>
		public IniParser(string path)
		{
			sections = new Dictionary<string, Dictionary<string, string>>();

			this.path = path;

			if (File.Exists(path))
			{
				using (var sr = new StreamReader(path))
				{
					string currentRoot = null;

					string line;
					while ((line = sr.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(line) && !line.StartsWith(";"))
						{
							if (line.StartsWith("[") && line.EndsWith("]"))
							{
								currentRoot = line.Substring(1, line.Length - 2).ToUpper();
							}
							else
							{
								if (string.IsNullOrEmpty(currentRoot))
								{
									currentRoot = "ROOT";
								}

								Dictionary<string, string> section;
								if (!sections.TryGetValue(currentRoot, out section))
								{
									section = new Dictionary<string, string>();
									sections.Add(currentRoot, section);
								}

								var keyPair = line.Split(new char[] { '=' }, 2);
								section[keyPair[0].ToUpper()] = keyPair.Length > 1 ? keyPair[1] : string.Empty;
							}
						}
					}
				}
			}
		}

		public bool HasSetting(string section, string setting)
		{
			if (sections.TryGetValue(section, out var temp))
			{
				return temp.ContainsKey(setting);
			}

			return false;
		}

		/// <summary>
		/// Returns the value for the given section, key pair.
		/// </summary>
		/// <param name="section">Section name.</param>
		/// <param name="setting">Key name.</param>
		public string GetSetting(string section, string setting)
		{
			return sections[section][setting];
		}

		/// <summary>
		/// Enumerates all lines for given section.
		/// </summary>
		public IEnumerator<string> EnumerateSections()
		{
			return sections.Select(kv => kv.Key).GetEnumerator();
		}

		public IEnumerator<string> EnumerateSectionSettings(string section)
		{
			return sections[section].Select(kv => kv.Key).GetEnumerator();
		}

		/// <summary>
		/// Adds or replaces a setting to the table to be saved with a null value.
		/// </summary>
		/// <param name="section">Section to add under.</param>
		/// <param name="setting">Key name to add.</param>
		public void AddSetting(string section, string setting)
		{
			AddSetting(section, setting, string.Empty);
		}

		/// <summary>
		/// Adds or replaces a setting to the table to be saved.
		/// </summary>
		/// <param name="section">Section to add under.</param>
		/// <param name="setting">Key name to add.</param>
		/// <param name="value">Value of key.</param>
		public void AddSetting(string section, string setting, string value)
		{
			section = section.ToUpper();
			setting = setting.ToUpper();

			if (!sections.TryGetValue(section, out var temp))
			{
				temp = new Dictionary<string, string>();
				sections.Add(section, temp);
			}

			temp[setting] = !string.IsNullOrEmpty(value) ? value : string.Empty;
		}

		/// <summary>
		/// Remove a setting.
		/// </summary>
		/// <param name="section">Section to add under.</param>
		public void DeleteSection(string section)
		{
			sections.Remove(section.ToUpper());
		}

		/// <summary>
		/// Remove a setting.
		/// </summary>
		/// <param name="section">Section to add under.</param>
		/// <param name="setting">Key name to add.</param>
		public void DeleteSetting(string section, string setting)
		{
			section = section.ToUpper();
			setting = setting.ToUpper();

			if (!sections.TryGetValue(section, out var temp))
			{
				temp = new Dictionary<string, string>();
				sections.Add(section, temp);
			}

			temp.Remove(setting);
		}

		/// <summary>
		/// Save settings back to ini file.
		/// </summary>
		public void SaveSettings()
		{
			SaveSettings(path);
		}

		/// <summary>
		/// Save settings to new file.
		/// </summary>
		/// <param name="path">New file path.</param>
		public void SaveSettings(string path)
		{
			var sb = new StringBuilder();
			foreach (var section in sections)
			{
				sb.AppendLine("[" + section.Key + "]");
				foreach (var val in section.Value)
				{
					sb.AppendLine(val.Key + "=" + val.Value);
				}
			}

			using (var sw = new StreamWriter(path))
			{
				sw.Write(sb.ToString());
			}
		}
	}
}