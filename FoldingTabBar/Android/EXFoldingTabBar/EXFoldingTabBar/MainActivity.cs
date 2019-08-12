using System;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.App;
using Android.Views;
using Android.Widget;
using FoldingTabBarAndroid;
namespace EXFoldingTabBar
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : Activity
	{
		FoldingTabBar tabBar;
		FrameLayout frameLayout;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			tabBar = (FoldingTabBar)FindViewById(Resource.Id.folding_tab_bar);
			frameLayout = (FrameLayout)FindViewById(Resource.Id.container);

			tabBar.SetBackgroundColor(Color.Purple);

			ChangeFragment(new FragmentOne());
			tabBar.OnMainButtonClickedListener += (sender, e) =>
			{

			};
			tabBar.OnFoldingItemClickListener += (id) =>
			{
				switch (id.ItemId)
				{
					case Resource.Id.ftb_new_chat:
						ChangeFragment(new FragmentOne());
						break;
					case Resource.Id.ftb_profile:
						ChangeFragment(new FragmentTwo());
						break;
				}
				return false;
			};
		}

		void ChangeFragment(Fragment fragment)
		{
			FragmentManager.BeginTransaction().Replace(Resource.Id.container, fragment).Commit();
		}
	}
}
