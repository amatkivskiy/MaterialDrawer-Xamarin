# MaterialDrawer-Xamarin
Xamarin bindings for https://github.com/mikepenz/MaterialDrawer

## Issues
Due to different generic type systems in C# and Java fluent api is not avalible for:
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
