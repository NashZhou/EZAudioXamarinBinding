using System;
using Android.Views;
using Android.App;
using Android.OS;
using Android.Graphics;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Widget;
using Android.Content;

namespace EXFoldingTabBar
{
	public class FragmentOne : SupportFragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_one, container, false);

			var btn = (Button)view.FindViewById(Resource.Id.btn);
			btn.Click += (object sender, EventArgs e) =>
			{
				var intent = new Intent(Context, typeof(SecondaryActivity));
				StartActivity(intent);
			};

			return view;
		}
	}
}
