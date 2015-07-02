# MaterialDrawer-Xamarin
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
