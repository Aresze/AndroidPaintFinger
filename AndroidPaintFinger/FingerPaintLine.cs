using Android.Graphics;

namespace AndroidPaintFinger
{
    public class Line
    {
        public float strokeWidth;
        public Color color;
        public Path point;

        public Line(float StorkeWidth,Color Color)
        {
            strokeWidth = StorkeWidth;
            color = Color;
            point = new Path();
        }

    }
}
