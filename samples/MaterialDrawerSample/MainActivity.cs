using Android.OS;
using Android.Support.V7.App;
using Android.App;
using Com.Mikepenz.Materialdrawer;
using Com.Mikepenz.Materialdrawer.Model.Interfaces;
using Com.Mikepenz.Materialdrawer.Model;
using Com.Mikepenz.Materialdrawer.Accountswitcher;
using Android.Support.V7.Widget;

namespace MaterialDrawerSample
{
  [Activity(Label = "MaterialDrawerSample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
  public class MainActivity : AppCompatActivity
  {
    private Drawer result;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.Main);

      Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
      SetSupportActionBar(toolbar);

      IProfile profile = new ProfileDrawerItem();
      profile.WithName("Mike Penz");
      profile.WithEmail("mikepenz@gmail.com");
      profile.WithIcon(Resources.GetDrawable(Resource.Drawable.Icon));

      var primaryItem = new PrimaryDrawerItem();
      primaryItem.WithName("Primary Item");
      primaryItem.Icon = Resources.GetDrawable(Resource.Drawable.Icon);

      var devider = new SectionDrawerItem();
      devider.WithName("About");

      var secondaryItem = new SecondaryDrawerItem();
      secondaryItem.WithName(Resource.String.app_name);
      secondaryItem.Icon = Resources.GetDrawable(Resource.Drawable.Icon);

      var header = new AccountHeaderBuilder()
        .WithActivity(this)
        .WithHeaderBackground(Android.Resource.Color.DarkerGray)
        .AddProfiles(profile)
        .WithDrawer(this.result)
        .WithSavedInstance(savedInstanceState)
        .Build();


      //Create the drawer
      var builder = new DrawerBuilder(this);
      builder.WithRootView(Resource.Id.drawer_container);
      builder.WithAccountHeader(header);
      builder.WithToolbar(toolbar);
      builder.WithActionBarDrawerToggleAnimated(true);
      builder.AddDrawerItems(primaryItem, devider, secondaryItem);
      builder.WithSavedInstance(savedInstanceState);

      this.result = builder.Build();
    }

    public override void OnBackPressed()
    {
      //handle the back press :D close the drawer first and if the drawer is closed close the activity
      if (result != null && result.IsDrawerOpen) {
        result.CloseDrawer();
      } else {
        base.OnBackPressed();
      }
    }

    protected override void OnSaveInstanceState(Bundle outState)
    {
      //add the values which need to be saved from the drawer to the bundle
      outState = result.SaveInstanceState(outState);
      base.OnSaveInstanceState(outState);
    }
  }
}


