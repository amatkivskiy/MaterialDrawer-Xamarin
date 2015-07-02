namespace Sample
{
  using Android.App;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Net;
  using Android.Views;
  using Android.Widget;
  using Android.Util;
  using Mikepenz.Google_material_typeface_library;
  using Mikepenz.Iconics;
  using Mikepenz.Iconics.Typeface;
  using Mikepenz.MaterialDrawer;
  using Mikepenz.MaterialDrawer.Accountswitcher;
  using Mikepenz.MaterialDrawer.Models;
  using Mikepenz.MaterialDrawer.Models.Interfaces;
  using Mikepenz.Octicons_typeface_library;

  [Activity(MainLauncher = true, Theme = "@style/MaterialDrawerTheme.Light.DarkToolbar.TranslucentStatus")]
  public class MainActivity : AppCompatActivity, AccountHeader.IOnAccountHeaderListener, Drawer.IOnDrawerItemClickListener, IOnCheckedChangeListener
  {
    private const int PROFILE_SETTING = 1;

    //save our header or result
    private AccountHeader headerResult = null;
    private Drawer result = null;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.activity_sample_dark_toolbar);

      var toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
      this.SetSupportActionBar(toolbar);

      // Create a few sample profile
      // NOTE you have to define the loader logic too. See the CustomApplication for more details

      var profile = new ProfileDrawerItem().WithName("Mike Penz").WithEmail("mikepenz@gmail.com").WithIcon("https://avatars3.githubusercontent.com/u/1476232?v=3&s=460");
      var profile2 = new ProfileDrawerItem().WithName("Bernat Borras").WithEmail("alorma@github.com").WithIcon(Uri.Parse("https://avatars3.githubusercontent.com/u/887462?v=3&s=460"));
      var profile3 = new ProfileDrawerItem().WithName("Max Muster").WithEmail("max.mustermann@gmail.com").WithIcon(Resources.GetDrawable(Resource.Drawable.profile2));
      var profile4 = new ProfileDrawerItem().WithName("Felix House").WithEmail("felix.house@gmail.com").WithIcon(Resources.GetDrawable(Resource.Drawable.profile3));
      var profile5 = new ProfileDrawerItem().WithName("Mr. X").WithEmail("mister.x.super@gmail.com").WithIcon(Resources.GetDrawable(Resource.Drawable.profile4)).WithIdentifier(4);
      var profile6 = new ProfileDrawerItem().WithName("Batman").WithEmail("batman@gmail.com").WithIcon(Resources.GetDrawable(Resource.Drawable.profile5));

      headerResult = new AccountHeaderBuilder()
        .WithActivity(this)
        .WithHeaderBackground(Resource.Drawable.header)
        .AddProfiles(
          profile,
          profile2,
          profile3,
          profile4,
          profile5,
          profile6,
          //don't ask but google uses 14dp for the add account icon in gmail but 20dp for the normal icons (like manage account)
          new ProfileSettingDrawerItem().WithName("Add Account").WithDescription("Add new GitHub Account").WithIcon(new IconicsDrawable(this, GoogleMaterial.Icon.GmdAdd)
            .PaddingDp(5).ColorRes(Resource.Color.material_drawer_primary_text)).WithIdentifier(PROFILE_SETTING),
          new ProfileSettingDrawerItem().WithName("Manage Account").WithIcon(GoogleMaterial.Icon.GmdSettings)
        )
        .WithOnAccountHeaderListener(this)
        .WithSavedInstance(savedInstanceState)
        .Build();


      var item1 = new PrimaryDrawerItem();
      item1.WithName(Resource.String.drawer_item_compact_header);
      item1.WithIcon(GoogleMaterial.Icon.GmdWbSunny);
      item1.WithIdentifier(1);
      item1.WithCheckable(false);

      var item2 = new PrimaryDrawerItem();
      item2.WithName(Resource.String.drawer_item_action_bar);
      item2.WithIcon(FontAwesome.Icon.FawHome);
      item2.WithIdentifier(2);
      item2.WithCheckable(false);

      var item3 = new PrimaryDrawerItem();
      item3.WithName(Resource.String.drawer_item_multi_drawer);
      item3.WithIcon(FontAwesome.Icon.FawGamepad);
      item3.WithIdentifier(3);
      item3.WithCheckable(false);

      var item4 = new PrimaryDrawerItem();
      item4.WithName(Resource.String.drawer_item_non_translucent_status_drawer);
      item4.WithIcon(FontAwesome.Icon.FawEye);
      item4.WithIdentifier(4);
      item4.WithCheckable(false);

      var item5 = new PrimaryDrawerItem();
      item5.WithDescription("A more complex sample");
      item5.WithName(Resource.String.drawer_item_complex_header_drawer);
      item5.WithIcon(GoogleMaterial.Icon.GmdAdb);
      item5.WithIdentifier(5);
      item5.WithCheckable(false);

      var item6 = new PrimaryDrawerItem();
      item6.WithName(Resource.String.drawer_item_simple_fragment_drawer);
      item6.WithIcon(GoogleMaterial.Icon.GmdStyle);
      item6.WithIdentifier(6);
      item6.WithCheckable(false);

      var item7 = new PrimaryDrawerItem();
      item7.WithName(Resource.String.drawer_item_embedded_drawer_dualpane);
      item7.WithIcon(GoogleMaterial.Icon.GmdBatteryChargingFull);
      item7.WithIdentifier(7);
      item7.WithCheckable(false);

      var item8 = new PrimaryDrawerItem();
      item8.WithName(Resource.String.drawer_item_fullscreen_drawer);
      item8.WithIcon(GoogleMaterial.Icon.GmdStyle);
      item8.WithIdentifier(8);
      item8.WithCheckable(false);

      var item9 = new PrimaryDrawerItem();
      item9.WithName(Resource.String.drawer_item_custom_container_drawer);
      item9.WithIcon(GoogleMaterial.Icon.GmdMyLocation);
      item9.WithIdentifier(9);
      item9.WithCheckable(false);


      var item10 = new SecondaryDrawerItem();
      item10.WithName(Resource.String.drawer_item_open_source);
      item10.WithIcon(FontAwesome.Icon.FawGithub);
      item10.WithIdentifier(20);
      item10.WithCheckable(false);

      var item11 = new SecondaryDrawerItem();
      item11.WithName(Resource.String.drawer_item_contact);
      item11.WithIcon(GoogleMaterial.Icon.GmdFormatColorFill);
      item11.WithIdentifier(10);
      item11.WithTag("Bullhorn");

      var item12 = new SwitchDrawerItem();
      item12.WithName("Switch");
      item12.WithIcon(Octicons.Icon.OctTools);
      item12.WithChecked(true);
      item12.WithOnCheckedChangeListener(this);

      var item13 = new SwitchDrawerItem();
      item13.WithName("Switch2");
      item13.WithIcon(Octicons.Icon.OctTools);
      item13.WithChecked(true);
      item13.WithOnCheckedChangeListener(this);

      var item14 = new ToggleDrawerItem();
      item14.WithName("Toggle");
      item14.WithIcon(Octicons.Icon.OctTools);
      item14.WithChecked(true);
      item14.WithOnCheckedChangeListener(this);

      result = new DrawerBuilder()
        .WithActivity(this)
        .WithToolbar(toolbar)
        .WithAccountHeader(headerResult) //set the AccountHeader we created earlier for the header
        .AddDrawerItems(
          item1,
          item2,
          item3,
          item4,
          item5,
          item6,
          item7,
          item8,
          item9,
          new SectionDrawerItem().WithName(Resource.String.drawer_item_section_header),
          item10,
          item11,
          new DividerDrawerItem(),
          item12,
          item13,
          item14
        ) // add the items we want to use With our Drawer
        .WithOnDrawerItemClickListener(this)
        .WithSavedInstance(savedInstanceState)
        .WithShowDrawerOnFirstLaunch(true)
        .Build();

      if (savedInstanceState == null) {
        // set the selection to the item with the identifier 10
        result.SetSelectionByIdentifier(10, false);

        //set the active profile
        headerResult.SetActiveProfile(profile3);
      }
    }

    public void OnCheckedChanged(IDrawerItem drawerItem, CompoundButton buttonView, bool isChecked)
    {
      if (drawerItem is INameable) {
        Log.Info("material-drawer", "DrawerItem: " + ((INameable) drawerItem).Name + " - toggleChecked: " + isChecked);
      } else {
        Log.Info("material-drawer", "toggleChecked: " + isChecked);
      }
    }

    public bool OnItemClick(AdapterView parent, View view, int position, long id, IDrawerItem drawerItem)
    {
      //check if the drawerItem is set.
      //there are different reasons for the drawerItem to be null
      //--> click on the header
      //--> click on the footer
      //those items don't contain a drawerItem

      if (drawerItem != null) {
//        Intent intent = null;
//        if (drawerItem.Identifier == 1) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, SimpleCompactHeaderDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 2) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, ActionBarDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 3) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, MultiDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 4) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, SimpleNonTranslucentDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 5) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, ComplexHeaderDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 6) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, SimpleFragmentDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 7) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, EmbeddedDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 8) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, FullscreenDrawerActivity.class);
//        } else if (drawerItem.getIdentifier() == 9) {
//          intent = new Intent(SimpleHeaderDrawerActivity.this, CustomContainerActivity.class);
//        } else if (drawerItem.getIdentifier() == 20) {
//          intent = new LibsBuilder()
//            .WithFields(R.string.class.getFields())
//            .WithActivityStyle(Libs.ActivityStyle.LIGHT_DARK_TOOLBAR)
//            .intent(SimpleHeaderDrawerActivity.this);
//        }
//        if (intent != null) {
//          SimpleHeaderDrawerActivity.this.startActivity(intent);
//        }
      }

      return false;
    }

    public bool OnProfileChanged(View view, IProfile profile, bool current)
    {
      //sample usage of the onProfileChanged listener
      //if the clicked item has the identifier 1 add a new profile ;)
      if (profile is IDrawerItem && ((IDrawerItem) profile).Identifier == PROFILE_SETTING) {
        IProfile newProfile = new ProfileDrawerItem().WithNameShown(true).WithName("Batman")
          .WithEmail("batman@gmail.com").WithIcon(Resources.GetDrawable(Resource.Drawable.profile5));
        if (headerResult.Profiles != null) {
          //we know that there are 2 setting elements. set the new profile above them ;)
          headerResult.AddProfile(newProfile, headerResult.Profiles.Count - 2);
        } else {
          headerResult.AddProfiles(newProfile);
        }
      }

      //false if you have not consumed the event and it should close the drawer
      return false;
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
      //add the values which need to be saved from the accountHeader to the bundle
      outState = headerResult.SaveInstanceState(outState);
      base.OnSaveInstanceState(outState);
    }
  }
}