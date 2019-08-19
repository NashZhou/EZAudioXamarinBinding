using System;
using System.Runtime.InteropServices;
using ObjCRuntime;
using UIKit;
namespace FoldingTabBariOS
{
	[StructLayout(LayoutKind.Sequential)]
	public struct YALAnimationParameters
	{
		public double beginTime;

		public double duration;

		public double fromValue;

		public double toValue;

		public double damping;

		public double velocity;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct YALAdditionalButtonsAnimationsParameters
	{
		public YALAnimationParameters scaleX;

		public YALAnimationParameters scaleY;

		public YALAnimationParameters rotation;

		public YALAnimationParameters bounce;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct YALCenterButtonAnimationsParameters
	{
		public YALAnimationParameters rotation;

		public YALAnimationParameters bounce;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct YALExtraTabBarItemViewAnimationParameters
	{
		public double duration;

		public double delay;

		public nfloat damping;

		public nfloat velocity;

		public UIViewAnimationOptions options;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct YALSelectedDotAnimationsParameters
	{
		public YALAnimationParameters scaleX;

		public YALAnimationParameters scaleY;
	}

	[Native]
	public enum YALTabBarState : ulong
	{
		Collapsed,
		Expanded
	}
}
