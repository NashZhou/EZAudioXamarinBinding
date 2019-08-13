# Android FoldingTabBar
### Original Library
***
| Owner | Library | Documentation | License |
| :-: | :-: | :-: | :-: |
| [Yalantis](https://github.com/Yalantis) | [OfficialFoldingTabBar.Android](https://github.com/Yalantis/OfficialFoldingTabBar.Android) | [README File](https://github.com/Yalantis/OfficialFoldingTabBar.Android/blob/develop/README.md) | MIT |

### Notes
***
* Namespace: FoldingTabBarAndroid
* Android SDK 19+
* [Issues here](https://github.com/Yalantis/OfficialFoldingTabBar.Android/issues)

### Constructors
***
| Constructor |
| - |
| FoldingTabBar([Context](https://docs.microsoft.com/en-us/dotnet/api/Android.Content.Context?view=xamarin-android-sdk-9)) |
| FoldingTabBar([Context](https://docs.microsoft.com/en-us/dotnet/api/Android.Content.Context?view=xamarin-android-sdk-9), [IAttributeSet](https://docs.microsoft.com/en-us/dotnet/api/Android.Util.IAttributeSet?view=xamarin-android-sdk-9)) |
| FoldingTabBar([Context](https://docs.microsoft.com/en-us/dotnet/api/Android.Content.Context?view=xamarin-android-sdk-9), [IAttributeSet](https://docs.microsoft.com/en-us/dotnet/api/Android.Util.IAttributeSet?view=xamarin-android-sdk-9), [Integer](https://docs.microsoft.com/en-us/dotnet/api/System.Int32?view=netframework-4.8)) |

### Methods
***
| Name | Description | Type |
| - | - | - |
| SetBackgroundColor([Color](https://docs.microsoft.com/en-us/dotnet/api/android.graphics.color?view=xamarin-android-sdk-9)) | Set FoldingTabBar's background color | void |

### XML Properties
***
| Name | Description |
| - | - |
| foldingMenu | Set the menu xml file |
| foldingItemPadding | Set the padding for menu items |
| foldingMainImage | Set the main image |
| foldingSelectionColor | Set the selection color |

### Sample Usage
***
Menu XML
```
<?xml version="1.0" encoding="UTF-8"  ?>
<menu xmlns:android="http://schemas.android.com/apk/res/android">
	<item android:id="@+id/ftb_new_chat"
		android:icon="@drawable/ic_new_chat_icon"
		android:title="Chat"/>
	<item android:id="@+id/ftb_profile"
		android:icon="@drawable/ic_profile_icon"
		android:title="Profile"/>
</menu>
```
XML  
```  
<!-- Remember to add xmlns:app="http://schemas.android.com/apk/res-auto" -->  
...
<FoldingTabBarAndroid.FoldingTabBar android:layout_width="wrap_content"
	android:layout_height="wrap_content"
	android:id="@+id/folding_tab_bar"
	app:foldingMenu="@menu/menu"
	app:foldingSelectionColor="@android:color/holo_red_light"
	android:layout_marginBottom="8dp"/>
...
```
C#
```
var tabBar  = (FoldingTabBar)FindViewById(Resource.Id.folding_tab_bar);
tabBar.OnMainButtonClickedListener += (sender, e) =>
{
	// Handle main button clicks
};
tabBar.OnFoldingItemClickListener  += (id) =>
{
	switch (id.ItemId)
	{
		case  Resource.Id.ftb_new_chat:
		// Handle change
		break;
		case  Resource.Id.ftb_profile:
		// Handle change
		break;
	}
	return  false;
};
```