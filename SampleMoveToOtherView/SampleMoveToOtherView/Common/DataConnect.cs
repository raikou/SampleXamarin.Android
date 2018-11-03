using Android.Content;
using Newtonsoft.Json;

namespace SampleMoveToOtherView.Common
{
	public static class DataConnect
	{
		/// <summary>
		/// 画面間で渡すデータを設定する
		/// </summary>
		/// <typeparam name="D">受け渡しデータの型</typeparam>
		/// <param name="intent">インテント</param>
		/// <param name="data">受け渡しをしたいデータを設定する</param>
		/// <returns></returns>
		public static void SetData<D>(Intent intent, D data)
		{
			string data2 = JsonConvert.SerializeObject(data);
			intent.PutExtra(typeof(D).Name, data2);
		}

		/// <summary>
		/// 画面間でデータを受け取る
		/// </summary>
		/// <typeparam name="D">受け渡しデータの型</typeparam>
		/// <param name="intent"></param>
		/// <returns></returns>
		public static D GetData<D>(Intent intent)
		{
			var r = intent.GetStringExtra(typeof(D).Name);
			var data2 = JsonConvert.DeserializeObject<D>(r);
			return data2;
		}
	}
}