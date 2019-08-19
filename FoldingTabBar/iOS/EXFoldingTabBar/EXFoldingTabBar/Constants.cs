using System;
using Foundation;
using UIKit;
using FoldingTabBariOS;
namespace EXFoldingTabBar
{
	public static class Constants
	{
		public const float YALBottomSelectedDotDefaultSize = 4.0f;
		public const float YALBottomSelectedDotOffset = 16.0f;
		public const float YALTabBarViewDefaultHeight = 80.0f;
		public const float YALExtraTabBarItemsDefaultHeight = 48.0f;
		public const float YALForExtraTabBarItemsDefaultOffset = 15.0f;
		public static UIEdgeInsets YALTabBarViewHDefaultEdgeInsets = new UIEdgeInsets(10.0f, 14.0f, 10.0f, 14.0f);
		public static UIEdgeInsets YALTabBarViewItemsDefaultEdgeInsets = new UIEdgeInsets(0f, 0f, 0f, 0f);
		public static NSString YALCenterButtonExpandAnimation = new NSString("CENTER_BUTTON_EXPAND_ANIMATION");
		public static NSString YALCenterButtonCollapseAnimation = new NSString("CENTER_BUTTON_COLLAPSE_ANIMATION");
		public static NSString YALAdditionalButtonsAnimation = new NSString("ADDITIONAL_BUTTONS_ANIMATION");
		public static NSString YALTabBarExpandAnimation = new NSString("TABBAR_EXPAND_ANIMATION");
		public static NSString YALTabBarExpandCollapseAnimation = new NSString("TABBAR_COLLAPSE_ANIMATION");
		public static NSString YALExtraLeftBarItemAnimation = new NSString("EXTRA_LEFT_BAR_ITEM_ANIMATION");
		public static NSString YALExtraRightBarItemAnimation = new NSString("EXTRA_RIGHT_BAR_ITEM_ANIMATION");
		public static double kYALExpandAnimationDuration = 1.0;
		public static float kDegreeToRadiansRatio = (float)Math.PI / 180f;

		public static YALAnimationParameters kYALBounceAnimationParameters = new YALAnimationParameters()
		{
			duration = kYALExpandAnimationDuration * 2.0 / 3.0,
			damping = 0.5,
			velocity = 3.0
		};

		public static YALAnimationParameters kYALExtraLeftTabBarItemAnimationParameters = new YALAnimationParameters()
		{
			duration = kYALExpandAnimationDuration * 3.0 / 4.0,
			damping = 0.74,
			velocity = 1.2,
			fromValue = 0,
			toValue = Math.PI * 2.0 * 2.0
		};

		public static YALAnimationParameters kYALExtraRightTabBarItemAnimationParameters = new YALAnimationParameters()
		{
			duration = kYALExpandAnimationDuration * 3.0 / 4.0,
			damping = 0.74,
			velocity = 1.2,
			fromValue = 0,
			toValue = Math.PI * 2.0 * -2.0
		};

		public static YALAnimationParameters kYALTabBarExpandAnimationParameters = new YALAnimationParameters()
		{
			duration = kYALExpandAnimationDuration / 2.0,
			damping = 0.5,
			velocity = 0.6
		};

		public static YALAnimationParameters kYALTabBarCollapseAnimationParameters = new YALAnimationParameters()
		{
			duration = kYALExpandAnimationDuration * 0.6,
			damping = 1,
			velocity = 0.2
		};

		public static YALCenterButtonAnimationsParameters kYALCenterButtonExpandAnimationParameters = new YALCenterButtonAnimationsParameters()
		{
			rotation = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = Math.PI * 2.0 + 45.0 * kDegreeToRadiansRatio
			},
			bounce = new YALAnimationParameters()
			{
				beginTime = kYALExpandAnimationDuration / 4.0,
				fromValue = 45.0 * kDegreeToRadiansRatio + Math.PI / 8.0,
				toValue = 45.0 * kDegreeToRadiansRatio
			}
		};

		public static YALCenterButtonAnimationsParameters kYALCenterButtonCollapseAnimationParameters = new YALCenterButtonAnimationsParameters()
		{
			rotation = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = 315.0 * kDegreeToRadiansRatio
			},
			bounce = new YALAnimationParameters()
			{
				beginTime = kYALExpandAnimationDuration / 4.0,
				fromValue = Math.PI / 8.0,
				toValue = 0.0
			}
		};

		public static YALSelectedDotAnimationsParameters kYALSelectedDotAnimationsParameters = new YALSelectedDotAnimationsParameters()
		{
			scaleX = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = 1.0
			},
			scaleY = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = 1.0
			}
		};

		public static YALAdditionalButtonsAnimationsParameters kYALAdditionalButtonsAnimationsParameters = new YALAdditionalButtonsAnimationsParameters()
		{
			scaleX = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = 1.0
			},
			scaleY = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = 1.0
			},
			rotation = new YALAnimationParameters()
			{
				duration = kYALExpandAnimationDuration / 4.0,
				fromValue = 0.0,
				toValue = Math.PI * 2.0 * 5.0
			},
			bounce = new YALAnimationParameters()
			{
				beginTime = kYALExpandAnimationDuration / 4.0,
				fromValue = Math.PI / 8.0,
				toValue = 0.0
			}
		};

		public static YALExtraTabBarItemViewAnimationParameters kYALShowExtraTabBarItemViewAnimationParameters = new YALExtraTabBarItemViewAnimationParameters()
		{
			duration = kYALExpandAnimationDuration / 2.0,
			damping = 0.5f
		};

		public static YALExtraTabBarItemViewAnimationParameters kYALHideExtraTabBarItemViewAnimationParameters = new YALExtraTabBarItemViewAnimationParameters()
		{
			duration = kYALExpandAnimationDuration / 8.0
		};
	}
}
