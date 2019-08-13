using System;
using Android.Views;
using Android.App;
using Android.OS;
using SupportFragment = Android.Support.V4.App.Fragment;
namespace EXFoldingTabBar
{
	public class FragmentTwo : SupportFragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_two, container, false);
			return view;
		}
	}
}
