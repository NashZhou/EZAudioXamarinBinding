# iOS FoldingTabBar
### Original Library
***
| Owner | Library | Documentation | License |
| :-: | :-: | :-: | :-: |
| [Yalantis](https://github.com/Yalantis) | [FoldingTabBar.iOS](https://github.com/Yalantis/FoldingTabBar.iOS) | [README File](https://github.com/Yalantis/FoldingTabBar.iOS/blob/master/README.md) | MIT |

### Notes
***
* [Available on NuGet](https://www.nuget.org/packages/FoldingTabBar.iOS/)
* Namespace: FoldingTabBariOS
* iOS 10+
* Binded API found in APIDefinition.cs
* [Issues here](https://github.com/Yalantis/FoldingTabBar.iOS/issues)

### YALFoldingTabBarController
***
| Property name | Type |
| - | - |
| LeftBarItems | YALTabBarItem[] |
| RightBarItems | YALTabBarItem[] |
| CenterButtonImage | [UIImage](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiimage?view=xamarin-ios-sdk-12) |
| TabBarViewHeight | [nfloat](https://docs.microsoft.com/en-us/dotnet/api/system.nfloat?view=xamarin-ios-sdk-12) |
| TabBarView | YALFoldingTabBar |

### YALTabBarItem
***
| Constructor |
| - |
| YALTabBarItem([UIImage](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiimage?view=xamarin-ios-sdk-12) itemImage, [UIImage](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiimage?view=xamarin-ios-sdk-12) leftItemImage, [UIImage](https://docs.microsoft.com/en-us/dotnet/api/uikit.uiimage?view=xamarin-ios-sdk-12) rightItemImage) |

### IYALTabBarDelegate
***
| Name | Description | Type |
| - | - | - |
| TabBarDidSelectItemAtIndex(YALFoldingTabBar tabBar, uint index) | Callback for selecting menu items | void |
| TabBarShouldSelectItemAtIndex(YALFoldingTabBar tabBar, uint index) | Callback for selecting menu items | void |
| TabBarWillCollapse(YALFoldingTabBar tabBar) | Callback for when the tab bar collapses | void |
| TabBarWillExpand(YALFoldingTabBar tabBar) | Callback for when the tab bar expands | void |
| TabBarDidCollapse(YALFoldingTabBar tabBar) | Callback for when the tab bar collapses | void |
| TabBarDidExpand(YALFoldingTabBar tabBar) | Callback for when the tab bar expands | void |
| TabBarDidSelectExtraLeftItem(YALFoldingTabBar tabBar) | Callback for left extra item tap | void |
| TabBarDidSelectExtraRightItem(YALFoldingTabBar tabBar) | Callback for right extra item tap | void |

### Programmatic Usage
***
* Refer to sample project
* Create a class that inherits from YALFoldingTabBarController
* Set class as rootViewController in AppDelegate
* Configure FoldingTabBar via the TabBarView property
* Set and implement delegate methods
* Configure the height of the FoldingTabBar
* Set the properties ViewControllers, LeftBarItems, RightBarItems, and CenterButtonImage