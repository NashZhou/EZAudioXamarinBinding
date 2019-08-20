using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UIKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;
using static Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page;
using PageUIStatusBarAnimation = Xamarin.Forms.PlatformConfiguration.iOSSpecific.UIStatusBarAnimation;

using FoldingTabBariOS;
using CoreGraphics;

using FoldingTabBar.Forms;
using FoldingTabBar.Forms.iOS;
[assembly: ExportRenderer(typeof(FoldingTabbedPage), typeof(FoldingTabbedRenderer))]
namespace FoldingTabBar.Forms.iOS
{
	// fixme without todo == already fixed
	// todo == haven't fixed
	public class FoldingTabbedRenderer : YALFoldingTabBarController, IVisualElementRenderer, IEffectControlProvider
	{
		//bool _barBackgroundColorWasSet;
		//bool _barTextColorWasSet;
		//UIColor _defaultBarTextColor;
		//bool _defaultBarTextColorSet;
		//UIColor _defaultBarColor;
		//bool _defaultBarColorSet;
		bool _loaded;
		Size _queuedSize;
		readonly BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

		Page Page => Element as Page;

		public FoldingTabbedRenderer()
		{
			if (TabBarView != null)
			{
				TabBarView.ExtraTabBarItemHeight = 48f;
				TabBarView.OffsetForExtraTabBarItems = 15f;
				TabBarViewHeight = 80f;
				TabBarView.BackgroundColor = UIColor.Clear;
				TabBarView.TabBarViewEdgeInsets = new UIEdgeInsets(10f, 14f, 10f, 14f);
				TabBarView.TabBarItemsEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
			}
		}

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public async static void Init()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
		{
			var temp = DateTime.Now;
		}

		void UpdateBarColor()
		{
			if (Element == null || TabBarView == null)
				return;
			TabBarView.TabBarColor = ((FoldingTabbedPage)Element).FoldingBarBackgroundColor.ToUIColor();
		}

		void UpdateDotColor()
		{
			if (Element == null || TabBarView == null)
				return;
			TabBarView.DotColor = ((FoldingTabbedPage)Element).FoldingSelectionColor.ToUIColor();
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
			nfloat height = (IsiPhoneX()) ? 80f + this.View.GetBottomInset() : 80f;
			var frame = TabBar.Frame;
			frame.Size = new CGSize(frame.Size.Width, height);
			frame.Y = View.Frame.Size.Height - height;
			TabBar.Frame = frame;
		}

		public override UIViewController SelectedViewController
		{
			get { return base.SelectedViewController; }
			set
			{
				base.SelectedViewController = value;
				UpdateCurrentPage();
			}
		}

		protected TabbedPage Tabbed
		{
			get { return (TabbedPage)Element; }
		}

		public VisualElement Element { get; private set; }

		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

		public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			return NativeView.GetSizeRequest(widthConstraint, heightConstraint);
		}

		public UIView NativeView
		{
			get { return View; }
		}

		public void SetElement(VisualElement element)
		{
			var oldElement = Element;
			Element = element;

			FinishedCustomizingViewControllers += HandleFinishedCustomizingViewControllers;
			Tabbed.PropertyChanged += OnPropertyChanged;
			Tabbed.PagesChanged += OnPagesChanged;

			OnElementChanged(new VisualElementChangedEventArgs(oldElement, element));

			OnPagesChanged(null, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

			if (element != null)
			{
				// fixme
				//element.SendViewInitialized(NativeView);

				var method = typeof(Xamarin.Forms.Forms).GetMethod("SendViewInitialized", flags);
				method.Invoke(null, new object[] { element, NativeView });
			}

			//disable edit/reorder of tabs
			CustomizableViewControllers = null;

			// fixme todo
			//EffectUtilities.RegisterEffectControlProvider(this, oldElement, element);
			//var type = Type.GetType("Xamarin.Forms.Internals.EffectUtilities");

			//var methods = type.GetMethods(flags);
			//foreach (MethodInfo info in methods)
			//{
			//    System.Diagnostics.Debug.WriteLine("METHOD : " + info);
			//}

			//var methodOfType = type.GetMethod("RegisterEffectControlProvider", flags);
			//methodOfType.Invoke(null, new object[] { this, oldElement, element });
		}

		public void SetElementSize(Size size)
		{
			if (_loaded)
				Element.Layout(new Rectangle(Element.X, Element.Y, size.Width, size.Height));
			else
				_queuedSize = size;
		}

		public UIViewController ViewController
		{
			get { return this; }
		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			View.SetNeedsLayout();
		}

		public override void ViewDidAppear(bool animated)
		{
			// fixme
			//Page.SendAppearing();
			var method = typeof(IPageController).GetMethod("SendAppearing", flags);
			method.Invoke(Page, null);

			base.ViewDidAppear(animated);
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			// fixme
			//Page.SendDisappearing();
			var method = typeof(IPageController).GetMethod("SendDisappearing", flags);
			method.Invoke(Page, null);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (Element == null)
				return;

			if (!Element.Bounds.IsEmpty)
			{
				View.Frame = new System.Drawing.RectangleF((float)Element.X, (float)Element.Y, (float)Element.Width, (float)Element.Height);
			}

			var frame = View.Frame;
			var tabBarFrame = TabBar.Frame;
			// fixme
			//Page.ContainerArea = new Rectangle(0, 0, frame.Width, frame.Height - tabBarFrame.Height);
			var property = typeof(IPageController).GetProperty("ContainerArea", flags);
			var propertyValue = property.GetValue(Page);
			if (propertyValue is Rectangle)
			{
				property.SetValue(Page, new Rectangle(0, 0, frame.Width, frame.Height - tabBarFrame.Height));
			}

			if (!_queuedSize.IsZero)
			{
				Element.Layout(new Rectangle(Element.X, Element.Y, _queuedSize.Width, _queuedSize.Height));
				_queuedSize = Size.Zero;
			}

			_loaded = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// fixme
				//Page.SendDisappearing();
				var method = Page.GetType().GetMethod("SendDisappearing", flags);
				method.Invoke(Page, null);

				Tabbed.PropertyChanged -= OnPropertyChanged;
				Tabbed.PagesChanged -= OnPagesChanged;
				FinishedCustomizingViewControllers -= HandleFinishedCustomizingViewControllers;

				((FoldingTabbedPage)Element).UpdateBarColor = null;
				((FoldingTabbedPage)Element).UpdateSelectionColor = null;
			}

			base.Dispose(disposing);
		}

		protected virtual void OnElementChanged(VisualElementChangedEventArgs e)
		{
			ElementChanged?.Invoke(this, e);

			((FoldingTabbedPage)Element).UpdateBarColor = UpdateBarColor;
			((FoldingTabbedPage)Element).UpdateSelectionColor = UpdateDotColor;
			UpdateBarColor();
			UpdateDotColor();
		}

		UIViewController GetViewController(Page page)
		{
			var renderer = Platform.GetRenderer(page);
			if (renderer == null)
				return null;

			return renderer.ViewController;
		}

		void HandleFinishedCustomizingViewControllers(object sender, UITabBarCustomizeChangeEventArgs e)
		{
			if (e.Changed)
				UpdateChildrenOrderIndex(e.ViewControllers);
		}

		void OnPagePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			// Setting TabBarItem.Title in iOS 10 causes rendering bugs
			// Work around this by creating a new UITabBarItem on each change


			// todo Edit icon when page icon is changed
			//bool IsiOS10OrNewer = UIDevice.CurrentDevice.CheckSystemVersion(10, 0);


			//if (e.PropertyName == Page.TitleProperty.PropertyName && !IsiOS10OrNewer)
			//{
			//  var page = (Page)sender;
			//  var renderer = Platform.GetRenderer(page);
			//  if (renderer == null)
			//      return;

			//  if (renderer.ViewController.TabBarItem != null)
			//      renderer.ViewController.TabBarItem.Title = page.Title;
			//}
			//else if (e.PropertyName == Page.IconProperty.PropertyName || e.PropertyName == Page.TitleProperty.PropertyName && IsiOS10OrNewer)
			//{
			//  var page = (Page)sender;

			//  IVisualElementRenderer renderer = Platform.GetRenderer(page);

			//  if (renderer?.ViewController.TabBarItem == null)
			//      return;

			//  SetTabBarItem(renderer);
			//}
		}

		void OnPagesChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			e.Apply((o, i, c) => SetupPage((Page)o, i), (o, i) => TeardownPage((Page)o, i), Reset);

			SetControllers();

			UIViewController controller = null;
			if (Tabbed.CurrentPage != null)
				controller = GetViewController(Tabbed.CurrentPage);
			if (controller != null && controller != base.SelectedViewController)
				base.SelectedViewController = controller;
		}

		void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(TabbedPage.CurrentPage))
			{
				var current = Tabbed.CurrentPage;
				if (current == null)
					return;

				var controller = GetViewController(current);
				if (controller == null)
					return;

				SelectedViewController = controller;
			}
			else if (e.PropertyName == PrefersStatusBarHiddenProperty.PropertyName)
				UpdatePrefersStatusBarHiddenOnPages();
			else if (e.PropertyName == PreferredStatusBarUpdateAnimationProperty.PropertyName)
				UpdateCurrentPagePreferredStatusBarUpdateAnimation();


			if (e.PropertyName == FoldingTabbedPage.FoldingBarBackgroundColorProperty.PropertyName)
				UpdateBarColor();
			if (e.PropertyName == FoldingTabbedPage.FoldingSelectionColorProperty.PropertyName)
				UpdateDotColor();
		}

		public override UIViewController ChildViewControllerForStatusBarHidden()
		{
			var current = Tabbed.CurrentPage;
			if (current == null)
				return null;

			return GetViewController(current);
		}

		void UpdateCurrentPagePreferredStatusBarUpdateAnimation()
		{
			PageUIStatusBarAnimation animation = ((Page)Element).OnThisPlatform().PreferredStatusBarUpdateAnimation();
			Tabbed.CurrentPage.OnThisPlatform().SetPreferredStatusBarUpdateAnimation(animation);
		}

		void UpdatePrefersStatusBarHiddenOnPages()
		{
			for (var i = 0; i < ViewControllers.Length; i++)
			{
				// fixme check
				//Tabbed.GetPageByIndex(i).OnThisPlatform().SetPrefersStatusBarHidden(Tabbed.OnThisPlatform().PrefersStatusBarHidden());
				var methodOne = Tabbed.GetType().GetMethod("GetPageByIndex", flags);
				var objectOne = methodOne.Invoke(Tabbed, new object[] { i });
				var methodTwo = objectOne.GetType().GetMethod("OnThisPlatform", flags);
				var objectTwo = methodTwo.Invoke(objectOne, null);
				var methodThree = objectTwo.GetType().GetMethod("SetPrefersStatusBarHidden", flags);
				methodThree.Invoke(objectTwo, new object[] { Tabbed.OnThisPlatform().PrefersStatusBarHidden() });
			}
		}

		void Reset()
		{
			var i = 0;
			foreach (var page in Tabbed.Children)
				SetupPage(page, i++);

			if (items.Count() == 0)
				return;

			if (items.Count() % 2 != 0)
				throw new Exception("Please have an even number of pages");

			// Find the middle
			CalculateInsets(this.View.Frame.Width);
			int middle = items.Count() / 2;

			List<YALTabBarItem> leftBarItems = new List<YALTabBarItem>();
			List<YALTabBarItem> rightBarItems = new List<YALTabBarItem>();

			for (int j = 0; j < items.Count() / 2; j++)
			{
				leftBarItems.Add(items[j]);
				rightBarItems.Add(items[j + middle]);
			}

			LeftBarItems = leftBarItems.ToArray();
			RightBarItems = rightBarItems.ToArray();

			CenterButtonImage = new UIImage("plus_icon");
		}

		public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
		{
			CalculateInsets(toSize.Width);
			base.ViewWillTransitionToSize(toSize, coordinator);
		}

		void CalculateInsets(nfloat width)
		{
			var mainBtnWidth = 75.5f; // Going off of iPhoneX size
			var sideInsets = (width / 2) - mainBtnWidth;
			int numOfAdditionalPairs = (items.Count / 2) - 1;
			sideInsets = sideInsets - (14 * 8 * numOfAdditionalPairs);
			if (sideInsets <= 14f)
				sideInsets = 14f;
			TabBarView.TabBarViewEdgeInsets = new UIEdgeInsets(10f, sideInsets, 10f, sideInsets);

			TabBarView.SetNeedsDisplay();
			TabBarView.SetNeedsLayout();
		}

		void SetControllers()
		{
			// fixme
			var list = new List<UIViewController>();

			var children = (IList<Element>)Element.GetType().GetProperty("LogicalChildrenInternal", flags).GetValue(Element);

			for (var i = 0; i < children.Count; i++)
			{
				var child = children[i];
				VisualElement v = null;
				try
				{
					v = (VisualElement)child;
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.Message);
					continue;
				}
				finally
				{
					if (Platform.GetRenderer(v) != null)
						list.Add(Platform.GetRenderer(v).ViewController);
				}
			}
			ViewControllers = list.ToArray();
		}

		void SetupPage(Page page, int index)
		{
			IVisualElementRenderer renderer = Platform.GetRenderer(page);
			if (renderer == null)
			{
				renderer = Platform.CreateRenderer(page);
				Platform.SetRenderer(page, renderer);
			}

			page.PropertyChanged += OnPagePropertyChanged;

			SetTabBarItem(renderer, index);
		}

		void TeardownPage(Page page, int index)
		{
			page.PropertyChanged -= OnPagePropertyChanged;

			Platform.SetRenderer(page, null);
		}


		void UpdateChildrenOrderIndex(UIViewController[] viewControllers)
		{
			for (var i = 0; i < viewControllers.Length; i++)
			{
				var originalIndex = -1;
				if (int.TryParse(viewControllers[i].TabBarItem.Tag.ToString(), out originalIndex))
				{
					// fixme check
					//var page = (Page)Tabbed.InternalChildren[originalIndex];
					//TabbedPage.SetIndex(page, i);

					var pageList = (ObservableCollection<Page>)Tabbed.GetType().GetProperty("InternalChildren", flags).GetValue(Tabbed);
					var page = pageList[originalIndex];
					typeof(TabbedPage).GetMethod("SetIndex", flags).Invoke(null, new object[] { page, i });

				}
			}
		}

		void UpdateCurrentPage()
		{
			// fixme
			//var count = Tabbed.InternalChildren.Count;
			var numberOfChildren = (ObservableCollection<Element>)typeof(IPageController).GetProperty("InternalChildren", flags).GetValue(Tabbed);
			var count = numberOfChildren.Count;

			var index = (int)SelectedIndex;

			var pageMethod = Tabbed.GetType().GetMethod("GetPageByIndex", flags).Invoke(Tabbed, new object[] { index });

			if (pageMethod is Page)
			{
				var page = (Page)pageMethod;
				((TabbedPage)Element).CurrentPage = index >= 0 && index < count ? page : null;
			}
			else
			{
				((TabbedPage)Element).CurrentPage = null;
			}
		}

		void IEffectControlProvider.RegisterEffect(Effect effect)
		{
			VisualElementRenderer<VisualElement>.RegisterEffect(effect, View);
		}

		List<YALTabBarItem> items = new List<YALTabBarItem>();

		void SetTabBarItem(IVisualElementRenderer renderer, int index)
		{
			var page = renderer.Element as Page;
			if (page == null)
				throw new InvalidCastException($"{nameof(renderer)} must be a {nameof(Page)} renderer.");

			var icons = GetIcon(page);

			YALTabBarItem item = new YALTabBarItem(icons.Item1, null, null);

			items.Add(item);

			icons?.Item1?.Dispose();
			icons?.Item2?.Dispose();
		}

		/// <summary>
		/// Get the icon for the tab bar item of this page
		/// </summary>
		/// <returns>
		/// A tuple containing as item1: the unselected version of the icon, item2: the selected version of the icon (item2 can be null),
		/// or null if no icon should be set.
		/// </returns>
		protected virtual Tuple<UIImage, UIImage> GetIcon(Page page)
		{
			if (!string.IsNullOrEmpty(page.Icon?.File))
			{
				// todo

				var icon = new UIImage(page.Icon.File);
				//var type = Type.GetType("Xamarin.Forms.Internals.Registrar");
				//var property = type.GetProperties(flags);
				//foreach (PropertyInfo info in property)
				//{
				//    System.Diagnostics.Debug.WriteLine("PROPERTY : " + property);
				//}
				//var source = Registrar.Registered.GetHandler<IImageSourceHandler>(page.Icon.GetType());
				//var icon = await source.LoadImageAsync(page.Icon);
				return Tuple.Create(icon, (UIImage)null);
			}

			return null;
		}

		private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
		{
			return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
		}

		bool IsiPhoneX()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
			{
				var window = UIApplication.SharedApplication.KeyWindow;
				return (window.SafeAreaInsets.Bottom > 0.0);
			}
			else
			{
				return false;
			}
		}
	}
}