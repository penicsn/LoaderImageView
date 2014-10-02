using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LoaderImageView
{
    [Activity(Label = "LoaderImageView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _loadButton;
        private LoaderImageView _loaderImageView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _loadButton = FindViewById<Button>(Resource.Id.LoadButton);
            _loaderImageView = FindViewById<LoaderImageView>(Resource.Id.Image);
            _loaderImageView.Visibility = ViewStates.Gone;
        }

        protected override void OnResume()
        {
            base.OnResume();

            _loadButton.Click += LoadButtonOnClick;
        }

        protected override void OnPause()
        {
            base.OnPause();

            _loadButton.Click -= LoadButtonOnClick;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Canceled)
                return;

            var imageUri = data.Data;
            var imagePath = ImageUtility.GetPathToImage(this, imageUri);

            if (String.IsNullOrWhiteSpace(imagePath))
            {
                Toast.MakeText(this, Resources.GetString(Resource.String.ImageLoadError), ToastLength.Long).Show();
                return;
            }

            _loaderImageView.Visibility = ViewStates.Visible;
            _loaderImageView.SetLocalImage(imagePath);
        }

        private void LoadButtonOnClick(object sender, EventArgs eventArgs)
        {
            var pickIntent = new Intent();
            pickIntent.SetType("image/*");
            pickIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(pickIntent, 0);
        }
    }
}
