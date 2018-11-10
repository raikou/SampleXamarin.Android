using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Dropbox.Api;
using Dropbox.Api.Files;
using SampleMoveToOtherView.Common;

namespace SampleMoveToOtherView.Dropbox
{
	public class dropBoxControl
	{

		public async Task Run()
		{
			//https://dropbox.github.io/dropbox-sdk-dotnet/html/Methods_T_Dropbox_Api_Files_Routes_FilesUserRoutes.htm
			//https://www.dropbox.com/developers/documentation/dotnet#tutorial

			using (var dbx = new DropboxClient(HiddenData.accessTokenForDropbox))
			{
				//var full = await dbx.Users.GetCurrentAccountAsync();
				//Console.WriteLine("WriteLine");
				//Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);

				ListRootFolder(dbx);
				Upload(dbx, "test", "test.txt", "aaaaa\r\n");
				//Download(dbx, "test", "test.txt");
			}
		}

		async Task ListRootFolder(DropboxClient dbx)
		{
			var list = await dbx.Files.ListFolderAsync(string.Empty);

			// show folders then files
			foreach (var item in list.Entries.Where(i => i.IsFolder))
			{
				Console.WriteLine("WriteLine");
				Console.WriteLine("D  {0}/", item.Name);
			}

			foreach (var item in list.Entries.Where(i => i.IsFile))
			{
				Console.WriteLine("WriteLine");
				Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
			}
		}

		public async Task Download(DropboxClient dbx,string folder, string file)
		{
			Console.WriteLine("ダウンロード");
			try
			{
				string path = "";
				if (string.IsNullOrWhiteSpace(folder) == false)
				{
					path = "/" + folder;
				}
				path += "/" + file;
				using (var response = await dbx.Files.DownloadAsync(path))
				{
					Console.WriteLine("WriteLine:Download");
					var data = await response.GetContentAsStringAsync();
					Console.WriteLine("Download:" + data);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("エラー："+e);
			}
			Console.WriteLine("ダウンロード-END");
		}

		public async Task Upload(DropboxClient dbx, string folder, string file, string content)
		{
			Console.WriteLine("アップロード");
			try
			{
				using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
				{
					var updated = await dbx.Files.UploadAsync(
						"/" + folder + "/" + file,
						WriteMode.Overwrite.Instance,
						body: mem);
					Console.WriteLine("WriteLine:Upload");
					Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("エラー："+e);
			}
			Console.WriteLine("アップロード-END");
		}

	}
}