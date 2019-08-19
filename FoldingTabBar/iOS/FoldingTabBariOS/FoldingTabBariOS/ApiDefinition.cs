using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
namespace FoldingTabBariOS
{
	// @interface Insets (UIView)
	[Category]
	[BaseType(typeof(UIView))]
	interface UIView_Insets
	{
		// @property (readonly, assign, nonatomic) CGFloat bottomInset;
		[Export("bottomInset")]
		nfloat GetBottomInset();

		[Export("setBottomInset:")]
		void SetBottomInset(nfloat bottomInset);
	}

	/*
	// @interface YALTabBarViewAnimations (CAAnimation)
	[BaseType(typeof(CAAnimation)), Category]
    interface CAAnimation_YALTabBarViewAnimations
	{
		// +(CAAnimation *)animationForAdditionalButton;
		[Static]
		[Export("animationForAdditionalButton")]
		//[Verify(MethodToProperty)]
		CAAnimation AnimationForAdditionalButton();

		// +(CAAnimation *)animationForExtraLeftBarItem;
		[Static]
		[Export("animationForExtraLeftBarItem")]
		//[Verify(MethodToProperty)]
		CAAnimation AnimationForExtraLeftBarItem();

		// +(CAAnimation *)animationForExtraRightBarItem;
		[Static]
		[Export("animationForExtraRightBarItem")]
		//[Verify(MethodToProperty)]
		CAAnimation AnimationForExtraRightBarItem();

		// +(CAAnimation *)animationForTabBarExpandFromRect:(CGRect)fromRect toRect:(CGRect)toRect;
		[Static]
		[Export("animationForTabBarExpandFromRect:toRect:")]
		CAAnimation AnimationForTabBarExpand(CGRect fromRect, CGRect toRect);

		// +(CAAnimation *)animationForTabBarCollapseFromRect:(CGRect)fromRect toRect:(CGRect)toRect;
		[Static]
		[Export("animationForTabBarCollapseFromRect:toRect:")]
		CoreAnimation.CAAnimation AnimationForTabBarCollapse(CGRect fromRect, CGRect toRect);

		// +(CAAnimation *)animationForCenterButtonExpand;
		[Static]
		[Export("animationForCenterButtonExpand")]
		//[Verify(MethodToProperty)]
		CAAnimation AnimationForCenterButtonExpand();

		// +(CAAnimation *)animationForCenterButtonCollapse;
		[Static]
		[Export("animationForCenterButtonCollapse")]
		//[Verify(MethodToProperty)]
		CAAnimation AnimationForCenterButtonCollapse();

		// +(CAAnimation *)showSelectedDotAnimation;
		[Static]
		[Export("showSelectedDotAnimation")]
		//[Verify(MethodToProperty)]
		CAAnimation ShowSelectedDotAnimation();
	}
    */

	/*
	// @interface TransactionWithAnimationsAndCompletion (CATransaction)
    [BaseType(typeof(CATransaction)), Category]
    interface CATransaction_TransactionWithAnimationsAndCompletion
	{
		// +(void)transactionWithAnimations:(void (^)(void))animations andCompletion:(void (^)(void))completion;
		[Static]
		[Export("transactionWithAnimations:andCompletion:")]
		void TransactionWithAnimations(Action animations, Action completion);
	}
    */

	/*
	//[Verify(ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern const CGFloat YALBottomSelectedDotDefaultSize;
		[Static]
		[Field("YALBottomSelectedDotDefaultSize")]
		nfloat YALBottomSelectedDotDefaultSize { get; }

		// extern const CGFloat YALBottomSelectedDotOffset;
		[Static]
		[Field("YALBottomSelectedDotOffset")]
		nfloat YALBottomSelectedDotOffset { get; }

		// extern const CGFloat YALTabBarViewDefaultHeight;
		[Static]
		[Field("YALTabBarViewDefaultHeight")]
		nfloat YALTabBarViewDefaultHeight { get; }

		// extern const CGFloat YALExtraTabBarItemsDefaultHeight;
		[Static]
		[Field("YALExtraTabBarItemsDefaultHeight")]
		nfloat YALExtraTabBarItemsDefaultHeight { get; }

		// extern const CGFloat YALForExtraTabBarItemsDefaultOffset;
		[Static]
		[Field("YALForExtraTabBarItemsDefaultOffset")]
		nfloat YALForExtraTabBarItemsDefaultOffset { get; }

		// extern const UIEdgeInsets YALTabBarViewHDefaultEdgeInsets;
		[Static]
		[Field("YALTabBarViewHDefaultEdgeInsets")]
		UIEdgeInsets YALTabBarViewHDefaultEdgeInsets { get; }

		// extern const UIEdgeInsets YALTabBarViewItemsDefaultEdgeInsets;
		[Static]
		[Field("YALTabBarViewItemsDefaultEdgeInsets")]
		UIEdgeInsets YALTabBarViewItemsDefaultEdgeInsets { get; }

		// extern NSString *const YALCenterButtonExpandAnimation;
		[Static]
		[Field("YALCenterButtonExpandAnimation")]
		NSString YALCenterButtonExpandAnimation { get; }

		// extern NSString *const YALCenterButtonCollapseAnimation;
		[Static]
		[Field("YALCenterButtonCollapseAnimation")]
		NSString YALCenterButtonCollapseAnimation { get; }

		// extern NSString *const YALAdditionalButtonsAnimation;
		[Static]
		[Field("YALAdditionalButtonsAnimation")]
		NSString YALAdditionalButtonsAnimation { get; }

		// extern NSString *const YALTabBarExpandAnimation;
		[Static]
		[Field("YALTabBarExpandAnimation")]
		NSString YALTabBarExpandAnimation { get; }

		// extern NSString *const YALTabBarExpandCollapseAnimation;
		[Static]
		[Field("YALTabBarExpandCollapseAnimation")]
		NSString YALTabBarExpandCollapseAnimation { get; }

		// extern NSString *const YALExtraLeftBarItemAnimation;
		[Static]
		[Field("YALExtraLeftBarItemAnimation")]
		NSString YALExtraLeftBarItemAnimation { get; }

		// extern NSString *const YALExtraRightBarItemAnimation;
		[Static]
		[Field("YALExtraRightBarItemAnimation")]
		NSString YALExtraRightBarItemAnimation { get; }

		// extern const CFTimeInterval kYALExpandAnimationDuration;
		[Static]
		[Field("kYALExpandAnimationDuration")]
		double kYALExpandAnimationDuration { get; }
	}

	//[Verify(ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern const YALAdditionalButtonsAnimationsParameters kYALAdditionalButtonsAnimationsParameters;
		[Static]
		[Field("kYALAdditionalButtonsAnimationsParameters")]
		YALAdditionalButtonsAnimationsParameters kYALAdditionalButtonsAnimationsParameters { get; }

		// extern const YALSelectedDotAnimationsParameters kYALSelectedDotAnimationsParameters;
		[Static]
		[Field("kYALSelectedDotAnimationsParameters")]
		YALSelectedDotAnimationsParameters kYALSelectedDotAnimationsParameters { get; }

		// extern const YALAnimationParameters kYALExtraLeftTabBarItemAnimationParameters;
		[Static]
		[Field("kYALExtraLeftTabBarItemAnimationParameters")]
		YALAnimationParameters kYALExtraLeftTabBarItemAnimationParameters { get; }

		// extern const YALAnimationParameters kYALExtraRightTabBarItemAnimationParameters;
		[Static]
		[Field("kYALExtraRightTabBarItemAnimationParameters")]
		YALAnimationParameters kYALExtraRightTabBarItemAnimationParameters { get; }

		// extern const YALAnimationParameters kYALTabBarExpandAnimationParameters;
		[Static]
		[Field("kYALTabBarExpandAnimationParameters")]
		YALAnimationParameters kYALTabBarExpandAnimationParameters { get; }

		// extern const YALAnimationParameters kYALTabBarCollapseAnimationParameters;
		[Static]
		[Field("kYALTabBarCollapseAnimationParameters")]
		YALAnimationParameters kYALTabBarCollapseAnimationParameters { get; }

		// extern const YALCenterButtonAnimationsParameters kYALCenterButtonExpandAnimationParameters;
		[Static]
		[Field("kYALCenterButtonExpandAnimationParameters")]
		YALCenterButtonAnimationsParameters kYALCenterButtonExpandAnimationParameters { get; }

		// extern const YALCenterButtonAnimationsParameters kYALCenterButtonCollapseAnimationParameters;
		[Static]
		[Field("kYALCenterButtonCollapseAnimationParameters")]
		YALCenterButtonAnimationsParameters kYALCenterButtonCollapseAnimationParameters { get; }

		// extern const YALAnimationParameters kYALBounceAnimationParameters;
		[Static]
		[Field("kYALBounceAnimationParameters")]
		YALAnimationParameters kYALBounceAnimationParameters { get; }

		// extern const YALExtraTabBarItemViewAnimationParameters kYALShowExtraTabBarItemViewAnimationParameters;
		[Static]
		[Field("kYALShowExtraTabBarItemViewAnimationParameters")]
		YALExtraTabBarItemViewAnimationParameters kYALShowExtraTabBarItemViewAnimationParameters { get; }

		// extern const YALExtraTabBarItemViewAnimationParameters kYALHideExtraTabBarItemViewAnimationParameters;
		[Static]
		[Field("kYALHideExtraTabBarItemViewAnimationParameters")]
		YALExtraTabBarItemViewAnimationParameters kYALHideExtraTabBarItemViewAnimationParameters { get; }
	}
    */

	// @interface YALCustomHeightTabBar : UITabBar
	[BaseType(typeof(UITabBar))]
	interface YALCustomHeightTabBar
	{
		// @property (nonatomic) CGFloat barHeight;
		[Export("barHeight")]
		nfloat BarHeight { get; set; }
	}

	// @protocol YALTabBarDataSource <NSObject>
	[BaseType(typeof(NSObject))]
	[Protocol, Model]
	interface YALTabBarDataSource
	{
		// @required -(NSArray * _Nonnull)leftTabBarItemsInTabBarView:(YALFoldingTabBar * _Nonnull)tabBarView;
		[Abstract]
		[Export("leftTabBarItemsInTabBarView:")]
		//[Verify(StronglyTypedNSArray)]
		YALTabBarItem[] LeftTabBarItemsInTabBarView(YALFoldingTabBar tabBarView);

		// @required -(NSArray * _Nonnull)rightTabBarItemsInTabBarView:(YALFoldingTabBar * _Nonnull)tabBarView;
		[Abstract]
		[Export("rightTabBarItemsInTabBarView:")]
		//[Verify(StronglyTypedNSArray)]
		YALTabBarItem[] RightTabBarItemsInTabBarView(YALFoldingTabBar tabBarView);

		// @required -(UIImage * _Nonnull)centerImageInTabBarView:(YALFoldingTabBar * _Nonnull)tabBarView;
		[Abstract]
		[Export("centerImageInTabBarView:")]
		UIImage CenterImageInTabBarView(YALFoldingTabBar tabBarView);
	}

	// @protocol YALTabBarDelegate <NSObject>
	[BaseType(typeof(NSObject))]
	[Protocol, Model]
	interface YALTabBarDelegate
	{
		// @optional -(void)tabBar:(YALFoldingTabBar * _Nonnull)tabBar didSelectItemAtIndex:(NSUInteger)index;
		[Export("tabBar:didSelectItemAtIndex:")]
		void TabBarDidSelectItemAtIndex(YALFoldingTabBar tabBar, uint index);

		// @optional -(BOOL)tabBar:(YALFoldingTabBar * _Nonnull)tabBar shouldSelectItemAtIndex:(NSUInteger)index;
		[Export("tabBar:shouldSelectItemAtIndex:")]
		bool TabBarShouldSelectItemAtIndex(YALFoldingTabBar tabBar, uint index);

		// @optional -(void)tabBarWillCollapse:(YALFoldingTabBar * _Nonnull)tabBar;
		[Export("tabBarWillCollapse:")]
		void TabBarWillCollapse(YALFoldingTabBar tabBar);

		// @optional -(void)tabBarWillExpand:(YALFoldingTabBar * _Nonnull)tabBar;
		[Export("tabBarWillExpand:")]
		void TabBarWillExpand(YALFoldingTabBar tabBar);

		// @optional -(void)tabBarDidCollapse:(YALFoldingTabBar * _Nonnull)tabBar;
		[Export("tabBarDidCollapse:")]
		void TabBarDidCollapse(YALFoldingTabBar tabBar);

		// @optional -(void)tabBarDidExpand:(YALFoldingTabBar * _Nonnull)tabBar;
		[Export("tabBarDidExpand:")]
		void TabBarDidExpand(YALFoldingTabBar tabBar);

		// @optional -(void)tabBarDidSelectExtraLeftItem:(YALFoldingTabBar * _Nonnull)tabBar;
		[Export("tabBarDidSelectExtraLeftItem:")]
		void TabBarDidSelectExtraLeftItem(YALFoldingTabBar tabBar);

		// @optional -(void)tabBarDidSelectExtraRightItem:(YALFoldingTabBar * _Nonnull)tabBar;
		[Export("tabBarDidSelectExtraRightItem:")]
		void TabBarDidSelectExtraRightItem(YALFoldingTabBar tabBar);
	}

	// @interface YALFoldingTabBar : UIView
	[BaseType(typeof(UIKit.UIView))]
	interface YALFoldingTabBar
	{
		// -(instancetype _Nonnull)initWithController:(YALFoldingTabBarController * _Nonnull)controller;
		[Export("initWithController:")]
		IntPtr Constructor(YALFoldingTabBarController controller);

		// @property (nonatomic, weak) id<YALTabBarDataSource> _Nullable dataSource;
		[NullAllowed, Export("dataSource", ArgumentSemantic.Weak)]
		YALTabBarDataSource DataSource { get; set; }

		[Wrap("WeakDelegate")]
		[NullAllowed]
		YALTabBarDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<YALTabBarDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) YALTabBarState state;
		[Export("state", ArgumentSemantic.Assign)]
		YALTabBarState State { get; set; }

		// @property (assign, nonatomic) NSUInteger selectedTabBarItemIndex;
		[Export("selectedTabBarItemIndex")]
		nuint SelectedTabBarItemIndex { get; set; }

		// @property (copy, nonatomic) UIColor * _Nonnull tabBarColor;
		[Export("tabBarColor", ArgumentSemantic.Copy)]
		UIColor TabBarColor { get; set; }

		// @property (copy, nonatomic) UIColor * _Nonnull dotColor;
		[Export("dotColor", ArgumentSemantic.Copy)]
		UIColor DotColor { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets tabBarViewEdgeInsets;
		[Export("tabBarViewEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TabBarViewEdgeInsets { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets tabBarItemsEdgeInsets;
		[Export("tabBarItemsEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TabBarItemsEdgeInsets { get; set; }

		// @property (assign, nonatomic) CGFloat extraTabBarItemHeight;
		[Export("extraTabBarItemHeight")]
		nfloat ExtraTabBarItemHeight { get; set; }

		// @property (assign, nonatomic) CGFloat offsetForExtraTabBarItems;
		[Export("offsetForExtraTabBarItems")]
		nfloat OffsetForExtraTabBarItems { get; set; }

		// -(void)changeExtraLeftTabBarItemWithImage:(UIImage * _Nullable)image;
		[Export("changeExtraLeftTabBarItemWithImage:")]
		void ChangeExtraLeftTabBarItemWithImage([NullAllowed] UIImage image);

		// -(void)changeExtraRightTabBarItemWithImage:(UIImage * _Nullable)image;
		[Export("changeExtraRightTabBarItemWithImage:")]
		void ChangeExtraRightTabBarItemWithImage([NullAllowed] UIImage image);
	}

	// @interface YALFoldingTabBarController : UITabBarController
	[BaseType(typeof(UITabBarController))]
	interface YALFoldingTabBarController
	{
		// @property (copy, nonatomic) NSArray * _Nonnull leftBarItems;
		[Export("leftBarItems", ArgumentSemantic.Copy)]
		//[Verify(StronglyTypedNSArray)]
		YALTabBarItem[] LeftBarItems { get; set; }

		// @property (copy, nonatomic) NSArray * _Nonnull rightBarItems;
		[Export("rightBarItems", ArgumentSemantic.Copy)]
		//[Verify(StronglyTypedNSArray)]
		YALTabBarItem[] RightBarItems { get; set; }

		// @property (nonatomic, strong) UIImage * _Nonnull centerButtonImage;
		[Export("centerButtonImage", ArgumentSemantic.Strong)]
		UIImage CenterButtonImage { get; set; }

		// @property (assign, nonatomic) CGFloat tabBarViewHeight;
		[Export("tabBarViewHeight")]
		nfloat TabBarViewHeight { get; set; }

		// @property (nonatomic, strong) YALFoldingTabBar * _Nonnull tabBarView;
		[Export("tabBarView", ArgumentSemantic.Strong)]
		YALFoldingTabBar TabBarView { get; set; }
	}

	// @interface YALSpringAnimation : CAKeyframeAnimation
	[BaseType(typeof(CAKeyFrameAnimation))]
	interface YALSpringAnimation
	{
		// +(instancetype)animationWithKeyPath:(NSString *)keyPath duration:(CFTimeInterval)duration damping:(double)damping velocity:(double)velocity fromValue:(double)fromValue toValue:(double)toValue;
		[Static]
		[Export("animationWithKeyPath:duration:damping:velocity:fromValue:toValue:")]
		YALSpringAnimation AnimationWithKeyPath(string keyPath, double duration, double damping, double velocity, double fromValue, double toValue);

		// +(instancetype)animationForRoundedRectPathWithduration:(CFTimeInterval)duration damping:(double)damping velocity:(double)velocity fromValue:(CGRect)fromValue toValue:(CGRect)toValue;
		[Static]
		[Export("animationForRoundedRectPathWithduration:damping:velocity:fromValue:toValue:")]
		YALSpringAnimation AnimationForRoundedRectPathWithduration(double duration, double damping, double velocity, CGRect fromValue, CGRect toValue);
	}

	// @interface YALTabBarItem : NSObject
	[BaseType(typeof(NSObject))]
	interface YALTabBarItem
	{
		// @property (nonatomic, strong) UIImage * _Nullable itemImage;
		[NullAllowed, Export("itemImage", ArgumentSemantic.Strong)]
		UIImage ItemImage { get; set; }

		// @property (nonatomic, strong) UIImage * _Nullable leftImage;
		[NullAllowed, Export("leftImage", ArgumentSemantic.Strong)]
		UIImage LeftImage { get; set; }

		// @property (nonatomic, strong) UIImage * _Nullable rightImage;
		[NullAllowed, Export("rightImage", ArgumentSemantic.Strong)]
		UIImage RightImage { get; set; }

		// @property (nonatomic, strong) UIImage * _Nullable leftHighlightedImage;
		[NullAllowed, Export("leftHighlightedImage", ArgumentSemantic.Strong)]
		UIImage LeftHighlightedImage { get; set; }

		// @property (nonatomic, strong) UIImage * _Nullable rightHighlightedImage;
		[NullAllowed, Export("rightHighlightedImage", ArgumentSemantic.Strong)]
		UIImage RightHighlightedImage { get; set; }

		// -(instancetype _Nonnull)initWithItemImage:(UIImage * _Nullable)itemImage leftItemImage:(UIImage * _Nullable)leftItemImage rightItemImage:(UIImage * _Nullable)rightItemImage;
		[Export("initWithItemImage:leftItemImage:rightItemImage:")]
		IntPtr Constructor([NullAllowed] UIImage itemImage, [NullAllowed] UIImage leftItemImage, [NullAllowed] UIImage rightItemImage);
	}
}
