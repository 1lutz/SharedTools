using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTools
{
    public struct Vector
    {
        private float[] _values;

        public int Dimension
        {
            get { return _values.Length; }
        }

        private static int ThrowIfIncompatible(Vector a, Vector b)
        {
            int dimension = a._values.Length;
            if (dimension != b._values.Length) throw new InvalidOperationException();
            return dimension;
        }

        public Vector(params float[] values)
        {
            if (values == null) throw new ArgumentNullException("values");
            if (values.Length == 0) throw new ArgumentException("values");
            _values = values;
        }

        public float this[int index]
        {
            get { return _values[index]; }
            set { _values[index] = value; }
        }

        public static Vector operator +(Vector a, Vector b)
        {
            int anz = ThrowIfIncompatible(a, b);
            for (int x = 0; x < anz; ++x) a._values[x] += b._values[x];
            return a;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            int anz = ThrowIfIncompatible(a, b);
            for (int x = 0; x < anz; ++x) a._values[x] -= b._values[x];
            return a;
        }

        public static float operator *(Vector a, Vector b)
        {
            int anz = ThrowIfIncompatible(a, b);
            float fertig = 0F;
            for (int x = 0; x < anz; ++x) fertig += a._values[x] * b._values[x];
            return fertig;
        }

        public static Vector operator *(float a, Vector b)
        {
            int anz = b._values.Length;
            for (int x = 0; x < anz; ++x) b._values[x] *= a;
            return b;
        }

        public static Vector operator /(Vector a, float b)
        {
            int anz = a._values.Length;
            for (int x = 0; x < anz; ++x) a._values[x] /= b;
            return a;
        }

        public float Betrag2()
        {
            int anz = _values.Length;
            float fertig = 0F;
            float aktuellerWert;

            for (int x = 0; x < anz; ++x)
            {
                aktuellerWert = _values[x];
                fertig += aktuellerWert * aktuellerWert;
            }
            return fertig;
        }

        public float Betrag()
        {
            return (float)Math.Sqrt(Betrag2());
        }

        public static bool IstKollinear(Vector a, Vector b)
        {
            int anz = ThrowIfIncompatible(a, b);
            float quotient = b._values[0] / a._values[0];

            for (int x = 1; x < anz; ++x)
            {
                if (b._values[x] / a._values[x] != quotient) return false;
            }
            return true;
        }

        public static float Schnittwinkel(Vector a, Vector b)
        {
            return (float)(Math.Acos(Math.Abs(a * b) / (a.Betrag() * b.Betrag())) / Math.PI * 180D);
        }

        public override string ToString()
        {
            int anz = _values.Length;
            StringBuilder fertig = new StringBuilder(anz * 2);
            fertig.Append(_values[0]);
            for (int x = 1; x < anz; ++x) fertig.Append(", ").Append(_values[x]);
            return fertig.ToString();
        }

        public static implicit operator Vector(float[] feld)
        {
            return new Vector(feld);
        }

        public static implicit operator float[] (Vector v)
        {
            return v._values;
        }

        public static explicit operator Vector2D(Vector v)
        {
            float[] vals = v._values;
            if (vals.Length != 2) throw new InvalidCastException();
            return new Vector2D(vals[0], vals[1]);
        }

        public static explicit operator Vector3D(Vector v)
        {
            float[] vals = v._values;
            if (vals.Length != 3) throw new InvalidCastException();
            return new Vector3D(vals[0], vals[1], vals[2]);
        }
    }
}
