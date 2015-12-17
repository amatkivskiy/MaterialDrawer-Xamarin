using Mikepenz.Iconics;
using Sample;
using Square.Picasso;

namespace MaterialDrawerSample
{
  using System;
  using Android.App;
  using Android.Runtime;
  using Mikepenz.MaterialDrawer.Utils;

  [Application]
  public class CustomApplication : Application
  {

    public CustomApplication(IntPtr handle, JniHandleOwnership ownerShip)
      : base(handle, ownerShip)
    {
			DrawerImageLoader.Init(new AbstractDrawerImageLoaderImpl());
    }

    public override void OnCreate()
    {
      base.OnCreate();
    }
  }

	class AbstractDrawerImageLoaderImpl : AbstractDrawerImageLoader
  {
		public override void Cancel(Android.Widget.ImageView imageView)
		{
			Picasso.With(imageView.Context).CancelRequest(imageView);
		}

		public override void Set(Android.Widget.ImageView imageView, Android.Net.Uri uri, Android.Graphics.Drawables.Drawable placeholder)
		{
			Picasso.With(imageView.Context).Load(uri).Placeholder(placeholder).Into(imageView);
		}

		public override Android.Graphics.Drawables.Drawable Placeholder(Android.Content.Context context, string tag)
		{
			//define different placeholders for different imageView targets
			//default tags are accessible via the DrawerImageLoader.Tags
			//custom ones can be checked via string. see the CustomUrlBasePrimaryDrawerItem LINE 111
			if (DrawerImageLoader.Tags.Profile.Name() == tag) {
				return DrawerUIUtils.GetPlaceHolder(context);
			} else if (DrawerImageLoader.Tags.AccountHeader.Name() == tag) {
				return new IconicsDrawable(context).IconText(" ").BackgroundColorRes(Resource.Color.primary).SizeDp(56);
			} else if ("customUrlItem" == tag) {
				return new IconicsDrawable(context).IconText(" ").BackgroundColorRes(Resource.Color.md_red_500).SizeDp(56);
			}

			//we use the default one for
			//DrawerImageLoader.Tags.PROFILE_DRAWER_ITEM.name()

			return base.Placeholder(context, tag);
		}
  }
}
