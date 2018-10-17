using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PetBellies.CustomControls;
using PetBellies.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomImageCircle), typeof(CustomImageCircleRenderer))]
namespace PetBellies.Droid.CustomRenderers
{
    public class CustomImageCircleRenderer: ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeImageView = (global::Android.Widget.ImageView)Control;
                nativeImageView.RefreshDrawableState();
                //var source = e.OldElement.Source.ToString();
                //nativeImageView.LongClickable = true;
                //nativeImageView.LongClick += NativeImageView_LongClick;
                //nativeImageView.SetImageDrawable(new CircleDrawable(GetBitmap(e.OldElement).Result));
            }
        }

        private void NativeImageView_LongClick(object sender, LongClickEventArgs e)
        {
            PopupWindow popupWindow = new PopupWindow(this.Context);
            
        }

        Task<Bitmap> GetBitmap(Xamarin.Forms.Image image)
        {
            var handler = new ImageLoaderSourceHandler();
            return handler.LoadImageAsync(image.Source, this.Context);
        }
    }

    public class CircleDrawable : Drawable
    {
        Bitmap bmp;
        BitmapShader bmpShader;
        Paint paint;
        RectF oval;

        public CircleDrawable(Bitmap bmp)
        {
            this.bmp = bmp;
            this.bmpShader = new BitmapShader(bmp, Shader.TileMode.Clamp, Shader.TileMode.Clamp);
            this.paint = new Paint() { AntiAlias = true };
            this.paint.SetShader(bmpShader);
            this.oval = new RectF();
        }

        public override void Draw(Canvas canvas)
        {
            canvas.DrawOval(oval, paint);
        }

        protected override void OnBoundsChange(Rect bounds)
        {
            base.OnBoundsChange(bounds);
            oval.Set(0, 0, bounds.Width(), bounds.Height());
        }

        public override int IntrinsicWidth
        {
            get
            {
                return bmp.Width;
            }
        }

        public override int IntrinsicHeight
        {
            get
            {
                return bmp.Height;
            }
        }

        public override void SetAlpha(int alpha)
        {

        }

        public override int Opacity
        {
            get
            {
                return (int)Format.Opaque;
            }
        }

        public override void SetColorFilter(ColorFilter cf)
        {

        }
    }
}