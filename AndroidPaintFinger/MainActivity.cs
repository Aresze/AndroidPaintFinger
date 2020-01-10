using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace AndroidPaintFinger
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DrawCanvas drawCanvas;
        float BrushSize = 5f;
        Color BrushColor = Color.Black;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            RelativeLayout relativeLayout = FindViewById<RelativeLayout>(Resource.Id.view);

            drawCanvas = new DrawCanvas(this);
            relativeLayout.AddView(drawCanvas);
           
            Button clear = FindViewById<Button>(Resource.Id.clear);
            clear.Click += (sender, e) =>
            {
                drawCanvas.Clear();
            };

            Button undo = FindViewById<Button>(Resource.Id.undo);
            undo.Click += (sender, e) =>
            {
                drawCanvas.Undoline();
            };

            SeekBar barBrushSize = FindViewById<SeekBar>(Resource.Id.brushSize);
            barBrushSize.ProgressChanged += (sender, e) =>
            {
                drawCanvas.BrushSize = e.Progress;
            };

            SeekBar barBrushColor = FindViewById<SeekBar>(Resource.Id.brushColor);
            barBrushColor.Max = 256 * 7 - 1;
            barBrushColor.ProgressChanged += (sender, e) =>
            {
                BrushColor = getColor(e.Progress,e.SeekBar.Max);
                drawCanvas.BrushColor = BrushColor;
                barBrushColor.Thumb.SetTint(BrushColor);

            };

            Color getColor(int position,int maxSize)
            {
                var r = 0;
                var g = 0;
                var b = 0;

                if (position < 256)
                {
                    b = position;
                }
                else if (position < 256 * 2)
                {
                    g = position % 256;
                    b = 256 - position % 256;
                }
                else if (position < 256 * 3)
                {
                    g = 255;
                    b = position % 256;
                }
                else if (position < 256 * 4)
                {
                    r = position % 256;
                    g = 256 - position % 256;
                    b = 256 - position % 256;
                }
                else if (position < 256 * 5)
                {
                    r = 255;
                    g = 0;
                    b = position % 256;
                }
                else if (position < 256 * 6)
                {
                    r = 255;
                    g = position % 256;
                    b = 256 - position % 256;
                }
                else if (position < 256 * 7)
                {
                    r = 255;
                    g = 255;
                    b = position % 256;
                }
                return Color.Rgb(r, g, b);
                
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

