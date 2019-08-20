using System.Collections.Generic;
using Android.App;
using Android.Widget;
using System.Collections.ObjectModel;
using Xamarin.Forms.Platform.Android.AppCompat;
using System.Linq;
using System.Runtime.CompilerServices;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using AView = Android.Views.View;
using ARelativeLayout = Android.Widget.RelativeLayout;
using System.Reflection;
using FoldingTabBarAndroidForms;
using Android.OS;

using FoldingTabBar.Forms;
using FoldingTabBar.Forms.Droid;
[assembly: ExportRenderer(typeof(FoldingTabbedPage), typeof(FoldingTabbedRenderer))]
namespace FoldingTabBar.Forms.Droid
{
	public class FoldingTabbedRenderer : VisualElementRenderer<FoldingTabbedPage>
	{
		readonly BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

		ARelativeLayout container;
		FoldingTabBarAndroidForms.FoldingTabBar tabbar;
		FrameLayout frameLayout;
		int itemId;

		public FoldingTabbedRenderer(Android.Content.Context context) : base(context)
		{
			AutoPackage = false;
		}

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public async static void Init()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			var temp = DateTime.Now;
		}

		IPageController PageController => Element as IPageController;

		protected override void Dispose(bool disposing)
		{
			if (disposing && Element != null && Element.Children.Count > 0)
			{
				RemoveAllViews();
				foreach (Page pageToRemove in Element.Children)
				{
					IVisualElementRenderer pageRenderer = Platform.GetRenderer(pageToRemove);
					if (pageRenderer != null)
					{
						pageRenderer.View.RemoveFromParent();
						pageRenderer.Dispose();
					}

					var rendererProperty = (BindableProperty)typeof(Platform).GetField("RendererProperty", flags).GetValue(null);
					pageToRemove.ClearValue(rendererProperty);
				}

				if (container != null)
				{
					container.Dispose();
					container = null;
				}

				if (tabbar != null)
				{
					tabbar.OnFoldingItemClickListener -= TabSwitched;
					tabbar.Dispose();
					tabbar = null;
				}

				if (frameLayout != null)
				{
					frameLayout.Dispose();
					frameLayout = null;
				}

			}

			base.Dispose(disposing);
		}

		protected override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			PageController.SendAppearing();
		}

		protected override void OnDetachedFromWindow()
		{
			base.OnDetachedFromWindow();
			PageController.SendDisappearing();
		}

		protected override void OnElementChanged(ElementChangedEventArgs<FoldingTabbedPage> e)
		{
			base.OnElementChanged(e);

			var activity = (FormsAppCompatActivity)Context;

			if (e.OldElement == null)
			{
				var layout = LayoutInflater.From(this.Context).Inflate(Resource.Layout.FoldingTabLayout, null);
				container = (ARelativeLayout)layout.FindViewById(Resource.Id.folding_tab_container);
				frameLayout = (FrameLayout)layout.FindViewById(Resource.Id.fragment_container);
				tabbar = (FoldingTabBarAndroidForms.FoldingTabBar)layout.FindViewById(Resource.Id.folding_tab_bar);

				layout.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

				List<FoldingTabBarAndroidForms.FoldingTabBar.MenuItem> list = new List<FoldingTabBarAndroidForms.FoldingTabBar.MenuItem>();
				for (int i = 0; i < Element.Children.Count; i++)
				{
					var resourceId = Context.Resources.GetIdentifier(Element.Children[i].Icon, "drawable", Context.PackageName);
					var menuItem = new FoldingTabBarAndroidForms.FoldingTabBar.MenuItem()
					{
						Icon = Context.Resources.GetDrawable(resourceId),
						ItemId = itemId,
						Title = Element.Children[i].Title,
						Checked = false
					};
					list.Add(menuItem);
					itemId++;
				}
				tabbar.AddToMenu(list);

				UpdateBarBackgroundColor();
				UpdateSelectionColor();

				tabbar.OnFoldingItemClickListener += TabSwitched;

				AddView(container);

				TabSwitched(0);
			}

			if (e.NewElement != null)
			{
				UpdateBarBackgroundColor();
				UpdateSelectionColor();
				FoldingTabbedPage tabs = e.NewElement;
				if (tabs.CurrentPage != null)
					SwitchContent(tabs.CurrentPage);
			}
		}

		bool TabSwitched(int position)
		{
			SwitchContent(Element.Children[position]);
			var foldingTabbedPage = Element as FoldingTabbedPage;
			foldingTabbedPage.CurrentPage = Element.Children[position];
			return false;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(TabbedPage.CurrentPage))
				SwitchContent(Element.CurrentPage);
			if (e.PropertyName == FoldingTabbedPage.FoldingBarBackgroundColorProperty.PropertyName)
				UpdateBarBackgroundColor();
			if (e.PropertyName == FoldingTabbedPage.FoldingSelectionColorProperty.PropertyName)
				UpdateSelectionColor();
			Invalidate();
		}

		void UpdateBarBackgroundColor()
		{
			if (tabbar != null)
				tabbar.SetBackgroundColor(Element.FoldingBarBackgroundColor.ToAndroid());
		}

		void UpdateSelectionColor()
		{
			if (tabbar != null)
				tabbar.SelectionColor = Element.FoldingSelectionColor.ToAndroid();
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			var width = r - l;
			var height = b - t;

			if (width > 0 && height > 0)
			{
				container.Measure(width + (int)MeasureSpecMode.Exactly, height + (int)MeasureSpecMode.Exactly);
				container.Layout(0, 0, width, height);
			}

			base.OnLayout(changed, l, t, r, b);
		}

		protected virtual void SwitchContent(Page view)
		{
			Context.HideKeyboard(this);

			frameLayout.RemoveAllViews();

			if (view == null)
				return;

			if (Platform.GetRenderer(view) == null)
				Platform.SetRenderer(view, Platform.CreateRendererWithContext(view, this.Context));
			//Platform.SetRenderer(view, Platform.CreateRenderer(view));

			frameLayout.AddView(Platform.GetRenderer(view).View);
		}
	}

	//public interface IPageController
	//{
	//  Rectangle ContainerArea { get; set; }

	//  bool IgnoresContainerArea { get; set; }

	//  ObservableCollection<Element> InternalChildren { get; }

	//  void SendAppearing();

	//  void SendDisappearing();
	//}
}
