echo OFF
clear
# Clear build directories
# Clear library project build directories
rm -rf "library/MaterialDrawerLibrary/bin"
rm -rf "library/MaterialDrawerLibrary/obj"

# Clear sample project build directories
rm -rf "samples/MaterialDrawerSample/bin"
rm -rf "samples/MaterialDrawerSample/obj"

# Build sample project to ensure that binded library referenced correctly.
xbuild "/p:Configuration=Release" /t:SignAndroidPackage samples/MaterialDrawerSample/MaterialDrawerSample.csproj
# xbuild "/p:Configuration=Release;AdbTarget=-d -r" /t:Clean,Install samples/MaterialDrawerSample/MaterialDrawerSample.csproj

# Create NuGet package
mono "dev/.nuget/NuGet.exe" pack -Verbosity detailed -NoDefaultExcludes "dev/Mikepenz.MaterialDrawer.Xamarin.Android.Mac.nuspec"

echo "Successfully built project."

