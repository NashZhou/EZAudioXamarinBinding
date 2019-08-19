using System;
namespace FoldingTabBarAndroidForms
{
	public class OddMenuItemsException : Exception
	{
		public override string Message
		{
			get { return "Your menu should have non-odd size"; }
		}
	}
}
