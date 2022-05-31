﻿#region Namespaces

using System;
using Autodesk.Revit.DB;

#endregion

namespace ElementOutline
{
    /// <summary>
    /// An integer-based 2D point class.
    /// </summary>
    internal class Point2dInt : IComparable<Point2dInt>
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Initialise a 2D millimetre integer
        /// point to the given values.
        /// </summary>
        public Point2dInt(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Convert a 2D Revit UV to a 2D millimetre
        /// integer point by scaling from feet to mm.
        /// </summary>
        public Point2dInt(UV p)
        {
            X = Util.ConvertFeetToMillimetres(p.U);
            Y = Util.ConvertFeetToMillimetres(p.V);
        }

        /// <summary>
        /// Convert a 3D Revit XYZ to a 2D millimetre
        /// integer point by discarding the Z coordinate
        /// and scaling from feet to mm.
        /// </summary>
        public Point2dInt(XYZ p)
        {
            X = Util.ConvertFeetToMillimetres(p.X);
            Y = Util.ConvertFeetToMillimetres(p.Y);
        }

        /// <summary>
        /// Convert Revit coordinates XYZ to a 2D
        /// millimetre integer point by scaling
        /// from feet to mm.
        /// </summary>
        public Point2dInt(double x, double y)
        {
            X = Util.ConvertFeetToMillimetres(x);
            Y = Util.ConvertFeetToMillimetres(y);
        }

        /// <summary>
        /// Comparison with another point, important
        /// for dictionary lookup support.
        /// </summary>
        public int CompareTo(Point2dInt a)
        {
            int d = X - a.X;

            if (0 == d)
            {
                d = Y - a.Y;
            }
            return d;
        }

        /// <summary>
        /// Distance from this point to another point.
        /// </summary>
        public double DistanceTo(Point2dInt a)
        {
            int dy = a.Y - Y;
            var dx = a.X - X;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Angle of line from this point to another point
        /// in the range (-pi,pi], just like Math.Atan2.
        /// </summary>
        public double AngleTo(Point2dInt a)
        {
            int dy = a.Y - Y;
            int dx = a.X - X;
            return Math.Atan2(dy, dx);
        }

        /// <summary>
        /// Display as a string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }

        /// <summary>
        /// Return a string suitable for use in an SVG
        /// path. For index i == 0, prefix with 'M', for
        /// i == 1 with 'L', and otherwise with nothing.
        /// </summary>
        public string SvgPath(int i)
        {
            return string.Format("{0}{1} {2}", (0 == i ? "M" : (1 == i ? "L" : "")), X, Util.SvgFlipY(Y));
        }

        /// <summary>
        /// Add two points, i.e. treat one of
        /// them as a translation vector.
        /// </summary>
        public static Point2dInt operator +(Point2dInt a, Point2dInt b)
        {
            return new Point2dInt(a.X + b.X, a.Y + b.Y);
        }
    }
}