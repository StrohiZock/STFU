﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace STFU.UploadLib.Automation
{
	public class PathInformation
	{
		public string Path { get; set; }
		public string Filter { get; set; }
		public bool SearchRecursively { get; set; }
		public int SelectedTemplateId { get; set; }

		public int? GetDifference(string pathToCheck)
		{
			int? result = null;

			DirectoryInfo directory = new DirectoryInfo(Path);
			FileInfo file = new FileInfo(pathToCheck);

			if (Matches(file, directory))
			{
				result = 0;
			}
			else if (SearchRecursively)
			{
				DirectoryInfo current = file.Directory;

				if (Matches(file, current))
				{
					// Datei wird durch den Filter rekursiv gefunden.
					result = 0;

					while (System.IO.Path.GetFullPath(current.FullName).ToLower() != System.IO.Path.GetFullPath(directory.FullName).ToLower() 
						&& current.Parent != null)
					{
						result++;
						current = current.Parent;
					}
				}
			}

			return result;
		}

		private bool Matches(FileInfo file, DirectoryInfo current)
		{
			var found = FilterDirs(current, file, Filter, SearchOption.TopDirectoryOnly);
			return found.Any(fd => fd.DirectoryName.ToLower() == file.DirectoryName.ToLower() && fd.Name.ToLower() == file.Name.ToLower());
		}

		private FileInfo[] FilterDirs(DirectoryInfo info, FileInfo file, string filter, SearchOption option)
		{
			string[] filters = filter.Split(';');
			List<FileInfo> results = new List<FileInfo>();

			foreach (var fil in filters)
			{
				results.AddRange(info.GetFiles(fil, option));
			}

			return results.ToArray();
		}
	}
}
