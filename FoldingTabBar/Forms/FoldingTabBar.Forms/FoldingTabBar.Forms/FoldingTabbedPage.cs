using System;
using Xamarin.Forms;
namespace FoldingTabBar.Forms
{
	public class FoldingTabbedPage : TabbedPage
	{
		/// <summary>
		/// Background color property of FoldingTabBar
		/// </summary>
		public static readonly BindableProperty FoldingBarBackgroundColorProperty = BindableProperty.Create(nameof(FoldingBarBackgroundColor),
																											typeof(Color),
																											typeof(FoldingTabbedPage),
																											Color.Gray);
		/// <summary>
		/// Background color of FoldingTabBar
		/// </summary>
		public Color FoldingBarBackgroundColor
		{
			get { return (Color)GetValue(FoldingBarBackgroundColorProperty); }
			set
			{
				SetValue(FoldingBarBackgroundColorProperty, value);
				UpdateBarColor?.Invoke();
			}
		}

		/// <summary>
		/// Selection color property of FoldingTabBar
		/// </summary>
		public static readonly BindableProperty FoldingSelectionColorProperty = BindableProperty.Create(nameof(FoldingSelectionColor),
																											typeof(Color),
																											typeof(FoldingTabbedPage),
																											Color.White);
		/// <summary>
		/// Selection color of FoldingTabBar
		/// </summary>
		public Color FoldingSelectionColor
		{
			get { return (Color)GetValue(FoldingSelectionColorProperty); }
			set
			{
				SetValue(FoldingSelectionColorProperty, value);
				UpdateSelectionColor?.Invoke();
			}
		}

		/// <summary>
		/// Action needed for iOS renderer
		/// </summary>
		public Action UpdateBarColor;
		/// <summary>
		/// Action needed for iOS renderer
		/// </summary>
		public Action UpdateSelectionColor;
	}
}
