# Forms FoldingTabBar (iOS & Android)
### Notes
***
* [Available on NuGet](https://www.nuget.org/packages/FoldingTabBar.Forms/)
* Namespace: FoldingTabBar.Forms;
* Install plugin on PCL, iOS, and Android projects
* Install dependencies for [Android](https://www.nuget.org/packages/FoldingTabBar.Android.For.Forms/) and [iOS](https://www.nuget.org/packages/FoldingTabBar.iOS/)
* FoldingTabbedPage works similarly to TabbedPage
* Images assets should be added to iOS and Android resource folders
* Call `FoldingTabbedRenderer.Init();` after each `Xamarin.Forms.Init();` in both the iOS and Android projects
* No event handlers yet

### XAML Properties
***
| Name | Description |
| - | - |
| FoldingBarBackgroundColor | Set the background color |
| FoldingSelectionColor | Set the selection dot color |

### Sample Usage
***
XAML
```
<?xml version="1.0" encoding="utf-8" ?>
<ftb:FoldingTabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
		xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
		xmlns:d="http://xamarin.com/schemas/2014/forms/design"
		xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
		xmlns:local="clr-namespace:EXFoldingTabBarForms"
		xmlns:ftb="clr-namespace:FoldingTabBar.Forms;assembly=FoldingTabBar.Forms"
		mc:Ignorable="d"
		x:Class="EXFoldingTabBarForms.MainPage"
		FoldingBarBackgroundColor="#edb721"
		FoldingSelectionColor="White">
	<local:PrimaryPage Icon="new_chat_icon"/>
	<local:SecondaryPage Icon="profile_icon"/>
</ftb:FoldingTabbedPage>
```