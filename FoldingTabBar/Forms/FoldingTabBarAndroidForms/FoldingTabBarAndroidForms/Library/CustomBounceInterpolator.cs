using System;
using Android.Views.Animations;
namespace FoldingTabBarAndroidForms
{
	class CustomBounceInterpolator : Java.Lang.Object, IInterpolator
	{
		public double Amplitude { get; set; } = 0.1;
		public double Frequency { get; set; } = 0.8;

		public float GetInterpolation(float input)
		{
			return (float)(-1.0 * Math.Exp(-input / Amplitude) * Math.Cos(Frequency * input) + 1);
		}
	}
}
