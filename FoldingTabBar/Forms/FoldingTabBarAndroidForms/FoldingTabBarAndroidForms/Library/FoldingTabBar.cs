using System;
using System.Collections.Generic;
using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.View;
using Android.Support.V7.View.Menu;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
namespace FoldingTabBarAndroidForms
{
	public class FoldingTabBar : LinearLayout
	{
		internal const long ANIMATION_DURATION = 500;
		internal const long START_DELAY = 150;
		internal const float MAIN_ROTATION_START = 0;
		internal const float MAIN_ROTATION_END = 405f;
		internal const float ITEM_ROTATION_START = 180f;
		internal const float ITEM_ROTATION_END = 360f;
		internal const float ROLL_UP_ROTATION_START = -45f;
		internal const float ROLL_UP_ROTATION_END = 360F;

		//** Old **//
		//public event Func<IMenuItem, bool> OnFoldingItemClickListener;
		//public void OnFoldingItemSelected(IMenuItem item) => OnFoldingItemClickListener?.Invoke(item);
		//** New **//
		public event Func<int, bool> OnFoldingItemClickListener;
		public void OnFoldingItemSelected(int item) => OnFoldingItemClickListener?.Invoke(item);


		public event EventHandler OnMainButtonClickedListener;
		public void OnMainButtonClicked() => OnMainButtonClickedListener?.Invoke(this, new EventArgs());

		internal List<SelectedMenuItem> mData;

		internal AnimatorSet mExpandingSet = new AnimatorSet();
		internal AnimatorSet mRollupSet = new AnimatorSet();
		internal bool isAnimating;

		internal MenuBuilder mMenu;

		internal int mSize;
		internal int indexCounter;
		// * Add Context in constructor
		internal ImageView mainImageView;
		internal ImageView selectedImageView;
		internal int selectedIndex;

		internal int itemsPadding;
		internal int drawableResource;
		internal Color selectionColor;

		//** New **//
		public void AddToMenu(IList<MenuItem> items)
		{
			if (mMenu == null || items == null)
				return;

			if (items.Count % 2 != 0)
				throw new OddMenuItemsException();

			foreach (MenuItem item in items)
			{
				mMenu.Add(Menu.None, item.ItemId, Menu.None, item.Title).SetIcon(item.Icon);
			}
		}


		//** New **//
		/**
         * @param menuItem object from Android Sdk. This is same menu item
         * that you are using e.g in NavigationView or any kind of native menus
         */
		internal SelectedMenuItem InitAgainAndAddMenuItem(MenuItem menuItem)
		{
			var result = new SelectedMenuItem(Context, selectionColor)
			{
				Activated = menuItem.Checked,
				LayoutParameters = new ViewGroup.LayoutParams(mSize, mSize),
				Visibility = ViewStates.Gone
			};
			result.SetImageDrawable(menuItem.Icon);
			result.SetPadding(itemsPadding, itemsPadding, itemsPadding, itemsPadding);
			// * Look here
			AddView(result, indexCounter);

			result.Click += (sender, e) =>
			{
				OnFoldingItemSelected(menuItem.ItemId);

				menuItem.Checked = true;

				if (selectedImageView != null)
					selectedImageView.Activated = false;
				selectedImageView = result;
				selectedIndex = IndexOfChild(result);
				AnimateMenu();
			};
			indexCounter++;
			return result;
		}

		//** New **//
		public class MenuItem
		{
			public int ItemId { get; set; } = 0;
			public string Title { get; set; } = string.Empty;
			public Drawable Icon { get; set; } = null;
			public bool Checked { get; set; } = false;
		}

		public override void SetBackgroundColor(Color color)
		{
			if (Background == null)
				return;
			GradientDrawable myDrawable = (GradientDrawable)Background;
			myDrawable.SetColor(color);
		}

		internal RollUpListener rollUpListener;
		internal ExpandingListener expandingListener;

		public FoldingTabBar(Context context) : this(context, null) { }
		public FoldingTabBar(Context context, IAttributeSet attrs) : this(context, attrs, 0) { }
		public FoldingTabBar(Context context, IAttributeSet attrs, int defStyleRes) : base(context, attrs, defStyleRes)
		{
			// * Newly added
			rollUpListener = new RollUpListener(this);
			expandingListener = new ExpandingListener(this);

			mainImageView = new ImageView(context);
			mMenu = new MenuBuilder(context);
			SetGravity(GravityFlags.Center);

			if (Background == null)
				SetBackgroundResource(Resource.Drawable.background_tabbar);

			TypedArray a = InitAttrs(attrs, defStyleRes);

			mSize = GetSizeDimension();
			InitViewTreeObserver(a);
		}

		/**
	     * Initializing attributes
	     */
		internal TypedArray InitAttrs(IAttributeSet attrs, int defStyleRes) => Context.ObtainStyledAttributes(attrs, Resource.Styleable.FoldingTabBar, 0, defStyleRes);
		/**
	     * Here is size of our FoldingTabBar. Simple
	     */
		internal int GetSizeDimension() => Resources.GetDimensionPixelSize(Resource.Dimension.ftb_size_normal);
		/**
	     * This is the padding for menu items
	     */
		internal int GetItemsPadding() => Resources.GetDimensionPixelSize(Resource.Dimension.ftb_item_padding);
		/**
	     * When folding tab bar pre-draws we should initialize
	     * inflate our menu, and also add menu items, into the
	     * FoldingTabBar, also here we are initializing animators
	     * and animation sets
	     */
		internal void InitViewTreeObserver(TypedArray a) => ViewTreeObserver.AddOnPreDrawListener(new PreDrawListener(this, ref a));

		internal class PreDrawListener : Java.Lang.Object, ViewTreeObserver.IOnPreDrawListener
		{
			readonly FoldingTabBar context;
			readonly TypedArray a;
			public PreDrawListener(FoldingTabBar context, ref TypedArray a)
			{
				this.context = context;
				this.a = a;
			}
			public bool OnPreDraw()
			{
				context.ViewTreeObserver.RemoveOnPreDrawListener(this);
				context.isAnimating = true;
				context.InitAttributesValues(a);
				context.InitExpandAnimators();
				context.InitRollUpAnimators();
				context.Select(context.selectedIndex);
				return true;
			}
		}

		/**
	     * Here we are initializing default values
	     * Also here we are binding new attributes into this values
	     *
	     * @param a - incoming typed array with attributes values
	     */
		internal void InitAttributesValues(TypedArray a)
		{
			drawableResource = Resource.Drawable.ic_action_plus;
			itemsPadding = GetItemsPadding();
			selectionColor = a.GetColor(Resource.Styleable.FoldingTabBar_foldingSelectionColor, Resource.Color.ftb_selected_dot_color);
			//selectionColor = Resource.Color.ftb_selected_dot_color;
			if (a.HasValue(Resource.Styleable.FoldingTabBar_foldingMainImage))
				drawableResource = a.GetResourceId(Resource.Styleable.FoldingTabBar_foldingMainImage, 0);
			if (a.HasValue(Resource.Styleable.FoldingTabBar_foldingItemPadding))
				itemsPadding = a.GetResourceId(Resource.Styleable.FoldingTabBar_foldingItemPadding, 0);
			// if (a.HasValue(Resource.Styleable.FoldingTabBar_foldingSelectionColor))
			//     selectionColor = a.GetResourceId(Resource.Styleable.FoldingTabBar_foldingSelectionColor, 0);
			if (a.HasValue(Resource.Styleable.FoldingTabBar_foldingMenu))
				InflateMenu(a.GetResourceId(Resource.Styleable.FoldingTabBar_foldingMenu, 0));
		}

		/**
	     * Expand animation. Whole animators
	     */
		internal void InitExpandAnimators()
		{
			mExpandingSet.SetDuration(ANIMATION_DURATION);

			var destWidth = ChildCount * mSize;

			var rotationSet = new AnimatorSet();
			var scalingSet = new AnimatorSet();

			var scalingAnimator = ValueAnimator.OfInt(mSize, destWidth);
			scalingAnimator.Update += (sender, e) =>
			{
				var v = (int)e.Animation.AnimatedValue;
				var layoutParams = LayoutParameters;
				layoutParams.Width = v;
				LayoutParameters = layoutParams;
			};
			scalingAnimator.AddListener(rollUpListener);

			var rotationAnimator = ValueAnimator.OfFloat(MAIN_ROTATION_START, MAIN_ROTATION_END);
			//rotationAnimator.AddUpdateListener(new Rotation1Listener(this));
			rotationAnimator.Update += (sender, e) =>
			{
				float v = (float)e.Animation.AnimatedValue;
				mainImageView.Rotation = v;
			};

			foreach (SelectedMenuItem item in mData)
			{
				var it = ValueAnimator.OfFloat(ITEM_ROTATION_START, ITEM_ROTATION_END);
				//it.AddUpdateListener(new ITUpdateListener(item, ref it));
				it.Update += (sender, e) =>
				{
					var fraction = it.AnimatedFraction;
					item.ScaleX = fraction;
					item.ScaleY = fraction;
					item.Rotation = (float)it.AnimatedValue;
				};
				it.AddListener(expandingListener);
				rotationSet.PlayTogether(it);
			}

			scalingSet.PlayTogether(scalingAnimator, rotationAnimator);
			scalingSet.SetInterpolator(new CustomBounceInterpolator());
			rotationSet.SetInterpolator(new BounceInterpolator());

			rotationSet.StartDelay = START_DELAY;
			mExpandingSet.PlayTogether(scalingSet, rotationSet);
		}

		/**
	     * Roll-up animators. Whole roll-up animation
	     */
		internal void InitRollUpAnimators()
		{
			mRollupSet.SetDuration(ANIMATION_DURATION);

			var destWidth = mMenu.Size() * mSize;

			var rotationSet = new AnimatorSet();

			var scalingAnimator = ValueAnimator.OfInt(destWidth, mSize);
			var rotationAnimator = ValueAnimator.OfFloat(ROLL_UP_ROTATION_START, ROLL_UP_ROTATION_END);

			scalingAnimator.Update += (sender, e) =>
			{
				var v = (int)e.Animation.AnimatedValue;
				var layoutParams = LayoutParameters;
				layoutParams.Width = v;
				LayoutParameters = layoutParams;
			};
			mRollupSet.AddListener(rollUpListener);

			//rotationAnimator.AddUpdateListener(new Rotation2Listener(this));
			rotationAnimator.Update += (sender, e) =>
			{
				float v = (float)e.Animation.AnimatedValue;
				mainImageView.Rotation = v;
			};
			var scalingSet = new AnimatorSet();
			scalingSet.PlayTogether(scalingAnimator, rotationAnimator);
			scalingSet.SetInterpolator(new CustomBounceInterpolator());
			rotationSet.SetInterpolator(new BounceInterpolator());

			mRollupSet.PlayTogether(scalingSet, rotationSet);
		}

		/**
	     * Menu inflating, we are getting list of visible items,
	     * and use them in method @link initAndAddMenuItem
	     * Be careful, don't use non-odd number of menu items
	     * FTB works not good for such menus. Anyway you will have an exception
	     *
	     * @param resId your menu resource id
	     */
		internal void InflateMenu(int resId)
		{
			GetMenuInflater().Inflate(resId, mMenu);
			if (mMenu.VisibleItems.Count % 2 != 0)
				throw new OddMenuItemsException();
			mData = new List<SelectedMenuItem>();
			foreach (MenuItemImpl item in mMenu.VisibleItems)
			{
				var i = InitAndAddMenuItem(item);
				mData.Add(i);
			}
			InitMainButton(mMenu.VisibleItems.Count / 2);
		}

		/**
	     * Here we are resolving sizes of your Folding tab bar.
	     * Depending on
	     * @param measureSpec we can understand what kind of parameters
	     * do you using in your layout file
	     * In case if you are using wrap_content, we are using @dimen/ftb_size_normal
	     * by default
	     *
	     * In case if you need some custom sizes, please use them)
	     */
		internal int ResolveAdustedSize(int desiredSize, int measureSpec)
		{
			var specMode = MeasureSpec.GetMode(measureSpec);
			var specSize = MeasureSpec.GetSize(measureSpec);

			// * Maybe look here
			switch (specMode)
			{
				case MeasureSpecMode.Unspecified:
					return desiredSize;
				case MeasureSpecMode.AtMost:
					return System.Math.Min(desiredSize, specSize);
				case MeasureSpecMode.Exactly:
					return specSize;
				default:
					return desiredSize;
			}
		}

		/**
	     * Here we are overriding onMeasure and here we are making our control
	     * squared
	     */
		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
			if (!isAnimating)
			{
				var preferredSize = GetSizeDimension();
				mSize = ResolveAdustedSize(preferredSize, widthMeasureSpec);
				SetMeasuredDimension(mSize, mSize);
			}
		}

		/**
	     * Here we are saving view state
	     */
		protected override IParcelable OnSaveInstanceState()
		{
			var superState = base.OnSaveInstanceState();
			var result = new SavedState(superState)
			{
				Selection = selectedIndex
			};
			return result;
		}

		/**
	     * Here we are restoring view state (state, selection)
	     */
		protected override void OnRestoreInstanceState(IParcelable state)
		{
			var s = state as SavedState;
			base.OnRestoreInstanceState(s.SuperState);
			selectedIndex = s.Selection;
		}

		/**
	     * Main button (+/x) initialization
	     * Adding listener to the main button click
	     */
		internal void InitMainButton(int mainButtonIndex)
		{
			mainImageView.SetImageResource(drawableResource);
			mainImageView.LayoutParameters = new ViewGroup.LayoutParams(mSize, mSize);
			mainImageView.Click += (sender, e) =>
			{
				OnMainButtonClicked();
				AnimateMenu();
			};
			AddView(mainImageView, mainButtonIndex);
			mainImageView.SetPadding(itemsPadding, itemsPadding, itemsPadding, itemsPadding);
		}

		/**
	     * @param menuItem object from Android Sdk. This is same menu item
	     * that you are using e.g in NavigationView or any kind of native menus
	     */
		internal SelectedMenuItem InitAndAddMenuItem(IMenuItem menuItem)
		{
			var result = new SelectedMenuItem(Context, selectionColor)
			{
				Activated = menuItem.IsChecked,
				LayoutParameters = new ViewGroup.LayoutParams(mSize, mSize),
				Visibility = ViewStates.Gone
			};
			result.SetImageDrawable(menuItem.Icon);
			result.SetPadding(itemsPadding, itemsPadding, itemsPadding, itemsPadding);
			// * Look here
			AddView(result, indexCounter);

			result.Click += (sender, e) =>
			{
				OnFoldingItemSelected(menuItem.ItemId);

				menuItem.SetChecked(true);

				if (selectedImageView != null)
					selectedImageView.Activated = false;
				selectedImageView = result;
				selectedIndex = IndexOfChild(result);
				AnimateMenu();
			};
			indexCounter++;
			return result;
		}

		internal void Select(int Position)
		{
			selectedImageView = (SelectedMenuItem)GetChildAt(Position);
			selectedImageView.Activated = true;
		}

		/**
	     * measuredWidth - mSize = 0 we can understand that our menu is closed
	     * But on some devices I've found a case when we don't have exactly 0. So
	     * now we defined some range to understand what is the state of our menu
	     */
		internal void AnimateMenu()
		{
			var s = MeasuredWidth - mSize;
			if (-2 <= s && s <= 2)
				Expand();
			else
				RollUp();
		}

		/**
	     * These two public functions can be used to open our menu
	     * externally
	     */
		internal void Expand() => mExpandingSet.Start();
		internal void RollUp() => mRollupSet.Start();

		/**
	     * Getting SupportMenuInflater to get all visible items from
	     * menu object
	     */
		internal MenuInflater GetMenuInflater() => new SupportMenuInflater(Context);

		/**
	     * Here we should hide all items, and deactivate menu item
	     */
		internal class RollUpListener : Java.Lang.Object, Animator.IAnimatorListener
		{
			readonly FoldingTabBar context;
			public RollUpListener(FoldingTabBar context) { this.context = context; }
			public void OnAnimationCancel(Animator animation) { }

			public void OnAnimationEnd(Animator animation) { }

			public void OnAnimationRepeat(Animator animation) { }

			public void OnAnimationStart(Animator animation)
			{
				foreach (SelectedMenuItem item in context.mData)
				{
					item.Visibility = ViewStates.Gone;
				}
				if (context.selectedImageView != null)
					context.selectedImageView.Activated = false;
			}
		}

		/**
	     * This listener we need to show our Menu items
	     * And also after animation was finished we should activate
	     * our SelectableImageView
	     */
		internal class ExpandingListener : Java.Lang.Object, Animator.IAnimatorListener
		{
			readonly FoldingTabBar context;
			public ExpandingListener(FoldingTabBar context) { this.context = context; }
			public void OnAnimationCancel(Animator animation) { }

			public void OnAnimationEnd(Animator animation)
			{
				if (context.selectedImageView != null)
					context.selectedImageView.Activated = true;
			}

			public void OnAnimationRepeat(Animator animation) { }

			public void OnAnimationStart(Animator animation)
			{
				foreach (SelectedMenuItem item in context.mData)
				{
					item.Visibility = ViewStates.Visible;
				}
			}
		}

		/**
	     * We have to save state and selection of our View
	     */
		internal class SavedState : BaseSavedState
		{
			public int Selection { get; set; }
			public SavedState(IParcelable superState) : base(superState) { }
			public SavedState(Parcel inp) : base(inp) { Selection = inp.ReadInt(); }

			public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
			{
				base.WriteToParcel(dest, flags);
				dest.WriteInt(Selection);
			}

			new class Creator : Java.Lang.Object, IParcelableCreator
			{
				public Java.Lang.Object CreateFromParcel(Parcel source)
				{
					return new SavedState(source);
				}

				public Java.Lang.Object[] NewArray(int size)
				{
					return new Java.Lang.Object[size];
				}
			}
		}
	}
}
