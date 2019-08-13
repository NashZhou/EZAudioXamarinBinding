using System;
using UIKit;
using CoreGraphics;
using Foundation;
using FoldingTabBariOS;
namespace EXFoldingTabBar
{
	public class CustomTabBarController : YALFoldingTabBarController, IYALTabBarDelegate
	{
		public CustomTabBarController()
		{
			//** Constants is not part of FoldingTabBariOS, look at this project to find the source **//
			TabBarView.ExtraTabBarItemHeight = Constants.YALExtraTabBarItemsDefaultHeight;
			TabBarView.OffsetForExtraTabBarItems = Constants.YALForExtraTabBarItemsDefaultOffset;

			TabBarView.BackgroundColor = new UIColor(94f / 255f, 91f / 255f, 149f / 255f, 1); //** #5e5b95 **//
																							  //TabBarView.BackgroundColor = UIColor.Clear; //** Make the background clear **//
			TabBarView.TabBarColor = new UIColor(72f / 255f, 211f / 255f, 178f / 255f, 1); //** #48d3b2 **//

			TabBarView.TabBarViewEdgeInsets = Constants.YALTabBarViewHDefaultEdgeInsets;
			TabBarView.TabBarItemsEdgeInsets = Constants.YALTabBarViewItemsDefaultEdgeInsets;

			//** Set the delegate for the YALFoldingTabBarController **//
			TabBarView.WeakDelegate = this;
		}

		#region YALTabBarDelegate

		[Export("tabBarWillExpand:")]
		public void TabBarWillExpand(YALFoldingTabBar tabBar)
		{
			System.Diagnostics.Debug.WriteLine("The bar will expand!");
		}

		[Export("tabBarDidExpand:")]
		public void TabBarDidExpand(YALFoldingTabBar tabBar)
		{
			System.Diagnostics.Debug.WriteLine("The bar expanded!");
		}

		[Export("tabBarWillCollapse:")]
		public void TabBarWillCollapse(YALFoldingTabBar tabBar)
		{
			System.Diagnostics.Debug.WriteLine("The bar will collapse!");
		}

		[Export("tabBarDidCollapse:")]
		public void TabBarDidCollapse(YALFoldingTabBar tabBar)
		{
			System.Diagnostics.Debug.WriteLine("The bar collapsed!");
		}

		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//** This class is used to show how the IYALTabBarDelegate works in a view controller **//
			var first = new TabPageViewController();
			first.View.BackgroundColor = UIColor.Red;
			first.Title = "Tab One";
			var firstNavigationController = new UINavigationController(first)
			{
				Title = "First"
			};

			var second = new UIViewController();
			second.View.BackgroundColor = UIColor.Green;
			second.Title = "Tab Two";
			var secondNavigationController = new UINavigationController(second)
			{
				Title = "Second"
			};

			//** Make sure to define the view controllers for the tab bar view controller **//
			ViewControllers = new UIViewController[]
			{
				firstNavigationController, secondNavigationController
			};

			//** The second parameter in YALTabBarItem will show up as a button on the left side when the first UIViewController is present **//
			YALTabBarItem item1 = new YALTabBarItem(new UIImage("profile_icon"), new UIImage("settings_icon"), null);
			YALTabBarItem item2 = new YALTabBarItem(new UIImage("search_icon"), null, null);

			//** The next 3 required properties to be defined are LeftBarItems, RightBarItems, and CenterButtonImage **//

			LeftBarItems = new YALTabBarItem[]
			{
				item1
			};
			RightBarItems = new YALTabBarItem[]
			{
				item2
			};

			CenterButtonImage = new UIImage("plus_icon");
		}


		public override void ViewWillLayoutSubviews()
		{
			//** Define the height needed for the FoldingTabBar to look correct **//
			nfloat height = (IsiPhoneX()) ? 80f + this.View.GetBottomInset() : 80f;
			var frame = TabBar.Frame;
			frame.Size = new CGSize(frame.Size.Width, height);
			frame.Y = View.Frame.Size.Height - height;
			TabBar.Frame = frame;
			base.ViewWillLayoutSubviews();
		}

		bool IsiPhoneX()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
			{
				var window = UIApplication.SharedApplication.KeyWindow;
				if (window.SafeAreaInsets.Bottom > 0.0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}
}
