using System;
using Android.Views;
using Android.App;
using Android.OS;
namespace EXFoldingTabBar
{
	public class FragmentOne : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_one, container, false);
			return view;
		}
	}
}
