using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleMoveToOtherView.Common;

namespace SampleMoveToOtherView.Resources.layout.TextMoveToView
{
	[Activity(Label = "testMoveToViewActivity")]
	public class testMoveToViewActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.testMoveToView);


			//データ取得
			var data = DataConnect.GetData<string>(this.Intent);

			var text = FindViewById<TextView>(Resource.Id.textView1);
			text.Text = data;

			// Create your application here
		}
	}
}