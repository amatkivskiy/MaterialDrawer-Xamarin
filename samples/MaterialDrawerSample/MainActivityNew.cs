using Mikepenz.Typeface;
using Mikepenz.MaterialDrawer.Utils;
using Mikepenz.MaterialDrawer.Holders;

namespace Sample
{
  using Android.App;
  using Android.OS;
  using Android.Support.V7.App;
  using Android.Net;
  using Android.Views;
  using Mikepenz.Iconics;
  using Mikepenz.MaterialDrawer;
  using Mikepenz.MaterialDrawer.Models;
  using Mikepenz.MaterialDrawer.Models.Interfaces;

  [Activity(MainLauncher = true, Theme = "@style/MaterialDrawerTheme.Light.DarkToolbar.TranslucentStatus")]
	public class MainActivity : AppCompatActivity, AccountHeader.IOnAccountHeaderListener, Drawer.IOnDrawerItemClickListener
  {
		const int PROFILE_SETTING = 1;

		//save our header or result
		AccountHeader headerResult = null;
		Drawer result = null;

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      SetContentView(Resource.Layout.activity_sample_dark_toolbar);

      var toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
      this.SetSupportActionBar(toolbar);

      // Create a few sample profile
      // NOTE you have to define the loader logic too. See the CustomApplication for more details

			var profile = new ProfileDrawerItem();
			profile.WithName("Mike Penz");
			profile.WithEmail("mikepenz@gmail.com");
			profile.WithIcon("https://avatars3.githubusercontent.com/u/1476232?v=3&s=460");
			profile.WithIdentifier(100);

			var profile2 = new ProfileDrawerItem();
			profile2.WithName("Bernat Borras");
			profile2.WithEmail("alorma@github.com");
			profile2.WithIcon(Uri.Parse("https://avatars3.githubusercontent.com/u/887462?v=3&s=460"));
			profile2.WithIdentifier(101);

			var profile3 = new ProfileDrawerItem();
			profile3.WithName("Max Muster");
			profile3.WithEmail("max.mustermann@gmail.com");
			profile3.WithIcon(Resource.Drawable.profile2);
			profile3.WithIdentifier(102);

			var profile4 = new ProfileDrawerItem();
			profile4.WithName("Felix House");
			profile4.WithEmail("felix.house@gmail.com");
			profile4.WithIcon(Resource.Drawable.profile3);
			profile4.WithIdentifier(103);

			var profile5 = new ProfileDrawerItem();
			profile5.WithName("Mr. X");
			profile5.WithEmail("mister.x.super@gmail.com");
			profile5.WithIcon(Resource.Drawable.profile4);
			profile5.WithIdentifier(104);

			var profile6 = new ProfileDrawerItem();
			profile6.WithName("Batman");
			profile6.WithEmail("batman@gmail.com");
			profile6.WithIcon(Resource.Drawable.profile5);
			profile6.WithIdentifier(105);

			var profileSettingItem = new ProfileSettingDrawerItem();
			profileSettingItem.WithName("Add Account");
			profileSettingItem.WithDescription("Add new GitHub Account");
			profileSettingItem.WithIcon(new IconicsDrawable(this, GoogleMaterial.Icon.GmdPlusOne).ActionBar().PaddingDp(5).ColorRes(Resource.Color.material_drawer_primary_text));
			profileSettingItem.WithIdentifier(PROFILE_SETTING);

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
					profileSettingItem,
					new ProfileSettingDrawerItem()
						.WithName("Manage Account")
						.WithIcon(GoogleMaterial.Icon.GmdSettings)
        )
        .WithOnAccountHeaderListener(this)
        .WithSavedInstance(savedInstanceState)
        .Build();

      var item1 = new PrimaryDrawerItem();
			item1.WithName(Resource.String.drawer_item_compact_header);
      item1.WithIcon(GoogleMaterial.Icon.GmdWbSunny);
      item1.WithIdentifier(1);
			item1.WithSelectable(false);
//
      var item2 = new PrimaryDrawerItem();
			item2.WithName(Resource.String.drawer_item_action_bar_drawer);
      item2.WithIcon(FontAwesome.Icon.FawHome);
      item2.WithIdentifier(2);
			item2.WithSelectable(false);
//
      var item10 = new SecondaryDrawerItem();
      item10.WithName(Resource.String.drawer_item_open_source);
      item10.WithIcon(FontAwesome.Icon.FawGithub);
      item10.WithIdentifier(20);
			item10.WithSelectable(false);
//
      var item11 = new SecondaryDrawerItem();
			item11.WithName(Resource.String.drawer_item_contact);
      item11.WithIcon(GoogleMaterial.Icon.GmdFormatColorFill);
      item11.WithIdentifier(10);
      item11.WithTag("Bullhorn");

//
      result = new DrawerBuilder()
        .WithActivity(this)
        .WithToolbar(toolbar)
        .WithAccountHeader(headerResult) //set the AccountHeader we created earlier for the header
        .AddDrawerItems(
          item1,
          item2,
          new SectionDrawerItem().WithName(Resource.String.drawer_item_section_header),
          item10,
          item11,
          new DividerDrawerItem()
        ) // add the items we want to use With our Drawer
        .WithOnDrawerItemClickListener(this)
        .WithSavedInstance(savedInstanceState)
        .WithShowDrawerOnFirstLaunch(true)
        .Build();

			//if you have many different types of DrawerItems you can magically pre-cache those items to get a better scroll performance
			//make sure to init the cache after the DrawerBuilder was created as this will first clear the cache to make sure no old elements are in
			RecyclerViewCacheUtil.Instance.WithCacheSize(2).Init(result);

			//only set the active selection or active profile if we do not recreate the activity
			if (savedInstanceState == null) {
				// set the selection to the item with the identifier 11
				result.SetSelection(21, false);

				//set the active profile
				headerResult.SetActiveProfile(profile3, false);
			}

			result.UpdateBadge(4, new StringHolder(10 + ""));
    }

		public bool OnItemClick(View view, int position, IDrawerItem drawerItem)
		{
			//check if the drawerItem is set.
			//there are different reasons for the drawerItem to be null
			//--> click on the header
			//--> click on the footer
			//those items don't contain a drawerItem

			if (drawerItem != null) {
			}

			return false;
		}

    public bool OnProfileChanged(View view, IProfile profile, bool current)
    {
			//sample usage of the onProfileChanged listener
			//if the clicked item has the identifier 1 add a new profile ;)
			if (profile is IDrawerItem && profile.Identifier == PROFILE_SETTING) {
				int count = 100 + headerResult.Profiles.Count + 1;
				IProfile newProfile = new ProfileDrawerItem().WithNameShown(true).WithName("Batman" + count).WithEmail("batman" + count + "@gmail.com").WithIcon(Resource.Drawable.profile5);;
				newProfile.WithIdentifier(count);
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