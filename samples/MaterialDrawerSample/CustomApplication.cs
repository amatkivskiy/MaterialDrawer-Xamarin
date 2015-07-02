using Mikepenz.MaterialDrawer.Utils;

namespace MaterialDrawerSample
{
  using System;
  using Android.App;
  using Android.Content;
  using Android.Graphics.Drawables;
  using Android.Runtime;
  using Android.Widget;
  using Square.Picasso;

  [Application]
  public class CustomApplication : Application
  {

    public CustomApplication(IntPtr handle, JniHandleOwnership ownerShip)
      : base(handle, ownerShip)
    {
      DrawerImageLoader.Init(new DrawerImageLoaderImpl());
    }

    public override void OnCreate()
    {
      base.OnCreate();
    }
  }

  class DrawerImageLoaderImpl : Java.Lang.Object, DrawerImageLoader.IDrawerImageLoader
  {
    public void Cancel(ImageView imageView)
    {
      Picasso.With(imageView.Context).CancelRequest(imageView);
    }

    public Drawable Placeholder(Context context)
    {
      return null;
    }

    public void Set(ImageView imageView, Android.Net.Uri uri, Drawable placeholder)
    {
      Picasso.With(imageView.Context).Load(uri).Placeholder(placeholder).Into(imageView);
    }
  }
}
