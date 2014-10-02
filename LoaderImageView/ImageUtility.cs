using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.Provider;
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

        public static string GetPathToImage(Context context, Android.Net.Uri uri)
        {
            // The projection contains the columns we want to return in our query.
            string[] projection = { MediaStore.Images.Media.InterfaceConsts.Data };

            using (ICursor cursor = context.ContentResolver.Query(uri, projection, null, null, null))
            {
                if (cursor == null)
                    return uri.Path;

                int columnIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                return cursor.GetString(columnIndex);
            }
        }
    }
}
