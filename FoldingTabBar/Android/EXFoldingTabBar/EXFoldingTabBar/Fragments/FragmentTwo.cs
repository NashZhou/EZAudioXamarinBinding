using System;
using Android.Views;
using Android.App;
using Android.OS;
namespace EXFoldingTabBar
{
	public class FragmentTwo : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.fragment_two, container, false);
			return view;
		}
	}
}
