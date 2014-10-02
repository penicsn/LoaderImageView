using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LoaderImageView
{
    public class LoaderImageView : RelativeLayout
    {
        private Context _context;
        private ProgressBar _progressBar;
        private ImageView _imageView;

        protected LoaderImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public LoaderImageView(Context context) : base(context)
        {
            Init(context, null);
        }

        public LoaderImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public LoaderImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Init(context, attrs);
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            _context = context;

            _progressBar = new ProgressBar(_context, attrs, Android.Resource.Attribute.ProgressBarStyle)
            {
                Id = Resource.Id.LoaderProgressBar,
                Indeterminate = true
            };
            _imageView = new ImageView(_context, attrs)
            {
                Id = Resource.Id.LoaderImageView,
                LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent),
                Visibility = ViewStates.Invisible
            };

            AddView(_progressBar);
            AddView(_imageView);

            SetProgressBarLayoutParams(LayoutRules.CenterInParent);
            SetImageViewScaleType(ImageView.ScaleType.CenterCrop);
            SetImageViewLayoutParams(LayoutRules.CenterInParent);
        }

        public void SetImageViewScaleType(ImageView.ScaleType scaleType)
        {
            _imageView.SetScaleType(scaleType);
        }

        public void SetImageViewLayoutParams(LayoutRules layoutRules)
        {
            var imageLayoutParams = (LayoutParams)_imageView.LayoutParameters;
            imageLayoutParams.AddRule(layoutRules);
            _imageView.LayoutParameters = imageLayoutParams;
        }

        public void SetProgressBarLayoutParams(LayoutRules layoutRules)
        {
            var progressLayoutParams = (LayoutParams)_progressBar.LayoutParameters;
            progressLayoutParams.AddRule(layoutRules);
            _progressBar.LayoutParameters = progressLayoutParams;
        }

        private void ShowImage()
        {
            _progressBar.Visibility = ViewStates.Gone;
            _imageView.Visibility = ViewStates.Visible;
        }

        private void ShowProgressBar()
        {
            _imageView.Visibility = ViewStates.Invisible;
            _imageView.LayoutParameters.Width = ViewGroup.LayoutParams.MatchParent;
            _imageView.LayoutParameters.Height = ViewGroup.LayoutParams.MatchParent;
            _progressBar.Visibility = ViewStates.Visible;
        }
    }
}
