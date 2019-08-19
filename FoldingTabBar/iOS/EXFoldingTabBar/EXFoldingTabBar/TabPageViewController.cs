using System;
using Foundation;
using UIKit;
using FoldingTabBariOS;
namespace EXFoldingTabBar
{
	public class TabPageViewController : UIViewController, IYALTabBarDelegate
	{
		//** This class is just to show how to use the left and right extra items **//
		//** The extra items are set when you new up a YALTabBarItem **//
		//** Other than that, this is just a normal UIViewController **//
		public TabPageViewController() { }

		[Export("tabBarDidSelectExtraLeftItem:")]
		public void TabBarDidSelectExtraLeftItem(YALFoldingTabBar tabBar)
		{
			System.Diagnostics.Debug.WriteLine("The left extra button was pressed!");
		}
	}
}
