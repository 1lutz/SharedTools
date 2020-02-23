using System;
using System.Drawing;

namespace SharedTools
{
    public struct Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public static readonly Vector2D Null = new Vector2D(0, 0);

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }

        public static float operator *(Vector2D a, Vector2D b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static Vector2D operator *(float a, Vector2D b)
        {
            b.X *= a;
            b.Y *= a;
            return b;
        }

        public static Vector2D operator /(Vector2D a, float b)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }

        public float Betrag2()
        {
            return X * X + Y * Y;
        }

        public float Betrag()
        {
            return (float)Math.Sqrt(Betrag2());
        }

        public static bool IstKollinear(Vector2D a, Vector2D b)
        {
            return b.X / a.X == b.Y / a.Y;
        }

        public static float Schnittwinkel(Vector2D a, Vector2D b)
        {
            return (float)(Math.Acos(Math.Abs(a * b) / (a.Betrag() * b.Betrag())) / Math.PI * 180D);
        }

        public override string ToString()
        {
            return X + ", " + Y;
        }

        public static implicit operator Vector2D(PointF p)
        {
            return new Vector2D(p.X, p.Y);
        }

        public static implicit operator PointF(Vector2D v)
        {
            return new PointF(v.X, v.Y);
        }

        public static implicit operator Vector(Vector2D v)
        {
            return new Vector(v.X, v.Y);
        }
    }
}
