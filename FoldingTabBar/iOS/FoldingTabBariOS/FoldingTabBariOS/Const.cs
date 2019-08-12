using System;
using UIKit;
using Foundation;
using ObjCRuntime;

namespace FoldingTabBariOS
{
	//partial class UIView_Insets
	//{
	//    public static nfloat GetBottomInsets()
	//    {
	//        return (null as UIView).GetBottomInset();
	//    }
	//}

	//partial class TransactionWithAnimationsAndCompletion 
	//{
	//    static readonly IntPtr class_ptr = Class.GetHandle("CATransaction");
	//}

	//partial class YALTabBarViewAnimations
	//{
	//    static readonly IntPtr class_ptr = Class.GetHandle("CAAnimation");
	//}

	//public struct YALAnimationParameters
	//{
	//    public double BeginTime;
	//    public double Duration;
	//    public double FromValue;
	//    public double ToValue;
	//    public double Damping;
	//    public double Velocity;
	//}
	//public struct YALAdditionalButtonsAnimationsParameters
	//{
	//    public YALAnimationParameters ScaleX;
	//    public YALAnimationParameters ScaleY;
	//    public YALAnimationParameters Rotation;
	//    public YALAnimationParameters Bounce;
	//}
	//public struct YALCenterButtonAnimationsParameters
	//{
	//    public YALAnimationParameters Rotation;
	//    public YALAnimationParameters Bounce;
	//}
	//public struct YALExtraTabBarItemViewAnimationParameters
	//{
	//    public double Duration;
	//    public double Delay;
	//    public float Damping;
	//    public float Velocity;
	//    public UIViewAnimationOptions Options;
	//}
	//public struct YALSelectedDotAnimationsParameters
	//{
	//    public YALAnimationParameters ScaleX;
	//    public YALAnimationParameters ScaleY;
	//}
	// public static class Constants
	// {
	//     const float YALBottomSelectedDotDefaultSize = 4.0f;
	//     const float YALBottomSelectedDotOffset = 16.0f;
	//     const float YALTabBarViewDefaultHeight = 80.0f;
	//     const float YALExtraTabBarItemsDefaultHeight = 48.0f;
	//     const float YALForExtraTabBarItemsDefaultOffset = 15.0f;
	//     static UIEdgeInsets YALTabBarViewHDefaultEdgeInsets = new UIEdgeInsets(10.0f, 14.0f, 10.0f, 14.0f);
	//     static UIEdgeInsets YALTabBarViewItemsDefaultEdgeInsets = new UIEdgeInsets(0f, 0f, 0f, 0f);
	//     static NSString YALCenterButtonExpandAnimation = new NSString("CENTER_BUTTON_EXPAND_ANIMATION");
	//     static NSString YALCenterButtonCollapseAnimation = new NSString("CENTER_BUTTON_COLLAPSE_ANIMATION");
	//     static NSString YALAdditionalButtonsAnimation = new NSString("ADDITIONAL_BUTTONS_ANIMATION");
	//     static NSString YALTabBarExpandAnimation = new NSString("TABBAR_EXPAND_ANIMATION");
	//     static NSString YALTabBarExpandCollapseAnimation = new NSString("TABBAR_COLLAPSE_ANIMATION");
	//     static NSString YALExtraLeftBarItemAnimation = new NSString("EXTRA_LEFT_BAR_ITEM_ANIMATION");
	//     static NSString YALExtraRightBarItemAnimation = new NSString("EXTRA_RIGHT_BAR_ITEM_ANIMATION");
	//     static double kYALExpandAnimationDuration = 1.0;
	//     static float kDegreeToRadiansRatio = (float)Math.PI / 180f;

	////     static YALAnimationParameters kYALBounceAnimationParameters = new YALAnimationParameters()
	////     { 
	////         duration = kYALExpandAnimationDuration * 2.0 / 3.0,
	////         damping = 0.5,
	////         velocity = 3.0
	////     };

	////     static YALAnimationParameters kYALExtraLeftTabBarItemAnimationParameters = new YALAnimationParameters()
	////     { 
	////         duration = kYALExpandAnimationDuration * 3.0 / 4.0,
	////         damping = 0.74,
	////         velocity = 1.2,
	////         fromValue = 0,
	////         toValue = Math.PI * 2.0 * 2.0
	////     };

	////     static YALAnimationParameters kYALExtraRightTabBarItemAnimationParameters = new YALAnimationParameters()
	////     {
	////duration = kYALExpandAnimationDuration * 3.0 / 4.0,
	////damping = 0.74,
	////velocity = 1.2,
	////fromValue = 0,
	////toValue = Math.PI * 2.0 * -2.0
	//    //};

	//    //static YALAnimationParameters kYALTabBarExpandAnimationParameters = new YALAnimationParameters()
	//    //{
	//    //    duration = kYALExpandAnimationDuration / 2.0,
	//    //    damping = 0.5,
	//    //    velocity = 0.6
	//    //};

	//    //static YALAnimationParameters kYALTabBarCollapseAnimationParameters = new YALAnimationParameters()
	//    //{
	//    //    duration = kYALExpandAnimationDuration * 0.6,
	//    //    damping = 1,
	//    //    velocity = 0.2
	//    //};

	//    //static YALCenterButtonAnimationsParameters kYALCenterButtonExpandAnimationParameters = new YALCenterButtonAnimationsParameters()
	//    //{
	//    //    rotation = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = Math.PI * 2.0 + 45.0 * kDegreeToRadiansRatio
	//    //    },
	//    //    bounce = new YALAnimationParameters()
	//    //    {
	//    //        beginTime = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 45.0 * kDegreeToRadiansRatio + Math.PI / 8.0,
	//    //        toValue = 45.0 * kDegreeToRadiansRatio
	//    //    }
	//    //};

	//    //static YALCenterButtonAnimationsParameters kYALCenterButtonCollapseAnimationParameters = new YALCenterButtonAnimationsParameters()
	//    //{ 
	//    //    rotation = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = 315.0 * kDegreeToRadiansRatio
	//    //    },
	//    //    bounce = new YALAnimationParameters()
	//    //    {
	//    //        beginTime = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = Math.PI / 8.0,
	//    //        toValue = 0.0
	//    //    }
	//    //};

	//    //static YALSelectedDotAnimationsParameters kYALSelectedDotAnimationsParameters = new YALSelectedDotAnimationsParameters()
	//    //{ 
	//    //    scaleX = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = 1.0
	//    //    },
	//    //    scaleY = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = 1.0
	//    //    }
	//    //};

	//    //static YALAdditionalButtonsAnimationsParameters kYALAdditionalButtonsAnimationsParameters = new YALAdditionalButtonsAnimationsParameters()
	//    //{ 
	//    //    scaleX = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = 1.0
	//    //    },
	//    //    scaleY = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = 1.0
	//    //    },
	//    //    rotation = new YALAnimationParameters()
	//    //    {
	//    //        duration = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = 0.0,
	//    //        toValue = Math.PI * 2.0 * 5.0
	//    //    },
	//    //    bounce = new YALAnimationParameters()
	//    //    {
	//    //        beginTime = kYALExpandAnimationDuration / 4.0,
	//    //        fromValue = Math.PI / 8.0,
	//    //        toValue = 0.0
	//    //    }
	//    //};

	//    //static YALExtraTabBarItemViewAnimationParameters kYALShowExtraTabBarItemViewAnimationParameters = new YALExtraTabBarItemViewAnimationParameters()
	//    //{
	//    //    duration = kYALExpandAnimationDuration / 2.0,
	//    //    damping = 0.5f
	//    //};

	//    //static YALExtraTabBarItemViewAnimationParameters kYALHideExtraTabBarItemViewAnimationParameters = new YALExtraTabBarItemViewAnimationParameters()
	//    //{ 
	//    //    duration = kYALExpandAnimationDuration / 8.0
	//    //};
	//}
}
