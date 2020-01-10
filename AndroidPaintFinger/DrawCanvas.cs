using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using System;
using System.Collections.Generic;

namespace AndroidPaintFinger
{
    public class DrawCanvas : View
    {
        public List<Line> Lines = new List<Line>();
        Paint paint;

        public float BrushSize = 10;
        public Color BrushColor = Color.Red;

        public DrawCanvas(Context context) : base(context)
        {
            paint = new Paint();
        }

        public DrawCanvas(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public DrawCanvas(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public DrawCanvas(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected DrawCanvas(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }


        public void Clear()
        {
            Lines.Clear();
            Invalidate();
        }
        public void Undoline()
        {
            if (Lines.Count > 0)
            {
                Lines.RemoveAt(Lines.Count - 1);
                Invalidate();
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            str += e.Action;
            float X = e.GetX();
            float Y = e.GetY();
            int lastLine;
            
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    {
                        Lines.Add(new Line(BrushSize, BrushColor));

                        lastLine = Lines.Count - 1;
                        Lines[lastLine].point.MoveTo(X, Y);
                        Lines[lastLine].point.LineTo(X, Y);
                        Invalidate();
                        break;
                    }
                case MotionEventActions.Move:
                    {
                        if (Lines != null)
                        {
                            lastLine = Lines.Count - 1;
                            Lines[lastLine].color = BrushColor;
                            Lines[lastLine].strokeWidth = BrushSize;

                            Lines[lastLine].point.LineTo(X, Y);
                            Invalidate();
                        }
                        break;
                    }
                default: break;
            }
            return true;
        }


        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            foreach (Line line in Lines)
            {
                paint.AntiAlias = true;
                paint.Color = line.color;
                paint.StrokeWidth = line.strokeWidth;
                paint.SetStyle(Paint.Style.Stroke);
                paint.StrokeCap = Paint.Cap.Round;
                paint.StrokeJoin = Paint.Join.Round;

                canvas.DrawPath(line.point, paint);
            }
        }
    }
}