using Disboard.Exceptions;
using Disboard.Misskey;
using MisskeyDriveSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MisskeyDriveSync.Services
{
	public static class MisskeyService
	{
		public static async Task CreateApp(MisskeyClient client)
		{
			var app = await client.App.CreateAsync(
				"MisskeyDriveSync",
				"MisskeyDrive sync app for Windows Desktop.",
				new[] { "drive-read", "drive-write" },
				null);

			client.ClientId = app.Id;
		}

		public static async Task<Disboard.Misskey.Models.User> Authorize(MisskeyClient client)
		{
			if (client.ClientSecret == null)
				throw new ApplicationException("ClientSecret is empty");

			var session = await client.Auth.Session.GenerateAsync();
			System.Diagnostics.Process.Start(session.Url);

			// polling user-key
			while (true)
			{
				try
				{
					var credential = await client.Auth.Session.UserKeyAsync(session.Token);
					if (credential.AccessToken != null)
					{
						return credential.User;
					}
				}
				catch (DisboardException ex)
				{
					if (ex.StatusCode != HttpStatusCode.BadRequest || ex.Message != "this session is not allowed yet")
					{
						throw;
					}
				}
				await Task.Delay(1000);
			}
		}

		private static async Task<MisskeyDriveTreeNode> Scan1(MisskeyClient client, MisskeyDriveTreeNode currentNode = null, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			currentNode = currentNode ?? new MisskeyDriveTreeNode();

			// - folder

			currentNode.Folder = currentFolder;

			// - children

			var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

			// serial
			foreach (var folder in folders)
			{
				var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
				currentNode.Children.Add(childNode);
				await Scan1(client, childNode, folder);
			}

			// - files

			currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();

			return currentNode;
		}

		private static async Task<MisskeyDriveTreeNode> Scan2(MisskeyClient client, MisskeyDriveTreeNode currentNode = null, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			currentNode = currentNode ?? new MisskeyDriveTreeNode();

			// - folder

			currentNode.Folder = currentFolder;

			// - children

			var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

			// concurrent
			await Task.WhenAll(
				folders.Select(async folder =>
				{
					var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
					currentNode.Children.Add(childNode);
					await Scan2(client, childNode, folder);
				}));

			// - files

			currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();

			return currentNode;
		}

		private static async Task<MisskeyDriveTreeNode> Scan3(MisskeyClient client, MisskeyDriveTreeNode currentNode = null, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			currentNode = currentNode ?? new MisskeyDriveTreeNode();

			// - folder

			currentNode.Folder = currentFolder;

			// - children

			var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

			// concurrent(Task.Run)
			await Task.WhenAll(
				folders.Select(folder => Task.Run(async () =>
				{
					var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
					currentNode.Children.Add(childNode);
					await Scan3(client, childNode, folder);
				})));

			// - files

			currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();

			return currentNode;
		}

		private static async Task<MisskeyDriveTreeNode> Scan4(MisskeyClient client, MisskeyDriveTreeNode currentNode = null, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			currentNode = currentNode ?? new MisskeyDriveTreeNode();

			// - folder

			currentNode.Folder = currentFolder;

			// - children

			var t1 = Task.Run(async () =>
			{
				var folders = await fetchAllFolders(client, currentFolder?.Id);

				// concurrent(Task.Run)
				await Task.WhenAll(
					folders.Select(folder => Task.Run(async () =>
					{
						var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
						currentNode.Children.Add(childNode);
						await Scan4(client, childNode, folder);
					})));
			});

			// - files

			var t2 = Task.Run(async () =>
			{
				currentNode.Files = (await fetchAllFiles(client, currentFolder?.Id)).ToList();
			});

			await Task.WhenAll(new[] { t1, t2 });

			return currentNode;
		}

		public static Task<MisskeyDriveTreeNode> Scan(MisskeyClient client, MisskeyDriveTreeNode currentNode = null, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			return Scan4(client, currentNode, currentFolder);
		}

		private static async Task<List<Disboard.Misskey.Models.Folder>> fetchAllFolders(MisskeyClient client, string folderId = null)
		{
			var folders = new List<Disboard.Misskey.Models.Folder>();
			string cursor = null;
			while (true)
			{
				var foldersPart = (await client.Drive.FoldersAsync(folderId: folderId, limit: 100, untilId: cursor)).ToList();
				if (foldersPart.Count > 0)
				{
					cursor = foldersPart[foldersPart.Count - 1].Id;
					folders.AddRange(foldersPart);
					//await Task.Delay(100);
				}
				else
				{
					cursor = null;
				}

				if (cursor == null || foldersPart.Count != 100)
				{
					break;
				}
			}

			return folders;
		}

		private static async Task<List<Disboard.Misskey.Models.File>> fetchAllFiles(MisskeyClient client, string folderId = null)
		{
			var files = new List<Disboard.Misskey.Models.File>();
			string cursor = null;
			while (true)
			{
				var filesPart = (await client.Drive.FilesAsync(folderId: folderId, limit: 100, untilId: cursor)).ToList();
				if (filesPart.Count > 0)
				{
					cursor = filesPart[filesPart.Count - 1].Id;
					files.AddRange(filesPart);
					//await Task.Delay(100);
				}
				else
				{
					cursor = null;
				}

				if (cursor == null || filesPart.Count != 100)
				{
					break;
				}
			}

			return files;
		}

		private static void writeLineIndent(string message, int indentLevel = 0)
		{
			Console.WriteLine(new String(' ', indentLevel * 2) + message);
		}

		public static void ShowNodeTree(MisskeyDriveTreeNode nodeTree, int indentLevel = 0)
		{
			if (!nodeTree.IsRoot)
			{
				writeLineIndent($"Folder: {nodeTree.Folder.Id} ({nodeTree.Folder.Name})", indentLevel);
			}

			foreach (var file in nodeTree.Files)
			{
				//WriteLineIndent($"File: {file.Name}", indentLevel + 1);
			}

			foreach (var childNode in nodeTree.Children)
			{
				ShowNodeTree(childNode, indentLevel + 1);
			}
		}

		private static WebClient webClient = new WebClient();

		/// <returns>ファイルパス</returns>
		public static async Task<string> DownloadFile(Disboard.Misskey.Models.File file)
		{
			if (!Directory.Exists("misskey-drive"))
				Directory.CreateDirectory("misskey-drive");

			var fileName = $@"misskey-drive/{file.Id}.{file.Name}";
			if (File.Exists(fileName)) throw new Exception("already exists filename");
			await webClient.DownloadFileTaskAsync(file.Url, fileName);

			return fileName;
		}
	}
}
