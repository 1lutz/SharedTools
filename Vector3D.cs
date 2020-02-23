using System;
using System.Drawing;

namespace SharedTools
{
    public struct Vector3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static readonly Vector3D Null = new Vector3D(0, 0, 0);

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }

        public static float operator *(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector3D operator *(float a, Vector3D b)
        {
            b.X *= a;
            b.Z *= a;
            return b;
        }

        public static Vector3D operator /(Vector3D a, float b)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }

        public float Betrag2()
        {
            return X * X + Y * Y + Z * Z;
        }

        public float Betrag()
        {
            return (float)Math.Sqrt(Betrag2());
        }

        public static bool IstKollinear(Vector3D a, Vector3D b)
        {
            float quotient = b.X / a.X;
            return quotient == b.Y / a.Y && quotient == b.Z / a.Z;
        }

        public static float Schnittwinkel(Vector3D a, Vector3D b)
        {
            return (float)(Math.Acos(Math.Abs(a * b) / (a.Betrag() * b.Betrag())) / Math.PI * 180D);
        }

        public static Vector3D Kreuzprodukt(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.Y * b.Z - a.Z * b.Y,
                                a.Z * b.X - a.X * b.Z,
                                a.X * b.Y - a.Y * b.X);
        }

        public static float Spatprodukt(Vector3D a, Vector3D b, Vector3D c)
        {
            return a.Y * b.Z * c.X
                 + a.Z * b.X * c.Y
                 + a.X * b.Y * c.Z
                 - a.Z * b.Y * c.X
                 - a.X * b.Z * c.Y
                 - a.Y * b.X * c.Z;
        }

        public override string ToString()
        {
            return X + ", " + Y + ", " + Z;
        }

        public static implicit operator Vector(Vector3D v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }
    }
}
