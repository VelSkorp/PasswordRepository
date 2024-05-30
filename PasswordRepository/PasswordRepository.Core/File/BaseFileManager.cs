﻿using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PasswordRepository.Core
{
	/// <summary>
	/// Handles reading/writing and querying the file system
	/// </summary>
	public class BaseFileManager : IFileManager
	{
		/// <summary>
		/// Writes the text to the specified file 
		/// </summary>
		/// <param name="text">The text to write</param>
		/// <param name="path">The path oF the file to write to</param>
		/// <param name="append">If true writes text to the end of the file, otherwise overrides any existing file</param>
		/// <returns></returns>
		public async Task WriteTextToFileAsync(string text, string path, bool append = false)
		{
			// Normalize path
			path = NormalizePath(path);

			// Resolve to absolute path
			path = ResolvePath(path);

			// Lock the Task
			await AsyncAwaiter.AwaitAsync(nameof(WriteTextToFileAsync) + path, async () =>
			 {
				 // Run the synchronous file access as a new task
				 await CoreDI.Task.Run(() =>
				 {
					 // Write the log message to file
					 using (var fileStream = (TextWriter)new StreamWriter(File.Open(path, append ? FileMode.Append : FileMode.Create)))
						 fileStream.Write(text);
				 });
			 });
		}

		/// <summary>
		/// Normalizing a path based on the current operating system
		/// </summary>
		/// <param name="path">The path to normalize</param>
		/// <returns></returns>
		public string NormalizePath(string path)
		{
			// If on Windows...
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				// Replace any / with \
				return path.Replace('/', '\\').Trim();
			// If on Linux/Mac
			else
				// Replace any \ with /
				return path.Replace('\\', '/').Trim();
		}

		/// <summary>
		/// Resolves any relative elements of the path to absolute
		/// </summary>
		/// <param name="path">The path to resolve</param>
		/// <returns></returns>
		public string ResolvePath(string path)
		{
			// Resolve the path to absolute
			return Path.GetFullPath(path);
		}
	}
}