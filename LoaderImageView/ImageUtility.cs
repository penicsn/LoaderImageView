using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Widget;

namespace LoaderImageView
{
    public static class ImageUtility
    {
        public static void SetImageFromFile(this ImageView imageView, string imagePath)
        {
            if (String.IsNullOrWhiteSpace(imagePath) || imageView == null)
                return;

            using (var bitmap = BitmapFactory.DecodeFile(imagePath))
            {
                imageView.SetImageBitmap(bitmap);
            }
        }

        public async static Task SetImageFromFileAsync(this ImageView imageView, string imagePath)
        {
            if (String.IsNullOrWhiteSpace(imagePath) || imageView == null)
                return;

            using (var bitmap = await BitmapFactory.DecodeFileAsync(imagePath))
            {
                imageView.SetImageBitmap(bitmap);
            }
        }
    }
}
