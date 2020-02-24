﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace PBRTSharp.Core
{
    public readonly struct Vector2f : IEquatable<Vector2f>
    {
        public double X { get; }
        public double Y { get; }
        public Vector2f(double x, double y)
        {
            X = x;
            Y = y;
            Debug.Assert(!HasNaNs());
        }

        public double this[in int i]
        {
            get {
                Debug.Assert(i == 0 || i == 1);
                return i == 0 ? X : Y;
            }
        }

        // Operator overloads
        public static Vector2f operator +(in Vector2f v1, in Vector2f v2) => new Vector2f(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2f operator -(in Vector2f v1, in Vector2f v2) => new Vector2f(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2f operator *(in Vector2f v, in double d) => new Vector2f(d * v.X, d * v.Y);
        public static Vector2f operator *(in double d, in Vector2f v) => v * d;
        public static Vector2f operator /(in Vector2f v, in double d)
        {
            Debug.Assert(d != 0);
            var recip = 1.0d / d;
            return v * recip;
        }
        public static Vector2f operator -(in Vector2f v) => new Vector2f(-v.X, -v.Y);
        public static bool operator ==(Vector2f v1, Vector2f v2) => v1.Equals(v2);
        public static bool operator !=(Vector2f v1, Vector2f v2) => !(v1 == v2);

        // Named operator overloads
        public static Vector2f Add(in Vector2f left, in Vector2f right) => left + right;
        public static Vector2f Subtract(in Vector2f left, in Vector2f right) => left - right;
        public static Vector2f Multiply(in Vector2f left, in double right) => left * right;
        public static Vector2f Multiply(in double left, in Vector2f right) => left * right;
        public static Vector2f Negate(in Vector2f item) => -item;
        public static Vector2f Divide(in Vector2f left, in double right) => left / right;

        // Static methods
        public static Vector2f ComponentMin(in Vector2f v1, in Vector2f v2) => new Vector2f(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y));
        public static Vector2f ComponentMax(in Vector2f v1, in Vector2f v2) => new Vector2f(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Vector2f && Equals((Vector2f)obj);
        public bool Equals(Vector2f other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y));

        // Public instance methods
        public Vector2f Abs() => new Vector2f(Math.Abs(X), Math.Abs(Y));
        public double Dot(in Vector2f other) => (X * other.X) + (Y * other.Y);
        public double AbsDot(in Vector2f other) => Math.Abs(Dot(other));
        public double LengthSquared() => (X * X) + (Y * Y);
        public double Length() => Math.Sqrt(LengthSquared());
        public Vector2f Normalize() => this / Length();
        public double MinComponent() => Math.Min(X, Y);
        public double MaxComponent() => Math.Max(X, Y);
        public double MaxDimension() => X > Y ? 0 : 1;
        public Vector2f Permute(in int X, in int Y) => new Vector2f(this[X], this[Y]);

        // Private instance methods
        private bool HasNaNs() => double.IsNaN(X) || double.IsNaN(Y);
    }
}
