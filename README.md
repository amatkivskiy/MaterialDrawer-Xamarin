[![No Maintenance Intended](http://unmaintained.tech/badge.svg)](http://unmaintained.tech/) 
# DEPRECATED

:warning: This is no longer supported - unfortunately, I am not in the Xamarin development anymore.  :warning:

# MaterialDrawer-Xamarin

[![Join the chat at https://gitter.im/amatkivskiy/MaterialDrawer-Xamarin](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/amatkivskiy/MaterialDrawer-Xamarin?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

![Nuget badge](https://badge.fury.io/nu/Mikepenz.MaterialDrawer.Xamarin.Android.svg)

Xamarin bindings for https://github.com/mikepenz/MaterialDrawer

## Download
[NuGet Package](https://www.nuget.org/packages/Mikepenz.MaterialDrawer.Xamarin.Android/)

## Issues
Due to different generic type systems in C# and Java fluent api is not available for:
- ```PrimaryDrawerItem```
- ```SecondaryDrawerItem```
- ```ToggleDrawerItem```
- ```SwitchDrawerItem```

Simple workaround is to replace 

```
new PrimaryDrawerItem().WithName(Resource.String.drawer_item_compact_header).WithIcon(GoogleMaterial.Icon.GmdWbSunny).WithIdentifier(1).WithCheckable(false);
```

with 

```
var header = new PrimaryDrawerItem();
header.WithName(Resource.String.drawer_item_compact_header);
header.WithIcon(GoogleMaterial.Icon.GmdWbSunny);
header.WithIdentifier(1);
header.WithCheckable(false);
```
