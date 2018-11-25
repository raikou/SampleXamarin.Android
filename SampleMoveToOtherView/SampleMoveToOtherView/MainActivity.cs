using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.TeamLog;
using SampleMoveToOtherView.Common;
using SampleMoveToOtherView.Dropbox;
using SampleMoveToOtherView.Resources.layout.TextMoveToView;

namespace SampleMoveToOtherView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);

            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

	        var button1 = FindViewById<Button>(Resource.Id.button1);
			button1.Click += Button1OnClick;

			//Dropboxにデータを作成
			//https://github.com/dropbox/dropbox-sdk-dotnet
			var button2 = FindViewById<Button>(Resource.Id.dropboxButton);//TODO:なぜかうまくできない
			button2.Click += Button2OnClickAsync;

        }

	    private async void Button2OnClickAsync(object sender, EventArgs e)
	    {
			//Dropboxとの連携
			//参考：https://www.dropbox.com/developers/documentation/dotnet#tutorial
			//var task = Task.Run((ConnectDropbox));
			//task.Wait();
		    try
		    {
			    var dropBoxControl = new dropBoxControl();
			    var task = Task.Run((Func<Task>)dropBoxControl.Run);
			    task.Wait();
			    Console.WriteLine("タスク完了");
		    }
			catch (Exception exception)
		    {
			    Console.WriteLine("エラー！：" + exception);
			    throw;
		    }
		    //var createFolder = Task.Run(dropBoxControl.CreateFolder);
		    //var connect = Task.Run(dropBoxControl.ConnectDropbox);

	    }

		private void Button1OnClick(object sender, EventArgs e)
	    {
		    var intent = new Intent(this, typeof(testMoveToViewActivity));
		    var text = FindViewById<EditText>(Resource.Id.editText1);
		    string data = text.Text;
			DataConnect.SetData(intent, data);
		    StartActivity(intent);
	    }

		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        //private void FabOnClick(object sender, EventArgs eventArgs)
        //{
        //    View view = (View) sender;
        //    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
        //        .SetAction("CreateFolder", (Android.Views.View.IOnClickListener)null).Show();
        //}
	}
}

