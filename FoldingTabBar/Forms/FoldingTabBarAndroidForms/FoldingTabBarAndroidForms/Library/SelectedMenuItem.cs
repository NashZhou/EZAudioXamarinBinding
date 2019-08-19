using System;
using Android.Content;
using Android.Graphics;
using Android.Support.Annotation;
using Android.Support.V4.Content.Res;
using Android.Util;
using Android.Widget;
namespace FoldingTabBarAndroidForms
{
	public class SelectedMenuItem : ImageView
	{
		internal Paint mCirclePaint;
		internal float radius;

		public SelectedMenuItem(Context context, Color colorRes) : this(context, null, colorRes) { }

		public SelectedMenuItem(Context context, IAttributeSet attrs, Color colorRes) : this(context, attrs, 0, colorRes) { }

		public SelectedMenuItem(Context context, IAttributeSet attrs, int defStyleRes, Color colorRes) : base(context, attrs, defStyleRes)
		{
			mCirclePaint = new Paint(PaintFlags.AntiAlias)
			{
				Color = colorRes
			};
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);
			if (Activated)
				DrawCircleIcon(canvas);
		}

		void DrawCircleIcon(Canvas canvas)
		{
			canvas.DrawCircle(canvas.Width / 2.0f, canvas.Height - PaddingBottom / 1.5f, radius, mCirclePaint);
			if (radius <= canvas.Width / 20.0f)
			{
				radius++;
				Invalidate();
			}
		}
	}
}
